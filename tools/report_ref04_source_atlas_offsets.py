#!/usr/bin/env python3
"""Diagnose 22 REF04 Sprite crop mismatches from exact XAPK rendering metadata.

Does NOT infer sprite padding from PNG dimensions or modify pixels. Sprite
renderData.textureRect/textureRectOffset and packed settings are read ONLY
from the matching original Sprite PathID in the independent source audit.
"""
from __future__ import annotations

import argparse
import collections
import json
import math
from pathlib import Path

try:
    # Direct invocation: python tools/report_ref04_source_atlas_offsets.py
    from report_ref04_png_rect_mismatch import analyze
except ModuleNotFoundError:
    # importlib-based unit tests with repository root in sys.path
    from tools.report_ref04_png_rect_mismatch import analyze

ROOT = Path(__file__).resolve().parents[1]


def vec(source, name):
    v = source.get(name)
    if isinstance(v, list) and len(v) == 2 and all(
        type(x) in (int, float) and math.isfinite(x) for x in v
    ):
        return tuple(float(x) for x in v)
    return None


def analyze_source_sprite_offsets(geometry, png_root):
    report = analyze(geometry, png_root)
    source_by_file = {}
    for item in geometry["images"]:
        name = item["spriteFile"]
        fields = {
            k: item.get(k)
            for k in (
                "sourceTextureRectOffset", "sourceTextureRectSize",
                "sourceAtlasRectOffset", "sourceSpriteOffset",
                "sourceSpritePivot", "sourceTextureRectOrigin",
                "sourceRectOrigin", "sourceSpriteSettingsRaw",
            )
        }
        if name in source_by_file and source_by_file[name] != fields:
            raise ValueError("Conflicting native Sprite metadata: " + name)
        source_by_file[name] = fields

    counts = collections.Counter()
    for sprite in report["sprites"]:
        source = source_by_file[sprite["spriteFile"]]
        sprite.update({k: v for k, v in source.items() if v is not None})
        if sprite["safeForNativeBorderImport"]:
            sprite["sourceTrimAssessment"] = "RECT_COMPATIBLE_NO_CROP"
            counts["RECT_COMPATIBLE_NO_CROP"] += 1
            continue
        native = sprite["sourceRectSize"]
        exported = sprite["exportedPngSize"]
        texture_rect = vec(source, "sourceTextureRectSize")
        offset = vec(source, "sourceTextureRectOffset")
        settings = source.get("sourceSpriteSettingsRaw")
        texture_matches = (
            texture_rect is not None
            and all(abs(a - b) <= 1 for a, b in zip(texture_rect, exported))
        )
        source_rect_contains = (
            offset is not None
            and all(-0.001 <= off and off + tex <= n + 0.001
                    for off, tex, n in zip(offset, exported, native))
        )
        if texture_matches and source_rect_contains and settings == 0:
            # "trim compatible" is still a diagnostic label; whether an
            # exported PNG's content corresponds to original pixel positions
            # must be established by comparing rendered XAPK and source image.
            state = "UNPACKED_TEXTURE_RECT_OFFSET_CONSISTENT_NEEDS_PIXEL_PROOF"
        elif texture_matches and source_rect_contains:
            state = "TEXTURE_RECT_OFFSET_CONSISTENT_PACKING_UNKNOWN"
        elif texture_matches:
            state = "TEXTURE_RECT_MATCHES_PNG_BUT_OFFSET_UNVERIFIED"
        else:
            state = "NATIVE_ATLAS_TRIM_GEOMETRY_UNRESOLVED"
        sprite["sourceTrimAssessment"] = state
        counts[state] += 1
    report["classification"] = "REF04_XAPK_SOURCE_SPRITE_TRIM_EVIDENCE"
    report["countsByTrimEvidence"] = dict(sorted(counts.items()))
    report["autoRepairAllowed"] = False
    report["warning"] = (
        "Even when m_RD.textureRectSize and textureRectOffset are consistent, "
        "this report does NOT prove where the missing pixels are or whether "
        "an Atlas Sprite was rotated/masked. No PNG rewriting occurs."
    )
    return report


def main():
    p = argparse.ArgumentParser(description=__doc__)
    p.add_argument("--root", type=Path, default=ROOT)
    args = p.parse_args()
    root = args.root.resolve()
    src = json.loads((root / "output/ref04-static-image-geometry.json")
                     .read_text(encoding="utf-8"))
    report = analyze_source_sprite_offsets(src, root / "output/local-ui-art")
    target = root / "output/ref04-source-sprite-trim-evidence.json"
    tmp = target.with_suffix(".tmp")
    tmp.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(target)
    print(json.dumps({
        "classification": report["classification"],
        "uniqueSpriteFiles": report["uniqueSpriteFiles"],
        "countsByTrimEvidence": report["countsByTrimEvidence"],
        "autoRepairAllowed": False,
    }, sort_keys=True))
    for row in report["sprites"]:
        if not row["safeForNativeBorderImport"]:
            print(json.dumps({
                "file": row["spriteFile"],
                "native": row["sourceRectSize"],
                "png": row["exportedPngSize"],
                "textureRect": row.get("sourceTextureRectSize"),
                "textureRectOffset": row.get("sourceTextureRectOffset"),
                "settingsRaw": row.get("sourceSpriteSettingsRaw"),
                "assessment": row["sourceTrimAssessment"],
            }, sort_keys=True))


if __name__ == "__main__":
    main()
