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
            'verified.verifiedComponents != 1108',
            'verified.verifiedFieldValues != 7451',
            'verified.singleBackendExcludedFieldValues != 651',
            'visual.sourceBindings != 963',
            'originalImages.TryGetValue(item.componentPathId, out var owner)',
            'originalSprites.TryGetValue(item.componentPathId, out var pointer)',
            'owner.gameObjectPathId != item.gameObjectPathId',
            'owner.rectTransformPathId != item.rectTransformPathId',
            'owner.rawObjectSha256 != item.sourceObjectSha256',
            'pointer.sourceObjectSha256 != item.sourceObjectSha256',
            'pointer.spriteFile != item.spriteFile',
            'x.name == "m_Type"',
            'type[0].intValue != item.verifiedImageType',
            'Sha(File.ReadAllBytes(raw)) != Sha(File.ReadAllBytes(local))',
        ):
            self.assertIn(x, SRC)

    def test_does_not_require_deleted_3e_study_prefab(self):
        # A failed 3E Build rolls back the disposable study Prefab. Source
        # PNG/import metadata can still be audited without reconstructing 3E.
        self.assertNotIn('RootCanvasViewportPrefabs', SRC)
        self.assertNotIn('SOURCE_ROOT_CANVAS_PREVIEW.prefab', SRC)
        self.assertNotIn('Build and Audit 3E first', SRC)
        self.assertNotIn('GetComponentsInChildren<ManagedUiSourceEvidence>', SRC)
        self.assertIn('originalImages.TryGetValue(', SRC)
        self.assertIn('originalSprites.TryGetValue(', SRC)
        self.assertIn('verified-ui-prefab-plan.json', SRC)
        self.assertIn('verified-visual-preview-plan.json', SRC)

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
