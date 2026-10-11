"""REF04 existing P6.2 client background-cover safety contract.

Static contract only: no attempt to claim Unity Editor/real source visual pass.
"""
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
COVER = (ROOT / "unity-ui-viewer/Assets/Scripts/Ref04ClientBackgroundCover.cs"
         ).read_text(encoding="utf-8")
BUILDER = (ROOT / "unity-ui-viewer/Assets/Editor/Ref04P62ClientSceneBuilder.cs"
           ).read_text(encoding="utf-8")
WORKSPACE = (ROOT / "unity-ui-viewer/Assets/Editor/Ref04P6OfflineWorkspace.cs"
             ).read_text(encoding="utf-8")


class BackgroundCoverSafety(unittest.TestCase):
    def test_cover_measures_actual_source_sprite_image_not_guess(self):
        for token in (
            "NEW_PROJECT_DESIGN",
            "sourceBackground.GetComponentsInChildren<Image>(true)",
            "item.sprite == null",
            "best.rectTransform.GetWorldCorners(corner);",
            "float cover = Mathf.Max((float)Screen.width / width,",
            "(float)Screen.height / height);",
            "sourceBackground.localScale = baselineBackgroundScale * cover;",
            "float.IsNaN(cover)",
            "background cover",
        ):
            self.assertIn(token, COVER.lower() if token == "background cover" else COVER)
        for forbidden in (
            "Image.sprite =", "Text.text =", "anchoredPosition =",
            "sizeDelta =", "new Sprite", "CreatePrimitive", "SetNativeSize(",
            "UnityWebRequest", "HttpClient",
        ):
            self.assertNotIn(forbidden, COVER)

    def test_client_scene_has_backup_and_source_identity_preflight(self):
        for token in (
            "Ref04P6OfflineWorkspace.ValidateSourceStudyForClientDesign();",
            "private const string BackupScene",
            "REF04-home-crew_NEW_CLIENT_FIT_BEFORE_BACKGROUND.unity",
            "AssetDatabase.CopyAsset(ClientScene, BackupScene)",
            "AssetDatabase.LoadAssetAtPath<SceneAsset>(BackupScene) != null",
            "source-proven UI root",
            'roots[0].transform.Find("Background")',
            "background.GetComponent<OriginalSerializedEvidence>()",
            "background.GetComponentsInChildren<Image>(true)",
            "roots[0].AddComponent<Ref04ClientBackgroundCover>();",
            "cover.Initialize(background.GetComponent<RectTransform>());",
            "EditorSceneManager.SaveScene(scene)",
            "AssetDatabase.CopyAsset(BackupScene, ClientScene)",
        ):
            self.assertIn(token, BUILDER)
        for forbidden in (
            "AssetDatabase.DeleteAsset(OriginalStudy)",
            "AssetDatabase.CopyAsset(ClientScene, OriginalStudy)",
            "AssetDatabase.DeleteAsset(PreviewPrefab)",
        ):
            self.assertNotIn(forbidden, BUILDER)

    def test_existing_client_study_can_be_repaired_without_another_phase(self):
        for token in (
            "8. Sua nen P6.2 - luu ban sao truoc",
            "Ref04P62ClientSceneBuilder.FixClientBackground();",
            "!hasClientScene || EditorApplication.isPlaying",
            "Khong sua Scene/Preset/Sprite goc",
        ):
            self.assertIn(token, WORKSPACE)


if __name__ == "__main__":
    unittest.main()
