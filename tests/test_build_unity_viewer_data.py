"""Tests for offline Unity viewer dataset and structural integrity."""
import importlib.util
import json
import pathlib
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[1]
SOURCE = ROOT / "tools/build_unity_viewer_data.py"
spec = importlib.util.spec_from_file_location("build_unity_viewer_data", SOURCE)
builder = importlib.util.module_from_spec(spec)
spec.loader.exec_module(builder)

class TestOfflineViewerData(unittest.TestCase):
    def test_vec2_fails_closed(self):
        self.assertEqual(builder.vec2("0.100,-2.000"), [0.1, -2.0])
        self.assertEqual(builder.vec2("invalid"), [0., 0.])
        self.assertEqual(builder.vec2("10000000,0"), [0., 0.])

    def test_descendants_respects_hierarchy_not_string_prefix(self):
        objs = {
            1: {"parent_transform_id": "0", "sibling_index": "-1"},
            2: {"parent_transform_id": "1", "sibling_index": "0"},
            3: {"parent_transform_id": "1", "sibling_index": "1"},
            4: {"parent_transform_id": "3", "sibling_index": "0"},
            5: {"parent_transform_id": "99", "sibling_index": "0"},
        }
        self.assertEqual(builder.descendants(1, objs), [1, 2, 3, 4])

    def test_cycle_guard(self):
        obj = {
            10: {"parent_transform_id": "11", "sibling_index": "0"},
            11: {"parent_transform_id": "10", "sibling_index": "0"}
        }
        result = builder.descendants(10, obj)
        self.assertEqual(set(result), {10, 11})
        self.assertEqual(len(result), 2)

    def test_validation_rejects_missing_parent_and_duplicate_ids(self):
        one = {"id": "REF01", "rootTransform": 1, "nodeCount": 2, "nodes": [
            {"id": 1, "parent": 0, "a0": [0, 0], "delta": [0, 0]},
            {"id": 2, "parent": 999, "a0": [0, 0], "delta": [0, 0]}
        ]}
        dataset = {"schemaVersion": 1, "scenes": [one] * 5}
        with self.assertRaises(ValueError):
            builder.validate(dataset)

    @classmethod
    def setUpClass(cls):
        cls.db = builder.build(ROOT)
        builder.validate(cls.db)

    def test_real_metadata_five_scene_candidates(self):
        scenes = self.db["scenes"]
        self.assertEqual(len(scenes), 5)
        self.assertEqual({s["id"] for s in scenes},
                         set(builder.REFERENCE_TITLES))
        self.assertTrue(all(s["nodeCount"] >= 15 for s in scenes))

    def test_scene_nodes_have_valid_parent_chain(self):
        for scene in self.db["scenes"]:
            ids = {n["id"] for n in scene["nodes"]}
            self.assertIn(scene["rootTransform"], ids)
            self.assertEqual(len(ids), scene["nodeCount"])
            for node in scene["nodes"]:
                if node["id"] != scene["rootTransform"]:
                    self.assertIn(node["parent"], ids)

    def test_resource_references_are_metadata_only(self):
        data = self.db
        self.assertNotIn("base64", json.dumps(data))
        self.assertNotIn("http", data["source"].lower())
        seen_spine = seen_sprites = seen_missing = 0
        for scene in data["scenes"]:
            for node in scene["nodes"]:
                self.assertIsInstance(node["sprites"], list)
                self.assertIsInstance(node["spine"], list)
                self.assertIsInstance(node["a0"], list)
                self.assertTrue(all(isinstance(n, str) for n in node["sprites"]))
                seen_spine += bool(node["spine"])
                seen_sprites += bool(node["sprites"])
                seen_missing += node["missingImages"]
        self.assertGreater(seen_sprites, 0)
        self.assertGreater(seen_spine, 0)
        self.assertGreater(seen_missing, 0)

if __name__ == "__main__":
    unittest.main()
