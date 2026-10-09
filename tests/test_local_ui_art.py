"""Pure mapping tests: no private assets, UnityPy or XAPK needed."""
from __future__ import annotations

import importlib.util
import pathlib
import tempfile
import types
import unittest
import zipfile

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

    def test_duplicate_ui_path_is_resolved_by_exact_original_component_id(self):
        # Both GameObjects have the same displayed path, but their serialized
        # Image component IDs and GameObject ownership are different.
        scenes = {"scenes": [{"id": "REF04", "nodes": [
            {"id": 10, "path": "/Canvas/Duplicate"},
            {"id": 11, "path": "/Canvas/Duplicate"},
        ]}]}
        images = [
            {"reference": "REF04", "ui_path": "/Canvas/Duplicate",
             "component_id": "550", "sprite_file": "assets__file123",
             "sprite_id": "3"},
            {"reference": "REF04", "ui_path": "/Canvas/Duplicate",
             "component_id": "551", "sprite_file": "assets__file123",
             "sprite_id": "4"},
        ]
        decoded = {(x["sprite_file"], x["sprite_id"]):
                   module.image_name((x["sprite_file"], x["sprite_id"]))
                   for x in images}
        manifest = module.make_manifest(images, scenes, decoded, {
            ("REF04", "550"): 10, ("REF04", "551"): 11
        })
        self.assertEqual(manifest["version"], 1)
        self.assertEqual(manifest["stats"]["ui_nodes_mapped"], 2)
        self.assertEqual(len(manifest["nodeBindings"]), 2)
        self.assertEqual(manifest["scenes"]["REF04"], {})
        self.assertEqual({row["nodeId"] for row in manifest["nodeBindings"]},
                         {10, 11})
        self.assertEqual(len(manifest["files"]), 2)

    def test_conflicting_sprites_one_original_image_component_are_not_guessed(self):
        scenes = {"scenes": [{"id": "REF04", "nodes": [
            {"id": 10, "path": "/Canvas/A"}
        ]}]}
        images = [
            {"reference": "REF04", "ui_path": "/Canvas/A",
             "component_id": "550", "sprite_file": "assets__file123",
             "sprite_id": str(number)} for number in (3, 4)
        ]
        decoded = {(x["sprite_file"], x["sprite_id"]):
                   module.image_name((x["sprite_file"], x["sprite_id"]))
                   for x in images}
        manifest = module.make_manifest(images, scenes, decoded, {
            ("REF04", "550"): 10
        })
        self.assertEqual(manifest["nodeBindings"], [])
        self.assertEqual(manifest["stats"]["ambiguous_component_bindings"], 1)

    def test_bundle_read_without_temporary_windows_file(self):
        """Regression: load byte buffer, never leave UnityPy mmap on bundle_2.unity3d."""
        with tempfile.TemporaryDirectory() as tmp:
            work = pathlib.Path(tmp)
            apk = work / "demo.apk"
            with zipfile.ZipFile(apk, "w") as archive:
                archive.writestr("assets/bin/Data/datapack.unity3d", b"fake-bundle-payload")
            class SampleImage:
                width, height = 12, 12
                def save(self, output, format):
                    self_outer.assertEqual(format, "PNG")
                    output.write(b"PNG-sample-bytes")
            self_outer = self
            class Reader:
                type = types.SimpleNamespace(name="Sprite")
                assets_file = types.SimpleNamespace(name="resources.assets")
                path_id = 123
                def read(self):
                    return types.SimpleNamespace(m_Name="logo", image=SampleImage())
            calls = []
            def load(blob):
                self.assertIsInstance(blob, bytes)
                calls.append(blob)
                return types.SimpleNamespace(objects=[Reader()])
            expected = {("002_UnityDataAssetPack_datapack__file110", "123"): {"logo"}}
            output = work / "art"
            output.mkdir()
            exported, issues = {}, []
            module.scan_apk(apk, "002_UnityDataAssetPack", expected, output,
                            exported, issues, types.SimpleNamespace(load=load), work)
            self.assertEqual(calls, [b"fake-bundle-payload"])
            self.assertEqual(len(exported), 1)
            self.assertEqual(issues, [])
            self.assertFalse(list(work.glob("bundle_*.unity3d")))
            self.assertTrue((output / next(iter(exported.values()))).is_file())

    def test_names_cannot_collide_on_filename(self):
        self.assertNotEqual(module.image_name(("first", "1")), module.image_name(("second", "1")))
        self.assertNotEqual(module.image_name(("first", "1")), module.image_name(("first", "2")))


if __name__ == "__main__":
    unittest.main()
