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

    def test_source_canvas_root_scale_is_not_a_child_transform(self):
        # Four serialized Canvas roots have (0,0), while the hero-detail root
        # has (1,1). Rebuilding a duplicate child with the raw root scale
        # hides all descendants in Unity. Guard the source evidence and fix.
        import json
        project_root = path.parents[1]
        data = json.loads((project_root / "unity-ui-viewer/Assets/StreamingAssets/ui-scenes.json")
                          .read_text(encoding="utf-8"))
        zero_roots = {scene["id"] for scene in data["scenes"]
                      if scene["nodes"][0]["scale"] == [0, 0]}
        self.assertEqual(zero_roots, {
            "REF01-ship-upgrade", "REF03-islands-map-A",
            "REF03-islands-map-B", "REF04-home-crew",
        })
        source = (project_root / "unity-ui-viewer/Assets/Editor/UnityCanvasReconstructor.cs"
                  ).read_text(encoding="utf-8")
        self.assertIn("if (node.id == scene.rootTransform)", source)
        self.assertIn("map.Add(node.id, rect);", source)
        self.assertIn("continue;", source)
        self.assertIn("Local Preview Camera", source)
        self.assertIn("Zero-scale reconstructed Canvas", source)
        self.assertIn("Audit 5 generated Canvas scenes", source)
        # Regression for the actual Unity Editor failure:
        # InvalidDataException: Zero-scale reconstructed Canvas: REF01-ship-upgrade
        self.assertIn("rect.localScale = Vector3.one;", source)
        self.assertIn("instance.transform.localScale = Vector3.one;", source)
        self.assertLess(source.index("rect.localScale = Vector3.one;"),
                        source.index("PrefabUtility.SaveAsPrefabAsset(go, prefabPath)"))
        self.assertIn("EnsurePreviewCamera(newScene);", source)
        self.assertIn("Repair existing 5 scenes (Camera + Canvas scale)", source)
        self.assertIn("EditorSceneManager.SaveScene(opened, path)", source)
        self.assertIn("camera.enabled = true;", source)

    def test_reject_private_image_paths(self):
        scenes, art, comps, candidates = self.fixture()
        art["files"] = ["../../private.png"]
        with self.assertRaises(ValueError):
            module.prepare(scenes, art, comps, candidates)


if __name__ == "__main__":
    unittest.main()
