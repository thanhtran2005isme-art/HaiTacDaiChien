"""Cross-backend field agreement must never permit inferred UI values."""
import copy
import pathlib
import sys
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[1]
sys.path.insert(0, str(ROOT / "tools"))
import compare_managed_ui_backends as checker


def fixture(backend):
    scenes = []
    counter = 0
    for ix in range(5):
        size = (244, 3043, 239, 256, 1564)[ix]
        components = []
        for j in range(size):
            counter += 1
            row = {"pathId": j + 1, "kind": "MonoBehaviour",
                   "className": "UnityEngine.UI.Image",
                   "assembly": "UnityEngine.UI",
                   "gameObjectId": j + 999,
                   "rectTransformId": j + 20000,
                   "scriptPointer": {"fileId": 1, "pathId": 55},
                   "status": "NO_MANAGED_TYPETREE"}
            if j == 0:
                row["status"] = checker.SUCCEEDED
                row["fields"] = {"m_Type": 0, "m_PreserveAspect": False}
            components.append(row)
        scenes.append({"sceneId": "REF%d" % ix, "components": components})
    assert counter == 5346
    return {"scenes": scenes, "stats": {"componentCount": 5346},
            "globalMonoScriptResolution": {"resolved": 2320},
            "generatedBinaryProof": {"backend": backend,
                "unityPyNativeHeader": True,
                "gameUnityVersion": "2022.3.51f1",
                "library": {"sha256": "lib-source"},
                "metadata": {"sha256": "meta-source"}}}


class IndependentBackendValidation(unittest.TestCase):
    def setUp(self):
        self.a = fixture("AssetStudio")
        self.b = fixture("AssetRipper")

    def test_identical_fields_get_cross_verified(self):
        s = checker.compare(self.a, self.b)
        self.assertEqual(s["independentlyMatchedComponents"], 5)
        self.assertEqual(s["independentlyMatchedFieldValues"], 10)

    def test_different_serialized_fields_must_fail(self):
        self.b["scenes"][2]["components"][0]["fields"]["m_Type"] = 2
        with self.assertRaisesRegex(ValueError, "disagree"):
            checker.compare(self.a, self.b)

    def test_missing_source_identity_must_fail(self):
        self.b["scenes"][0]["components"][0]["scriptPointer"]["pathId"] = 123
        with self.assertRaisesRegex(ValueError, "identities differ"):
            checker.compare(self.a, self.b)

    def test_one_backend_can_remain_unknown_without_guessing(self):
        entry = self.b["scenes"][3]["components"][0]
        entry["status"] = "NO_MANAGED_TYPETREE"
        entry.pop("fields")
        result = checker.compare(self.a, self.b)
        self.assertEqual(result["studioOnlyComponents"], 1)
        self.assertEqual(result["independentlyMatchedComponents"], 4)

    def test_source_version_or_hash_disagreement_is_rejected(self):
        self.b["generatedBinaryProof"]["metadata"]["sha256"] = "different"
        with self.assertRaisesRegex(ValueError, "binaries differ"):
            checker.compare(self.a, self.b)

    def test_source_counts_and_backend_identity_are_required(self):
        self.a["generatedBinaryProof"]["backend"] = "AssetsTools"
        with self.assertRaisesRegex(ValueError, "independent"):
            checker.compare(self.a, self.b)
        self.a = fixture("AssetStudio")
        self.a["stats"]["componentCount"] -= 1
        with self.assertRaisesRegex(ValueError, "5346"):
            checker.compare(self.a, self.b)


if __name__ == "__main__":
    unittest.main()
