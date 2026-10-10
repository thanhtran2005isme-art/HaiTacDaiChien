"""Exact pixel audit never guesses missing XAPK screenshot or rescales images."""
import importlib.util
from pathlib import Path
import tempfile
import unittest

from PIL import Image

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location(
    "ref04compare", ROOT / "tools/compare_ref04_game_screenshots.py")
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)


class ScreenshotComparison(unittest.TestCase):
    def test_identical_screenshot_reports_zero_pixel_error(self):
        with tempfile.TemporaryDirectory() as temp:
            root = Path(temp)
            a, b, out = root / "source.png", root / "game.png", root / "qa"
            Image.new("RGB", (640, 360), (25, 50, 75)).save(a)
            Image.new("RGB", (640, 360), (25, 50, 75)).save(b)
            report = mod.compare(a, b, out)
            self.assertTrue(report["identicalPixels"])
            self.assertEqual(report["rgbAbsoluteMeanError"], 0)
            self.assertTrue((out / "ref04-comparison.json").exists())
            self.assertTrue((out / "ref04-50-50-overlay.png").exists())
            self.assertFalse(report["sourceRuntimeStateProvenEquivalent"])

    def test_different_viewport_size_is_blocked_not_scaled(self):
        with tempfile.TemporaryDirectory() as temp:
            root = Path(temp)
            a, b = root / "source.png", root / "game.png"
            Image.new("RGB", (640, 360)).save(a)
            Image.new("RGB", (1280, 720)).save(b)
            with self.assertRaisesRegex(ValueError, "SCREENSHOT_NOT_COMPARABLE"):
                mod.compare(a, b, root / "out")

    def test_different_images_get_nonzero_diagnostics(self):
        with tempfile.TemporaryDirectory() as temp:
            root = Path(temp)
            a, b = root / "source.png", root / "game.png"
            Image.new("RGB", (640, 360), (0, 0, 0)).save(a)
            Image.new("RGB", (640, 360), (50, 50, 50)).save(b)
            report = mod.compare(a, b, root / "out")
            self.assertFalse(report["identicalPixels"])
            self.assertGreater(report["rgbAbsoluteMeanError"], 0)
            self.assertEqual(
                report["percentPixelsWithLumaDifferenceAtLeast5"], 100.0)

    def test_same_file_cannot_be_its_own_original_xapk_reference(self):
        with tempfile.TemporaryDirectory() as temp:
            root = Path(temp)
            a = root / "source.png"
            Image.new("RGB", (640, 360)).save(a)
            with self.assertRaisesRegex(ValueError, "must be distinct"):
                mod.compare(a, a, root / "out")


if __name__ == "__main__":
    unittest.main()
