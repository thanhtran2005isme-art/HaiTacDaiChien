#!/usr/bin/env python3
"""Conservative candidate Spine object references for five screenshot scene roots.

Only PPtr destinations that resolve to actual MonoBehaviours with a known
Spine target class are included. Offset is NOT mapped to an original C# field.
No skin/default animation or texture pixels are inferred.
"""
from __future__ import annotations

import argparse
import collections
import csv
import json
import shutil
import struct
import tempfile
import zipfile
from pathlib import Path

from il2cpp_refs import build_file_registry, resolve_pointer
from sprite_links import endian_of

TARGET_CLASSES = {
    "Spine.Unity.SkeletonDataAsset",
    "Spine.Unity.SpineAtlasAsset",
}
FIELDS = (
    "reference", "ui_path", "source_serialized_file", "source_component_id",
    "source_class", "relation", "target_serialized_file", "target_component_id",
    "target_class", "byte_offset", "pointer_resolution",
    "field_is_known", "skin_is_known",
)
MAX_SCAN = 16384


def load_csv(path):
    with path.open("r", newline="", encoding="utf-8") as fd:
        return list(csv.DictReader(fd))


def find_typed_targets(raw, endian, external_count, resolver, typed_lookup,
                       max_scan=MAX_SCAN):
    if endian not in ("<", ">"):
        return []
    results = []
    for pos in range(0, min(len(raw), max_scan) - 11, 4):
        fid, pid = struct.unpack_from(endian + "iq", raw, pos)
        if fid < 0 or fid > external_count or pid <= 0:
            continue
        target, status = resolver(fid, pid)
        if target is None:
            continue
        key = (id(target.assets_file), int(target.path_id))
        target_cls = typed_lookup.get(key)
        if target_cls not in TARGET_CLASSES:
            continue
        results.append((pos, target, target_cls, status))
    return results


def collect(environments, related, scenes):
    registry, _, names, aliases = build_file_registry(environments)
    indexed = {(names[ident], int(pid)): reader
               for (ident, pid), reader in registry.items()}
    classes = {}
    for row in related:
        if row.get("class") not in TARGET_CLASSES:
            continue
        try:
            reader = indexed[(row["serialized_file"], int(row["component_id"]))]
        except (ValueError, KeyError):
            continue
        classes[(id(reader.assets_file), int(reader.path_id))] = row["class"]

    summary = collections.Counter()
    records, first_hop = [], []
    for row in scenes:
        summary["scene_spine_components"] += 1
        try:
            reader = indexed[(row["reference_bundle"], int(row["component_id"]))]
        except (ValueError, KeyError):
            summary["missing_scene_component"] += 1
            continue
        fileobj = reader.assets_file
        externals = len(getattr(fileobj, "externals", []) or [])
        def resolver(fid, pid):
            return resolve_pointer({"m_FileID": fid, "m_PathID": pid},
                                   fileobj, registry, aliases)
        try:
            candidates = find_typed_targets(
                reader.get_raw_data(), endian_of(reader),
                externals, resolver, classes)
        except Exception:
            summary["read_failures"] += 1
            continue
        summary["direct_candidate_links"] += len(candidates)
        if not candidates:
            summary["scene_components_without_direct_targets"] += 1
        if len(candidates) > 1:
            summary["scene_components_with_multiple_targets"] += 1
        for offset, target, target_class, status in candidates:
            first_hop.append((row, target, target_class))
            records.append({
                "reference": row["reference"], "ui_path": row["ui_path"],
                "source_serialized_file": row["reference_bundle"],
                "source_component_id": row["component_id"],
                "source_class": row["class"],
                "relation": "direct_typed_pointer_candidate",
                "target_serialized_file": names.get(id(target.assets_file), ""),
                "target_component_id": int(target.path_id),
                "target_class": target_class,
                "byte_offset": offset, "pointer_resolution": status,
                "field_is_known": False, "skin_is_known": False,
            })
    seen = set()
    for row, reader, klass in first_hop:
        if klass != "Spine.Unity.SkeletonDataAsset":
            continue
        key = (row["reference"], id(reader.assets_file), int(reader.path_id))
        if key in seen:
            continue
        seen.add(key)
        fileobj = reader.assets_file
        def resolver(fid, pid):
            return resolve_pointer({"m_FileID": fid, "m_PathID": pid},
                                   fileobj, registry, aliases)
        try:
            sub = find_typed_targets(reader.get_raw_data(), endian_of(reader),
                                     len(getattr(fileobj, "externals", []) or []),
                                     resolver, classes)
        except Exception:
            summary["skeleton_data_read_failures"] += 1
            continue
        for offset, atlas, candidate_cls, status in sub:
            if candidate_cls != "Spine.Unity.SpineAtlasAsset":
                continue
            summary["skeleton_to_atlas_candidate_links"] += 1
            records.append({
                "reference": row["reference"], "ui_path": row["ui_path"],
                "source_serialized_file": names.get(id(reader.assets_file), ""),
                "source_component_id": int(reader.path_id),
                "source_class": klass,
                "relation": "data_asset_to_atlas_pointer_candidate",
                "target_serialized_file": names.get(id(atlas.assets_file), ""),
                "target_component_id": int(atlas.path_id),
                "target_class": candidate_cls,
                "byte_offset": offset, "pointer_resolution": status,
                "field_is_known": False, "skin_is_known": False,
            })
    summary["known_asset_target_components"] = len(classes)
    return records, dict(summary)


def main():
    p = argparse.ArgumentParser()
    p.add_argument("--apk-dir", type=Path, default=Path("output/apks"))
    p.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    args = p.parse_args()
    folder = args.report_dir
    candidates = load_csv(folder / "scene-spine-components.csv")
    references = {row["reference"]: row["bundle"]
                  for row in load_csv(folder / "ui-screenshot-reference-previews.csv")}
    for row in candidates:
        row["reference_bundle"] = references[row["reference"]]
    related = load_csv(folder / "canvas-spine-components.csv")
    files = sorted(args.apk_dir.glob("*.apk"))
    if not files:
        p.error("No unpacked APKs")
    import UnityPy
    with tempfile.TemporaryDirectory(prefix="spine_ref_probe_") as tmp:
        envs = []
        for apk in files:
            with zipfile.ZipFile(apk) as zf:
                for member in zf.infolist():
                    if member.is_dir() or not member.filename.lower().endswith(
                            (".unity3d", ".bundle", ".assetbundle")):
                        continue
                    path = Path(tmp) / f"{len(envs)}.unity3d"
                    with zf.open(member) as inp, path.open("wb") as out:
                        shutil.copyfileobj(inp, out)
                    envs.append((f"{apk.stem}_{Path(member.filename).stem}",
                                 UnityPy.load(str(path))))
        rows, totals = collect(envs, related, candidates)

    with (folder / "scene-spine-asset-candidates.csv").open(
            "w", newline="", encoding="utf-8") as stream:
        writer = csv.DictWriter(stream, fieldnames=FIELDS)
        writer.writeheader()
        writer.writerows(rows)
    result = {"totals": totals, "rows": len(rows),
              "limits": [
                  "Candidate pointers resolve to typed Spine assets, but serialized field offset is unverified.",
                  "One scene component can contain multiple typed pointers; do not pick one arbitrarily.",
                  "Default skin, selected character, active animation and texture pixels are not known.",
                  "No binaries, game artwork or private screenshots exported."]}
    (folder / "scene-spine-probe-summary.json").write_text(
        json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    lines = [
        "# Spine dependency pointer candidates",
        "",
        "Static Unity PPtr matches to known Spine MonoBehaviour targets, NOT a reconstructed skin/animation.",
        "", "| Metric | Count |", "|---|---:|",
    ]
    lines.extend(f"| {k} | {v} |" for k, v in sorted(totals.items()))
    lines.extend([
        "", "## Unresolved properties", "",
        "- SkeletonDataAsset references are candidates only; the original serialized field is unknown.",
        "- Skin selection and animation track are UNKNOWN and require type reconstruction or runtime inspection.",
        "- No downloaded assets, Spine atlas textures or frame animations were exported.",
        "",
        "See scene-spine-asset-candidates.csv for exact pointer IDs and offsets.", "",
    ])
    (folder / "SCENE_SPINE_PROBE.md").write_text("\n".join(lines), encoding="utf-8")
    print(json.dumps({"status": "PASS", "rows": len(rows), **totals}, ensure_ascii=False))


if __name__ == "__main__":
    main()
