"""Guards for the opt-in Unity Editor study-copy importer (no Unity license needed)."""
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
EDITOR = ROOT / "unity-ui-viewer/Assets/Editor/VerifiedManagedUiPrefabImporter.cs"
MARKER = ROOT / "unity-ui-viewer/Assets/Scripts/ManagedUiSourceEvidence.cs"
GRAPH = ROOT / "unity-ui-viewer/Assets/Editor/OriginalSerializedGraphImporter.cs"
PLAN = ROOT / "tools/build_verified_ui_prefab_plan.py"


class TestManagedUiStudyPrefabContract(unittest.TestCase):
    def test_do_not_modify_original_evidence_graph(self):
        code = EDITOR.read_text(encoding="utf-8")
        self.assertIn('VerifiedManagedFieldPrefabs', code)
        self.assertIn('VerifiedManagedFieldScenes', code)
        self.assertIn('_SERIALIZED_GRAPH.prefab', code)
        self.assertIn('_VERIFIED_FIELDS_STUDY.prefab', code)
        self.assertIn('PrefabUtility.LoadPrefabContents(source)', code)
        self.assertIn('PrefabUtility.SaveAsPrefabAsset(copy, TargetPath(scene.sceneId))', code)
        self.assertIn('refusing to overwrite local edits', code)
        self.assertIn('AssetDatabase.DeleteAsset(path)', code)
        self.assertIn('SaveCurrentModifiedScenesIfUserWantsTo', code)
        graph = GRAPH.read_text(encoding="utf-8")
        self.assertNotIn('AddComponent<Image>', graph)
        self.assertNotIn('AddComponent<Canvas>', graph)

    def test_readonly_strict_proof_and_parent_links(self):
        source = EDITOR.read_text(encoding="utf-8")
        for fragment in (
            'TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS',
            'Sha256(graphBytes) != plan.sourceGraphSha256',
            'plan.verifiedComponents != 1108',
            'plan.verifiedFieldValues != 7451',
            'plan.singleBackendExcludedComponents != 93',
            'plan.singleBackendExcludedFieldValues != 651',
            'src.gameObjectPointer.pathId != target.gameObjectPathId',
            'node.componentIds.Contains(target.componentPathId)',
            'Image CanvasRenderer source missing.',
            'CanvasScaler native Canvas source missing.',
            'Multiple same-class source uGUI components',
            'note.exactTwoBackendFieldAgreement = true',
            'Prefab field audit PASS',
            'SameManaged(component, field)',
        ):
            self.assertIn(fragment, source)

    def test_allowlist_exact_four_cross_verified_classes(self):
        code = EDITOR.read_text(encoding="utf-8")
        field_plan = PLAN.read_text(encoding="utf-8")
        for value in ('UnityEngine.UI.Image', 'UnityEngine.UI.CanvasScaler',
                      'UnityEngine.UI.Mask', 'UnityEngine.UI.ContentSizeFitter'):
            self.assertIn(value, code)
            self.assertIn(value, field_plan)
        self.assertIn('Unknown jointly verified UI class', field_plan)
        self.assertIn('m_Color', code)
        self.assertIn('m_ReferenceResolution', code)
        self.assertNotIn('AddComponent<SkeletonGraphic>', code)
        self.assertNotIn('AddComponent<HorizontalLayoutGroup>', code)
        self.assertNotIn('AddComponent<VerticalLayoutGroup>', code)
        self.assertNotIn('AddComponent<Camera>', code)

    def test_manifest_and_evidence_are_local_only(self):
        plan = PLAN.read_text(encoding="utf-8")
        marker = MARKER.read_text(encoding="utf-8")
        ignore = (ROOT / ".gitignore").read_text(encoding="utf-8")
        self.assertIn('output/verified-ui-prefab-plan.json', plan)
        self.assertIn('comparison.compare(studio, ripper)', plan)
        self.assertIn('rawObjectSha256', plan)
        self.assertIn('exactSourcePointerChecked', plan)
        self.assertIn('strictObjectSizeChecked', plan)
        self.assertIn('UNITYPY_EXACT_SOURCE_UNITY_VERSION', plan)
        self.assertIn('exactTwoBackendFieldAgreement', marker)
        self.assertIn('NOT an original XAPK component', marker)
        self.assertIn('/output/', ignore)
        self.assertIn('/unity-ui-viewer/Assets/LocalReconstruction/', ignore)


if __name__ == "__main__":
    unittest.main()
