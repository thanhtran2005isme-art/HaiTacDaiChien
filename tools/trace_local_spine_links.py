#!/usr/bin/env python3
"""Audit original Spine component → SkeletonDataAsset → AtlasAsset → TextAsset.

Strictly requires serialized Unity PPtrs and original source file ordinals.
Reports only evidence/animation *names*, never asset data or guessed skin state.
A pointer at a plausible offset is NOT proof of its C# field name.
"""
from __future__ import annotations
import argparse
import collections
import csv
import json
import struct
from pathlib import Path

from export_local_ui_layout import ROOT, build as layout_build, scene_bundle_prefix
from export_local_ui_layout import MAX_BUNDLE, SAFE_ID
from export_local_ui_layout import choose_serialized_file
from probe_spine_payloads import classify_payload, payload_bytes
from export_local_spine import atlas_pages, make_package_name
from sprite_links import endian_of

MAX_RAW = 16384


def read_csv(path):
    with path.open(encoding="utf-8", newline="") as stream:
        return list(csv.DictReader(stream))


def exact_file(groups, label):
    try:
        index = int(label.rsplit("__file", 1)[1])
        return list(groups.values())[index]
    except (IndexError, ValueError, KeyError):
        return {}


def raw_local_targets(reader, readers, target_type):
    """Candidate local pointers with correct endian/type, never unknown fid."""
    raw = reader.get_raw_data()
    endian = endian_of(reader)
    if endian not in ("<", ">"):
        return []
    found = {}
    for offset in range(0, min(MAX_RAW, len(raw)) - 11, 4):
        fid, pid = struct.unpack_from(endian + "iq", raw, offset)
        if fid != 0 or pid <= 0:
            continue
        target = readers.get(pid)
        if target is None or target.type.name != target_type:
            continue
        found[(pid, offset)] = target
    return [(pid, offset, target) for (pid, offset), target in found.items()]


def named_text_candidates(reader, readers, requested_kind):
    out = []
    for pid, offset, candidate in raw_local_targets(reader, readers, "TextAsset"):
        try:
            obj = candidate.read()
            name = str(getattr(obj, "m_Name", "") or "")
            raw = payload_bytes(obj)
            if raw is None or classify_payload(name, raw) != requested_kind:
                continue
            version = None
            animations = []
            pages = []
            if requested_kind == "spine_json_verified":
                skeleton = json.loads(raw.decode("utf-8-sig"))
                version = str(skeleton.get("skeleton", {}).get("spine", ""))
                animations = sorted(skeleton.get("animations", {}))
            else:
                pages = atlas_pages(raw)
            out.append({"pathId": pid, "byteOffset": offset, "name": name,
                        "version": version, "animations": animations[:200],
                        "pages": pages})
        except (OSError, ValueError, AttributeError, TypeError):
            continue
    return out


def trace_one(scene_id, node_id, component_id, direct, atlas_rows, groups,
              texture_names):
    base = {"sceneId": scene_id, "nodeId": node_id,
            "componentId": component_id,
            "status": "no_typed_skeleton_pointer",
            "fieldVerified": False, "skinVerified": False,
            "activeAnimationVerified": False}
    if len(direct) != 1:
        base["status"] = "ambiguous_skeleton_candidates" if direct else base["status"]
        base["skeletonCandidateCount"] = len(direct)
        return base
    row = direct[0]
    readers = exact_file(groups, row["target_serialized_file"])
    skeleton_reader = readers.get(int(row["target_component_id"]))
    if skeleton_reader is None or skeleton_reader.type.name != "MonoBehaviour":
        base["status"] = "skeleton_component_not_found"
        return base
    base["skeletonDataAssetId"] = int(row["target_component_id"])
    base["skeletonSourceFile"] = row["target_serialized_file"]
    skeletons = named_text_candidates(skeleton_reader, readers, "spine_json_verified")
    base["jsonContentCandidates"] = len(skeletons)
    relevant_atlases = [a for a in atlas_rows if
                        a["source_serialized_file"] == row["target_serialized_file"] and
                        a["source_component_id"] == row["target_component_id"]]
    if len(skeletons) != 1 or len(relevant_atlases) != 1:
        base["status"] = "incomplete_or_ambiguous_textasset_chain"
        base["atlasAssetCandidates"] = len(relevant_atlases)
        return base
    matched = relevant_atlases[0]
    atlas_group = exact_file(groups, matched["target_serialized_file"])
    atlas_reader = atlas_group.get(int(matched["target_component_id"]))
    if atlas_reader is None or atlas_reader.type.name != "MonoBehaviour":
        base["status"] = "atlas_component_not_found"
        return base
    atlases = named_text_candidates(atlas_reader, atlas_group, "atlas_text_candidate")
    base["atlasTextCandidates"] = len(atlases)
    if len(atlases) != 1:
        base["status"] = "ambiguous_or_missing_atlas_textasset"
        return base
    skeleton, atlas = skeletons[0], atlases[0]
    if atlas["name"].removesuffix(".atlas") != skeleton["name"] or not atlas["pages"]:
        base["status"] = "skeleton_atlas_name_or_page_mismatch"
        return base
    base.update({
        "status": "content_chain_verified_field_unverified",
        "skeletonName": skeleton["name"],
        "spineVersion": skeleton["version"],
        "animationNames": skeleton["animations"],
        "atlasName": atlas["name"],
        "atlasPages": atlas["pages"],
        "missingTextureNames": [
            p for p in atlas["pages"] if p not in texture_names and
            Path(p).stem not in texture_names],
        "localPackId": make_package_name(skeleton["name"]),
        "skeletonJsonPathId": skeleton["pathId"],
        "atlasTextPathId": atlas["pathId"],
        "atlasAssetId": int(matched["target_component_id"]),
    })
    # Content identity is proven at local TextAsset offsets; the original
    # SkeletonGraphic private field and runtime skin/track remain unverified.
    return base


def inspect_bundle(env, scenes, rows, texture_names=None):
    groups = collections.defaultdict(dict)
    for obj in env.objects:
        groups[id(obj.assets_file)][int(obj.path_id)] = obj
    # Discover texture names without copying or exporting their image pixels.
    if texture_names is None:
        texture_names = set()
        for group in groups.values():
            for item in group.values():
                if item.type.name != "Texture2D":
                    continue
                try:
                    name = str(getattr(item.read(), "m_Name", "") or "")
                    if name:
                        texture_names.add(name)
                except Exception:
                    continue
    direct = collections.defaultdict(list)
    atlases = []
    for row in rows:
        if row["relation"] == "direct_typed_pointer_candidate" and (
                row["target_class"] == "Spine.Unity.SkeletonDataAsset"):
            direct[(row["reference"], row["source_component_id"])].append(row)
        elif row["relation"] == "data_asset_to_atlas_pointer_candidate" and (
                row["target_class"] == "Spine.Unity.SpineAtlasAsset"):
            atlases.append(row)
    output = []
    for scene in scenes:
        paths = collections.defaultdict(list)
        for node in scene["nodes"]:
            paths[node["path"]].append(node["id"])
        for component in scene["spine_components"]:
            if component["class"] != "Spine.Unity.SkeletonGraphic":
                continue
            c_id = component["component_id"]
            located = paths.get(component["ui_path"], [])
            if len(located) != 1:
                continue
            record = trace_one(scene["id"], int(located[0]), c_id,
                               direct.get((scene["id"], c_id), []),
                               atlases, groups, texture_names)
            output.append(record)
    return output


def build(root=ROOT, xapk=None, unitypy=None):
    """Reuse safe local XAPK wrapper and exact scene SerializedFile naming."""
    # Avoid duplicating the archive scan: the same extracted JSON is used by the
    # UI layout importer. Instrument the UnityPy loader to inspect loaded
    # environments, which never leave local memory or the runner.
    import export_local_ui_layout as layout
    if unitypy is None:
        import UnityPy as unitypy
    source = json.loads((root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json")
                        .read_text(encoding="utf-8"))["scenes"]
    refs = {scene["id"]: scene for scene in source}
    candidates = read_csv(root / "reports/xapk/scene-spine-asset-candidates.csv")
    components = read_csv(root / "reports/xapk/scene-spine-components.csv")
    if len(source) != 5:
        raise ValueError("Expected exactly 5 source candidates")
    prepared = []
    for scene in source:
        joined = dict(scene)
        joined["spine_components"] = [
            c for c in components if c["reference"] == scene["id"]]
        prepared.append(joined)
    reports = []
    class HookedUnity:
        def load(self, blob):
            env = unitypy.load(blob)
            if not reports:
                reports.extend(inspect_bundle(env, prepared, candidates))
            return env
    # Use the same exact bundle choice that previously passed on Windows/Linux.
    checked = layout.build(root, xapk=xapk, unitypy=HookedUnity())
    if checked["stats"]["verifiedTransforms"] != 1670:
        raise ValueError("Incomplete source hierarchy")
    counts = collections.Counter(x["status"] for x in reports)
    return {"version": 1, "status": "evidence_only_no_runtime_binding",
            "source": "local XAPK real serialized PPtr and TextAsset content",
            "records": reports, "stats": dict(counts),
            "limits": [
                "PPtr offsets are typed candidates, not named C# fields.",
                "Even a unique skeleton/atlas content chain does not prove active skin or animation.",
                "No runtime SkeletonGraphic is automatically created without an official Spine Unity runtime.",
                "No original or commercial game assets are saved to GitHub.",
            ]}


def main():
    p = argparse.ArgumentParser(description=__doc__)
    p.add_argument("--root", type=Path, default=ROOT)
    p.add_argument("--xapk", type=Path)
    p.add_argument("--output", type=Path)
    args = p.parse_args()
    root = args.root.resolve()
    data = build(root, args.xapk)
    path = args.output or root / "output/local-spine-link-evidence.json"
    path.parent.mkdir(parents=True, exist_ok=True)
    tmp = path.with_suffix(".tmp")
    tmp.write_text(json.dumps(data, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    tmp.replace(path)
    print(json.dumps({"status": "PASS", "links": len(data["records"]),
                      "counts": data["stats"], "output": str(path)}, ensure_ascii=False))


if __name__ == "__main__":
    main()
