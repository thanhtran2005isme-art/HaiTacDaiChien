#!/usr/bin/env python3
"""REF04 native Sprite m_Rect versus decoded PNG IHDR; strictly diagnostic.

A Sprite decoded with UnityPy may have a different pixel extent than native
m_Rect (e.g. source 84x92 / exported 84x86). It is NOT safe to apply native
9-slice borders/PPU to those dimensions or pad the image without proven
SpriteAtlas/mesh/offset data. Never rewrites images or import settings.
"""
from __future__ import annotations

import argparse
import collections
import hashlib
import json
import math
import struct
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SIGNATURE = bytes.fromhex("89504e470d0a1a0a")


def png_size(path: Path) -> tuple[int, int]:
    with path.open("rb") as handle:
        first = handle.read(24)
    if (len(first) != 24 or first[:8] != SIGNATURE or
            first[12:16] != b"IHDR" or first[8:12] != b"\x00\x00\x00\x0d"):
        raise ValueError("Not a standard Sprite PNG with IHDR: " + path.name)
    width, height = struct.unpack(">II", first[16:24])
    if not (0 < width <= 16384 and 0 < height <= 16384):
        raise ValueError("Invalid exported Sprite PNG pixel bounds: " + path.name)
    return width, height


def analyze(source: dict, png_root: Path):
    if (source.get("schemaVersion") != 1 or
            source.get("classification") !=
                "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY" or
            source.get("sceneId") != "REF04-home-crew" or
            source.get("sourceBindings") != 265 or
            len(source.get("images", [])) != 265):
        raise ValueError("Not the strict REF04 original native image inventory")
    by_file = {}
    for image in source["images"]:
        if not image.get("applyGeometry"):
            raise ValueError("Native source geometry not proven for Image " +
                             str(image.get("componentPathId")))
        size = image.get("sourceRectSize")
        file = image.get("spriteFile", "")
        if (len(file) != 36 or not file.endswith(".png") or
                not all(x in "0123456789abcdef" for x in file[:32]) or
                not isinstance(size, list) or len(size) != 2 or
                any(not isinstance(x, (float, int)) or not math.isfinite(x)
                    or x <= 0 for x in size)):
            raise ValueError("Native Sprite file identity/size invalid")
        old = by_file.get(file)
        if old and old["sourceRectSize"] != size:
            raise ValueError("Conflicting native rect for Sprite " + file)
        if old:
            old["imageComponentPathIds"].append(image["componentPathId"])
        else:
            by_file[file] = {
                "spriteFile": file,
                "sourceRectSize": size,
                "imageComponentPathIds": [image["componentPathId"]],
            }
    rows, counts = [], collections.Counter()
    for file, item in sorted(by_file.items()):
        width, height = png_size(png_root / file)
        width_diff = width - item["sourceRectSize"][0]
        height_diff = height - item["sourceRectSize"][1]
        compatible = abs(width_diff) <= 1.0 and abs(height_diff) <= 1.0
        status = ("RECT_COMPATIBLE" if compatible else
                  "NATIVE_RECT_VS_DECODED_PNG_MISMATCH_NO_AUTO_REPAIR")
        row = {
            **item,
            "exportedPngSize": [width, height],
            "sizeDifferencePngMinusNative": [width_diff, height_diff],
            "status": status,
            "safeForNativeBorderImport": compatible,
        }
        rows.append(row)
        counts[status] += 1
        counts["imageBindingsAffectedByMismatch" if not compatible else
               "imageBindingsWithCompatibleGeometry"] += len(
                   item["imageComponentPathIds"])
    return {
        "schemaVersion": 1,
        "classification": "REF04_XAPK_SPRITE_PNG_DIMENSION_RECONCILIATION",
        "sourceBindings": len(source["images"]),
        "uniqueSpriteFiles": len(rows),
        "counts": dict(sorted(counts.items())),
        "sprites": rows,
        "noAssetChanges": True,
        "limitation": "Pixel rectangle differs from native Sprite m_Rect; "
                      "could involve atlas trimming/packing or other rendering "
                      "details. No original offset/alignment was verified, "
                      "so no image padding, 9-slice adjustment or Canvas change "
                      "is performed for incompatible images.",
    }


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    root = args.root.resolve()
    raw = (root / "output/ref04-static-image-geometry.json").read_bytes()
    source = json.loads(raw)
    report = analyze(source, root / "output/local-ui-art")
    report["sourceNativeGeometryManifestSha256"] = hashlib.sha256(raw).hexdigest()
    target = root / "output/ref04-png-rect-reconciliation.json"
    tmp = target.with_suffix(".tmp")
    tmp.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(target)
    print(json.dumps({
        "status": report["classification"],
        "sourceBindings": report["sourceBindings"],
        "uniqueSpriteFiles": report["uniqueSpriteFiles"],
        "counts": report["counts"],
        "blockedNativeBorderImports": report["counts"].get(
            "NATIVE_RECT_VS_DECODED_PNG_MISMATCH_NO_AUTO_REPAIR", 0),
        "unityAssetsChanged": False,
    }, sort_keys=True))


if __name__ == "__main__":
    main()
