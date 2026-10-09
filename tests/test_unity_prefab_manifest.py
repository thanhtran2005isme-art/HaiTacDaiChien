"""No Unity runtime needed: test exact source mapping and Spine uncertainty."""
from __future__ import annotations
import importlib.util
import pathlib
import unittest

path = pathlib.Path(__file__).resolve().parents[1] / "tools/build_unity_prefab_manifest.py"
spec = importlib.util.spec_from_file_location("unity_plan", path)
module = importlib.util.module_from_spec(spec)
spec.loader.exec_module(module)


class TestUnityReconstructionPlan(unittest.TestCase):
    def fixture(self):
        nodes = [
            {"id": 10, "parent": 0, "path": "/Canvas"},
            {"id": 11, "parent": 10, "path": "/Canvas/Hero"},
            {"id": 12, "parent": 10, "path": "/Canvas/Spine"},
            {"id": 13, "parent": 10, "path": "/Canvas/Dup"},
            {"id": 14, "parent": 10, "path": "/Canvas/Dup"},
        ]
        scenes = {"schemaVersion": 1, "scenes": [{
            "id": "REF01-ship-upgrade", "rootTransform": 10, "nodes": nodes
        }]}
        name = "a" * 32 + ".png"
        art = {"version": 1, "files": [name], "scenes": {
            "REF01-ship-upgrade": {
                "/Canvas/Hero": name, "/Canvas/Dup": name,
                "/Canvas/unknown": name,
            },
        }}
        comps = [{"reference": "REF01-ship-upgrade", "ui_path": "/Canvas/Spine",
                  "component_id": "901", "class": "Spine.Unity.SkeletonGraphic"}]
        candidates = [{
            "reference": "REF01-ship-upgrade", "ui_path": "/Canvas/Spine",
            "source_component_id": "901", "target_class": "Spine.Unity.SkeletonDataAsset",
            "relation": "direct_typed_pointer_candidate",
        }]
        return scenes, art, comps, candidates

    def test_exact_mapping_and_no_fake_spine_binding(self):
        result = module.prepare(*self.fixture())
        self.assertEqual(len(result["sprites"]), 1)
        self.assertEqual(result["sprites"][0]["nodeId"], 11)
        self.assertEqual(result["spine"][0]["nodeId"], 12)
        self.assertEqual(result["spine"][0]["bindingStatus"], "pointer_candidates_unverified")
        self.assertNotIn("skin", result["spine"][0])
        self.assertEqual(result["scenes"][0]["mappedSprites"], 1)

    def test_reject_bad_parent_order(self):
        scenes, art, comps, candidates = self.fixture()
        scenes["scenes"][0]["nodes"][1]["parent"] = 14
        with self.assertRaises(ValueError):
            module.prepare(scenes, art, comps, candidates)

    def test_reject_private_image_paths(self):
        scenes, art, comps, candidates = self.fixture()
        art["files"] = ["../../private.png"]
        with self.assertRaises(ValueError):
            module.prepare(scenes, art, comps, candidates)


if __name__ == "__main__":
    unittest.main()
