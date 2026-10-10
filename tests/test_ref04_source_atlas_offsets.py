"""No source Sprite packing inference without original m_RD evidence."""
import importlib.util
from pathlib import Path
import struct
import tempfile
import unittest

ROOT = Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location(
    "ref04_src_atlas", ROOT / "tools/report_ref04_source_atlas_offsets.py")
mod = importlib.util.module_from_spec(spec)
spec.loader.exec_module(mod)
FILE = "e02e91470b2bd555b85e5073babfb59f.png"


def evidence(offset=None, tex_size=None, settings=None):
    row = {
        "componentPathId": 100, "spriteFile": FILE, "applyGeometry": True,
        "sourceRectSize": [84, 92],
    }
    if offset is not None:
        row["sourceTextureRectOffset"] = offset
    if tex_size is not None:
        row["sourceTextureRectSize"] = tex_size
    if settings is not None:
        row["sourceSpriteSettingsRaw"] = settings
    return {
        "schemaVersion": 1,
        "sceneId": "REF04-home-crew",
        "classification": "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY",
        "sourceBindings": 265,
        "images": [dict(row, componentPathId=i) for i in range(265)],
    }


def write_png(path):
    path.write_bytes(
        bytes.fromhex("89504e470d0a1a0a")
        + b"\0\0\0\rIHDR" + struct.pack(">II", 84, 86)
        + b"\x08\x06\x00\x00\x00")


class AtlasOffsets(unittest.TestCase):
    def check(self, data):
        with tempfile.TemporaryDirectory() as temp:
            root = Path(temp)
            write_png(root / FILE)
            return mod.analyze_source_sprite_offsets(data, root)

    def test_no_offset_never_claims_proof(self):
        result = self.check(evidence())
        self.assertEqual(
            result["sprites"][0]["sourceTrimAssessment"],
            "NATIVE_ATLAS_TRIM_GEOMETRY_UNRESOLVED")
        self.assertFalse(result["autoRepairAllowed"])

    def test_matching_texture_rect_without_offset_not_enough(self):
        result = self.check(evidence(tex_size=[84, 86]))
        self.assertEqual(
            result["sprites"][0]["sourceTrimAssessment"],
            "TEXTURE_RECT_MATCHES_PNG_BUT_OFFSET_UNVERIFIED")
        self.assertFalse(result["autoRepairAllowed"])

    def test_known_offset_and_unknown_packing_still_not_repaired(self):
        result = self.check(evidence(
            offset=[0, 3], tex_size=[84, 86], settings=3))
        self.assertEqual(
            result["sprites"][0]["sourceTrimAssessment"],
            "TEXTURE_RECT_OFFSET_CONSISTENT_PACKING_UNKNOWN")
        self.assertFalse(result["autoRepairAllowed"])

    def test_known_unpacked_offset_is_diagnostic_not_pixel_proof(self):
        result = self.check(evidence(
            offset=[0, 3], tex_size=[84, 86], settings=0))
        self.assertEqual(
            result["sprites"][0]["sourceTrimAssessment"],
            "UNPACKED_TEXTURE_RECT_OFFSET_CONSISTENT_NEEDS_PIXEL_PROOF")
        self.assertFalse(result["autoRepairAllowed"])

    def test_out_of_native_bounds_offset_blocked(self):
        result = self.check(evidence(
            offset=[0, 8], tex_size=[84, 86], settings=0))
        self.assertEqual(
            result["sprites"][0]["sourceTrimAssessment"],
            "TEXTURE_RECT_MATCHES_PNG_BUT_OFFSET_UNVERIFIED")


if __name__ == "__main__":
    unittest.main()
