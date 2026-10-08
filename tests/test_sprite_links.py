"""Unit tests: no accidental Sprite matches and no fabricated linkage."""
import collections
import csv
import importlib.util
import io
import struct
import sys
import unittest
from pathlib import Path
from types import SimpleNamespace as N

TOOLS = Path(__file__).resolve().parents[1] / "tools"
sys.path.insert(0, str(TOOLS))
import sprite_links as mod

def ref(name):
    return N(type=N(name=name), assets_file=N(), path_id=55)

class TestSpriteLinks(unittest.TestCase):
    def test_valid_pptr_little_endian(self):
        data = b"\0" * 12 + struct.pack("<iq", 2, 55) + b"\0" * 8
        target = ref("Sprite")
        hits = mod.raw_pointer_candidates(
            data, "<", 2,
            lambda f, p: (target, "external_resolved") if (f, p) == (2, 55) else (None, "missing"))
        self.assertEqual([(h["offset"], h["path_id"]) for h in hits], [(12, 55)])

    def test_not_false_positive_non_sprite(self):
        data = struct.pack("<iq", 1, 55)
        hits = mod.raw_pointer_candidates(data, "<", 1, lambda f,p: (ref("Texture2D"), "external"))
        self.assertEqual(hits, [])

    def test_invalid_external_never_matched(self):
        data = struct.pack("<iq", 4, 55)
        self.assertEqual(mod.raw_pointer_candidates(data, "<", 2, lambda f,p: (ref("Sprite"), "ok")), [])

    def test_alignment_prevents_shifted_matches(self):
        data = b"\x7f\x00" + struct.pack("<iq", 1, 55)
        self.assertEqual(mod.raw_pointer_candidates(data, "<", 1, lambda f,p: (ref("Sprite"), "ok")), [])

    def test_big_endian_is_explicit(self):
        data = b"\x00" * 4 + struct.pack(">iq", 1, 55)
        hits = mod.raw_pointer_candidates(data, ">", 1, lambda f,p: (ref("Sprite"), "ok"))
        self.assertEqual(hits[0]["offset"], 4)
        with self.assertRaises(ValueError):
            mod.raw_pointer_candidates(data, "unknown", 1, lambda *_: (None, "missing"))

    def test_calibration_and_ambiguity(self):
        obj = {"offset": 80, "reader": ref("Sprite")}
        other = {"offset": 124, "reader": ref("Sprite")}
        counts = mod.calibration([[obj], [obj], [other], [obj, other], []])
        self.assertEqual(counts[80], 2)
        self.assertEqual(counts[124], 1)
        self.assertEqual(mod.rank_match([obj], counts)[1], "probable_m_sprite")
        self.assertEqual(mod.rank_match([other], counts)[1], "unconfirmed_sprite_pointer")
        self.assertEqual(mod.rank_match([obj, other], counts),
                         (None, "ambiguous_multiple_sprite_pointers"))
        self.assertEqual(mod.rank_match([], counts), (None, "unresolved"))

    def test_no_cross_file_calibration_leak(self):
        source_a = mod.calibration([[{"offset": 100}]])
        source_b = mod.calibration([[{"offset": 100}]])
        self.assertEqual(source_a[100], 1)
        self.assertEqual(source_b[100], 1)

    def test_only_image_not_rawimage(self):
        self.assertEqual(mod.IMAGE_TYPES, {"Image"})

    def test_endian_unknown_is_fail_closed(self):
        self.assertIsNone(mod.endian_of(N(assets_file=N(reader=N(endian=None)),
                                          reader=N(endian="weird"))))
        self.assertEqual(mod.endian_of(N(assets_file=N(reader=N(endian=">")),
                                       reader=N(endian=None))), ">")

if __name__ == "__main__":
    unittest.main()
