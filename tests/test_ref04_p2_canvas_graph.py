"""REF04 P2 source Canvas ancestry checks, never a runtime layout proof."""
import importlib
from pathlib import Path
import sys
import unittest

ROOT = Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
graph = importlib.import_module("ref04_p2_canvas_graph")
p2 = importlib.import_module("audit_ref04_p2_runtime_alignment")

def source_row(kind, cid, go, chain, canvas=1000):
    return {
        "category": kind, "componentPathId": cid, "gameObjectPathId": go,
        "rectTransformPathId": chain[0], "verifiedSerializedFieldNames": ["m_ReferenceResolution"],
        "ancestry": {
            "originalRectTransformPathIdsLeafToAncestor": chain,
            "originalNearestCanvasRectTransformPathId": canvas,
            "sourceParentTraceStatus": "SERIALIZED_ROOT_REACHED_RUNTIME_ROOT_UNPROVEN",
            "runtimeCanvasOrViewportProven": False,
        },
        "unityImportAllowed": False, "runtimeFormulaProven": False,
    }

def source_rows():
    return ([source_row("Canvas", 500, 100, [1000]),
             source_row("CanvasScaler", 501, 100, [1000])]
            + [source_row("SafeArea", 510+i, 110+i, [1010+i, 1000]) for i in range(6)]
            + [source_row("PanelHome2", 600, 120, [1020, 1010, 1000])])

class SourceCanvasGraphTests(unittest.TestCase):
    def test_exact_serialized_canvas_scaler_safearea_panel_graph(self):
        result = graph.build_graph(source_rows())
        self.assertEqual(result["sourceCanvas"]["rectTransformPathId"], 1000)
        self.assertTrue(result["sourceCanvasScaler"]["originalSameGameObjectAsCanvas"])
        self.assertIn("m_ReferenceResolution",
                      result["sourceCanvasScaler"]["originalSerializedScaleConfigFieldNames"])
        self.assertTrue(result["sourceCanvasScaler"]["originalSerializedScaleConfigurationSourceOnly"])
        self.assertEqual(result["sourceAncestryCounts"]["SafeArea:SOURCE_CANVAS_ANCESTOR_VERIFIED"], 6)
        panel = next(x for x in result["relatedComponents"] if x["category"] == "PanelHome2")
        self.assertEqual(panel["originalCanvasDistanceInParentEdges"], 2)
        self.assertEqual(panel["serializedSafeAreaAncestorRectTransformPathIds"], [1010])
        self.assertFalse(panel["runtimeScriptBindingProven"])
        self.assertIsNone(result["runtimeSafeAreaPanelHome2Formula"])
        self.assertFalse(result["runtimeLayoutProven"])
        self.assertFalse(result["unityImportAllowed"])

    def test_external_parent_is_not_filled_in_by_guessing(self):
        rows = source_rows()
        rows[2]["ancestry"].update(
            originalRectTransformPathIdsLeafToAncestor=[1010],
            originalNearestCanvasRectTransformPathId=None,
            sourceParentTraceStatus="BLOCKED_PARENT_IN_EXTERNAL_SERIALIZED_FILE")
        out = graph.build_graph(rows)
        item = next(x for x in out["relatedComponents"] if x["componentPathId"] == 510)
        self.assertFalse(item["originalCanvasAncestorConfirmed"])
        self.assertIsNone(item["originalCanvasDistanceInParentEdges"])

    def test_scaler_different_go_is_reported_not_fabricated(self):
        rows = source_rows()
        rows[1]["gameObjectPathId"] = 999
        rows[1]["rectTransformPathId"] = 1001
        rows[1]["ancestry"]["originalRectTransformPathIdsLeafToAncestor"] = [1001, 1000]
        out = graph.build_graph(rows)
        self.assertFalse(out["sourceCanvasScaler"]["originalSameGameObjectAsCanvas"])
        self.assertEqual(out["sourceCanvasScaler"]["originalSameGameObjectStatus"],
                         "SOURCE_DIFFERENT_GAMEOBJECT")

    def test_reject_cycle_fake_ancestor_and_runtime_claim(self):
        rows = source_rows()
        rows[4]["ancestry"]["originalNearestCanvasRectTransformPathId"] = 3333
        with self.assertRaisesRegex(ValueError, "ancestor"):
            graph.build_graph(rows)
        rows = source_rows()
        rows[3]["ancestry"]["runtimeCanvasOrViewportProven"] = True
        with self.assertRaisesRegex(ValueError, "runtime"):
            graph.build_graph(rows)
        rows = source_rows()
        rows[4]["componentPathId"] = 510
        with self.assertRaisesRegex(ValueError, "identity"):
            graph.build_graph(rows)
        rows = source_rows()
        rows[8]["ancestry"]["originalRectTransformPathIdsLeafToAncestor"] = [1020, 1020]
        with self.assertRaisesRegex(ValueError, "cyclic"):
            graph.build_graph(rows)

    def test_real_audit_includes_source_graph_and_never_formula(self):
        from test_ref04_p2_runtime_alignment import fixture
        result = p2.build(*fixture())
        self.assertEqual(result["sourceCanvasGraph"]["sourceCanvas"]["rectTransformPathId"], 1000)
        self.assertEqual(result["counts"]["SafeAreaAdapter"], 6)
        self.assertFalse(result["sourceCanvasGraph"]["runtimeLayoutProven"])
        self.assertFalse(result["runtimeAlignmentProven"])
        self.assertIsNone(result["runtimeAlignmentFormula"])

if __name__ == "__main__":
    unittest.main()
