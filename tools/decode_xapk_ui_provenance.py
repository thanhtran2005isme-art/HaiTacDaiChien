#!/usr/bin/env python3
"""Audit exact UI MonoScript and IL2CPP metadata evidence from local XAPK.

Never guess missing managed fields, offsets, CanvasScaler or scene layout.
Output stays in ignored output/; no XAPK, game art or scripts are published.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import os
import shutil
import tempfile
import zipfile
from pathlib import Path

import audit_local_ui_components as ui
import audit_original_unity_graph as graph_tool
import export_local_ui_layout as layout
import il2cpp_refs as refs

ROOT = layout.ROOT
UI_FIELDS = ui.FIELDS
NATIVE_FIELDS = {
    "Canvas": ("m_Enabled", "m_RenderMode", "m_SortingOrder",
               "m_OverrideSorting", "m_TargetDisplay", "m_PixelPerfect"),
    "RectTransform": ("m_AnchorMin", "m_AnchorMax", "m_Pivot",
                      "m_SizeDelta", "m_AnchoredPosition", "m_LocalScale"),
}
METADATA_NAMES = sorted({name for fields in UI_FIELDS.values() for name in fields})


def script_identity(reader):
    if reader is None or reader.type.name != "MonoScript":
        return None
    try:
        script = reader.read()
        name = str(layout.get(script, "m_ClassName", "") or "")
        ns = str(layout.get(script, "m_Namespace", "") or "")
        assembly = str(layout.get(script, "m_AssemblyName", "") or "")
        if not name or len(name) > 128 or len(ns) > 128:
            return None
        return {"class": ns + "." + name if ns else name,
                "assembly": assembly[:128]}
    except Exception:
        return None


def resolve_script(ptr, component_reader, registry):
    """Never select another MonoScript based only on an identical name."""
    file_id, path_id = refs.pptr(ptr)
    source = {"fileId": file_id, "pathId": path_id}
    if path_id == 0:
        return None, "NULL_SCRIPT_POINTER", source
    if file_id == 0:
        obj = registry["readers"].get((id(component_reader.assets_file), path_id))
        return script_identity(obj), "LOCAL_PATHID", source
    obj, status = refs.resolve_pointer(
        ptr, component_reader.assets_file,
        registry["readers"], registry["aliases"])
    found = script_identity(obj)
    if found is None and hasattr(ptr, "deref"):
        try:
            found = script_identity(ptr.deref())
            if found:
                status = "unitypy_deref"
        except Exception:
            pass
    return found, status.upper(), source


def allowed_typetree_fields(reader, full_class):
    try:
        data = reader.read_typetree()
    except Exception:
        return "NO_MANAGED_TYPETREE", {}
    if not isinstance(data, dict):
        return "TYPETREE_NOT_OBJECT", {}
    values = {}
    for name in UI_FIELDS[full_class]:
        if name in data:
            value = ui.plain(data[name])
            if value is not None:
                values[name] = value
    return ("SERIALIZED_FIELDS_VERIFIED" if values
            else "TYPETREE_NO_TARGET_FIELDS"), values


def inspect_component(reader, expected_kind, owner_go, registry):
    if reader.type.name != expected_kind:
        raise ValueError("Component type differs from original graph at " +
                         str(reader.path_id))
    row = {"pathId": int(reader.path_id), "kind": expected_kind,
           "status": "NOT_TARGET"}
    if expected_kind == "MonoBehaviour":
        try:
            head = reader.parse_monobehaviour_head()
        except Exception:
            row["status"] = "HEADER_UNREADABLE"
            return row
        if refs.pptr(layout.get(head, "m_GameObject")) != (0, owner_go):
            raise ValueError("MonoBehaviour owner differs from source GameObject")
        script, state, pointer = resolve_script(
            layout.get(head, "m_Script"), reader, registry)
        row.update(scriptPointer=pointer, scriptResolution=state)
        if pointer["fileId"] > 0:
            externals = layout.get(reader.assets_file, "externals", []) or []
            if pointer["fileId"] <= len(externals):
                source_path = layout.get(
                    externals[pointer["fileId"] - 1], "path", "")
                row["externalScriptAlias"] = refs.normalize(source_path)
        enabled = layout.get(head, "m_Enabled")
        if isinstance(enabled, (bool, int)) and enabled in (0, 1):
            row["nativeEnabled"] = bool(enabled)
        if script is None:
            row["status"] = "SOURCE_SCRIPT_UNRESOLVED"
            return row
        row.update(className=script["class"], assembly=script["assembly"])
        if script["class"] in UI_FIELDS:
            row["expectedFields"] = UI_FIELDS[script["class"]]
            row["status"], fields = allowed_typetree_fields(reader, script["class"])
            if fields:
                row["fields"] = fields
        return row

    if expected_kind in NATIVE_FIELDS:
        try:
            obj = reader.read()
        except Exception:
            row["status"] = "NATIVE_READ_FAILED"
            return row
        if refs.pptr(layout.get(obj, "m_GameObject")) != (0, owner_go):
            raise ValueError("Native component owner differs from GameObject")
        fields = {}
        for key in NATIVE_FIELDS[expected_kind]:
            value = ui.plain(layout.get(obj, key))
            if value is not None:
                fields[key] = value
        row.update(status="NATIVE_FIELDS", fields=fields)
    return row


def inspect_scene(scene, original, groups, registry):
    by_id = layout.choose_serialized_file(scene, groups)
    if scene["id"] != original["sceneId"]:
        raise ValueError("Source graph scene ID mismatch")
    verified = {int(x["pathId"]): x for x in original["components"]}
    results = []
    seen = set()
    for node in original["nodes"]:
        for cid in node["componentIds"]:
            if cid in seen:
                raise ValueError("Duplicate source component ID")
            seen.add(cid)
            prior = verified.get(cid)
            reader = by_id.get(cid)
            if prior is None or reader is None or prior["kind"] == "MISSING":
                raise ValueError("Source component missing " + str(cid))
            row = inspect_component(reader, prior["kind"],
                                    node["gameObjectId"], registry)
            row["rectTransformId"] = node["rectTransformId"]
            row["gameObjectId"] = node["gameObjectId"]
            row["graphFieldStatus"] = prior["fieldStatus"]
            results.append(row)
    if len(results) != original["stats"]["componentReferences"]:
        raise ValueError("Source component counts changed")
    counts = collections.Counter()
    for row in results:
        counts[row["status"]] += 1
        if row.get("className") in UI_FIELDS:
            counts["recognizedUIClasses"] += 1
            counts["class:" + row["className"]] += 1
            if row["status"] == "SERIALIZED_FIELDS_VERIFIED":
                counts["verifiedManagedFields"] += len(row["fields"])
    return {"sceneId": scene["id"], "sourceFile": scene["source"],
            "components": results, "stats": dict(counts)}


def resolve_cross_bundle_scripts(xapk, scenes, unitypy=None):
    """Read only MonoScript records from OTHER bundles referenced by source PPtrs.

    The target is an exact (external file alias, MonoScript PathID), not a
    filename/class-name guess. All Unity bundle files are scanned so alias
    duplicates can be detected and rejected. No metadata binaries exported.
    """
    if unitypy is None:
        import UnityPy as unitypy
    wanted = collections.defaultdict(set)
    for scene in scenes:
        for component in scene["components"]:
            if component["status"] != "SOURCE_SCRIPT_UNRESOLVED":
                continue
            alias = component.get("externalScriptAlias")
            ptr = component.get("scriptPointer", {})
            if alias and ptr.get("pathId", 0) > 0:
                wanted[alias].add(ptr["pathId"])
    if not wanted:
        return {"resolved": 0, "ambiguous": 0, "scannedBundles": 0}

    indexed = collections.defaultdict(set)
    candidate_files = collections.defaultdict(set)
    scanned = 0
    with tempfile.TemporaryDirectory(prefix="haitac_monoscripts_") as tmp:
        with zipfile.ZipFile(xapk) as outer:
            for n, member in enumerate(outer.infolist()):
                if member.is_dir() or not member.filename.lower().endswith(".apk"):
                    continue
                if member.file_size > layout.MAX_ARCHIVE:
                    raise ValueError("Nested APK exceeds maximum supported size")
                apk_file = Path(tmp) / (str(n) + ".apk")
                with outer.open(member) as source, apk_file.open("wb") as dst:
                    shutil.copyfileobj(source, dst, 1024 * 1024)
                try:
                    with zipfile.ZipFile(apk_file) as archive:
                        for asset in archive.infolist():
                            if asset.is_dir() or not asset.filename.lower().endswith(
                                (".bundle", ".unity3d", ".assetbundle")
                            ):
                                continue
                            if asset.file_size > layout.MAX_BUNDLE:
                                raise ValueError("Unity asset bundle exceeds limit")
                            with archive.open(asset) as stream:
                                env = unitypy.load(stream.read())
                            scanned += 1
                            aliases_by_file = collections.defaultdict(set)
                            for alias, assets_file in getattr(
                                env, "files", {}).items():
                                if hasattr(assets_file, "objects"):
                                    aliases_by_file[id(assets_file)].add(
                                        refs.normalize(alias))
                            for item in env.objects:
                                if item.type.name != "MonoScript":
                                    continue
                                af = item.assets_file
                                names = set(aliases_by_file.get(id(af), set()))
                                names.update(refs.aliases(af))
                                useful = names.intersection(wanted)
                                pid = int(item.path_id)
                                if not useful or not any(
                                    pid in wanted[alias] for alias in useful
                                ):
                                    continue
                                info = script_identity(item)
                                if info is None:
                                    continue
                                for alias in useful:
                                    if pid not in wanted[alias]:
                                        continue
                                    key = (alias, pid)
                                    indexed[key].add((info["class"], info["assembly"]))
                                    # Unique serialized source is required, even
                                    # when two bundles coincidentally share names.
                                    candidate_files[key].add(
                                        (n, asset.filename, id(af)))
                finally:
                    apk_file.unlink(missing_ok=True)

    resolved = 0
    ambiguous = 0
    for scene in scenes:
        for row in scene["components"]:
            if row["status"] != "SOURCE_SCRIPT_UNRESOLVED":
                continue
            alias = row.get("externalScriptAlias")
            pid = row.get("scriptPointer", {}).get("pathId")
            key = (alias, pid)
            choices = indexed.get(key, set())
            if len(choices) != 1 or len(candidate_files.get(key, set())) != 1:
                if choices:
                    ambiguous += 1
                    row["scriptResolution"] = "GLOBAL_SCRIPT_ALIAS_AMBIGUOUS"
                continue
            cls, assembly = next(iter(choices))
            row.update(className=cls, assembly=assembly,
                       scriptResolution="GLOBAL_XAPK_EXACT_FILE_ALIAS_PATHID")
            if cls in UI_FIELDS:
                row["expectedFields"] = UI_FIELDS[cls]
                # Typetree status from a separately verified direct
                # inspection of precisely this component path ID.
                source_status = row.get("graphFieldStatus")
                row["status"] = (
                    "NO_MANAGED_TYPETREE" if source_status ==
                    "managed_fields_unavailable"
                    else "TYPE_TREE_RECHECK_REQUIRED"
                )
            else:
                row["status"] = "NOT_TARGET"
            resolved += 1
    return {"resolved": resolved, "ambiguous": ambiguous,
            "scannedBundles": scanned}


def inspect_il2cpp_header(apk_dir, xapk=None):
    """Read metadata from nested APK within canonical local XAPK.

    Output/apks is optional: upstream exporters intentionally delete temp
    unpacked APKs, and stale placeholders must never count as evidence.
    """
    result = []

    def scan_apk(archive, name):
        for entry in archive.infolist():
            if not entry.filename.lower().endswith("/global-metadata.dat"):
                continue
            if not 0 < entry.file_size <= refs.MAX_METADATA_BYTES:
                raise ValueError("Unsupported IL2CPP metadata size")
            raw = archive.read(entry)
            header = refs.metadata_header(raw[:264], len(raw))
            state = header["state"]
            hints = {}
            if state == "standard_header" and header["version"] == 31:
                hints = {item: (item.encode("utf-8") + b"\0") in raw
                         for item in METADATA_NAMES}
                state = "V31_HEADER_VALIDATED_NAME_HINTS_ONLY"
            result.append({
                "sourceApk": name, "sha256": hashlib.sha256(raw).hexdigest(),
                "version": header.get("version"), "status": state,
                "fieldNameHints": hints,
            })

    for apk in sorted(apk_dir.glob("*.apk")):
        try:
            with zipfile.ZipFile(apk) as archive:
                scan_apk(archive, apk.name)
        except zipfile.BadZipFile:
            continue

    if not result and xapk is not None:
        if not xapk.is_file() or not zipfile.is_zipfile(xapk):
            raise ValueError("Canonical local XAPK not accessible for IL2CPP metadata")
        with zipfile.ZipFile(xapk) as outer, tempfile.TemporaryDirectory(
            prefix="haitac_meta_") as temporary:
            for index, entry in enumerate(outer.infolist()):
                if entry.is_dir() or not entry.filename.lower().endswith(".apk"):
                    continue
                if entry.file_size > layout.MAX_ARCHIVE:
                    raise ValueError("Oversized nested APK")
                destination = Path(temporary) / (str(index) + ".apk")
                with outer.open(entry) as source, destination.open("wb") as target:
                    shutil.copyfileobj(source, target, 1024 * 1024)
                try:
                    with zipfile.ZipFile(destination) as archive:
                        scan_apk(archive, entry.filename)
                finally:
                    destination.unlink(missing_ok=True)
    return {"files": result,
            "limitation": "Metadata strings do not establish field layout, "
                          "MonoScript ownership, offsets, types or values."}


def summarize(scenes):
    statuses = collections.Counter()
    field_counts = collections.Counter()
    count = 0
    for scene in scenes:
        for c in scene["components"]:
            count += 1
            statuses[c["status"]] += 1
            if c.get("className") in UI_FIELDS:
                for name in c.get("fields", {}):
                    field_counts[c["className"] + "." + name] += 1
    return {"componentCount": count, "statuses": dict(sorted(statuses.items())),
            "verifiedManagedFields": dict(sorted(field_counts.items())),
            "sceneCounts": {x["sceneId"]: x["stats"] for x in scenes}}


def build(root=ROOT, xapk=None, unitypy=None):
    if unitypy is None:
        import UnityPy as unitypy
    original_path = root / "output/original-unity-graph.json"
    if not original_path.is_file():
        raise FileNotFoundError("Run tools/audit_original_unity_graph.py first")
    graph = json.loads(original_path.read_text(encoding="utf-8"))
    scenes = json.loads((root /
        "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json").read_text(
        encoding="utf-8"))["scenes"]
    if len(graph["scenes"]) != 5 or len(scenes) != 5:
        raise ValueError("Expected five verified candidate scenes")
    original_by_id = {x["sceneId"]: x for x in graph["scenes"]}
    analyzed = []
    completed = set()

    class Interceptor:
        def load(self, buffer):
            env = unitypy.load(buffer)
            groups = collections.defaultdict(dict)
            for reader in env.objects:
                groups[id(reader.assets_file)][int(reader.path_id)] = reader
            indexed, _, _, aliases = refs.build_file_registry([("bundle", env)])
            registry = {"readers": indexed, "aliases": aliases}
            for scene in scenes:
                if scene["id"] in completed:
                    continue
                try:
                    layout.choose_serialized_file(scene, groups)
                except ValueError:
                    continue
                analyzed.append(inspect_scene(scene, original_by_id[scene["id"]],
                                              groups, registry))
                completed.add(scene["id"])
            return env

    layout.build(root=root, xapk=xapk, unitypy=Interceptor())
    if len(completed) != 5:
        raise ValueError("Not all original XAPK candidate serialized files found")
    canonical_xapk = xapk or next(iter(sorted(root.glob("*.xapk"))), None)
    if canonical_xapk is None:
        raise FileNotFoundError("Canonical XAPK not found")
    cross = resolve_cross_bundle_scripts(canonical_xapk, analyzed, unitypy)
    # Recompute class counts AFTER global source MonoScript resolution.
    for item in analyzed:
        stats = collections.Counter()
        for row in item["components"]:
            stats[row["status"]] += 1
            if row.get("className") in UI_FIELDS:
                stats["recognizedUIClasses"] += 1
                stats["class:" + row["className"]] += 1
                if row["status"] == "SERIALIZED_FIELDS_VERIFIED":
                    stats["verifiedManagedFields"] += len(row["fields"])
        item["stats"] = dict(stats)
    summary = summarize(analyzed)
    if summary["componentCount"] != graph["stats"]["components"]:
        raise ValueError("Exact source component count mismatch")
    return {
        "schemaVersion": 1,
        "status": "VERIFIED_POINTER_AUDIT_NOT_FIELD_OFFSET_RECOVERY",
        "scenes": sorted(analyzed, key=lambda x: x["sceneId"]),
        "stats": summary,
        "globalMonoScriptResolution": cross,
        "il2cpp": inspect_il2cpp_header(root / "output/apks", canonical_xapk),
        "warning": "Never synthesize masked UI/layout or interpret arbitrary "
                   "IL2CPP metadata strings as serialized component values."
    }


def render_report(data):
    lines = [
        "# Serialized UI source provenance from local XAPK",
        "",
        "This report does not claim the original Editor Prefabs or "
        "runtime UI have been restored.",
        "",
        "| Candidate | Components | UI types identified | Managed fields verified |",
        "|---|---:|---:|---:|",
    ]
    for scene in data["scenes"]:
        c = scene["stats"]
        lines.append("| " + scene["sceneId"] + " | " +
                     str(len(scene["components"])) + " | " +
                     str(c.get("recognizedUIClasses", 0)) + " | " +
                     str(c.get("verifiedManagedFields", 0)) + " |")
    lines += ["", "## Verified UI managed field names", ""]
    if data["stats"]["verifiedManagedFields"]:
        lines.extend("- " + k + ": " + str(v) for k, v in
                     data["stats"]["verifiedManagedFields"].items())
    else:
        lines.append("No target managed field values verified via typetree.")
    lines += ["", "## Remaining blockers", "",
              "- MonoScript class links are PPtr-based and may remain unresolved "
              "if the referenced SerializedFile is unavailable.",
              "- Missing managed typetrees are UNKNOWN, not defaults.",
              "- IL2CPP v31 header and metadata name hits are only hints, "
              "not class ownership, field offsets, or field values.",
              "- Recovering additional fields requires independently validated "
              "type layouts for this exact build and a byte-level verifier.",
              "- Local detailed component records are ignored by Git.",
              ""]
    return "\n".join(lines)


def main():
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--root", type=Path, default=ROOT)
    ap.add_argument("--xapk", type=Path)
    args = ap.parse_args()
    root = args.root.resolve()
    try:
        data = build(root=root, xapk=args.xapk)
    except (OSError, ValueError, KeyError, TypeError) as exc:
        ap.exit(1, "BLOCKED: " + str(exc)[:400] + "\n")
    folder = root / "output"
    folder.mkdir(parents=True, exist_ok=True)
    for target, contents in [
        ("deep-ui-source-evidence.json", json.dumps(
            data, ensure_ascii=False, indent=2) + "\n"),
        ("deep-ui-source-evidence.md", render_report(data)),
    ]:
        with tempfile.NamedTemporaryFile(
            "w", encoding="utf-8", dir=folder, prefix="ui_deep_",
            suffix=".tmp", delete=False) as file:
            file.write(contents)
            temp = file.name
        os.replace(temp, folder / target)
    print(json.dumps({
        "status": data["status"], "components": data["stats"]["componentCount"],
        "managedFieldNames": data["stats"]["verifiedManagedFields"],
        "metadataFiles": len(data["il2cpp"]["files"]),
        "globalMonoScripts": data["globalMonoScriptResolution"]
    }, ensure_ascii=False))


if __name__ == "__main__":
    main()
