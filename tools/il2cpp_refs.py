#!/usr/bin/env python3
"""Audits IL2CPP metadata and Unity MonoBehaviour references across serialized files.

Metadata-only analysis. Does not patch the APK, dump source, export sprites,
publish binaries, infer server code, or bypass runtime asset encryption.
"""
from __future__ import annotations
import argparse
import collections
import csv
import hashlib
import json
import re
import shutil
import struct
import tempfile
import zipfile
from pathlib import Path

IL2CPP_MAGIC = 0xFAB11BAF
UI_TYPES = {
    "Image", "RawImage", "Button", "Text", "InputField", "Toggle",
    "Slider", "Scrollbar", "ScrollRect", "Dropdown", "Mask", "RectMask2D",
    "TextMeshProUGUI", "TMP_Text", "TMP_InputField", "TMP_Dropdown",
}
MAX_METADATA_BYTES = 128 * 1024 * 1024

def get(obj, key, default=None):
    return obj.get(key, default) if isinstance(obj, dict) else getattr(obj, key, default)

def pptr(ptr):
    try:
        return int(get(ptr, "m_FileID", get(ptr, "file_id", 0)) or 0), int(
            get(ptr, "m_PathID", get(ptr, "path_id", 0)) or 0)
    except (TypeError, ValueError):
        return 0, 0

def normalize(value):
    s = str(value or "").replace("\\", "/").lower()
    if s.startswith("archive:/"):
        s = s[len("archive:/"):]
    return s.rsplit("/", 1)[-1]

def classify(name):
    name = str(name or "").split(".")[-1]
    if name in UI_TYPES:
        return name
    if name.startswith("TMP_") or name.startswith("TextMeshPro"):
        return "TextMeshPro_family"
    return "Other"

def metadata_header(prefix, file_length):
    if len(prefix) < 8:
        return {"state": "too_short"}
    magic, version = struct.unpack_from("<II", prefix, 0)
    if magic != IL2CPP_MAGIC:
        return {"state": "nonstandard_or_obfuscated",
                "magic_matches": False, "version": None}
    pairs = []
    if 16 <= version <= 50:
        limit = min(len(prefix), 264)
        for index in range(8, limit - 7, 8):
            off, size = struct.unpack_from("<II", prefix, index)
            pairs.append((off, size))
    plausible = sum(1 for off, size in pairs if off <= file_length and size <= file_length - off)
    return {"state": "standard_header", "magic_matches": True, "version": version,
            "header_offset_size_pairs_checked": len(pairs),
            "plausible_pairs": plausible}

def hash_member(zf, member):
    h = hashlib.sha256()
    with zf.open(member) as stream:
        for block in iter(lambda: stream.read(1024 * 1024), b""):
            h.update(block)
    return h.hexdigest()

def audit_native(apks):
    records = []
    for apk in apks:
        with zipfile.ZipFile(apk) as zf:
            for member in zf.infolist():
                name = member.filename.lower()
                base = normalize(name)
                if base not in ("global-metadata.dat", "libil2cpp.so", "libunity.so",
                                "scriptingassemblies.json"):
                    continue
                row = {"apk": apk.name, "path": member.filename,
                       "size": member.file_size, "sha256": hash_member(zf, member)}
                if base == "global-metadata.dat":
                    with zf.open(member) as stream:
                        head = stream.read(264)
                    row["metadata"] = metadata_header(head, member.file_size)
                if base == "scriptingassemblies.json":
                    if member.file_size <= 1024 * 1024:
                        try:
                            body = json.loads(zf.read(member))
                            names = body.get("names", []) if isinstance(body, dict) else []
                            row["assembly_count"] = len(names)
                            row["contains_assembly_csharp"] = any(
                                "Assembly-CSharp" in str(x) for x in names)
                        except (ValueError, UnicodeDecodeError, TypeError):
                            row["json_state"] = "unreadable"
                records.append(row)
    return records

def aliases(file):
    out = set()
    for key in ("name", "file_name", "path", "original_path"):
        value = get(file, key)
        if value:
            out.add(normalize(value))
    return {value for value in out if value}

def build_file_registry(environments):
    """Globally index Unity SerializedFiles while keeping ambiguous aliases explicit."""
    by_object, readers, alias_map, serial_ids, file_labels = {}, {}, collections.defaultdict(set), {}, {}
    for source, env in environments:
        grouped = {}
        for reader in env.objects:
            af = reader.assets_file
            identity = id(af)
            if identity not in grouped:
                grouped[identity] = f"{source}__file{len(grouped):03d}"
                serial_ids[identity] = af
                file_labels[identity] = grouped[identity]
            readers[(identity, int(reader.path_id))] = reader
        for entry in getattr(env, "files", {}).items():
            name, af = entry
            if not hasattr(af, "objects"):
                continue
            aid = id(af)
            if aid in file_labels:
                alias_map[normalize(name)].add(aid)
        for identity, af in serial_ids.items():
            for candidate in aliases(af):
                alias_map[candidate].add(identity)
    return readers, serial_ids, file_labels, alias_map

def resolve_pointer(ptr, source_file, readers, alias_map):
    file_id, path_id = pptr(ptr)
    if path_id == 0:
        return None, "null"
    origin = id(source_file)
    if file_id == 0:
        return readers.get((origin, path_id)), "local" if (origin, path_id) in readers else "local_missing"
    externals = get(source_file, "externals", []) or []
    if file_id < 1 or file_id > len(externals):
        return None, "invalid_external_id"
    name = normalize(get(externals[file_id - 1], "path", ""))
    candidates = alias_map.get(name, set())
    if len(candidates) == 1:
        identity = next(iter(candidates))
        reader = readers.get((identity, path_id))
        return (reader, "external_resolved") if reader else (None, "external_object_missing")
    if len(candidates) > 1:
        return None, "ambiguous_external_name"
    # UnityPy can resolve within an Environment even when aliases are absent.
    if ptr is not None and hasattr(ptr, "deref"):
        try:
            obj = ptr.deref()
            return obj, "unitypy_deref"
        except Exception:
            pass
    return None, "external_file_missing"

def scan_unity(apks, inventory):
    import UnityPy
    stats = collections.Counter()
    script_counts = collections.Counter()
    examples = collections.defaultdict(list)
    outcomes = collections.Counter()
    type_links = collections.Counter()
    details = []
    with tempfile.TemporaryDirectory(prefix="unity_references_") as temp:
        envs = []
        for apk in apks:
            with zipfile.ZipFile(apk) as zf:
                for index, member in enumerate(zf.infolist()):
                    if member.is_dir() or not member.filename.lower().endswith(
                            (".unity3d", ".assetbundle", ".bundle")):
                        continue
                    label = f"{apk.stem}_{Path(member.filename).stem}"
                    target = Path(temp) / f"{len(envs):03d}.unity3d"
                    with zf.open(member) as src, target.open("wb") as dst:
                        shutil.copyfileobj(src, dst)
                    envs.append((label, UnityPy.load(str(target))))
        readers, serial_files, labels, aliases_map = build_file_registry(envs)
        stats["serialized_files"] = len(serial_files)
        stats["unity_objects_indexed"] = len(readers)
        stats["external_aliases"] = len(aliases_map)
        names_by_go = {}
        scripts_cache = {}
        for (identity, pid), reader in readers.items():
            kind = reader.type.name
            if kind == "GameObject":
                try:
                    names_by_go[(identity, pid)] = str(get(reader.read(), "m_Name", "") or "")
                except Exception:
                    stats["gameobject_name_unreadable"] += 1
            elif kind == "MonoScript":
                try:
                    sc = reader.read()
                    scripts_cache[(identity, pid)] = {
                        "name": str(get(sc, "m_ClassName", "") or ""),
                        "namespace": str(get(sc, "m_Namespace", "") or ""),
                        "assembly": str(get(sc, "m_AssemblyName", "") or "")}
                except Exception:
                    stats["script_name_unreadable"] += 1
        stats["mono_script_indexed"] = len(scripts_cache)
        for (identity, pid), reader in readers.items():
            if reader.type.name not in ("MonoBehaviour", "SpriteRenderer"):
                continue
            if reader.type.name == "SpriteRenderer":
                stats["sprite_renderers"] += 1
                try:
                    body = reader.read()
                    ptr = get(body, "m_Sprite")
                    if ptr and pptr(ptr)[1]:
                        ref, outcome = resolve_pointer(ptr, reader.assets_file, readers, aliases_map)
                        outcomes["sprite_renderer_" + outcome] += 1
                        if ref and ref.type.name == "Sprite":
                            stats["sprite_renderer_sprite_links"] += 1
                except Exception:
                    stats["sprite_renderer_unreadable"] += 1
                continue
            stats["mono_behaviours"] += 1
            try:
                head = reader.parse_monobehaviour_head()
            except Exception:
                try:
                    head = reader.read()
                except Exception:
                    stats["mono_head_unreadable"] += 1
                    continue
            scriptptr = get(head, "m_Script")
            script_reader, outcome = resolve_pointer(scriptptr, reader.assets_file, readers, aliases_map)
            outcomes["mono_script_" + outcome] += 1
            info = None
            if script_reader is not None and script_reader.type.name == "MonoScript":
                info = scripts_cache.get((id(script_reader.assets_file), int(script_reader.path_id)))
            if not info:
                stats["mono_script_unresolved"] += 1
                continue
            full_class = (info["namespace"] + "." if info["namespace"] else "") + info["name"]
            kind = classify(info["name"])
            script_counts[full_class] += 1
            if kind == "Other":
                continue
            stats["ui_components_identified"] += 1
            type_links[kind] += 1
            goptr = get(head, "m_GameObject")
            go_reader, go_status = resolve_pointer(goptr, reader.assets_file, readers, aliases_map)
            outcomes["ui_gameobject_" + go_status] += 1
            go_name = names_by_go.get(
                (id(go_reader.assets_file), int(go_reader.path_id)), "") if go_reader else ""
            target_file = labels.get(identity, "")
            sprite_status = "no_typetree"
            spr = None
            if kind in ("Image", "RawImage"):
                try:
                    tree = reader.read_typetree()
                    ptr = get(tree, "m_Sprite")
                    if pptr(ptr)[1]:
                        spr, sprite_status = resolve_pointer(ptr, reader.assets_file, readers, aliases_map)
                        outcomes["image_sprite_" + sprite_status] += 1
                    else:
                        sprite_status = "no_sprite"
                except Exception:
                    outcomes["image_typetree_unavailable"] += 1
            if spr is not None and spr.type.name == "Sprite":
                stats["image_sprite_links"] += 1
            if len(examples[kind]) < 12:
                examples[kind].append({"serialized_file": target_file,
                                      "gameobject": go_name[:100],
                                      "class": full_class})
            details.append({"file": target_file, "path_id": pid, "class": full_class,
                            "ui_type": kind, "gameobject": go_name[:120],
                            "gameobject_status": go_status,
                            "sprite_reference": sprite_status})
        inventory["serialized_files"] = len(serial_files)
        inventory["alias_keys"] = len(aliases_map)
    return {"counts": dict(stats), "ui_type_counts": dict(type_links.most_common()),
            "top_script_classes": dict(script_counts.most_common(30)),
            "resolution": dict(outcomes.most_common()),
            "samples": dict(examples), "components": details}

def save_csv(path, rows):
    with path.open("w", newline="", encoding="utf-8") as f:
        cols = ["file", "path_id", "class", "ui_type", "gameobject",
                "gameobject_status", "sprite_reference"]
        w = csv.DictWriter(f, fieldnames=cols)
        w.writeheader()
        w.writerows(rows)

def main():
    p = argparse.ArgumentParser()
    p.add_argument("--apk-dir", type=Path, default=Path("output/apks"))
    p.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    args = p.parse_args()
    apks = sorted(args.apk_dir.glob("*.apk"))
    if not apks:
        p.error("No unpacked APK files found")
    args.report_dir.mkdir(parents=True, exist_ok=True)
    native = audit_native(apks)
    scan_context = {}
    ui = scan_unity(apks, scan_context)
    result = {"native_files": native, "asset_reference_context": scan_context,
              "unity": {key: value for key, value in ui.items() if key != "components"},
              "limits": [
                  "Classifications are inferred from linked MonoScript class names only.",
                  "This does not recover method implementations or proprietary UI source.",
                  "Unity UI.Image m_Sprite needs a serialized typetree or verified class layout.",
                  "Ambiguous file aliases remain unresolved, not guessed.",
                  "No runtime-fetched assets included, no binaries exported or modified."]}
    (args.report_dir / "il2cpp-ref-summary.json").write_text(
        json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    save_csv(args.report_dir / "resolved-ui-components.csv", ui["components"])
    lines = ["# IL2CPP & Unity cross-file UI references", "",
             "This is a **static metadata analysis**, not an editable game client.", "",
             "## Native/metadata presence", "",
             "| File | APK | Bytes | IL2CPP header |", "|---|---|---:|---|"]
    for f in native:
        status = f.get("metadata", {}).get("state", "—")
        ver = f.get("metadata", {}).get("version")
        if ver is not None:
            status += f" (v{ver})"
        lines.append(f"| {f['path']} | {f['apk']} | {f['size']:,} | {status} |")
    lines += ["", "## UI classes resolved through MonoScript", "",
              "| Type | Components |", "|---|---:|"]
    lines.extend(f"| {name} | {count:,} |" for name, count in ui["ui_type_counts"].items())
    lines += ["", "## Resolution outcomes", "", "| Status | Count |", "|---|---:|"]
    lines.extend(f"| {name} | {count:,} |" for name, count in ui["resolution"].items())
    lines += ["", "## Caveats", "",
              "- MonoBehaviour classification comes from MonoScript references and class names.",
              "- Unity SpriteRenderer is not necessarily UI.Image.",
              "- Missing type trees mean Image.sprite references can remain unresolved.",
              "- Missing or protected IL2CPP metadata is reported, not bypassed.",
              "- No packaged game assets or source have been committed.", "",
              "See il2cpp-ref-summary.json and resolved-ui-components.csv for details.", ""]
    (args.report_dir / "IL2CPP_REFERENCE_REPORT.md").write_text(
        "\n".join(lines), encoding="utf-8")
    print(json.dumps({"status": "PASS", "native_files": len(native),
                      "mono_behaviours": ui["counts"].get("mono_behaviours", 0),
                      "ui_types": ui["ui_type_counts"],
                      "image_sprite_links": ui["counts"].get("image_sprite_links", 0)},
                     ensure_ascii=False))

if __name__ == "__main__":
    main()
