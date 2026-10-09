"""Evidence-only tests for Sprite border, typed UI and Spine pointer chains."""
import importlib
import pathlib
import sys
import types
import unittest
from unittest.mock import patch

ROOT = pathlib.Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
ui = importlib.import_module("audit_local_ui_components")
spine = importlib.import_module("trace_local_spine_links")


def obj(kind, data):
    return types.SimpleNamespace(
        type=types.SimpleNamespace(name=kind),
        read=lambda: data,
    )


class TestDeepEvidence(unittest.TestCase):
    def test_sprite_original_border_and_ppu(self):
        src = types.SimpleNamespace(
            m_Border=types.SimpleNamespace(x=2., y=3., z=4., w=5.),
            m_PixelsToUnits=100.,
            m_Rect=types.SimpleNamespace(width=40., height=50.))
        data = ui.sprite_details(obj("Sprite", src))
        self.assertEqual(data["border"], [2, 3, 4, 5])
        self.assertEqual(data["pixelsPerUnit"], 100)
        self.assertEqual(data["sourceRectSize"], [40, 50])

    def test_reject_border_bigger_than_sprite(self):
        src = types.SimpleNamespace(
            m_Border=types.SimpleNamespace(x=25., y=0., z=25., w=0.),
            m_PixelsToUnits=-1.,
            m_Rect=types.SimpleNamespace(width=20., height=20.))
        self.assertNotIn("border", ui.sprite_details(obj("Sprite", src)))
        self.assertNotIn("pixelsPerUnit", ui.sprite_details(obj("Sprite", src)))

    def test_ui_only_whitelist_fields(self):
        self.assertEqual(ui.plain({"m_Left": 4, "m_Right": 5, "privateFile": "secret"}),
                         {"m_Left": 4, "m_Right": 5})
        self.assertIsNone(ui.plain({"m_Left": float("nan")}))
        self.assertIsNone(ui.plain("private object"))
        self.assertIn("UnityEngine.UI.CanvasScaler", ui.FIELDS)
        self.assertIn("UnityEngine.UI.GridLayoutGroup", ui.FIELDS)

    def test_spine_requires_one_typed_candidate(self):
        base = {"reference": "REF04-home-crew",
                "source_component_id": "1440",
                "target_component_id": "75313",
                "target_serialized_file": "002_UnityDataAssetPack_datapack__file025"}
        record = spine.trace_one("REF04-home-crew", 200, "1440",
                                 [base, base], [], {}, set())
        self.assertEqual(record["status"], "ambiguous_skeleton_candidates")
        self.assertFalse(record["fieldVerified"])
        self.assertFalse(record["skinVerified"])

    def test_spine_requires_typed_skeleton_reader(self):
        base = {"source_component_id": "1440",
                "target_component_id": "75313",
                "target_serialized_file": "002_UnityDataAssetPack_datapack__file000"}
        groups = {1: {75313: obj("TextAsset", {})}}
        record = spine.trace_one("REF04-home-crew", 200, "1440",
                                 [base], [], groups, set())
        self.assertEqual(record["status"], "skeleton_component_not_found")

    def test_spine_content_chain_not_runtime_animation(self):
        direct = {"source_component_id": "1440",
                  "target_component_id": "123",
                  "target_serialized_file": "002_UnityDataAssetPack_datapack__file000"}
        atlas = {"source_serialized_file": direct["target_serialized_file"],
                 "source_component_id": "123", "target_component_id": "124",
                 "target_serialized_file": direct["target_serialized_file"]}
        groups = {1: {123: obj("MonoBehaviour", {}),
                      124: obj("MonoBehaviour", {})}}
        sj = {"name": "Ace", "version": "3.8.1", "animations": ["idle"],
              "pathId": 11, "pages": []}
        sa = {"name": "Ace.atlas", "pages": ["ace.png"], "pathId": 12}
        with patch.object(spine, "named_text_candidates", side_effect=[[sj], [sa]]):
            result = spine.trace_one("REF04-home-crew", 20, "1440", [direct],
                                     [atlas], groups, {"ace.png"})
        self.assertEqual(result["status"], "content_chain_verified_field_unverified")
        self.assertEqual(result["animationNames"], ["idle"])
        self.assertFalse(result["fieldVerified"])
        self.assertFalse(result["skinVerified"])
        self.assertFalse(result["activeAnimationVerified"])
        self.assertEqual(result["missingTextureNames"], [])
        self.assertEqual(len(result["localPackId"]), 24)


if __name__ == "__main__":
    unittest.main()
