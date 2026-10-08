#!/usr/bin/env python3
"""Private measurement of authorized screenshots; does not upload any images.

This measures screenshot dimensions and optionally compares them with Android
wm size. It CANNOT recover Unity CanvasScaler or prove correct prefab layout.
Usage: python tools/private_runtime_measure.py --capture REF01=ship.png
       --capture REF04=home.png --device 2400x1080 --out C:/ui-private/check.json
"""
from __future__ import annotations
import argparse
import json
from pathlib import Path


def parse_resolution(value):
    a, sep, b = value.lower().partition("x")
    if not sep:
        raise ValueError("resolution should look like 2400x1080")
    w, h = int(a), int(b)
    if min(w, h) < 1 or max(w, h) > 20000:
        raise ValueError("invalid display resolution")
    return w, h


def classify_capture(width, height, device=None):
    aspect = round(width / height, 5)
    if device:
        # A video frame, resized image, or crop is NOT a full-size ADB screenshot.
        exact = (width, height) == device
    else:
        exact = None
    return {
        "pixels": [width, height], "aspect_ratio": aspect,
        "same_pixels_as_device": exact,
        "observation": (
            "Pixel match is necessary but not sufficient to verify full-screen capture"
            if exact else "Device size not provided; this is an image dimension only"
            if exact is None else "Capture differs from device pixels (possible crop/resize/video frame)"
        ),
        "unity_canvas_scaler_verified": False,
    }


def measure(captures, device=None):
    from PIL import Image, ImageOps
    result = {}
    for reference, path in captures:
        if reference in result:
            raise ValueError("duplicate reference: " + reference)
        if not reference.startswith("REF") or len(reference) > 40:
            raise ValueError("reference must be a REF identifier")
        with Image.open(path) as opened:
            image = ImageOps.exif_transpose(opened)
            width, height = image.size
        result[reference] = classify_capture(width, height, device)
    return result


def main():
    p = argparse.ArgumentParser()
    p.add_argument("--capture", action="append", required=True,
                   help="REF01=local-image.png; repeat for each reference")
    p.add_argument("--device", default="", help="Full native adb wm size W x H")
    p.add_argument("--out", type=Path, required=True,
                   help="PRIVATE local JSON path outside the public git repository")
    args = p.parse_args()
    captured = []
    for item in args.capture:
        key, sep, value = item.partition("=")
        if not sep or not value:
            p.error("capture must be REF=filename")
        captured.append((key, Path(value)))
    device = parse_resolution(args.device) if args.device else None
    result = {"captures": measure(captured, device),
              "device_pixels_from_user": list(device) if device else None,
              "limitations": [
                  "A screenshot or a video frame cannot reveal Unity CanvasScaler values.",
                  "A matching pixel size is not proof the screenshot is a live uncropped UI.",
                  "Character skins, Spine animation and runtime dynamic sprites remain unverified.",
                  "No images or paths are stored in the output; no network upload occurs.",
              ]}
    args.out.parent.mkdir(parents=True, exist_ok=True)
    args.out.write_text(json.dumps(result, indent=2, ensure_ascii=False) + "\n",
                        encoding="utf-8")
    print(json.dumps({"status": "PASS", "references": list(result["captures"]),
                      "report_path": str(args.out)}, ensure_ascii=False))


if __name__ == "__main__":
    main()
