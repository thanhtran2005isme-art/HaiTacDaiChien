"""Strict source-only plan: never apply a single-backend or unowned UI field."""
import copy
import pathlib
import sys
import unittest
from unittest import mock

ROOT = pathlib.Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
import build_verified_ui_prefab_plan as plan
import compare_managed_ui_backends as comparison

SIZES = (244, 3043, 239, 256, 1564)


def fixture():
    scenes, graph_scenes = [], []
    for k, count in enumerate(SIZES):
        rows, nodes, comps = [], [], []
        for idx in range(count):
            cid, gid, tid = idx + 1, 5000 + idx, 8000 + idx
            row = {
                "pathId": cid, "kind": "MonoBehaviour",
                "className": "Other.Unknown",
                "assembly": "Game",
                "gameObjectId": gid, "rectTransformId": tid,
                "scriptPointer": {"fileId": 1, "pathId": 78},
                "status": "NO_MANAGED_TYPETREE",
            }
            record = {
                "pathId": cid, "kind": "MonoBehaviour",
                "gameObjectPointer": {"fileId": 0, "pathId": gid},
                "monoScriptPointer": copy.deepcopy(row["scriptPointer"]),
            }
            rows.append(row)
            comps.append(record)
            nodes.append({"rectTransformId": tid, "gameObjectId": gid,
                          "componentIds": [cid]})
        if k == 0:
            row = rows[0]
            row.update({
                "className": "UnityEngine.UI.Image",
                "assembly": "UnityEngine.UI",
                "nativeEnabled": True,
                "status": comparison.SUCCEEDED,
                "fields": {
                    "m_Color": {"r": 1.0, "g": 0.5, "b": 0.2, "a": 1.0},
                    "m_Type": 0, "m_PreserveAspect": False,
                    "m_FillMethod": 0, "m_FillAmount": 1.0,
                    "m_FillOrigin": 0, "m_FillClockwise": False,
                },
                "binaryProof": {
                    "rawObjectSha256": "f" * 64,
                    "rawObjectBytes": 64,
                    "strictObjectSizeChecked": True,
                    "exactSourcePointerChecked": True,
                    "nativeHeaderMethod": "UNITYPY_EXACT_SOURCE_UNITY_VERSION",
                },
            })
            # CanvasRenderer is an actual serialized native component linked
            # to precisely the same GameObject as the source Image.
            nodes[0]["componentIds"].append(2)
            comps[1]["kind"] = "CanvasRenderer"
            comps[1]["gameObjectPointer"] = {"fileId": 0, "pathId": rows[0]["gameObjectId"]}
            nodes.pop(1)
        scenes.append({"sceneId": "REF%d" % k, "components": rows})
        graph_scenes.append({"sceneId": "REF%d" % k,
                             "nodes": nodes, "components": comps})
    evidence = {"scenes": scenes, "stats": {"componentCount": 5346},
                "globalMonoScriptResolution": {"resolved": 2320},
                "generatedBinaryProof": {
                    "backend": "AssetStudio", "unityPyNativeHeader": True,
                    "gameUnityVersion": "2022.3.51f1",
                    "library": {"sha256": "a" * 64},
                    "metadata": {"sha256": "b" * 64},
                }}
    ripper = copy.deepcopy(evidence)
    ripper["generatedBinaryProof"]["backend"] = "AssetRipper"
    graph = {"version": 1,
             "classification":
                 "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE",
             "scenes": graph_scenes}
    return evidence, ripper, graph


def minimal(studio, ripper, graph):
    with mock.patch.object(plan, "EXPECTED_CLASSES", {"UnityEngine.UI.Image": 1}), \
         mock.patch.object(plan, "EXPECTED_FIELDS", 7), \
         mock.patch.object(plan, "EXPECTED_COMPONENTS", 1), \
         mock.patch.object(plan, "EXPECTED_EXCLUDED", 0), \
         mock.patch.object(plan, "EXPECTED_EXCLUDED_FIELDS", 0):
        return plan.prepare(studio, ripper, graph, "c" * 64)


class TestExactManagedStudyPlan(unittest.TestCase):
    def setUp(self):
        self.studio, self.ripper, self.graph = fixture()

    def test_source_bound_verified_image_fields(self):
        actual = minimal(self.studio, self.ripper, self.graph)
        self.assertEqual(actual["classification"],
                         "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS")
        self.assertEqual(actual["verifiedFieldValues"], 7)
        self.assertEqual(sum(map(lambda s: len(s["components"]),actual["scenes"])),1)
        item = actual["scenes"][0]["components"][0]
        self.assertEqual(item["componentPathId"], 1)
        self.assertTrue(item["enabled"])
        self.assertEqual(item["rawObjectSha256"], "f" * 64)
        self.assertEqual({x["name"] for x in item["fields"]},
                         set(plan.CLASS_FIELDS["UnityEngine.UI.Image"]))
        self.assertFalse(any(s["components"] for s in actual["scenes"][1:]))

    def test_backend_field_disagreement_must_block(self):
        self.ripper["scenes"][0]["components"][0]["fields"]["m_Type"] = 3
        with self.assertRaisesRegex(ValueError, "disagree"):
            minimal(self.studio, self.ripper, self.graph)

    def test_original_graph_owner_disagreement_must_block(self):
        self.graph["scenes"][0]["components"][0]["gameObjectPointer"]["pathId"] += 9
        with self.assertRaisesRegex(ValueError, "graph script/owner"):
            minimal(self.studio, self.ripper, self.graph)

    def test_original_canvasrenderer_dependency_must_be_source_proven(self):
        self.graph["scenes"][0]["components"][1]["kind"] = "Other.Native"
        with self.assertRaisesRegex(ValueError, "CanvasRenderer"):
            minimal(self.studio, self.ripper, self.graph)

    def test_missing_binary_checksum_or_full_read_must_block(self):
        self.ripper["scenes"][0]["components"][0]["binaryProof"]["rawObjectSha256"] = "0"*64
        with self.assertRaisesRegex(ValueError, "bytes not identical"):
            minimal(self.studio, self.ripper, self.graph)
        self.ripper["scenes"][0]["components"][0]["binaryProof"]["rawObjectSha256"] = "f"*64
        self.studio["scenes"][0]["components"][0]["binaryProof"]["strictObjectSizeChecked"] = False
        with self.assertRaisesRegex(ValueError, "strict source binary proof"):
            minimal(self.studio, self.ripper, self.graph)

    def test_invalid_types_and_extra_fields_must_block(self):
        self.studio["scenes"][0]["components"][0]["fields"]["m_Type"] = True
        self.ripper["scenes"][0]["components"][0]["fields"]["m_Type"] = True
        with self.assertRaisesRegex(ValueError, "Integer field type"):
            minimal(self.studio, self.ripper, self.graph)
        self.studio, self.ripper, self.graph = fixture()
        self.studio["scenes"][0]["components"][0]["fields"]["m_Fake"] = 1
        self.ripper["scenes"][0]["components"][0]["fields"]["m_Fake"] = 1
        with self.assertRaisesRegex(ValueError, "Missing or unexpected"):
            minimal(self.studio, self.ripper, self.graph)

    def test_out_of_range_enum_must_never_be_exported(self):
        self.studio["scenes"][0]["components"][0]["fields"]["m_Type"] = 999
        self.ripper["scenes"][0]["components"][0]["fields"]["m_Type"] = 999
        with self.assertRaisesRegex(ValueError, "Invalid source field value/range"):
            minimal(self.studio, self.ripper, self.graph)

    def test_source_identity_changed_even_with_same_fields_must_block(self):
        self.ripper["scenes"][0]["components"][0]["scriptPointer"]["pathId"] += 1
        with self.assertRaisesRegex(ValueError, "identities differ"):
            minimal(self.studio, self.ripper, self.graph)


if __name__ == "__main__":
    unittest.main()
