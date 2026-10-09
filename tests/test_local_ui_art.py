"""Pure mapping tests: no private assets, UnityPy or XAPK needed."""
from __future__ import annotations

import importlib.util
import pathlib
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[1]
spec = importlib.util.spec_from_file_location("export_local_ui_art", ROOT / "tools/export_local_ui_art.py")
module = importlib.util.module_from_spec(spec)
spec.loader.exec_module(module)


class TestLocalArtManifest(unittest.TestCase):
    def test_verified_exact_sprite_id_and_ambiguity(self):
        scenes = {"scenes": [
            {"id": "REF01", "nodes": [
                {"path": "/Root/Bg"}, {"path": "/Root/Duplicate"},
                {"path": "/Root/Duplicate"}, {"path": "/Root/Unavailable"},
                {"path": "/Root/Ambiguous"}]},
        ]}
        def item(path, serial, sid):
            return {"reference": "REF01", "ui_path": path,
                    "sprite_file": serial, "sprite_id": str(sid)}
        links = [
            item("/Root/Bg", "asset__file01", 30),
            item("/Root/Bg", "asset__file01", 30),
            item("/Root/Duplicate", "asset__file01", 30),
            item("/Root/Unavailable", "asset__file01", 70),
            item("/Root/Ambiguous", "asset__file01", 30),
            item("/Root/Ambiguous", "asset__file01", 40),
        ]
        out = module.make_manifest(links, scenes, {("asset__file01", "30"): module.image_name(("asset__file01", "30"))})
        self.assertEqual(list(out["scenes"]["REF01"]), ["/Root/Bg"])
        self.assertEqual(out["stats"]["ui_nodes_mapped"], 1)
        self.assertEqual(out["stats"]["ambiguous_paths"], 1)
        self.assertEqual(len(out["files"]), 1)
        self.assertTrue(module.FILE_RE.fullmatch(out["files"][0]))

    def test_names_cannot_collide_on_filename(self):
        self.assertNotEqual(module.image_name(("first", "1")), module.image_name(("second", "1")))
        self.assertNotEqual(module.image_name(("first", "1")), module.image_name(("first", "2")))


if __name__ == "__main__":
    unittest.main()
