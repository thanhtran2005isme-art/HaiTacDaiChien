#!/usr/bin/env python3
"""Join scene-level UI Image→Sprite→Texture2D, Spine, unresolved metadata.

Input is previous static Unity inventories. No copyrighted image/asset bytes,
screenshots, decompiled client source or player data are output.
"""
import argparse
import collections
import csv
import json
from pathlib import Path

IMAGE_FIELDS = ("reference", "ui_path", "component_id", "sprite_name", "sprite_file",
                "sprite_id", "confidence", "texture_name", "texture_file",
                "texture_id", "texture_size", "texture_status")
SPINE_FIELDS = ("reference", "ui_path", "class", "component_id",
                "gameobject_id", "typetree_status", "skin_status")
MISSING_FIELDS = ("reference", "ui_path", "component_id", "gameobject",
                  "active", "classification")

def read_csv(path):
    with path.open(newline="", encoding="utf-8") as f:
        return list(csv.DictReader(f))

def write_csv(path, data, columns):
    with path.open("w", newline="", encoding="utf-8") as f:
        w = csv.DictWriter(f, fieldnames=columns, extrasaction="ignore")
        w.writeheader()
        w.writerows(data)

def under_root(path, root):
    return bool(path and root and (path == root or path.startswith(root + "/")))

def inspect(references, sprites, textures, spine, missing):
    names = set()
    indexed_textures = {(r["sprite_serialized_file"], r["sprite_id"]): r
                        for r in textures}
    summary, image_rows, spine_rows, missing_rows = [], [], [], []
    for ref in references:
        ref_id, bundle = ref["reference"], ref["bundle"]
        if ref_id in names:
            raise ValueError("duplicate screenshot reference: " + ref_id)
        names.add(ref_id)
        prefix = "/" + ref["root"].strip("/")
        image_matches = [r for r in sprites if r["bundle"] == bundle
                         and under_root(r["ui_path"], prefix)]
        spine_matches = [r for r in spine if r["serialized_file"] == bundle
                         and r["category"] == "spine"
                         and under_root(r.get("ui_path", ""), prefix)]
        missing_matches = [r for r in missing if r["bundle"] == bundle
                           and under_root(r["ui_path"], prefix)]
        texture_keys = set()
        missing_textures = 0
        for r in image_matches:
            t = indexed_textures.get((r["sprite_serialized_file"], r["sprite_id"]))
            if not t or not t.get("texture_id") or not t.get("texture_serialized_file"):
                missing_textures += 1
            else:
                texture_keys.add((t["texture_serialized_file"], t["texture_id"]))
            image_rows.append({
                "reference": ref_id, "ui_path": r["ui_path"],
                "component_id": r["component_id"],
                "sprite_name": r["sprite_name"],
                "sprite_file": r["sprite_serialized_file"],
                "sprite_id": r["sprite_id"], "confidence": r["confidence"],
                "texture_name": t.get("texture_name", "") if t else "",
                "texture_file": t.get("texture_serialized_file", "") if t else "",
                "texture_id": t.get("texture_id", "") if t else "",
                "texture_size": t.get("texture_size", "") if t else "",
                "texture_status": (t.get("texture_resolution", "")
                                   if t else "texture_metadata_missing")
            })
        for r in spine_matches:
            spine_rows.append({
                "reference": ref_id, "ui_path": r["ui_path"],
                "class": r["class"], "component_id": r["component_id"],
                "gameobject_id": r["gameobject_id"],
                "typetree_status": r["typetree_status"],
                "skin_status": "unknown_not_proven_from_static_metadata",
            })
        for r in missing_matches:
            missing_rows.append({
                "reference": ref_id, "ui_path": r["ui_path"],
                "component_id": r["component_id"],
                "gameobject": r.get("gameobject", ""),
                "active": r.get("active", ""),
                "classification": r.get("classification", "unknown"),
            })
        summary.append({
            "reference": ref_id, "bundle": bundle, "root": ref["root"],
            "root_id": ref["root_id"], "confidence": ref["confidence"],
            "linked_image_components": len(image_matches),
            "unique_sprite_targets": len({(r["sprite_serialized_file"], r["sprite_id"])
                                          for r in image_matches}),
            "unique_texture_targets": len(texture_keys),
            "missing_texture_metadata": missing_textures,
            "spine_visual_component_count": len(spine_matches),
            "spine_visual_class_counts": dict(collections.Counter(
                r["class"] for r in spine_matches).most_common()),
            "unlinked_image_components": len(missing_matches),
            "unlinked_pointer_classes": dict(collections.Counter(
                r.get("classification", "unknown") for r in missing_matches).most_common()),
            "runtime_spine_skin": None,
            "runtime_animation_track": None,
            "physical_canvas_size": None,
        })
    return summary, image_rows, spine_rows, missing_rows

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    a = parser.parse_args()
    folder = a.report_dir
    summaries, images, spine, missing = inspect(
        read_csv(folder / "ui-screenshot-reference-previews.csv"),
        read_csv(folder / "ui-image-sprite-links.csv"),
        read_csv(folder / "sprite-texture-metadata.csv"),
        read_csv(folder / "canvas-spine-components.csv"),
        read_csv(folder / "image-null-pointer-audit.csv"),
    )
    write_csv(folder / "scene-image-texture-links.csv", images, IMAGE_FIELDS)
    write_csv(folder / "scene-spine-components.csv", spine, SPINE_FIELDS)
    write_csv(folder / "scene-unresolved-images.csv", missing, MISSING_FIELDS)
    totals = {"reference_candidates": len(summaries), "linked_images": len(images),
              "spine_visual_components": len(spine), "unlinked_images": len(missing)}
    result = {
        "references": summaries, "totals": totals,
        "limitations": [
            "Scene associations derive from four user screenshots and are not pixel verified.",
            "Spine classes do not prove animation state, default skin, or rendering.",
            "Null Image Sprite pointer does not prove a runtime assignment.",
            "Neither actual Canvas size nor Unity reference resolution has been recovered.",
            "No proprietary game artwork or source code was exported."
        ]
    }
    (folder / "scene-visual-summary.json").write_text(
        json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    lines = [
        "# Scene visual dependency audit", "",
        "Four screenshots correspond to five serialized scene candidates.",
        "This is a static asset dependency inventory, not an actual game render.",
        "", "| Reference | Images linked | Textures | Spine component | Unlinked Images |",
        "|---|---:|---:|---:|---:|",
    ]
    for r in summaries:
        lines.append(f"| {r['reference']} | {r['linked_image_components']} | "
                     f"{r['unique_texture_targets']} | "
                     f"{r['spine_visual_component_count']} | "
                     f"{r['unlinked_image_components']} |")
    lines += [
        "", "## Required checks before claiming visual reconstruction", "",
        "- Spine skins and playback tracks: UNKNOWN (MonoBehaviour serialized fields unavailable).",
        "- Image null pointers: not automatically errors or runtime assignments.",
        "- Device pixel resolution and CanvasScaler sizing: NOT CONFIRMED.",
        "- REF03 scene variants A and B: NOT DISTINGUISHED at runtime.",
        "- Source images and animation are NOT part of this report.",
        "",
        "## Metadata exports", "",
        "- scene-image-texture-links.csv: per-image Sprite and Texture2D identity",
        "- scene-spine-components.csv: Spine classes and hierarchy paths",
        "- scene-unresolved-images.csv: unlinked UI Images by scene",
        "- scene-visual-summary.json: counts and explicit unknowns", "",
    ]
    (folder / "SCENE_VISUAL_AUDIT.md").write_text(
        "\n".join(lines), encoding="utf-8")
    print(json.dumps({"status": "PASS", **totals}, ensure_ascii=False))

if __name__ == "__main__":
    main()
