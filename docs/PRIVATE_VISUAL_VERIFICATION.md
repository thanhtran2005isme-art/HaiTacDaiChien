# Private visual verification (Windows / Android)

This workflow is designed to check **actual game screenshots** against the existing Unity static asset inventory. It does not decompile source, use account credentials, bypass anti-cheat, create a server, or publish game images.

**Current status:** 4 reference images have been reviewed visually; no authorized device/runtime screenshots or CanvasScaler values have been captured by this repository automation.

## 1. Take clean in-game screenshots

Run in CMD with Android platform-tools installed (change `C:\platform-tools` to your actual installation):

```bat
cd /d C:\platform-tools
adb devices
adb shell wm size
adb shell wm density
adb shell screencap -p /sdcard/ui-ref01.png
adb pull /sdcard/ui-ref01.png C:\ui-private\REF01.png
```

Repeat after navigating to REF02 (hero), REF03 (islands), REF04 (home). Use a permitted game client/account. Avoid screenshots from a video player when testing native resolution. Redact personal player info in images shared with others.

The four previously supplied frames may include video controls, cropping or scaling. Treat them as **visual reference only**, not trusted device-native screenshots.

## 2. Measure capture dimensions without uploading graphics

On Windows from the repository root:

```bat
py -m pip install Pillow
py tools\private_runtime_measure.py --capture REF01=C:\ui-private\REF01.png --capture REF02=C:\ui-private\REF02.png --capture REF03=C:\ui-private\REF03.png --capture REF04=C:\ui-private\REF04.png --device 2400x1080 --out C:\ui-private\capture-check.json
```

Replace `2400x1080` with the **real** `adb shell wm size` value. Do not invent or reuse the example size. Keep `C:\ui-private` outside the public Git repository. The script writes only image pixel dimensions, ratios and warnings; never writes the images or their file paths into its JSON.

## 3. Cross-reference with extracted Unity metadata

- Static Image / Sprite / Texture: `reports/xapk/scene-image-texture-links.csv`
- Spine component hierarchy: `reports/xapk/scene-spine-components.csv`
- Null and other unknown sprite references: `reports/xapk/scene-unresolved-images.csv`
- Original candidate scene mapping: `docs/SCREEN_REFERENCE_MATCHING.md`
- Screenshot-reference wireframes: `reports/xapk/wireframes/REF*.svg`

For each real screen, record: actual capture dimension, scene candidate, background match, button/image anchors, dynamic character appearance, missing art, animation timing and any uncertainty.

**Do not infer** Spine skin names or current animation state from `SkeletonGraphic` class names. The original data may also be downloaded after the game starts, and frame-perfect reconstruction cannot be validated with a still screenshot.

## 4. Completion criteria

- [ ] REF01: Confirm 3 card rows, animated ship body and actual action-button labels
- [ ] REF02: Confirm 2-panel layout, character skin and observed action animations
- [ ] REF03: Select Canvas file083 or file101 using real island positions/labels
- [ ] REF04: Select Canvas file025 or file071 and verify the full hero scene
- [ ] Native screenshot dimensions checked against `adb shell wm size`
- [ ] CanvasScaler settings verified through a permitted runtime/debug project (still **unknown** in current APK metadata)
- [ ] No private player data, game artwork or asset binaries published to GitHub

Static asset relationships are **candidates**, not the original editable Unity source or proof of permission to redistribute copyrighted resources.
