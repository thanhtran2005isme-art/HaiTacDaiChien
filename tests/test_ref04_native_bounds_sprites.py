"""Native Tight mesh logical Sprite previews preserve existing decoded pixels."""
import importlib.util
from pathlib import Path
import tempfile
import unittest

from PIL import Image

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location(
    "ref04logical", ROOT / "tools/build_ref04_native_bounds_sprites.py")
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)
FILE = "e02e91470b2bd555b85e5073babfb59f.png"


def geometry(settings=64, offset=(0,0), rect=(84,92), texture=(84,86)):
    image = {
        "spriteFile": FILE,
        "componentPathId": 100,
        "sourceRectSize": list(rect),
        "sourceTextureRectSize": list(texture),
        "sourceTextureRectOffset": list(offset),
        "sourceSpriteSettingsRaw": settings,
        "border": [1,2,3,4],
        "pixelsPerUnit": 100,
        "applyGeometry": True,
    }
    return {
        "schemaVersion": 1, "sourceBindings": 265,
        "classification": "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY",
        "sceneId": "REF04-home-crew",
        "images": [dict(image,componentPathId=k) for k in range(265)],
    }


class LogicalSourceSprite(unittest.TestCase):
    def test_native_source_84x92_and_trimmed_84x86_are_aligned_without_pixel_resizing(self):
        with tempfile.TemporaryDirectory() as t:
            root = Path(t)
            (root / "original").mkdir()
            decoded = Image.new("RGBA", (84,86), (200,50,20,255))
            decoded.save(root / "original" / FILE)
            old_data = (root / "original" / FILE).read_bytes()
            manifest = mod.build(
                geometry(), root / "original", root / "preview")
            output = root / "preview" / FILE
            self.assertTrue(output.exists())
            with Image.open(output) as new:
                self.assertEqual(new.size,(84,92))
                self.assertEqual(new.getpixel((0,0)),(0,0,0,0))
                self.assertEqual(new.getpixel((0,6)),(200,50,20,255))
                self.assertEqual(new.getpixel((83,91)),(200,50,20,255))
            self.assertEqual((root/"original"/FILE).read_bytes(),old_data)
            self.assertEqual(manifest["counts"][
                "UNPACKED_TIGHT_SOURCE_LOGICAL_BOUNDS_PREVIEW"], 1)
            self.assertFalse(manifest["sourceVerifiedPixelPlacement"])

    def test_nonzero_bottom_offset_maps_to_png_top_coordinate_correctly(self):
        desc, status = mod.decide({
            "source": {
                "sourceRectSize": [100,100],
                "sourceTextureRectSize": [80.0, 80.0],
                "sourceTextureRectOffset": [7.0, 5.0],
                "sourceSpriteSettingsRaw": 64,
            }
        }, (80,80))
        self.assertEqual(desc["placementInPngTopLeft"],[7,15])
        self.assertEqual(status,
            "UNPACKED_TIGHT_SOURCE_LOGICAL_BOUNDS_PREVIEW")

    def test_packed_or_rotated_or_non_tight_sprites_are_blocked(self):
        for settings in (0,1,65,127,128,None):
            with self.subTest(settings=settings):
                desc, status = mod.decide({
                    "source": {
                        "sourceRectSize": [84,92],
                        "sourceTextureRectSize": [84,86],
                        "sourceTextureRectOffset": [0,0],
                        "sourceSpriteSettingsRaw": settings,
                    }
                }, (84,86))
                self.assertIsNone(desc)
                self.assertEqual(status, "UNPACKED_TIGHT_SOURCE_SETTINGS_NOT_PROVEN")

    def test_missing_offset_and_conflicting_original_geometry_fail_closed(self):
        fields = {"sourceRectSize": [84,92],
                  "sourceTextureRectSize": [84,86],
                  "sourceSpriteSettingsRaw": 64}
        desc, status = mod.decide({"source":fields}, (84,86))
        self.assertIsNone(desc)
        self.assertEqual(status,
            "ORIGINAL_TIGHT_MESH_OFFSET_NOT_PIXEL_ALIGNED")
        doc = geometry()
        doc["images"][1]["sourceSpriteSettingsRaw"] = 0
        with self.assertRaisesRegex(ValueError, "Conflicting native geometry"):
            mod.sources(doc)

    def test_fractional_offset_not_proven_is_not_rounded(self):
        fields = {"sourceRectSize":[84,92],
                  "sourceTextureRectSize":[84,86],
                  "sourceSpriteSettingsRaw":64,
                  "sourceTextureRectOffset":[0,0.49]}
        desc,status = mod.decide({"source":fields}, (84,86))
        self.assertIsNone(desc)
        self.assertEqual(status,
            "ORIGINAL_TIGHT_MESH_OFFSET_NOT_PIXEL_ALIGNED")


if __name__ == "__main__":
    unittest.main()
