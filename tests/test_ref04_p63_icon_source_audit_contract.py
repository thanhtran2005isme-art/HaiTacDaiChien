"""P6.3 Unity Scene source icon audit is a READ-ONLY evidence acquisition gate."""
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
AUDIT = (ROOT / "unity-ui-viewer/Assets/Editor/Ref04P63IconLayoutAudit.cs"
         ).read_text(encoding="utf-8")
WINDOW = (ROOT / "unity-ui-viewer/Assets/Editor/Ref04P6OfflineWorkspace.cs"
          ).read_text(encoding="utf-8")


class P63SourceIconEvidence(unittest.TestCase):
    def test_actual_299_source_image_provenance_collected(self):
        for token in (
            "Ref04P6OfflineWorkspace.ValidateSourceStudyForClientDesign();",
            "SceneManager.GetActiveScene();",
            "NEW_CLIENT_FIT_STUDY.unity",
            "GetComponentsInChildren<Image>(true)",
            "GetComponent<ManagedUiSourceEvidence>()",
            'note.originalClassName == "UnityEngine.UI.Image"',
            "note.exactTwoBackendFieldAgreement",
            "sourceMonoBehaviourPathId",
            "sourceGameObjectPathId",
            "sourceRectTransformPathId",
            "image.sprite.name",
            "GetWorldCorners(corners)",
            "OriginalSerializedEvidence",
            "root.GetComponentsInChildren<SpineReferenceEvidence>",
            "report.managedSourceImageProofs != 299",
        ):
            self.assertIn(token, AUDIT)

    def test_hero_icons_are_not_relocated_without_origin_classification(self):
        for token in (
            "sourceRootGroup = group",
            "TopGroup(trans, root)",
            "HierarchyPath(trans, root)",
            "sourceSpineEvidenceInAncestor",
            "sourceSpineEvidenceInDescendants",
            "A round fist icon is not identified as hero/HUD solely",
            "World corners are Unity CLIENT-STUDY observations",
            "No icons moved or renamed",
            "originalGameRuntimeLayoutProven = \"NO\"",
            "modifiesUnityAssets = false",
            "changesOriginalSourceEvidence = false",
        ):
            self.assertIn(token, AUDIT)
        for forbidden in (
            "anchoredPosition =", "SetParent(", "SetSiblingIndex(",
            "DestroyImmediate(", "AddComponent<Image>(",
            "AssetDatabase.DeleteAsset(", "AssetDatabase.CopyAsset(",
            "Image.sprite =", "Image.enabled =",
        ):
            self.assertNotIn(forbidden, AUDIT)

    def test_private_json_never_committed_and_no_overwrite(self):
        for token in (
            "ref04-p63-client-icon-layout-audit.json",
            'Path.Combine(\n                RepositoryRoot, "output", OutputName)',
            "if (File.Exists(destination))",
            "preserving previous evidence",
            "File.WriteAllText(destination",
            "new UTF8Encoding(false)",
        ):
            self.assertIn(token, AUDIT)
        gitignore = (ROOT / ".gitignore").read_text(encoding="utf-8")
        self.assertIn("/output/", gitignore)

    def test_workspace_button_exposes_report_not_fake_ui_fix(self):
        for token in (
            "P6.3 - Xac dinh nguon icon va o nhan vat",
            "9. Kiem tra icon, HUD va PlayerHeroes",
            "Ref04P63IconLayoutAudit.BuildReport();",
            "chua co",
        ):
            if token == "chua co":
                self.assertIn("chua co", WINDOW.lower())
            else:
                self.assertIn(token, WINDOW)


if __name__ == "__main__":
    unittest.main()
