"""P6.2 must materially change a separate Unity Game View viewport, no source data."""
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
FIT = (ROOT / "unity-ui-viewer/Assets/Scripts/Ref04ClientViewportFit.cs").read_text(
    encoding="utf-8")
BUILD = (ROOT / "unity-ui-viewer/Assets/Editor/Ref04P62ClientSceneBuilder.cs").read_text(
    encoding="utf-8")
WORK = (ROOT / "unity-ui-viewer/Assets/Editor/Ref04P6OfflineWorkspace.cs").read_text(
    encoding="utf-8")


class Ref04NewClientPreviewContract(unittest.TestCase):
    def test_new_client_frame_fit_is_explicit_design_not_original_claim(self):
        for required in (
            "[ExecuteAlways]",
            "newProjectDesignNotOriginalXapk = true",
            "NEW_PROJECT_DESIGN",
            "NOT original CanvasScaler/SafeArea formula",
            "float currentAspect = (float)Screen.width / Screen.height;",
            "float match = currentAspect < designAspect ? 0f : 1f;",
            "CanvasScaler.ScaleMode.ScaleWithScreenSize",
            "CanvasScaler.ScreenMatchMode.MatchWidthOrHeight",
            "scaler.matchWidthOrHeight = match;",
            "observedWidth != Screen.width",
            "observedHeight != Screen.height",
        ):
            self.assertIn(required, FIT)

    def test_fit_does_not_change_source_children_or_invent_content(self):
        for forbidden in (
            "GetComponentsInChildren", "anchoredPosition =",
            "sizeDelta =", "transform.localScale =", "Text.text =",
            "Image.sprite =", "UnityWebRequest", "HttpClient", "Screen.safeArea =",
        ):
            self.assertNotIn(forbidden, FIT)

    def test_scene_is_copy_of_existing_study_and_never_overwrites_original(self):
        for required in (
            "ValidateSourceStudyForClientDesign();",
            "AssetDatabase.CopyAsset(OriginalStudy, ClientScene)",
            "Ref04OfflineClientScenes",
            "REF04-home-crew_NEW_CLIENT_FIT_STUDY.unity",
            "Ref04ClientViewportFit",
            "root.AddComponent<Ref04ClientViewportFit>();",
            "fit.newProjectDesignNotOriginalXapk = true;",
            "EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()",
            "EditorSceneManager.OpenScene(ClientScene, OpenSceneMode.Single)",
            "AssetDatabase.DeleteAsset(ClientScene);",
            "Refusing to overwrite",
            "NOT recovered",
        ):
            if required == "NOT recovered":
                self.assertIn("NOT a pixel-perfect restoration", BUILD)
            else:
                self.assertIn(required, BUILD)
        for forbidden in (
            "AssetDatabase.DeleteAsset(OriginalStudy)",
            "AssetDatabase.MoveAsset(OriginalStudy",
            "AssetDatabase.CopyAsset(ClientScene, OriginalStudy)",
            "PrefabUtility.SaveAsPrefabAsset(",
            "AssetDatabase.DeleteAsset(PreviewPrefab)",
        ):
            self.assertNotIn(forbidden, BUILD)

    def test_editor_exposes_real_new_scene_buttons_after_older_study(self):
        for required in (
            "public static void ValidateSourceStudyForClientDesign()",
            "VerifyGeneratedStudy(source);",
            "Ref04P62ClientSceneBuilder.ClientScene",
            "Ref04P62ClientSceneBuilder.Build();",
            "Ref04P62ClientSceneBuilder.Open();",
            "!hasCompleteStudy || hasClientScene",
            "6. Tao P6.2 NEW CLIENT viewport Study",
            "7. Mo P6.2 NEW CLIENT Scene",
            "NEW_PROJECT_DESIGN",
        ):
            self.assertIn(required, WORK)


if __name__ == "__main__":
    unittest.main()
