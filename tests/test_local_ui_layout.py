"""Metadata evidence tests: no XAPK, commercial textures or Unity runtime required."""
from __future__ import annotations
import importlib.util
import pathlib
import types
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[1]
file = ROOT / "tools/export_local_ui_layout.py"
spec = importlib.util.spec_from_file_location("ui_layout", file)
module = importlib.util.module_from_spec(spec)
spec.loader.exec_module(module)


def pointer(pid, fid=0):
    return {"m_FileID": fid, "m_PathID": pid}


def reader(kind, data, typetree=None):
    class Reader:
        type = types.SimpleNamespace(name=kind)
        def read(self):
            return data
        def parse_monobehaviour_head(self):
            return data
        def read_typetree(self):
            if typetree is None:
                raise ValueError("IL2CPP typetree unavailable")
            return typetree
    return Reader()


class TestLayoutEvidence(unittest.TestCase):
    def source(self):
        scene = {
            "id": "REF01-ship-upgrade", "rootTransform": 10,
            "source": "002_UnityDataAssetPack_datapack__file000",
            "nodes": [
                {"id": 10, "parent": 0, "path": "/Canvas"},
                {"id": 11, "parent": 10, "path": "/Canvas/Panel"},
            ],
        }
        def transform(go, parent, q):
            return reader("RectTransform", {
                "m_GameObject": pointer(go), "m_Father": pointer(parent),
                "m_LocalRotation": {"x": q[0], "y": q[1], "z": q[2], "w": q[3]},
                "m_LocalScale": {"x": 1, "y": 1, "z": 1},
                "m_LocalPosition": {"x": 0, "y": 0, "z": -3},
            })
        rects = {
            10: transform(100, 0, [0, 0, 0, 1]),
            11: transform(101, 10, [0, 0, 0.382683432, 0.923879533]),
            21: reader("Canvas", {
                "m_GameObject": pointer(100),
                "m_SortingOrder": 12, "m_OverrideSorting": True,
                "m_Enabled": 0, "m_RenderMode": 1, "m_TargetDisplay": 0,
            }),
            55: reader("MonoBehaviour", {
                       "m_GameObject": pointer(101), "m_Enabled": 0},
                       {"m_Type": 3, "m_PreserveAspect": True,
                        "m_FillAmount": 0.6,
                        "m_Color": {"r": 1, "g": 0.5, "b": 0.4, "a": 0.9}}),
        }
        return scene, rects

    def test_exact_rotation_canvas_and_image_properties(self):
        scene, objects = self.source()
        chosen = module.choose_serialized_file(scene, {1: objects, 2: {}})
        self.assertIs(chosen, objects)
        out = module.verified_scene(scene, objects, [{
            "ui_path": "/Canvas/Panel", "component_id": "55"
        }])
        self.assertEqual(len(out["nodes"]), 2)
        self.assertEqual(out["nodes"][1]["rotation"][3], 0.923879533)
        self.assertEqual(out["nodes"][1]["localPositionZ"], -3)
        self.assertEqual(out["canvases"][0]["sortingOrder"], 12)
        self.assertTrue(out["canvases"][0]["hasSortingOrder"])
        self.assertTrue(out["canvases"][0]["hasEnabled"])
        self.assertEqual(out["canvases"][0]["enabled"], 0)
        self.assertTrue(out["canvases"][0]["hasRenderMode"])
        self.assertEqual(out["canvases"][0]["renderMode"], 1)
        self.assertTrue(out["canvases"][0]["hasTargetDisplay"])
        image = out["images"][0]
        self.assertEqual(image["nodeId"], 11)
        self.assertTrue(image["hasType"])
        self.assertEqual(image["type"], 3)
        self.assertTrue(image["preserveAspect"])
        self.assertTrue(image["hasColor"])
        self.assertEqual(out["imageBindings"], [{
            "nodeId": 11, "componentId": 55, "gameObjectId": 101,
            "hasEnabled": True, "enabled": False
        }])

    def test_canvas_missing_native_fields_are_not_guessed(self):
        scene, objects = self.source()
        objects[21] = reader("Canvas", {"m_GameObject": pointer(100)})
        result = module.verified_scene(scene, objects, [])
        canvas = result["canvases"][0]
        self.assertNotIn("hasEnabled", canvas)
        self.assertNotIn("hasRenderMode", canvas)
        self.assertNotIn("hasTargetDisplay", canvas)

    def test_unity_canvas_importer_never_switches_to_unverified_camera(self):
        csharp = (ROOT / "unity-ui-viewer/Assets/Editor/UnityCanvasReconstructor.cs"
                  ).read_text(encoding="utf-8")
        self.assertIn("if (record.hasEnabled) canvas.enabled = record.enabled", csharp)
        self.assertIn("sourceCanvasesWithRenderMode", csharp)
        self.assertNotIn("canvas.renderMode = (RenderMode)record.renderMode", csharp)

    def test_missing_typetree_does_not_invent_properties(self):
        scene, objects = self.source()
        objects[55] = reader("MonoBehaviour", {"m_GameObject": pointer(101)})
        out = module.verified_scene(scene, objects, [{
            "ui_path": "/Canvas/Panel", "component_id": "55"
        }])
        self.assertEqual(out["images"], [])
        self.assertEqual(out["limitations"]["image_typetree_unavailable"], 1)
        # Even with IL2CPP-stripped Image fields, the verified header gives
        # the exact original m_Enabled; do not force the Image visible.
        self.assertEqual(out["imageBindings"][0]["nodeId"], 11)
        self.assertFalse(out["imageBindings"][0]["hasEnabled"])

    def test_image_binding_rejects_mismatched_gameobject_owner(self):
        scene, objects = self.source()
        objects[55] = reader("MonoBehaviour", {
            "m_GameObject": pointer(999), "m_Enabled": 0})
        out = module.verified_scene(scene, objects, [{
            "ui_path": "/Canvas/Panel", "component_id": "55"
        }])
        self.assertEqual(out["imageBindings"], [])
        self.assertEqual(out["limitations"]["image_gameobject_mismatch"], 1)

    def test_duplicate_link_rows_deduplicate_same_original_component(self):
        scene, objects = self.source()
        link = {"ui_path": "/Canvas/Panel", "component_id": "55"}
        out = module.verified_scene(scene, objects, [link, link])
        self.assertEqual(len(out["imageBindings"]), 1)

    def test_two_image_components_one_gameobject_must_not_be_guessed(self):
        scene, objects = self.source()
        objects[56] = reader("MonoBehaviour", {
            "m_GameObject": pointer(101), "m_Enabled": 1})
        out = module.verified_scene(scene, objects, [
            {"ui_path": "/Canvas/Panel", "component_id": "55"},
            {"ui_path": "/Canvas/Panel", "component_id": "56"},
        ])
        self.assertEqual(out["imageBindings"], [])
        self.assertEqual(out["limitations"]["ambiguous_image_component_per_node"], 1)

    def test_unity_preview_applies_source_enabled_not_every_image_enabled(self):
        code = (ROOT / "unity-ui-viewer/Assets/Editor/UnityCanvasReconstructor.cs"
                ).read_text(encoding="utf-8")
        self.assertIn("db.version != 2", code)
        self.assertIn("ApplyExactImageEnabled(node.id, image, exactImage,", code)
        self.assertIn("image.enabled = binding.enabled", code)
        self.assertIn("record.type >= 0 && record.type <= 3", code)
        self.assertIn("sourceDisabledImages", code)

    def test_duplicate_ids_are_resolved_using_original_serialized_file_order(self):
        scene, objects = self.source()
        # Different SerializedFiles can use identical path IDs.
        first = {10: objects[10], 11: objects[11]}
        second = dict(objects)
        scene["source"] = "002_UnityDataAssetPack_datapack__file001"
        found = module.choose_serialized_file(scene, {1: first, 2: second})
        self.assertIs(found, second)
        scene["source"] = "002_UnityDataAssetPack_datapack__file002"
        with self.assertRaisesRegex(ValueError, "out of range"):
            module.choose_serialized_file(scene, {1: first, 2: second})

    def test_cross_file_gameobject_reference_rejected(self):
        scene, objects = self.source()
        objects[11] = reader("RectTransform", {
            "m_GameObject": pointer(101, 1), "m_Father": pointer(10),
            "m_LocalRotation": {"x": 0, "y": 0, "z": 0, "w": 1},
        })
        with self.assertRaisesRegex(ValueError, "Unverifiable GameObject"):
            module.verified_scene(scene, objects, [])

    def test_rotation_and_color_validation(self):
        self.assertIsNone(module.quaternion({"x": 5, "y": 0, "z": 0, "w": 0}))
        self.assertIsNone(module.valid_color({"r": 1, "g": -1, "b": 0, "a": 1}))
        self.assertEqual(module.local_id(pointer(123)), 123)
        self.assertEqual(module.local_id(pointer(123, 2)), 0)

    def test_unity_importer_only_uses_evidence_not_guessed_z_rotation(self):
        code = (ROOT / "unity-ui-viewer/Assets/Editor/UnityCanvasReconstructor.cs"
                ).read_text(encoding="utf-8")
        self.assertIn("ReadLayoutEvidence(root, scenes)", code)
        self.assertIn("ReadRotation(sourceNode.rotation)", code)
        self.assertIn("ApplyCanvasSettings", code)
        self.assertIn("ApplyImageSettings", code)
        self.assertNotIn("Quaternion.Euler(0, 0, node.rotationZ)", code)


if __name__ == "__main__":
    unittest.main()
