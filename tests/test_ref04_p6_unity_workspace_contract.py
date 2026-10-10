"""P6 Unity Editor source-only workspace safety contract (static; not Editor PlayMode)."""
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
EDITOR = ROOT / "unity-ui-viewer/Assets/Editor/Ref04P6OfflineWorkspace.cs"
S = EDITOR.read_text(encoding="utf-8")


class TestP6UnityOfflineWorkspaceContract(unittest.TestCase):
    def test_menu_opens_real_unity_editor_study_workflow(self):
        for token in (
            'P6 - REF04 offline Unity workspace',
            'public sealed class Ref04P6OfflineWorkspace : EditorWindow',
            'VerifiedManagedUiPrefabImporter.Build();',
            'Ref04NativeBoundsPreviewImporter.Build();',
            'EditorSceneManager.OpenScene(PreviewScene);',
            'VerifyGeneratedStudy(doc);',
            'SaveCurrentModifiedScenesIfUserWantsTo()',
        ):
            self.assertIn(token, S)

    def test_p6_p5_source_reports_bound_to_private_sha_never_web_or_adb(self):
        for token in (
            'ref04-p6-offline-ui-gaps.json',
            'ref04-p5-cross-phase-source-integrity.json',
            'ref04-full-source-inventory.json',
            'ref04-static-image-geometry.json',
            'p5ReportSha256',
            'inventoryReportSha256',
            'geometryReportSha256',
            'Digest(OutputPath("ref04-p5-cross-phase-source-integrity.json"))',
            'Digest(OutputPath("ref04-full-source-inventory.json"))',
            'Digest(OutputPath("ref04-static-image-geometry.json"))',
            'spriteGeometryAudit',
            'ValidSha(item.originalImageObjectSha256)',
            'ids.Add(item.originalImageComponentPathId)',
        ):
            self.assertIn(token, S)
        for forbidden in (
            'HttpClient', 'UnityWebRequest', 'adb ', 'Screen.safeArea =',
            'new Vector2(1600, 900);', 'ref04-p6-runtime-evidence.json'
        ):
            self.assertNotIn(forbidden, S)

    def test_strict_original_image_coverage_with_sprite_less_fields(self):
        for token in (
            'report.counts.spriteLinkedImages == 265',
            'report.counts.allDualVerifiedImages == 299',
            'report.counts.verifiedImagesWithoutSourceSprite == 34',
            'report.counts.layoutSerializedFieldsTwoSchemaVerified == 168',
            'report.counts.originalTextSourceComponents == 62',
            'report.counts.sourceGeometryVerified +',
            'report.counts.sourceGeometryBlocked == 265',
            'doc.counts.sourceGeometryVerified == 265',
            'doc.counts.sourceGeometryBlocked == 0',
            'notes.Length == 299',
            'note.sourceBoundSprites == 265',
            'byId.Count - source.spriteGeometryAudit.Length == 34',
            'fromSource.Count == 503 && inStudy.Count == 503',
            'src.Key == rootId) continue',
            'Vector2.Distance(a.anchoredPosition, b.anchoredPosition)',
            'a.GetSiblingIndex() == b.GetSiblingIndex()',
            'a.gameObject.activeSelf == b.gameObject.activeSelf',
            'item.GetComponent<Image>().sprite != null',
            'item.sourceObjectSha256 ==',
            'src.originalImageObjectSha256',
        ):
            self.assertIn(token, S)

    def test_3c_builder_must_never_overwrite_existing_any_scene_studies(self):
        for token in (
            'private static bool AnyExistingThreeCOutput()',
            'AssetDatabase.FindAssets("t:Prefab"',
            'AssetDatabase.FindAssets("t:Scene"',
            'any3c || EditorApplication.isPlaying',
            'Require(!AnyExistingThreeCOutput(),',
            '3C study outputs đã tồn tại, từ chối ghi đè',
            'using (new EditorGUI.DisabledScope(',
            '!has3c || !nativeReady || hasStudy',
            'Không chạm source art hoặc gameplay',
        ):
            self.assertIn(token, S)
        for bad in (
            'File.Copy(', 'AssetDatabase.DeleteAsset(', 'SetNativeSize(',
            'img.sprite =', 'img.color =', 'PrefabUtility.SaveAsPrefabAsset('
        ):
            self.assertNotIn(bad, S)

    def test_existing_study_is_explained_and_partial_outputs_fail_closed(self):
        for token in (
            'bool hasStudyPrefab =',
            'bool hasStudyScene =',
            'bool hasAnyStudy = hasStudyPrefab || hasStudyScene;',
            'bool hasCompleteStudy = hasStudyPrefab && hasStudyScene;',
            'if (hasCompleteStudy)',
            'else if (hasAnyStudy)',
            'REF04 Study: đã có Prefab + Scene.',
            'REF04 Study CHƯA ĐẦY ĐỦ:',
            'chỉ có Prefab, thiếu Scene.',
            'chỉ có Scene, thiếu Prefab.',
            '!has3c || !nativeReady || hasAnyStudy',
            '!hasCompleteStudy || EditorApplication.isPlaying',
            'Study đã tồn tại; từ chối ghi đè. Kiểm tra bước 4.',
        ):
            self.assertIn(token, S)
        self.assertNotIn('bool hasStudy =', S)

    def test_runtime_uncertainty_and_design_separation_is_visible_to_user(self):
        for token in (
            'Canvas 1600×900 của Study chỉ là PREVIEW',
            'Canvas/viewport gốc vẫn BLOCKED',
            'NEW_PROJECT_DESIGN',
            'Backend mới là thiết kế riêng',
            'runtime HUD.',
            'native Sprite',
        ):
            self.assertIn(token, S)
        self.assertIn(
            'note.limitations.Contains("NOT authenticated")', S)


if __name__ == "__main__":
    unittest.main()
