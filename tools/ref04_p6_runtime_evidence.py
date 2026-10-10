#!/usr/bin/env python3
"""P6 REF04 ADB screenshot evidence: strict, local-only, NO runtime UI claims.

A matching PNG SHA proves file identity, not the original APK's running code,
the displayed scene, Canvas scale, SafeArea formula, Text setters or pixel-perfect
Unity reconstruction. All screenshots/manifests/reports stay under ignored output/.
"""
from __future__ import annotations

import hashlib
import json
from pathlib import Path
import re

from PIL import Image

from compare_ref04_game_screenshots import compare as compare_pixels

KIND = "REF04_P6_ADB_CAPTURE_AUDIT_SCREENSHOTS_NOT_RUNTIME_UI_FORMULAS"
CAPTURE_KIND = "REF04_P6_ADB_FOREGROUND_SCREENSHOT_V1"
P5_KIND = "REF04_P5_CROSS_PHASE_SOURCE_PROVENANCE_NO_RUNTIME_COORDINATES"
NAME = re.compile(r"[A-Za-z0-9][A-Za-z0-9_-]{1,63}\Z")
PACKAGE = re.compile(r"[A-Za-z_][A-Za-z0-9_]*(?:\.[A-Za-z_][A-Za-z0-9_]*)+\Z")
SHA = re.compile(r"[0-9a-f]{64}\Z")
MAX_PNG_BYTES = 60 * 1024 * 1024
MAX_JSON_BYTES = 64 * 1024


def blocked(message):
    raise ValueError("P6 BLOCKED: " + message)


def require(condition, message):
    if not condition:
        blocked(message)


def sha_bytes(data):
    return hashlib.sha256(data).hexdigest()


def file_sha(path):
    digest = hashlib.sha256()
    with path.open("rb") as handle:
        for chunk in iter(lambda: handle.read(1024 * 1024), b""):
            digest.update(chunk)
    return digest.hexdigest()


def capture_root(root):
    return Path(root).resolve() / "output" / "ref04-p6-runtime"


def choose_xapk(root, xapk=None):
    root = Path(root).resolve()
    if xapk is None:
        matches = sorted(root.glob("*.xapk"))
        require(len(matches) == 1,
                "exactly one original root XAPK required; use --xapk if ambiguous")
        candidate = matches[0]
    else:
        candidate = Path(xapk).resolve()
    require(candidate.is_file() and not candidate.is_symlink() and
            candidate.suffix.lower() == ".xapk" and candidate.parent == root,
            "original XAPK must be a regular root file (not a symlink)")
    return candidate


def load_p5(root):
    path = Path(root).resolve() / "output" / "ref04-p5-cross-phase-source-integrity.json"
    require(path.is_file() and not path.is_symlink() and
            path.stat().st_size <= MAX_JSON_BYTES * 160,
            "P5 private source audit report missing or oversized")
    raw = path.read_bytes()
    doc = json.loads(raw)
    cov = doc.get("sourceCoverage", {})
    require(doc.get("classification") == P5_KIND and
            cov.get("P1OriginalLayoutGroupFields") == 168 and
            cov.get("P2OriginalCanvasAndRelatedComponents") == 15 and
            cov.get("P4OriginalTextComponents") == 62 and
            cov.get("totalSourceProvenanceEntries") == 245 and
            doc.get("runtimeCoordinates") is None and
            doc.get("runtimeLayoutProven") is False and
            doc.get("unityImportAllowed") is False and
            doc.get("originalUiAssetsChanged") is False,
            "P5 source report coverage or no-runtime gate invalid")
    return sha_bytes(raw)


def foreground_line(package, activities, window):
    """Accept an ADB observed focused/resumed Activity, never pidof alone."""
    require(isinstance(package, str) and PACKAGE.fullmatch(package),
            "package id must be explicit and valid")
    pattern = re.compile(r"(?<![A-Za-z0-9_.])" + re.escape(package) + r"/")
    markers = ("mResumedActivity", "topResumedActivity", "mCurrentFocus",
               "mFocusedApp", "topRunningActivity")
    for output in (activities, window):
        for line in output.splitlines():
            if (any(marker in line for marker in markers) and
                    pattern.search(line) and len(line) <= 1000):
                return line.strip()
    blocked("requested package not demonstrably foreground/resumed in ADB")


def png_size(path):
    require(path.is_file() and not path.is_symlink() and
            100 <= path.stat().st_size <= MAX_PNG_BYTES,
            "capture PNG missing/oversized/symlink")
    with Image.open(path) as check:
        require(check.format == "PNG", "image must be PNG")
        width, height = check.size
        require(300 <= width <= 8000 and 300 <= height <= 8000 and
                width * height <= 25_000_000,
                "invalid screenshot pixel bounds")
        check.verify()
    return [width, height]


def private_capture_path(root, capture_id):
    require(isinstance(capture_id, str) and NAME.fullmatch(capture_id),
            "unsafe or invalid capture identifier")
    return capture_root(root) / (capture_id + ".json")


def load_capture(root, capture_id, p5_sha, xapk_sha):
    folder = capture_root(root)
    manifest = private_capture_path(root, capture_id)
    require(manifest.is_file() and not manifest.is_symlink() and
            manifest.stat().st_size <= MAX_JSON_BYTES,
            "missing/unsafe P6 private capture manifest")
    doc = json.loads(manifest.read_text(encoding="utf-8"))
    require(doc.get("classification") == CAPTURE_KIND and
            doc.get("captureId") == capture_id and
            doc.get("role") in ("original", "study") and
            isinstance(doc.get("package"), str) and
            PACKAGE.fullmatch(doc["package"]) and
            doc.get("foregroundVerifiedAtCapture") is True and
            doc.get("captureMethod") == "ADB_EXEC_OUT_SCREENCAP_PNG" and
            doc.get("operatorSceneLabel") == "REF04_HOME_CREW" and
            doc.get("p5ReportSha256") == p5_sha and
            doc.get("originalXapkSha256") == xapk_sha and
            doc.get("deviceByteAttestationProven") is False and
            doc.get("installedPackageBytesMatchOriginalXapk") is False,
            "P6 capture metadata/source identity untrusted")
    for key in ("deviceSerialSha256", "deviceFingerprintSha256", "pngSha256"):
        require(isinstance(doc.get(key), str) and SHA.fullmatch(doc[key]),
                key + " missing/invalid")
    line = doc.get("foregroundEvidenceLine")
    require(isinstance(line, str) and
            foreground_line(doc["package"], line, line) == line.strip(),
            "foreground evidence line unavailable or not for package")
    png_name = capture_id + ".png"
    require(doc.get("screenshotFileName") == png_name,
            "screenshot path/filename must be canonical")
    png = folder / png_name
    size = png_size(png)
    require(doc.get("screenshotPixels") == size and file_sha(png) == doc["pngSha256"],
            "screenshot dimensions or original PNG SHA changed")
    return doc, png, size


def audit(root, original_id, study_id=None, xapk=None):
    """Validate actual local capture files; never infer runtime Unity values."""
    root = Path(root).resolve()
    p5_sha = load_p5(root)
    xapk_sha = file_sha(choose_xapk(root, xapk))
    original, reference_png, viewport = load_capture(root, original_id, p5_sha, xapk_sha)
    require(original["role"] == "original", "reference must be foreground original package")
    result = {
        "classification": KIND,
        "originalCaptureId": original_id,
        "studyCaptureId": study_id,
        "sourceP5Sha256": p5_sha,
        "originalXapkSha256": xapk_sha,
        "referenceScreenshotPngVerified": True,
        "measuredScreenshotPixels": viewport,
        "originalForegroundObservedByAdb": True,
        "originalXapkInstalledBytesProven": False,
        "sameRef04SceneStateIndependentlyProven": False,
        "deviceSafeAreaInsetsIndependentlyMeasured": False,
        "runtimeCanvasScalerFormulaProven": False,
        "runtimeSafeAreaAdapterFormulaProven": False,
        "panelHome2RuntimeMutationsProven": False,
        "dynamicTextUpdatesProven": False,
        "runtimeTextPositions": None,
        "runtimeCoordinates": None,
        "pixelPerfectUiProven": False,
        "unityImportAllowed": False,
        "originalUiAssetsChanged": False,
        "visualComparison": None,
        "blockers": [
            "CAPTURE_RECORD_IS_NOT_DEVICE_CRYPTOGRAPHIC_ATTESTATION",
            "INSTALLED_ORIGINAL_APK_NOT_YET_MATCHED_TO_LOCAL_XAPK_SPLITS",
            "ORIGINAL_REF04_SCENE_STATE_NOT_INDEPENDENTLY_ATTESTED",
            "NO_RUNTIME_CANVAS_SCALER_OR_SCREEN_SAFEAREA_TELEMETRY",
            "NO_NATIVE_PANELHOME2_OR_TEXT_SETTER_WRITE_TRACE",
        ],
    }
    if study_id is not None:
        require(study_id != original_id, "original and study capture IDs must be distinct")
        study, study_png, other_pixels = load_capture(root, study_id, p5_sha, xapk_sha)
        require(study["role"] == "study" and study["package"] != original["package"],
                "study must use distinct foreground application")
        require(study["deviceSerialSha256"] == original["deviceSerialSha256"] and
                study["deviceFingerprintSha256"] == original["deviceFingerprintSha256"],
                "screenshots must come from the same ADB device/build")
        require(other_pixels == viewport, "SCREENSHOT_NOT_COMPARABLE: no resize allowed")
        comparison = compare_pixels(
            reference_png, study_png, capture_root(root) / "visual-qa")
        result["visualComparison"] = {
            "identicalSampledPixels": comparison["identicalPixels"],
            "rgbAbsoluteMeanError": comparison["rgbAbsoluteMeanError"],
            "percentPixelsWithLumaDifferenceAtLeast5":
                comparison["percentPixelsWithLumaDifferenceAtLeast5"],
            "sameDeviceRecorded": True,
            "sameResolutionMeasured": True,
            "sameStateProven": False,
        }
    destination = capture_root(root) / "ref04-p6-runtime-evidence.json"
    require(not destination.is_symlink(), "refuse symlink output path")
    destination.parent.mkdir(parents=True, exist_ok=True)
    tmp = destination.with_suffix(".tmp")
    tmp.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n",
                   encoding="utf-8")
    tmp.replace(destination)
    return result
