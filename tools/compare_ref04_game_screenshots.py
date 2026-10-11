#!/usr/bin/env python3
"""Exact-size REF04 visual comparison: runtime XAPK screenshot vs Unity Game.

No rescaling, cropping, invented layout, or source-art modification. Comparison
is invalid when screenshots differ in pixel resolution or content state.
Outputs local-only diff and overlay PNGs under ignored output/ref04-visual-qa.
"""
from __future__ import annotations

import argparse
import json
from pathlib import Path

from PIL import Image, ImageChops, ImageEnhance, ImageStat


def compare(source: Path, unity: Path, destination: Path):
    if source.resolve() == unity.resolve():
        raise ValueError("XAPK and Unity reference screenshots must be distinct")
    with Image.open(source) as reference_raw, Image.open(unity) as scene_raw:
        reference = reference_raw.convert("RGB")
        scene = scene_raw.convert("RGB")
    if reference.size != scene.size:
        raise ValueError(
            "SCREENSHOT_NOT_COMPARABLE: XAPK and Unity must have identical "
            f"pixel dimensions (XAPK={reference.size}, Unity={scene.size}). "
            "Capture same scene and resolution; do not auto-scale images.")
    if min(reference.size) < 300:
        raise ValueError("Screenshots too small for exact-pixel UI audit")
    difference = ImageChops.difference(reference, scene)
    stats = ImageStat.Stat(difference)
    mae = sum(stats.mean) / 3.0
    extrema = difference.getbbox()
    intensity = difference.convert("L")
    histogram = intensity.histogram()
    total = reference.width * reference.height
    changed = total - sum(histogram[:5])
    destination.mkdir(parents=True, exist_ok=True)
    difference = ImageEnhance.Contrast(difference).enhance(2.5)
    difference.save(destination / "ref04-raw-difference.png")
    Image.blend(reference, scene, 0.5).save(
        destination / "ref04-50-50-overlay.png")
    report = {
        "classification": "REF04_XAPK_RUNTIME_VS_UNITY_GAME_PIXEL_COMPARISON",
        "source": str(source.resolve()),
        "unity": str(unity.resolve()),
        "resolution": list(reference.size),
        "rgbAbsoluteMeanError": round(mae, 6),
        "percentPixelsWithLumaDifferenceAtLeast5": round(
            changed * 100.0 / total, 4),
        "differentPixelBoundingBox": list(extrema) if extrema else None,
        "sourceRuntimeStateProvenEquivalent": False,
        "identicalPixels": extrema is None,
        "limitations": "Captures must show same REF04 runtime state and equal "
                       "screen resolution. Different Spine/gameplay/HUD states "
                       "will affect pixel error. Pixel comparison is not a "
                       "semantic UI/layout correctness proof.",
    }
    (destination / "ref04-comparison.json").write_text(
        json.dumps(report, ensure_ascii=False, indent=2) + "\n",
        encoding="utf-8")
    return report


def main():
    p = argparse.ArgumentParser(description=__doc__)
    p.add_argument("--xapk-reference", type=Path, required=True,
                   help="Screenshot from running original XAPK at REF04")
    p.add_argument("--unity-game", type=Path, required=True,
                   help="Unity Game tab screenshot at exactly same dimensions")
    p.add_argument("--output-dir", type=Path,
                   default=Path(__file__).resolve().parents[1] /
                   "output/ref04-visual-qa")
    args = p.parse_args()
    result = compare(args.xapk_reference, args.unity_game, args.output_dir)
    print(json.dumps(result, ensure_ascii=False))


if __name__ == "__main__":
    main()
