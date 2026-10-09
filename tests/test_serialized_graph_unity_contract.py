"""Guard the *evidence-only* Unity scene graph builder from invented original UI."""
from pathlib import Path
import unittest

ROOT=Path(__file__).resolve().parents[1]
EDITOR=(ROOT/"unity-ui-viewer/Assets/Editor/OriginalSerializedGraphImporter.cs")
EVIDENCE=(ROOT/"unity-ui-viewer/Assets/Scripts/OriginalSerializedEvidence.cs")
EXPORT=(ROOT/"tools/audit_original_unity_graph.py")


class TestSerializedGraphUnityContract(unittest.TestCase):
    def test_only_verified_transform_and_components_metadata(self):
        code=EDITOR.read_text(encoding="utf-8")
        self.assertIn("Build evidence-only serialized graph prefabs",code)
        self.assertIn("candidateRootTransform",code)
        self.assertIn("OriginalSerializedEvidence",code)
        self.assertIn("source.nodes",code)
        self.assertIn("SetSiblingIndex",code)
        self.assertIn("localScale = new Vector3",code)
        self.assertIn("new Quaternion(",code)
        self.assertIn('NOT original game',code) if 'NOT original game' in code else self.assertIn(
            "NOT original",code)
        for prohibited in ("new Vector2(1600, 900)",
                           "AddComponent<Image>", "AddComponent<Canvas>",
                           "AddComponent<RectMask2D>", "AddComponent<SkeletonGraphic>",
                           "AddComponent<Camera>"):
            self.assertNotIn(prohibited,code)
        self.assertIn('output", "original-unity-graph.json"',code)
        self.assertIn("LocalReconstruction",code)
        self.assertIn("SaveCurrentModifiedScenesIfUserWantsTo",code)

    def test_original_component_evidence_class_owns_one_script(self):
        code=EVIDENCE.read_text(encoding="utf-8")
        self.assertIn("class OriginalSerializedEvidence : MonoBehaviour",code)
        self.assertIn("originalComponentPathIds",code)
        self.assertIn("gameObjectPathId",code)
        self.assertIn("originalComponentTypes",code)

    def test_local_only_private_graph_and_provenance(self):
        code=EXPORT.read_text(encoding="utf-8")
        self.assertIn("output/original-unity-graph.json",code)
        self.assertIn("SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE",code)
        self.assertIn('originalEditorPrefabOrScene": "NOT_PROVEN"',code)
        self.assertIn("m_Component",code)
        self.assertIn("m_Children",code)
        self.assertIn("m_Father",code)
        self.assertIn('is_relative_to(private)',code)
        ignore=(ROOT/".gitignore").read_text(encoding="utf-8")
        self.assertIn("/output/",ignore)
        self.assertIn("/unity-ui-viewer/Assets/LocalReconstruction/",ignore)


if __name__=="__main__":
    unittest.main()
