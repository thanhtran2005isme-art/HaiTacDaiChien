#!/usr/bin/env python3
"""Audit shared Sprite textures (atlas candidates), animation and UI renderer data.

Reports only names, object IDs and dimensions; no copyrighted texture pixels.
Shared Texture2D != proof of a named SpriteAtlas asset.
"""
import argparse
import collections
import csv
import json
import shutil
import tempfile
import zipfile
from pathlib import Path

from il2cpp_refs import build_file_registry, get, pptr, resolve_pointer

def fields(obj, *keys):
    for key in keys:
        value = get(obj, key)
        if value is not None:
            return value
    return None

def sprite_texture_pointer(sprite):
    rd = fields(sprite, "m_RD", "m_RenderData")
    if rd is None:
        return None
    return fields(rd, "texture", "m_Texture")

def simple_rect(value):
    if value is None:
        return ""
    try:
        return f"{float(get(value, 'width', 0)):.1f}x{float(get(value, 'height', 0)):.1f}"
    except (ValueError, TypeError):
        return ""

def inspect(apks):
    import UnityPy
    with tempfile.TemporaryDirectory(prefix="unity_media_") as temp:
        environments = []
        for apk in apks:
            with zipfile.ZipFile(apk) as zf:
                for member in zf.infolist():
                    if member.is_dir() or not member.filename.lower().endswith(
                            (".unity3d", ".assetbundle", ".bundle")):
                        continue
                    path = Path(temp) / f"{len(environments)}.unity3d"
                    with zf.open(member) as src, path.open("wb") as dest:
                        shutil.copyfileobj(src, dest)
                    environments.append((f"{apk.stem}_{Path(member.filename).stem}",
                                         UnityPy.load(str(path))))
        readers, serials, labels, aliases = build_file_registry(environments)
        sprite_rows, animation_rows, atlas_groups = [], [], collections.defaultdict(set)
        summary = collections.Counter()
        texture_cache, go_cache = {}, {}
        for (fid, pid), reader in readers.items():
            kind = reader.type.name
            if kind not in ("Sprite", "AnimationClip", "Animator", "SpriteAtlas",
                            "AnimatorController"):
                continue
            summary["objects_" + kind] += 1
            label = labels.get(fid, "")
            if kind == "SpriteAtlas":
                # This name is an object type only; no data copied.
                continue
            try:
                asset = reader.read()
            except Exception:
                summary["read_errors_" + kind] += 1
                continue
            name = str(fields(asset, "m_Name") or "")
            if kind == "Sprite":
                texture_ptr = sprite_texture_pointer(asset)
                status = "texture_pointer_absent"
                texture_file = texture_id = texture_name = texture_wh = ""
                if texture_ptr is not None and pptr(texture_ptr)[1]:
                    target, status = resolve_pointer(texture_ptr, reader.assets_file,
                                                     readers, aliases)
                    if target is not None and target.type.name == "Texture2D":
                        cachekey = (id(target.assets_file), int(target.path_id))
                        if cachekey not in texture_cache:
                            try:
                                t = target.read()
                                texture_cache[cachekey] = (
                                    str(fields(t, "m_Name") or ""),
                                    f"{fields(t, 'm_Width') or 0}x{fields(t, 'm_Height') or 0}")
                            except Exception:
                                texture_cache[cachekey] = ("", "")
                                summary["texture_read_errors"] += 1
                        texture_name, texture_wh = texture_cache[cachekey]
                        texture_file = labels.get(id(target.assets_file), "")
                        texture_id = str(int(target.path_id))
                        atlas_groups[cachekey].add((label, pid))
                        summary["sprite_texture_resolved"] += 1
                    else:
                        status = status + "_not_Texture2D"
                sprite_rows.append({
                    "sprite_serialized_file": label, "sprite_id": pid, "sprite_name": name,
                    "texture_serialized_file": texture_file, "texture_id": texture_id,
                    "texture_name": texture_name, "texture_size": texture_wh,
                    "sprite_rect": simple_rect(fields(asset, "m_Rect")),
                    "texture_resolution": status})
            elif kind == "AnimationClip":
                animation_rows.append({"serialized_file": label, "path_id": pid,
                                       "type": kind, "name": name, "gameobject": ""})
            elif kind == "Animator":
                ptr = fields(asset, "m_GameObject")
                target, status = resolve_pointer(ptr, reader.assets_file, readers, aliases)
                go_name = ""
                if target is not None and target.type.name == "GameObject":
                    key = (id(target.assets_file), int(target.path_id))
                    if key not in go_cache:
                        try:
                            go_cache[key] = str(fields(target.read(), "m_Name") or "")
                        except Exception:
                            go_cache[key] = ""
                    go_name = go_cache[key]
                animation_rows.append({"serialized_file": label, "path_id": pid,
                                       "type": kind, "name": name, "gameobject": go_name})
            else:
                animation_rows.append({"serialized_file": label, "path_id": pid,
                                       "type": kind, "name": name, "gameobject": ""})
        summary["total_sprites"] = len(sprite_rows)
        summary["distinct_linked_textures"] = len(atlas_groups)
        summary["shared_texture_groups"] = sum(len(v) > 1 for v in atlas_groups.values())
        summary["sprites_on_shared_textures"] = sum(len(v) for v in atlas_groups.values()
                                                  if len(v) > 1)
        for row in sprite_rows:
            if row["texture_id"]:
                fid = next((k for k,v in labels.items()
                            if v == row["texture_serialized_file"]), None)
                if fid is not None:
                    size = len(atlas_groups.get((fid, int(row["texture_id"])), set()))
                    row["shared_texture_sprite_count"] = size
                else:
                    row["shared_texture_sprite_count"] = 0
            else:
                row["shared_texture_sprite_count"] = 0
        return sprite_rows, animation_rows, summary

def save_csv(path, data, cols):
    with path.open("w", encoding="utf-8", newline="") as f:
        writer = csv.DictWriter(f, fieldnames=cols)
        writer.writeheader()
        writer.writerows(data)

def main():
    p = argparse.ArgumentParser()
    p.add_argument("--apk-dir", type=Path, default=Path("output/apks"))
    p.add_argument("--report-dir", type=Path, default=Path("reports/xapk"))
    args = p.parse_args()
    apks = sorted(args.apk_dir.glob("*.apk"))
    if not apks:
        p.error("No unpacked APK files")
    args.report_dir.mkdir(parents=True, exist_ok=True)
    sprite_rows, anim_rows, counts = inspect(apks)
    save_csv(args.report_dir / "sprite-texture-metadata.csv", sprite_rows,
             ["sprite_serialized_file", "sprite_id", "sprite_name",
              "texture_serialized_file", "texture_id", "texture_name",
              "texture_size", "sprite_rect", "texture_resolution",
              "shared_texture_sprite_count"])
    save_csv(args.report_dir / "ui-animation-metadata.csv", anim_rows,
             ["serialized_file", "path_id", "type", "name", "gameobject"])
    summary = {"counts": dict(counts),
               "limitations": [
                   "Sprites sharing a Texture2D are atlas candidates; this does not prove a SpriteAtlas asset exists.",
                   "AnimationClip names and Animator ownership are indexed; no animation keyframes or visual playback reconstructed.",
                   "Canvas sorting order and sibling order are metadata; runtime render order is not verified.",
                   "No actual copyrighted images exported."]}
    (args.report_dir / "ui-media-summary.json").write_text(
        json.dumps(summary, indent=2, ensure_ascii=False) + "\n", encoding="utf-8")
    lines = ["# Sprite Texture and Animation audit", "",
             "Metadata only. No texture pixels or original animations rendered.", "",
             "| Item | Count |", "|---|---:|"]
    lines.extend(f"| {k} | {v:,} |" for k, v in sorted(counts.items()))
    lines.extend(["", "Shared Texture2D objects indicate **possible packed atlas textures**,",
                  "not definitive SpriteAtlas files.", "",
                  "See sprite-texture-metadata.csv and ui-animation-metadata.csv.", ""])
    (args.report_dir / "UI_MEDIA_AUDIT_REPORT.md").write_text("\n".join(lines), encoding="utf-8")
    print(json.dumps({"status": "PASS", "counts": dict(counts)}, ensure_ascii=False))

if __name__ == "__main__":
    main()
