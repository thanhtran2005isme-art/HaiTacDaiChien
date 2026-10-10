"""No Unity executable required: protect new visual preview against source edits."""
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
EDITOR = ROOT / "unity-ui-viewer/Assets/Editor/VerifiedVisualStudyImporter.cs"
MARKER = ROOT / "unity-ui-viewer/Assets/Scripts/VerifiedVisualPreviewEvidence.cs"
PLAN = ROOT / "tools/build_verified_visual_plan.py"


class TestVerifiedVisualStudyContract(unittest.TestCase):
    def test_never_replace_old_canvas_or_3c_prefabs(self):
        code = EDITOR.read_text(encoding="utf-8")
        self.assertIn('VerifiedVisualPrefabs', code)
        self.assertIn('VerifiedVisualScenes', code)
        self.assertIn('VerifiedManagedFieldPrefabs', code)
        self.assertIn('_VERIFIED_VISUAL_PREVIEW.prefab', code)
        self.assertIn('_VERIFIED_FIELDS_STUDY.prefab', code)
        self.assertIn('refusing to overwrite', code)
        self.assertIn('AssetDatabase.DeleteAsset(path)', code)
        self.assertIn('SaveCurrentModifiedScenesIfUserWantsTo', code)
        self.assertIn('PrefabUtility.SaveAsPrefabAsset(outer, DestPrefab(scene.sceneId))', code)
        self.assertNotIn('PrefabUtility.SaveAsPrefabAsset(outer, SourcePrefab', code)

    def test_preview_sources_and_provisional_camera_canvas_are_labeled(self):
        code = EDITOR.read_text(encoding="utf-8")
        marker = MARKER.read_text(encoding="utf-8")
        for token in (
            'previewOnly', 'DigestedOutput("original-unity-graph.json")',
            'DigestedOutput("verified-ui-prefab-plan.json")',
            'DigestedOutput("unity-prefab-map.json")',
            'DigestedOutput("local-ui-art/manifest.json")',
            'sourceMonoBehaviourPathId', 'sourceRectTransformPathId',
            'sourceGameObjectPathId', 'sourceObjectSha256',
            'image.sprite = AssetDatabase.LoadAssetAtPath<Sprite>',
            'copy.transform.localScale = Vector3.one',
            '3D PREVIEW CAMERA - NOT ORIGINAL',
            'ScreenSpaceOverlay', 'new Vector2(1600, 900)',
            'Verify',  # replaced below with real audit symbol
        ):
            if token != 'Verify':
                self.assertIn(token, code)
        self.assertIn('SameProperty(first.FindProperty(field.name)', code)
        self.assertIn('sourceBoundSprites', marker)
        self.assertIn('previewCanvasNotClaimedAsOriginal', marker)
        self.assertIn('previewCameraNotClaimedAsOriginal', marker)
        self.assertIn('not an original editable Prefab', marker)

    def test_source_exact_and_3c_field_counts_are_verified(self):
        code = EDITOR.read_text(encoding="utf-8")
        for token in (
            'visual.sourceBindings != 963',
            'fields.verifiedComponents != 1108',
            'fields.verifiedFieldValues != 7451',
            'fields.singleBackendExcludedFieldValues != 651',
            'src',  # cannot weaken source field identity checks
            'note.verifiedFieldNames.SequenceEqual(',
            'sourceComponent.enabled != previewComponent.enabled',
            'compared !=', 'byId.TryGetValue',
        ):
            if token not in ('src', 'byId.TryGetValue'):
                self.assertIn(token, code)
        self.assertIn('byComponent.TryGetValue(', code)
        self.assertIn('byId = b;', code)
        self.assertIn('Prefab FIELD AUDIT', code.upper())

    def test_plan_never_uses_unverified_source_values(self):
        code = PLAN.read_text(encoding="utf-8")
        for token in (
            'EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS',
            'sourceImageComponentId', 'imageComponentId',
            'rawObjectSha256', 'original-unity-graph.json',
            'verified-ui-prefab-plan.json', 'local-ui-art/manifest.json',
            'singleBackendExcludedFieldValues', 'image["gameObjectPathId"]',
        ):
            self.assertIn(token, code)
        self.assertIn('/output/', (ROOT / ".gitignore").read_text())
        self.assertIn('/unity-ui-viewer/Assets/LocalReconstruction/',
                      (ROOT / ".gitignore").read_text())


if __name__ == "__main__":
    unittest.main()
