"""P6 unit tests: synthetic screenshot fixtures are NOT real Android runtime proof."""
from __future__ import annotations

import io
import json
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch

from PIL import Image

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))

import ref04_p6_runtime_evidence as evidence
import ref04_p6_runtime_capture as capture_module


def png_bytes(color=(3, 9, 15), size=(360, 640)):
    file = io.BytesIO()
    Image.new("RGB", size, color).save(file, format="PNG")
    return file.getvalue()


class P6RuntimeEvidence(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.TemporaryDirectory()
        self.addCleanup(self.tmp.cleanup)
        self.root = Path(self.tmp.name)
        (self.root / "output").mkdir()
        # Deliberately synthetic P5 and XAPK-like fixtures. Real P5 is
        # separately validated by the original-XAPK source workflow.
        self.xapk = self.root / "synthetic-private.xapk"
        self.xapk.write_bytes(b"synthetic fixture; not an actual XAPK")
        self.p5 = self.root / "output/ref04-p5-cross-phase-source-integrity.json"
        self.p5.write_text(json.dumps({
            "classification": evidence.P5_KIND,
            "sourceCoverage": {
                "P1OriginalLayoutGroupFields": 168,
                "P2OriginalCanvasAndRelatedComponents": 15,
                "P4OriginalTextComponents": 62,
                "totalSourceProvenanceEntries": 245,
            },
            "runtimeCoordinates": None, "runtimeLayoutProven": False,
            "unityImportAllowed": False, "originalUiAssetsChanged": False,
        }), encoding="utf-8")
        self.folder = evidence.capture_root(self.root)
        self.folder.mkdir()

    def manifest(self, capture_id, role="original", package="com.example.original",
                 color=(3, 9, 15), size=(360, 640), device="test-device"):
        raw = png_bytes(color=color, size=size)
        (self.folder / (capture_id + ".png")).write_bytes(raw)
        manifest = {
            "classification": evidence.CAPTURE_KIND,
            "captureId": capture_id, "role": role, "package": package,
            "operatorSceneLabel": "REF04_HOME_CREW",
            "captureMethod": "ADB_EXEC_OUT_SCREENCAP_PNG",
            "captureUtc": "2026-10-11T00:00:00+00:00",
            "deviceSerialSha256": evidence.sha_bytes(device.encode()),
            "deviceFingerprintSha256": evidence.sha_bytes(b"test-build"),
            "foregroundVerifiedAtCapture": True,
            "foregroundEvidenceLine": (
                "mResumedActivity: ActivityRecord{test u0 " +
                package + "/.Main t123}"),
            "screenshotFileName": capture_id + ".png",
            "screenshotPixels": list(size),
            "pngSha256": evidence.sha_bytes(raw),
            "originalXapkSha256": evidence.file_sha(self.xapk),
            "p5ReportSha256": evidence.file_sha(self.p5),
            "deviceByteAttestationProven": False,
            "installedPackageBytesMatchOriginalXapk": False,
        }
        (self.folder / (capture_id + ".json")).write_text(
            json.dumps(manifest), encoding="utf-8")
        return manifest

    def test_synthetic_reference_audit_keeps_all_runtime_claims_blocked(self):
        self.manifest("original-home")
        report = evidence.audit(self.root, "original-home")
        self.assertEqual(report["measuredScreenshotPixels"], [360, 640])
        self.assertTrue(report["referenceScreenshotPngVerified"])
        self.assertFalse(report["originalXapkInstalledBytesProven"])
        self.assertFalse(report["sameRef04SceneStateIndependentlyProven"])
        self.assertIsNone(report["runtimeCoordinates"])
        self.assertFalse(report["runtimeCanvasScalerFormulaProven"])
        self.assertFalse(report["runtimeSafeAreaAdapterFormulaProven"])
        self.assertFalse(report["dynamicTextUpdatesProven"])
        self.assertFalse(report["pixelPerfectUiProven"])
        self.assertIsNone(report["visualComparison"])
        self.assertTrue((self.folder / "ref04-p6-runtime-evidence.json").exists())

    def test_two_screen_comparison_measures_only_pixels_and_same_device(self):
        self.manifest("original-home")
        self.manifest("study-home", role="study",
                      package="com.example.unityviewer", color=(7, 12, 21))
        report = evidence.audit(self.root, "original-home", "study-home")
        metrics = report["visualComparison"]
        self.assertGreater(metrics["rgbAbsoluteMeanError"], 0)
        self.assertFalse(metrics["identicalSampledPixels"])
        self.assertFalse(metrics["sameStateProven"])
        self.assertFalse(report["pixelPerfectUiProven"])
        self.assertTrue((self.folder / "visual-qa/ref04-raw-difference.png").exists())

    def test_original_and_viewer_identical_pixels_do_not_prove_same_runtime_state(self):
        self.manifest("original-home")
        self.manifest("study-home", role="study",
                      package="com.example.unityviewer")
        report = evidence.audit(self.root, "original-home", "study-home")
        self.assertTrue(report["visualComparison"]["identicalSampledPixels"])
        self.assertFalse(report["sameRef04SceneStateIndependentlyProven"])
        self.assertFalse(report["pixelPerfectUiProven"])

    def test_png_sha_tampering_is_rejected(self):
        self.manifest("original-home")
        (self.folder / "original-home.png").write_bytes(png_bytes((1, 2, 3)))
        with self.assertRaisesRegex(ValueError, "SHA changed"):
            evidence.audit(self.root, "original-home")

    def test_p5_or_original_xapk_change_rejects_old_capture(self):
        self.manifest("original-home")
        self.p5.write_text(self.p5.read_text() + "\n", encoding="utf-8")
        with self.assertRaisesRegex(ValueError, "metadata/source identity"):
            evidence.audit(self.root, "original-home")
        self.p5.write_text(self.p5.read_text().rstrip(), encoding="utf-8")
        self.xapk.write_bytes(b"changed source")
        with self.assertRaisesRegex(ValueError, "metadata/source identity"):
            evidence.audit(self.root, "original-home")

    def test_invalid_capture_metadata_and_path_traversal_rejected(self):
        doc = self.manifest("original-home")
        with self.assertRaisesRegex(ValueError, "unsafe or invalid"):
            evidence.audit(self.root, "../original-home")
        doc["screenshotFileName"] = "../private.png"
        (self.folder / "original-home.json").write_text(json.dumps(doc))
        with self.assertRaisesRegex(ValueError, "canonical"):
            evidence.audit(self.root, "original-home")

    def test_mismatched_viewport_and_device_rejected_no_rescaling(self):
        self.manifest("original-home")
        self.manifest("study-home", role="study",
                      package="com.example.viewer", size=(420, 640))
        with self.assertRaisesRegex(ValueError, "SCREENSHOT_NOT_COMPARABLE"):
            evidence.audit(self.root, "original-home", "study-home")
        self.manifest("study-home", role="study",
                      package="com.example.viewer", device="different")
        with self.assertRaisesRegex(ValueError, "same ADB device"):
            evidence.audit(self.root, "original-home", "study-home")

    def test_background_app_cannot_be_claimed_foreground(self):
        with self.assertRaisesRegex(ValueError, "not demonstrably foreground"):
            evidence.foreground_line(
                "com.example.original",
                "mResumedActivity: ActivityRecord{a u0 com.someone.else/.Main}",
                "mCurrentFocus=Window{a com.someone.else/.Main}")
        with self.assertRaisesRegex(ValueError, "not demonstrably foreground"):
            evidence.foreground_line(
                "com.example.original",
                "mResumedActivity: com.example.original.fake/.Main", "")

    def test_capture_uses_adb_live_png_and_does_not_overwrite(self):
        def fake_adb(executable, serial, *args, timeout=30):
            if args == ("get-serialno",):
                return b"test-device\n"
            if args[-2:] == ("getprop", "ro.build.fingerprint"):
                return b"test-build\n"
            if args[:2] == ("shell", "pidof"):
                return b"3199\n"
            if args[:3] == ("shell", "dumpsys", "activity"):
                return b"mResumedActivity: ActivityRecord{a u0 com.example.original/.Main t1}\n"
            if args[:3] == ("shell", "dumpsys", "window"):
                return b"mCurrentFocus=Window{com.example.original/.Main}\n"
            if args == ("exec-out", "screencap", "-p"):
                return png_bytes()
            raise AssertionError("Unexpected ADB arguments: " + repr(args))
        with patch.object(capture_module.shutil, "which", return_value="adb"), \
             patch.object(capture_module, "adb_run", side_effect=fake_adb):
            result = capture_module.capture(
                self.root, "original-home", "com.example.original", "original")
            self.assertEqual(result["pixelsMeasured"], [360, 640])
            self.assertFalse(result["runtimeUiFormulaProven"])
            evidence.audit(self.root, "original-home")
            with self.assertRaisesRegex(ValueError, "already exists"):
                capture_module.capture(
                    self.root, "original-home", "com.example.original", "original")


if __name__ == "__main__":
    unittest.main()
