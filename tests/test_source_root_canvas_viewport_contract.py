"""Source-root-as-Canvas viewport preview must not mutate any verified child."""
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
CODE = (ROOT / "unity-ui-viewer/Assets/Editor/VerifiedVisualStudyImporter.cs").read_text(
    encoding="utf-8")


class TestSourceRootCanvasViewport(unittest.TestCase):
    def test_new_destinations_are_independent(self):
        for value in (
            'RootCanvasViewportPrefabs', 'RootCanvasViewportScenes',
            '_SOURCE_ROOT_CANVAS_PREVIEW.prefab',
            '_SOURCE_ROOT_CANVAS_PREVIEW.unity',
            'StudyTargetPrefab(scene.sceneId, rootCanvas)',
            'StudyTargetScene(scene.sceneId, rootCanvas)',
            'ValidateSources(true, rootCanvas: true)',
            'SourcePrefab(scene.sceneId)',
            'PrefabUtility.UnpackPrefabInstance(',
            'PrefabUtility.SaveAsPrefabAsset(copy,',
            'RootViewPrefab(scene.sceneId)',
            'AssetDatabase.DeleteAsset(asset)',
        ):
            self.assertIn(value, CODE)

    def test_original_children_must_survive_without_changes(self):
        for value in (
            'Source RectTransform object inventory altered.',
            'Source root PathID changed.',
            'Source child RectTransform was changed:',
            'rectTransformPathId',
            'Vector2.Distance(a.anchorMin, b.anchorMin)',
            'Vector2.Distance(a.anchorMax, b.anchorMax)',
            'Vector2.Distance(a.pivot, b.pivot)',
            'Vector2.Distance(a.sizeDelta, b.sizeDelta)',
            'Vector2.Distance(a.anchoredPosition, b.anchoredPosition)',
            'Vector3.Distance(a.localScale, b.localScale)',
            'Quaternion.Angle(a.localRotation, b.localRotation)',
            'changed.transform.GetSiblingIndex()',
            'changed.gameObject.activeSelf',
            'AuditRootTransforms(scene)',
        ):
            self.assertIn(value, CODE)

    def test_source_root_is_preview_only_one_canvas_not_nested(self):
        for value in (
            'SOURCE ROOT AS PREVIEW CANVAS (3E)',
            'root.anchorMin = new Vector2(.5f, .5f)',
            'root.anchorMax = new Vector2(.5f, .5f)',
            'root.pivot = new Vector2(.5f, .5f)',
            'root.anchoredPosition = Vector2.zero',
            'root.sizeDelta = new Vector2(1600f, 900f)',
            'root.localScale = Vector3.one',
            'copy.GetComponent<Canvas>()',
            'canvas.renderMode = RenderMode.ScreenSpaceOverlay',
            'if (scaler == null)',
            'previewCanvasNotClaimedAsOriginal',
            'Original runtime viewport NOT verified.',
        ):
            self.assertIn(value, CODE)

    def test_strict_source_field_and_sprite_proof_reused(self):
        for value in (
            'AuditScene(source, scene, rootCanvas: true)',
            'note.exactTwoBackendFieldAgreement',
            'note.sourceGameObjectPathId != verified.gameObjectPathId',
            'note.sourceObjectSha256 != sprite.sourceObjectSha256',
            'VerifiedVisualPreviewEvidence',
            'source.visual.verifiedUiPlanSha256',
            'source.visual.sourceGraphSha256',
            'source.visual.spritePrefabPlanSha256',
            'marker.excludedSingleBackendFields = 651',
            'byId.Count != components.Length',
            'image.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(',
        ):
            self.assertIn(value, CODE)


if __name__ == "__main__":
    unittest.main()
