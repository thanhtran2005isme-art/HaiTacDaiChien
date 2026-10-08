#!/usr/bin/env python3
"""Link Unity UI.Image components to Sprite metadata by verified PPtr targets.

No copyrighted images or binaries are copied. When MonoBehaviour type trees
are unavailable, the location of m_Sprite is *inferred*, not reconstructed.
Only pointers resolving to genuine Sprite objects are candidates. Ambiguous
matches remain ambiguous. Nothing is assigned by filename similarity.
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

MAX_SCAN_BYTES = 8192
ENDIANS = ("<", ">")
IMAGE_TYPES = {"Image"}  # RawImage uses m_Texture, not m_Sprite.
RESULT_COLS = ["bundle", "component_id", "ui_path", "gameobject",
               "sprite_serialized_file", "sprite_id", "sprite_name",
               "raw_byte_offset", "match_count_at_offset",
               "resolution", "confidence", "method"]


def endian_of(reader):
    """Use serialized-file endian, never silently assume little-endian."""
    for candidate in (getattr(reader.assets_file, "reader", None),
                      getattr(reader, "reader", None)):
        value = getattr(candidate, "endian", None)
        if value in ENDIANS:
            return value
        if isinstance(value, str) and value.lower() in ("little", "big"):
            return "<" if value.lower() == "little" else ">"
    return None


def raw_pointer_candidates(raw, endian, max_external, lookup, max_scan=MAX_SCAN_BYTES):
    """Return exactly typed Sprite matches, never arbitrary 64-bit numbers.

    A Unity PPtr is signed i32 fileID + signed i64 pathID on this asset version.
    Respect 4-byte alignment and reject values out of bounds.
    lookup(file_id, path_id) must verify the resolved object's actual Unity type.
    """
    if endian not in ENDIANS:
        raise ValueError("unrecognized endian")
    rows = []
    for off in range(0, min(len(raw), max_scan) - 11, 4):
        file_id, path_id = struct.unpack_from(endian + "iq", raw, off)
        if not (0 <= file_id <= max_external and path_id > 0):
            continue
        ref, status = lookup(file_id, path_id)
        if ref is None or getattr(getattr(ref, "type", None), "name", None) != "Sprite":
            continue
        rows.append({"offset": off, "file_id": file_id,
                     "path_id": path_id, "reader": ref, "status": status})
    return rows


def calibration(matches):
    """Only repeated, independent single-candidate hits validate field offset."""
    frequencies = collections.Counter()
    for hits in matches:
        keys = {x["offset"] for x in hits}
        if len(hits) == 1 and len(keys) == 1:
            frequencies[hits[0]["offset"]] += 1
    return frequencies


def rank_match(hits, frequencies):
    """Conservative labeling: ambiguity cannot be promoted by popularity."""
    if not hits:
        return None, "unresolved"
    if len(hits) != 1:
        return None, "ambiguous_multiple_sprite_pointers"
    candidate = hits[0]
    # Two independent Image components corroborate a serialized offset.
    return candidate, ("probable_m_sprite" if frequencies[candidate["offset"]] >= 2
                       else "unconfirmed_sprite_pointer")


def load_component_index(file_path):
    with file_path.open("r", encoding="utf-8", newline="") as stream:
        rows = list(csv.DictReader(stream))
    index = {}
    for row in rows:
        if row.get("ui_type") in IMAGE_TYPES:
            try:
                index[(row["file"], int(row["path_id"]))] = row
            except (KeyError, ValueError):
                continue
    return index


def load_ui_paths(file_path):
    by_object = {}
    if not file_path.exists():
        return by_object
    with file_path.open("r", encoding="utf-8", newline="") as f:
        for row in csv.DictReader(f):
            try:
                by_object[(row["bundle"], int(row["gameobject_id"]))] = row["path"]
            except (KeyError, ValueError):
                continue
    return by_object


def tree_has_sprite(node):
    if not node:
        return False
    try:
        return "m_Sprite" in node.dump_structure()
    except Exception:
        return False


def decode_type_tree_sprites(reader, resolver):
    """Only rely on complete type trees with an explicit m_Sprite field."""
    node = getattr(getattr(reader, "serialized_type", None), "node", None)
    if not tree_has_sprite(node):
        return None, "missing_declared_m_sprite"
    try:
        tree = reader.read_typetree()
        ptr = tree.get("m_Sprite") if isinstance(tree, dict) else getattr(tree, "m_Sprite", None)
        f_id, p_id = (int(getattr(ptr, "m_FileID", ptr.get("m_FileID", 0) if isinstance(ptr, dict) else 0)),
                     int(getattr(ptr, "m_PathID", ptr.get("m_PathID", 0) if isinstance(ptr, dict) else 0)))
        if not p_id:
            return None, "declared_null_sprite"
        target, status = resolver(f_id, p_id)
        if target is not None and target.type.name == "Sprite":
            return {"offset": None, "file_id": f_id, "path_id": p_id,
                    "reader": target, "status": status}, "verified_typetree"
        return None, "declared_sprite_not_resolved"
    except Exception as exc:
        return None, "type_tree_read_error_" + type(exc).__name__


def analyze(apks, index, paths):
    import UnityPy
    stats, issues = collections.Counter(), []
    output = []
    sprite_names = {}
    with tempfile.TemporaryDirectory(prefix="unity_sprite_refs_") as work:
        envs = []
        for apk in apks:
            with zipfile.ZipFile(apk) as archive:
                for member in archive.infolist():
                    if member.is_dir() or not member.filename.lower().endswith(
                            (".unity3d", ".bundle", ".assetbundle")):
                        continue
                    filename = Path(work) / f"bundle_{len(envs)}.unity3d"
                    with archive.open(member) as source, filename.open("wb") as dest:
                        shutil.copyfileobj(source, dest)
                    label = f"{apk.stem}_{Path(member.filename).stem}"
                    envs.append((label, UnityPy.load(str(filename))))
        readers, files, labels, aliases = build_file_registry(envs)
        by_label = {(labels[k], p): reader for (k, p), reader in readers.items()}
        stats["serialized_files"] = len(files)
        candidates = []
        for (label, cid), item in index.items():
            stats["image_components_from_prior_audit"] += 1
            reader = by_label.get((label, cid))
            if reader is None:
                stats["missing_image_component"] += 1
                continue
            go_id = int(item.get("gameobject", "0") or 0) if False else 0
            # Earlier inventory contains the GameObject name, not ID. Resolve it from
            # m_GameObject in the MonoBehaviour head to join the authoritative tree.
            try:
                head = reader.parse_monobehaviour_head()
                ptr = head.m_GameObject
                go_id = int(getattr(ptr, "m_PathID", 0) or 0) if int(
                    getattr(ptr, "m_FileID", 0) or 0) == 0 else 0
            except Exception:
                stats["ui_head_read_failed"] += 1
            ui_path = paths.get((label, go_id), "")
            raw = reader.get_raw_data()
            endian = endian_of(reader)
            if not endian:
                stats["unknown_byte_order"] += 1
                continue
            externals = getattr(reader.assets_file, "externals", []) or []
            def resolve(f_id, p_id):
                return resolve_pointer({"m_FileID": f_id, "m_PathID": p_id},
                                       reader.assets_file, readers, aliases)
            typed, typed_state = decode_type_tree_sprites(reader, resolve)
            if typed_state == "verified_typetree":
                stats["explicit_typetree_sprite"] += 1
                hits = [typed]
            elif typed_state == "declared_null_sprite":
                stats["explicit_typetree_null"] += 1
                hits = []
            else:
                stats["no_usable_image_typetree"] += 1
                hits = raw_pointer_candidates(raw, endian, len(externals), resolve)
            if len(raw) > MAX_SCAN_BYTES:
                stats["scan_limited_large_components"] += 1
            stats["total_resolved_sprite_candidate_pointers"] += len(hits)
            if len(hits) == 1:
                stats["images_with_one_candidate"] += 1
            elif len(hits) > 1:
                stats["images_with_multiple_candidates"] += 1
            else:
                stats["images_without_matching_sprite"] += 1
            candidates.append({"label": label, "component_id": cid,
                               "gameobject": item.get("gameobject", ""),
                               "ui_path": ui_path, "hits": hits,
                               "typed_state": typed_state})
        # Offset calibration is per Unity serialized file; no cross-version mixing.
        calib = collections.defaultdict(list)
        for row in candidates:
            if row["typed_state"] != "verified_typetree":
                calib[row["label"]].append(row["hits"])
        offset_counts = {label: calibration(hitsets) for label, hitsets in calib.items()}
        for item in candidates:
            hits = item["hits"]
            if item["typed_state"] == "verified_typetree":
                chosen, confidence = hits[0], "verified_typetree"
            else:
                chosen, confidence = rank_match(hits, offset_counts.get(item["label"], {}))
            stats["confidence_" + confidence] += 1
            if not chosen:
                continue
            spr = chosen["reader"]
            key = (id(spr.assets_file), int(spr.path_id))
            if key not in sprite_names:
                try:
                    sprite_names[key] = str(getattr(spr.read(), "m_Name", "") or "")
                except Exception:
                    sprite_names[key] = ""
                    stats["sprite_name_unreadable"] += 1
            target_label = labels.get(id(spr.assets_file), "")
            offset = chosen["offset"]
            output.append({
                "bundle": item["label"], "component_id": item["component_id"],
                "ui_path": item["ui_path"], "gameobject": item["gameobject"],
                "sprite_serialized_file": target_label,
                "sprite_id": int(spr.path_id), "sprite_name": sprite_names[key],
                "raw_byte_offset": "" if offset is None else offset,
                "match_count_at_offset": ("" if offset is None else
                                          offset_counts.get(item["label"], {}).get(offset, 0)),
                "resolution": chosen["status"],
                "confidence": confidence,
                "method": "explicit_typetree" if item["typed_state"] == "verified_typetree"
                          else "typed_sprite_pptr_fallback"})
        patterns = []
        for label, freqs in offset_counts.items():
            if freqs:
                patterns.append({"serialized_file": label,
                                 "leading_offsets": [{"byte_offset": off, "single_matches": count}
                                                     for off, count in freqs.most_common(8)]})
        return output, stats, patterns, issues


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--apk-dir", type=Path, default=Path("output/apks"))
    parser.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    args = parser.parse_args()
    report = args.report_dir
    index = load_component_index(report / "resolved-ui-components.csv")
    if not index:
        parser.error("resolved-ui-components.csv is absent or contains no Image components")
    paths = load_ui_paths(report / "ui-hierarchy.csv")
    apks = sorted(args.apk_dir.glob("*.apk"))
    if not apks:
        parser.error("No unpacked APKs found")
    matches, stats, calibration_summary, issues = analyze(apks, index, paths)
    report.mkdir(parents=True, exist_ok=True)
    with (report / "ui-image-sprite-links.csv").open("w", newline="", encoding="utf-8") as fp:
        writer = csv.DictWriter(fp, fieldnames=RESULT_COLS)
        writer.writeheader()
        writer.writerows(matches)
    summary = {"totals": dict(stats), "linked_rows": len(matches),
               "offset_calibration": calibration_summary,
               "issues": issues,
               "interpretation": [
                   "verified_typetree: explicit serialized m_Sprite field points to Sprite.",
                   "probable_m_sprite: one type-checked Sprite PPtr with repeated offset within same file.",
                   "unconfirmed_sprite_pointer: a single matching Sprite PPtr without offset corroboration.",
                   "ambiguous matches excluded, null sprites are not counted as recovered.",
                   "This is a static inferred link; complete source/prefab functionality is not recovered.",
                   "Only metadata and object names, not game images or bundles, are published."]}
    (report / "ui-image-sprite-summary.json").write_text(
        json.dumps(summary, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    lines = ["# Unity Image ↔ Sprite reference analysis", "",
             "This scanner verifies every Sprite candidate against an actual Unity Sprite",
             "object and its serialized-file destination. **It does not manufacture type trees.**",
             "", "## Counts", "", "| Metric | Value |", "|---|---:|"]
    lines += [f"| {key} | {value:,} |" for key, value in sorted(stats.items())]
    lines += ["", "## Confidence levels", "",
              "- **verified_typetree**: parsed explicit m_Sprite through type tree",
              "- **probable_m_sprite**: unique validated Sprite pointer at an offset repeated in >=2 Image components from same serialized file",
              "- **unconfirmed_sprite_pointer**: unique pointer without corroborated offset",
              "- Ambiguous or missing pointers remain unlinked", "",
              "## Output", "",
              "See ui-image-sprite-links.csv and ui-image-sprite-summary.json.",
              "This report is only structural research metadata, not a working game UI.", ""]
    (report / "UI_SPRITE_LINK_REPORT.md").write_text("\n".join(lines), encoding="utf-8")
    print(json.dumps({"status": "PASS", "linked_rows": len(matches),
                      "totals": dict(stats)}, ensure_ascii=False))


if __name__ == "__main__":
    main()
