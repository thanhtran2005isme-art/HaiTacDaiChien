"""Tests for safe, exact Spine source/atlas/texture assembly."""
import importlib.util
import pathlib
import unittest
from unittest.mock import Mock

path = pathlib.Path(__file__).resolve().parents[1] / "tools/export_local_spine.py"
spec = importlib.util.spec_from_file_location("spine_export", path)
import sys
sys.path.insert(0, str(path.parent))
m = importlib.util.module_from_spec(spec)
spec.loader.exec_module(m)


class TestPackMatching(unittest.TestCase):
    def test_atlas_pages_and_sanitize(self):
        text = b"hero.png\nsize: 64,64\nfilter: Linear,Linear\n\nhead\nrotate: false\n"
        self.assertEqual(m.atlas_pages(text), ["hero.png"])
        self.assertEqual(m.atlas_pages(b"../bad.png\nfoo/bar.png\nok.png\n"), ["ok.png"])

    def test_exact_match_and_no_ambiguous_fallback(self):
        v = Mock(name="Texture2D")
        self.assertIs(m.texture_candidates("hero.png", {"hero": [v]}), v)
        self.assertIsNone(m.texture_candidates("hero.png", {"hero": [v, Mock()]}))
        self.assertIsNone(m.texture_candidates("hero.png", {"hero.png": [v, Mock()]}))

    def test_selected_name_and_deterministic_id(self):
        s = {"Ace": [1], "Hero_175": [1], "Hero_99": [1]}
        self.assertEqual(m.choose_packages(s, s, limit=2), ["Hero_175", "Hero_99"])
        self.assertEqual(m.make_package_name("Ace"), m.make_package_name("Ace"))
        self.assertNotEqual(m.make_package_name("Ace"), m.make_package_name("ace"))


if __name__ == "__main__":
    unittest.main()
