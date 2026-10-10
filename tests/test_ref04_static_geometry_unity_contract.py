"""REF04 border/PPU repair must never modify XAPK pixels or UI layout."""
from pathlib import Path
import unittest

ROOT = Path(__file__).resolve().parents[1]
EDITOR = ROOT / "unity-ui-viewer/Assets/Editor/Ref04SourceSpriteGeometryAudit.cs"
SRC = EDITOR.read_text(encoding="utf-8")


class TestRef04NativeSpriteEditorContract(unittest.TestCase):
    def test_immutability_and_source_preflight(self):
        for x in (
            'ref04-static-image-geometry.json',
            'original-unity-graph.json',
            'verified-ui-prefab-plan.json',
            'verified-visual-preview-plan.json',
            'local-ui-components.json',
            'src.sourceBindings != 265',
            'sourceMonoBehaviourPathId',
            'sourceGameObjectPathId != item.gameObjectPathId',
            'sourceRectTransformPathId != item.rectTransformPathId',
            'sourceObjectSha256 != item.sourceObjectSha256',
            'exactTwoBackendFieldAgreement',
            '(int)image.type != item.verifiedImageType',
            'AssetDatabase.GetAssetPath(image.sprite)',
            'Sha(File.ReadAllBytes(raw)) != Sha(File.ReadAllBytes(local))',
        ):
            self.assertIn(x, SRC)

    def test_repairs_only_verified_local_sprite_import_settings(self):
        self.assertIn('item.importer.spriteBorder=item.border;', SRC)
        self.assertIn('item.importer.spritePixelsPerUnit=item.ppu;', SRC)
        self.assertIn('item.importer.SaveAndReimport();', SRC)
        self.assertIn('item.importer.spriteBorder=change.border;', SRC)
        self.assertIn('item.importer.spritePixelsPerUnit=change.ppu;', SRC)
        self.assertIn('No original border/PPU mismatches;', SRC)
        self.assertNotIn('image.SetNativeSize(', SRC)
        self.assertNotIn('image.color =', SRC)
        self.assertNotIn('image.type =', SRC)
        self.assertNotIn('root.sizeDelta =', SRC)
        self.assertNotIn('root.localScale =', SRC)
        self.assertNotIn('PrefabUtility.SaveAsPrefabAsset(', SRC)
        self.assertNotIn('File.Copy(', SRC)

    def test_conflicting_native_sprite_geometry_is_rejected(self):
        self.assertIn('Conflicting source geometry for one Sprite:', SRC)
        self.assertIn('Untrusted native Sprite geometry.', SRC)
        self.assertIn('not changing any asset', SRC)
        self.assertIn('Original PNG bytes untouched', SRC)
        self.assertIn('not proven', SRC)


if __name__ == "__main__":
    unittest.main()
