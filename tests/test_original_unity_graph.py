"""Unit checks for original Unity GO/Component graph evidence, not UI guesses."""
from __future__ import annotations
import importlib
import sys
import pathlib
import types
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
native = importlib.import_module("audit_original_unity_graph")


def ptr(pid, fileid=0):
    return {"m_PathID": pid, "m_FileID": fileid}


class Reader:
    def __init__(self, kind, data, typetree=None, raw=b"serialized"):
        self.type = types.SimpleNamespace(name=kind)
        self.data = data
        self.typetree = typetree
        self.raw = raw

    def read(self):
        return self.data

    def parse_monobehaviour_head(self):
        return self.data

    def read_typetree(self):
        if self.typetree is None:
            raise TypeError("IL2CPP managed fields stripped")
        return self.typetree

    def get_raw_data(self):
        return self.raw


class TestRealGraph(unittest.TestCase):
    def fixture(self):
        def rect(gid, father, children):
            return Reader("RectTransform", {
                "m_GameObject": ptr(gid), "m_Father": ptr(father),
                "m_Children": [ptr(c) for c in children],
                "m_AnchorMin": {"x": 0, "y": 0},
                "m_AnchorMax": {"x": 1, "y": 1},
                "m_Pivot": {"x": 0.5, "y": 0.5},
                "m_SizeDelta": {"x": 800, "y": 600},
                "m_AnchoredPosition": {"x": 4, "y": 5},
                "m_LocalScale": {"x": 1, "y": 1, "z": 1},
                "m_LocalRotation": {"x": 0, "y": 0, "z": 0, "w": 1},
            })
        readers = {
            10: rect(100, 0, [11]),
            11: rect(101, 10, []),
            100: Reader("GameObject", {
                "m_Name": "Canvas",
                "m_IsActive": 1,
                "m_Component": [{"component": ptr(10)}, {"component": ptr(20)}],
            }),
            101: Reader("GameObject", {
                "m_Name": "Image",
                "m_IsActive": 1,
                "m_Component": [{"component": ptr(11)}, {"component": ptr(21)}],
            }),
            20: Reader("Canvas", {"m_GameObject": ptr(100), "m_Enabled": 1}),
            21: Reader("MonoBehaviour", {
                "m_GameObject": ptr(101),
                "m_Script": ptr(700, 2),
                "m_Enabled": 1
            }, raw=b"unknown managed fields"),
        }
        scene = {
            "id": "REF04-home-crew",
            "rootTransform": 10,
            "source": "002_UnityDataAssetPack_datapack__file025",
            "nodes": [
                {"id": 10, "parent": 0},
                {"id": 11, "parent": 10},
            ]
        }
        return scene, readers

    def test_go_and_component_pointers_are_exact(self):
        scene, readers = self.fixture()
        audit = native.records_for_scene(scene, readers, {
            "Canvas": 1, "SceneAsset": 0, "PrefabInstance": 0,
        })
        self.assertEqual(audit["stats"]["nodes"], 2)
        self.assertEqual(audit["stats"]["componentReferences"], 4)
        self.assertEqual(audit["stats"]["serializedComponents"], 4)
        self.assertEqual(audit["nodes"][0]["gameObjectId"], 100)
        self.assertEqual(audit["nodes"][0]["childTransformIds"], [11])
        self.assertEqual(audit["nodes"][1]["componentIds"], [11, 21])
        self.assertEqual(audit["provenance"]["originalEditorPrefabOrScene"],
                         "NOT_PROVEN")

    def test_missing_typetree_is_reported_not_filled(self):
        scene, readers = self.fixture()
        audit = native.records_for_scene(scene, readers, {})
        mono = next(x for x in audit["components"] if x["pathId"] == 21)
        self.assertEqual(mono["fieldStatus"], "managed_fields_unavailable")
        self.assertEqual(mono["monoScriptPointer"]["fileId"], 2)
        self.assertNotIn("ImageType", mono)
        self.assertEqual(len(mono["rawEvidence"]["sha256"]), 64)

    def test_invalid_parent_is_rejected(self):
        scene, readers = self.fixture()
        scene["nodes"][1]["parent"] = 75
        with self.assertRaisesRegex(ValueError, "(parent mismatch|parent/child pointers disagree)"):
            native.records_for_scene(scene, readers, {})

    def test_missing_transform_on_gameobject_is_rejected(self):
        scene, readers = self.fixture()
        readers[100].data["m_Component"] = [{"component": ptr(20)}]
        with self.assertRaisesRegex(ValueError, "not in GameObject components"):
            native.records_for_scene(scene, readers, {})

    def test_wrong_component_id_and_foreign_pointer_are_not_fabricated(self):
        scene, readers = self.fixture()
        readers[101].data["m_Component"].append({"component": ptr(400, 1)})
        audit = native.records_for_scene(scene, readers, {})
        self.assertEqual(audit["stats"]["errors"]["external_or_invalid_component_ref"], 1)
        self.assertEqual(audit["stats"]["componentReferences"], 4)
        self.assertEqual(native.pointer({"m_FileID": 1, "m_PathID": 44}),
                         {"fileId": 1, "pathId": 44})

    def test_prefab_does_not_infer_from_gameobject_names(self):
        scene, readers = self.fixture()
        readers[100].data["m_Name"] = "GameMasterPrefab"
        audit = native.records_for_scene(scene, readers, {"Prefab": 0})
        self.assertEqual(audit["provenance"]["sourceTypeCounts"]["Prefab"], 0)
        self.assertEqual(audit["provenance"]["originalEditorPrefabOrScene"],
                         "NOT_PROVEN")

    def test_actual_prefab_pointer_is_recorded_not_assumed_source(self):
        scene, readers = self.fixture()
        readers[100].data["m_PrefabInstance"] = ptr(300, 3)
        audit = native.records_for_scene(scene, readers, {"PrefabInstance": 1})
        self.assertEqual(audit["provenance"]["gameObjectsWithPrefabPointers"], 1)
        self.assertEqual(audit["provenance"]["originalEditorPrefabOrScene"],
                         "NOT_PROVEN")
        self.assertEqual(audit["nodes"][0]["prefabPointers"]["m_PrefabInstance"],
                         {"fileId": 3, "pathId": 300})


if __name__ == "__main__":
    unittest.main()
