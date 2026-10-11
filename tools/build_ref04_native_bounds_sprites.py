#!/usr/bin/env python3
"""Source-offset-backed REF04 *preview* reconstruction for Tight Sprite crops.

Do not modify original XAPK/UnityPy PNGs, Unity Assets or existing Prefabs.
Only when original m_RD.textureRect/textureRectOffset, m_Rect, and the native
SpriteSettingsRaw=64 (unpacked, Tight mesh) agree, place the existing decoded
pixels into their source-proven *logical* native Sprite rectangle. Unrendered
Tight-mesh outer pixels are transparent in the PREVIEW, not invented artwork.
Fractional offsets must be within 0.125 pixel of integer grid; remaining
sprites stay explicitly blocked. Native pixel/mesh reconstruction is NOT proven
identical to the running XAPK until an actual runtime screenshot is compared.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import math
from pathlib import Path

from PIL import Image

ROOT = Path(__file__).resolve().parents[1]
SCENE = "REF04-home-crew"
TIGHT_UNPACKED_SETTINGS = 64


def _sha(blob):
    return hashlib.sha256(blob).hexdigest()


def positive_integer(v):
    return (type(v) in (int, float) and math.isfinite(v) and
            abs(v - round(v)) <= .01 and 0 < v <= 16384)


def _integer_offset(v):
    return (type(v) in (int, float) and math.isfinite(v) and
            abs(v - round(v)) <= .125 and v >= -.125)


def sources(geometry):
    if (geometry.get("schemaVersion") != 1 or
        geometry.get("classification") !=
            "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY" or
        geometry.get("sceneId") != SCENE or
        geometry.get("sourceBindings") != 265 or
        len(geometry.get("images", [])) != 265):
        raise ValueError("REF04 XAPK evidence identity missing")
    records = {}
    for image in geometry["images"]:
        if not image.get("applyGeometry"):
            raise ValueError("REF04 unverified Sprite image is not admissible")
        name = image.get("spriteFile", "")
        if (len(name) != 36 or not name.endswith(".png") or
            any(c not in "0123456789abcdef" for c in name[:32])):
            raise ValueError("Untrusted source PNG name")
        entry = {key: image.get(key) for key in (
            "sourceRectSize", "sourceTextureRectSize",
            "sourceTextureRectOffset", "sourceSpriteSettingsRaw",
            "border", "pixelsPerUnit"
        )}
        if name in records:
            if records[name]["source"] != entry:
                raise ValueError("Conflicting native geometry for same Sprite " + name)
            records[name]["imageComponentPathIds"].append(
                image["componentPathId"])
        else:
            records[name] = {
                "source": entry,
                "imageComponentPathIds": [image["componentPathId"]]}
    return records


def decide(entry, decoded_size):
    original = entry["source"]
    logical = original.get("sourceRectSize")
    texture_rect = original.get("sourceTextureRectSize")
    offset = original.get("sourceTextureRectOffset")
    if (not isinstance(logical, list) or len(logical) != 2 or
        not all(positive_integer(v) for v in logical)):
        return None, "ORIGINAL_LOGICAL_RECT_UNVERIFIED"
    native = tuple(round(x) for x in logical)
    if (all(abs(native[i] - decoded_size[i]) <= 1
            for i in range(2))):
        return None, "ALREADY_RECT_COMPATIBLE_NO_CHANGES"
    if original.get("sourceSpriteSettingsRaw") != TIGHT_UNPACKED_SETTINGS:
        return None, "UNPACKED_TIGHT_SOURCE_SETTINGS_NOT_PROVEN"
    if (not isinstance(texture_rect, list) or len(texture_rect) != 2 or
        not all(type(v) in (float, int) and math.isfinite(v)
                for v in texture_rect) or
        any(abs(texture_rect[i] - decoded_size[i]) > .25
            for i in range(2))):
        return None, "SOURCE_TEXTURE_RECT_NOT_EQUAL_DECODED_PNG"
    if (not isinstance(offset, list) or len(offset) != 2 or
        not all(_integer_offset(v) for v in offset)):
        return None, "ORIGINAL_TIGHT_MESH_OFFSET_NOT_PIXEL_ALIGNED"
    x, y_bottom = (round(x) for x in offset)
    if (x < 0 or y_bottom < 0 or
        x + decoded_size[0] > native[0] or
        y_bottom + decoded_size[1] > native[1]):
        return None, "ORIGINAL_CROP_OFFSET_OUTSIDE_NATIVE_RECT"
    return {
        "nativeSize": list(native),
        "sourceTextureRectSize": texture_rect,
        "sourceTextureRectOffset": offset,
        "placementInPngTopLeft": [x, native[1]-decoded_size[1]-y_bottom],
        "settingsRaw": TIGHT_UNPACKED_SETTINGS,
    }, "UNPACKED_TIGHT_SOURCE_LOGICAL_BOUNDS_PREVIEW"


def build(geometry, art_folder: Path, output: Path, *, write=True):
    indexed = sources(geometry)
    output.mkdir(parents=True, exist_ok=True) if write else None
    summary, files = collections.Counter(), []
    for name, entry in sorted(indexed.items()):
        raw = (art_folder / name).read_bytes()
        with Image.open(art_folder / name) as image:
            decoded = image.convert("RGBA")
        decoded.load()
        desc, status = decide(entry, decoded.size)
        summary[status] += 1
        record = {
            "spriteFile": name,
            "sourceImageComponentPathIds": entry["imageComponentPathIds"],
            "sourceExportPngSha256": _sha(raw),
            "exportedSize": list(decoded.size),
            "status": status,
            "previewOnly": True,
        }
        if desc is not None:
            # Add only transparent, UNRENDERED Tight-mesh canvas area with
            # source-backed lower-left offset. No pixels are interpolated.
            canvas = Image.new("RGBA", tuple(desc["nativeSize"]), (0,0,0,0))
            canvas.paste(decoded, tuple(desc["placementInPngTopLeft"]))
            record.update(desc)
            record["originalBorder"] = entry["source"].get("border")
            record["originalPixelsPerUnit"] = entry["source"].get(
                "pixelsPerUnit")
            if write:
                target = output / name
                canvas.save(target, "PNG")
                record["previewPngSha256"] = _sha(target.read_bytes())
        files.append(record)
    report = {
        "schemaVersion": 1,
        "classification": "REF04_SOURCE_TIGHT_MESH_LOGICAL_BOUNDS_PREVIEW",
        "originalNativeGeometryManifestSha256": _sha(
            (json.dumps(geometry, ensure_ascii=False, sort_keys=True)
             ).encode("utf-8")),
        "originalImageBindings": 265,
        "spriteFiles": len(files),
        "counts": dict(sorted(summary.items())),
        "sourceVerifiedPixelPlacement": False,
        "previewOnly": True,
        "newSceneRequiredForVisualTest": True,
        "sprites": files,
        "limitations": "A logical bounds PREVIEW for unpacked Tight sprites. "
                       "No guessing opaque pixels, alpha content or runtime "
                       "animation; transparent outer area is unrendered in "
                       "the original Tight mesh. Real-XAPK Game screenshot "
                       "comparison is still mandatory before claiming fidelity.",
    }
    if write:
        dest = output / "manifest.json"
        tmp = dest.with_suffix(".tmp")
        tmp.write_text(json.dumps(report,ensure_ascii=False,indent=2)+"\n",
                       encoding="utf-8")
        tmp.replace(dest)
    return report


def main():
    p = argparse.ArgumentParser(description=__doc__)
    p.add_argument("--root", type=Path, default=ROOT)
    args = p.parse_args()
    root = args.root.resolve()
    doc = json.loads((root / "output/ref04-static-image-geometry.json")
                     .read_text(encoding="utf-8"))
    target = root / "output/ref04-source-logical-sprite-previews"
    result = build(doc, root / "output/local-ui-art", target)
    result["nativeGeometryManifestFileSha256"] = _sha(
        (root / "output/ref04-static-image-geometry.json").read_bytes())
    (target / "manifest.json").write_text(
        json.dumps(result,ensure_ascii=False,indent=2)+"\n",
        encoding="utf-8")
    print(json.dumps({
        "status": result["classification"],
        "counts": result["counts"],
        "originalImageBindings": result["originalImageBindings"],
        "originalPngFilesModified": 0,
        "existingUnityAssetsModified": 0,
        "runtimeVisualFidelityProven": False,
    }, sort_keys=True))


if __name__ == "__main__":
    main()
