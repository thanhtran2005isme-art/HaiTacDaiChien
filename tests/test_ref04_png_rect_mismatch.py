"""REF04 PNG source size mismatches cannot be silently 'fixed' with padding."""
from pathlib import Path
import importlib.util
import struct
import tempfile
import unittest

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location(
    "ref04rect", ROOT / "tools/report_ref04_png_rect_mismatch.py")
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)

NAME = "e02e91470b2bd555b85e5073babfb59f.png"


def fake_png(root, name, w, h):
    (root / name).write_bytes(
        mod.SIGNATURE + b"\x00\x00\x00\x0dIHDR" +
        struct.pack(">II", w, h) + b"\x08\x06\x00\x00\x00")


def source(size):
    return {
        "schemaVersion": 1,
        "classification": "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY",
        "sceneId": "REF04-home-crew",
        "sourceBindings": 265,
        "images": [
            {"applyGeometry": True, "componentPathId": k + 1,
             "spriteFile": NAME, "sourceRectSize": list(size)}
            for k in range(265)
        ]
    }


class TestRef04PngRect(unittest.TestCase):
    def test_exact_user_error_must_be_blocked_not_padded(self):
        with tempfile.TemporaryDirectory() as d:
            root = Path(d)
            fake_png(root, NAME, 84, 86)
            report = mod.analyze(source((84, 92)), root)
        self.assertEqual(report["sourceBindings"], 265)
        self.assertEqual(report["uniqueSpriteFiles"], 1)
        self.assertEqual(report["counts"][
            "NATIVE_RECT_VS_DECODED_PNG_MISMATCH_NO_AUTO_REPAIR"], 1)
        row = report["sprites"][0]
        self.assertEqual(row["sizeDifferencePngMinusNative"], [0, -6])
        self.assertFalse(row["safeForNativeBorderImport"])
        self.assertTrue(report["noAssetChanges"])

    def test_matching_rect_safe_for_import_only(self):
        with tempfile.TemporaryDirectory() as d:
            root = Path(d)
            fake_png(root, NAME, 84, 92)
            report = mod.analyze(source((84, 92)), root)
        self.assertTrue(report["sprites"][0]["safeForNativeBorderImport"])
        self.assertEqual(report["counts"]["RECT_COMPATIBLE"], 1)

    def test_source_conflicting_dimensions_rejected(self):
        with tempfile.TemporaryDirectory() as d:
            root = Path(d)
            fake_png(root, NAME, 84, 92)
            src = source((84, 92))
            src["images"][1]["sourceRectSize"] = [84, 99]
            with self.assertRaisesRegex(ValueError, "Conflicting native rect"):
                mod.analyze(src, root)

    def test_absent_exported_png_not_silently_skipped(self):
        with tempfile.TemporaryDirectory() as d:
            with self.assertRaises(FileNotFoundError):
                mod.analyze(source((84, 92)), Path(d))

    def test_corrupt_png_rejected(self):
        with tempfile.TemporaryDirectory() as d:
            root = Path(d)
            (root / NAME).write_bytes(b"not a png")
            with self.assertRaisesRegex(ValueError, "Not a standard"):
                mod.analyze(source((84, 92)), root)


if __name__ == "__main__":
    unittest.main()
