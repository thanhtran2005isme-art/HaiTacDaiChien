"""Local-only, per-component original serialized UI gap-report tests."""
from __future__ import annotations
import pathlib
import sys
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
import summarize_original_unity_gaps as gaps


class TestSourceComponentGaps(unittest.TestCase):
    def test_unknown_managed_never_counts_as_renderable(self):
        self.assertEqual(gaps.classify({
            "kind": "MonoBehaviour", "fieldStatus": "managed_fields_unavailable"
        }), "NEEDS_MANAGED_TYPE_TREE")
        self.assertEqual(gaps.classify({
            "kind": "MonoBehaviour", "fieldStatus": "typetree_available"
        }), "NEEDS_EXACT_RUNTIME_FIELD_BINDING")
        self.assertEqual(gaps.classify({
            "kind": "Canvas", "fieldStatus": "native"
        }), "NATIVE_FIELDS_REQUIRE_UNITY_PARITY_TEST")
        self.assertEqual(gaps.classify({
            "kind": "MISSING", "fieldStatus": "missing_serialized_object"
        }), "MISSING_COMPONENT")

    def test_no_gameobject_or_component_is_invented(self):
        scenes = []
        for i in range(5):
            scenes.append({
                "sceneId": "REF" + str(i + 1),
                "sourceSerializedFile": "002_UnityData__file" + str(i),
                "nodes": [{
                    "rectTransformId": 10, "gameObjectId": 200 + i,
                    "name": "Go", "componentIds": [10, 12]
                }],
                "components": [
                    {"pathId": 10, "kind": "RectTransform",
                     "fieldStatus": "native"},
                    {"pathId": 12, "kind": "MonoBehaviour",
                     "fieldStatus": "managed_fields_unavailable",
                     "monoScriptPointer": {"fileId": 1, "pathId": 44}}
                ]
            })
        graph = {"version": 1, "stats": {"components": 10},
                 "scenes": scenes}
        rows = gaps.rows_from_graph(graph)
        self.assertEqual(len(rows), 10)
        self.assertEqual(rows[1]["gameObjectPathId"], 200)
        self.assertEqual(rows[1]["monoScriptFileId"], 1)
        self.assertEqual(rows[1]["monoScriptPathId"], 44)
        self.assertIn("NEEDS_MANAGED_TYPE_TREE", gaps.format_report(rows, graph))
        graph["stats"]["components"] = 11
        with self.assertRaisesRegex(ValueError, "not uniquely matched"):
            gaps.rows_from_graph(graph)


if __name__ == "__main__":
    unittest.main()
