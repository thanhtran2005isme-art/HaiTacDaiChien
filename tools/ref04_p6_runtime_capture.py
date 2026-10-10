#!/usr/bin/env python3
"""P6: capture an original/study Android app screenshot ONLY while foreground.

Run locally with authorized device and explicit package. Never upload game art
or ADB output to Git. Foreground/activity evidence is a local ADB observation,
not cryptographic APK attestation or proof of REF04 runtime formulas.
"""
from __future__ import annotations

import argparse
from datetime import datetime, timezone
import json
from pathlib import Path
import shutil
import subprocess

from ref04_p6_runtime_evidence import (
    CAPTURE_KIND, NAME, PACKAGE, audit, blocked, capture_root, choose_xapk,
    file_sha, foreground_line, load_p5, png_size, require, sha_bytes,
)

ROOT = Path(__file__).resolve().parents[1]


def adb_run(adb, serial, *arguments, timeout=30):
    command = [adb]
    if serial is not None:
        command.extend(["-s", serial])
    command.extend(arguments)
    try:
        process = subprocess.run(
            command, stdout=subprocess.PIPE, stderr=subprocess.PIPE,
            timeout=timeout, check=False)
    except (OSError, subprocess.TimeoutExpired) as exc:
        blocked("ADB command unavailable/timed out: " + type(exc).__name__)
    require(process.returncode == 0,
            "ADB command failed: " + " ".join(arguments[:3]) + " (verify authorized USB)")
    return process.stdout


def utf8(data):
    return data.decode("utf-8", errors="replace").strip()


def capture(root, capture_id, package, role, adb="adb", serial=None, xapk=None):
    root = Path(root).resolve()
    require(isinstance(capture_id, str) and NAME.fullmatch(capture_id),
            "invalid capture-id; letters, digits, hyphen, underscore only")
    require(isinstance(package, str) and PACKAGE.fullmatch(package),
            "explicit original/study Android package name is required")
    require(role in ("original", "study"), "role must be original or study")
    executable = shutil.which(adb)
    require(executable is not None, "adb executable not found (use --adb path)")
    original_xapk = choose_xapk(root, xapk)
    p5_sha = load_p5(root)
    device = utf8(adb_run(executable, serial, "get-serialno"))
    require(device and device not in ("unknown", "offline"),
            "ADB device not authorized or selected")
    fingerprint = utf8(adb_run(
        executable, serial, "shell", "getprop", "ro.build.fingerprint"))
    require(bool(fingerprint), "ADB Android build fingerprint unavailable")
    pid = utf8(adb_run(executable, serial, "shell", "pidof", package))
    require(pid and all(token.isdecimal() for token in pid.split()),
            "target app process not running on the selected device")
    activity = utf8(adb_run(
        executable, serial, "shell", "dumpsys", "activity", "activities"))
    window = utf8(adb_run(
        executable, serial, "shell", "dumpsys", "window", "windows"))
    foreground = foreground_line(package, activity, window)
    screenshot = adb_run(
        executable, serial, "exec-out", "screencap", "-p", timeout=35)
    require(100 <= len(screenshot) <= 60 * 1024 * 1024 and
            screenshot.startswith(b"\x89PNG\r\n\x1a\n"),
            "ADB screencap did not return canonical PNG bytes")
    folder = capture_root(root)
    require(not folder.is_symlink(), "private capture directory is a symlink")
    folder.mkdir(parents=True, exist_ok=True)
    png = folder / (capture_id + ".png")
    manifest = folder / (capture_id + ".json")
    require(not png.exists() and not manifest.exists() and
            not png.is_symlink() and not manifest.is_symlink(),
            "capture-id already exists; refuse overwriting evidence")
    temp_png = folder / (capture_id + ".png.tmp")
    require(not temp_png.exists() and not temp_png.is_symlink(),
            "stale/temp screenshot exists")
    try:
        temp_png.write_bytes(screenshot)
        pixels = png_size(temp_png)
        temp_png.replace(png)
        metadata = {
            "classification": CAPTURE_KIND,
            "captureId": capture_id,
            "role": role,
            "package": package,
            "operatorSceneLabel": "REF04_HOME_CREW",
            "captureMethod": "ADB_EXEC_OUT_SCREENCAP_PNG",
            "captureUtc": datetime.now(timezone.utc).isoformat(),
            "deviceSerialSha256": sha_bytes(device.encode("utf-8")),
            "deviceFingerprintSha256": sha_bytes(fingerprint.encode("utf-8")),
            "foregroundVerifiedAtCapture": True,
            "foregroundEvidenceLine": foreground,
            "screenshotFileName": png.name,
            "screenshotPixels": pixels,
            "pngSha256": sha_bytes(screenshot),
            "originalXapkSha256": file_sha(original_xapk),
            "p5ReportSha256": p5_sha,
            "deviceByteAttestationProven": False,
            "installedPackageBytesMatchOriginalXapk": False,
            "runtimeCanvasScaleFormula": None,
            "runtimeSafeAreaFormula": None,
            "dynamicTextWriteTrace": None,
        }
        temp_manifest = folder / (capture_id + ".json.tmp")
        require(not temp_manifest.exists() and not temp_manifest.is_symlink(),
                "stale/temp capture manifest exists")
        temp_manifest.write_text(
            json.dumps(metadata, ensure_ascii=False, indent=2) + "\n",
            encoding="utf-8")
        temp_manifest.replace(manifest)
    except Exception:
        temp_png.unlink(missing_ok=True)
        if not manifest.exists():
            png.unlink(missing_ok=True)
        raise
    return {
        "classification": CAPTURE_KIND,
        "captureId": capture_id,
        "role": role,
        "pixelsMeasured": pixels,
        "pngSha256": metadata["pngSha256"],
        "foregroundObserved": True,
        "installedApkMatchedToOriginalXapk": False,
        "runtimeUiFormulaProven": False,
        "privateManifest": str(manifest),
    }


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    sub = parser.add_subparsers(dest="command", required=True)
    grab = sub.add_parser("capture", help="Take a live original/study ADB screenshot")
    grab.add_argument("--capture-id", required=True)
    grab.add_argument("--package", required=True)
    grab.add_argument("--role", choices=("original", "study"), required=True)
    grab.add_argument("--adb", default="adb")
    grab.add_argument("--serial")
    grab.add_argument("--xapk", type=Path)
    grab.add_argument("--root", type=Path, default=ROOT)
    check = sub.add_parser("audit", help="Check screenshots and P5 source linkage")
    check.add_argument("--original", required=True, help="Capture ID, not path")
    check.add_argument("--study", help="Optional distinct study capture ID")
    check.add_argument("--xapk", type=Path)
    check.add_argument("--root", type=Path, default=ROOT)
    args = parser.parse_args()
    if args.command == "capture":
        result = capture(args.root, args.capture_id, args.package, args.role,
                         args.adb, args.serial, args.xapk)
    else:
        result = audit(args.root, args.original, args.study, args.xapk)
        # Print a compact source-vs-runtime gate without sensitive paths/fields.
        result = {
            "classification": result["classification"],
            "referenceScreenshotPngVerified":
                result["referenceScreenshotPngVerified"],
            "measuredScreenshotPixels": result["measuredScreenshotPixels"],
            "visualComparison": result["visualComparison"],
            "runtimeCanvasScalerFormulaProven":
                result["runtimeCanvasScalerFormulaProven"],
            "runtimeSafeAreaAdapterFormulaProven":
                result["runtimeSafeAreaAdapterFormulaProven"],
            "dynamicTextUpdatesProven": result["dynamicTextUpdatesProven"],
            "pixelPerfectUiProven": result["pixelPerfectUiProven"],
            "originalXapkInstalledBytesProven":
                result["originalXapkInstalledBytesProven"],
            "unityImportAllowed": result["unityImportAllowed"],
        }
    print(json.dumps(result, ensure_ascii=False, sort_keys=True))


if __name__ == "__main__":
    main()
