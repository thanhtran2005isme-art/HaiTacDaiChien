"""REF04 source-tight Sprite UI study may not overwrite source Prefabs or art."""
from pathlib import Path
import unittest

ROOT=Path(__file__).resolve().parents[1]
SRC=(ROOT/"unity-ui-viewer/Assets/Editor/Ref04NativeBoundsPreviewImporter.cs"
     ).read_text(encoding="utf-8")

class NativeBoundsStudyContract(unittest.TestCase):
    def test_new_separate_destination_and_original_source_3c_prefab(self):
        for x in (
            'VerifiedManagedFieldPrefabs/REF04-home-crew_VERIFIED_FIELDS_STUDY.prefab',
            'Ref04NativeBoundsSprites',
            'Ref04NativeBoundsStudyPrefabs',
            'Ref04NativeBoundsStudyScenes',
            'Ref04SourceBounds',
            'NewPrefab',
            'NewScene',
            'SaveCurrentModifiedScenesIfUserWantsTo',
            'Refusing to overwrite.',
            'PrefabUtility.SaveAsPrefabAsset(go,NewPrefab)',
            'PrefabUtility.InstantiatePrefab(sourceAsset,scene)',
            'AssetDatabase.DeleteAsset(path)',
            '3C/3D/3E are untouched.',
        ):
            if x != "Ref04SourceBounds":
                self.assertIn(x,SRC)
        self.assertNotIn('SaveAsPrefabAsset(go,Study)',SRC)
        self.assertNotIn('File.Copy(PreviewFile(item.spriteFile),SourceFile',SRC)

    def test_independent_original_image_and_preview_sha_proof(self):
        for x in (
            'src.images.Length!=265',
            'plan.spriteFiles!=88',
            'plan.originalImageBindings!=265',
            'plan.nativeGeometryManifestFileSha256!=FileDigest(NativeGeometryPath)',
            'FileDigest(SourceFile(item.spriteFile)) != item.sourceExportPngSha256',
            'FileDigest(PreviewFile(item.spriteFile))!=item.previewPngSha256',
            'sourceImageComponentPathIds',
            'sourceMonoBehaviourPathId',
            'sourceObjectSha256!=item.sourceObjectSha256',
            'sourceRectTransformPathId!=item.rectTransformPathId',
            'sourceGameObjectPathId!=item.gameObjectPathId',
            'exactTwoBackendFieldAgreement',
            '(int)img.type!=item.verifiedImageType',
        ):
            self.assertIn(x,SRC)

    def test_verified_3c_all_image_set_is_not_confused_with_265_sprite_links(self):
        # The old importer incorrectly required notes.Count == 265. Some
        # source-verified Images have no Sprite PPtr and must remain in study.
        for token in (
            'private static Dictionary<int,Verified3cComponent> ExpectedAll3cImages(',
            'private static Dictionary<int,ManagedUiSourceEvidence> Check3cImageEvidence(',
            'list.Length<265',
            'actual.Count!=expected.Count',
            'Check3cImageEvidence(source3c,src)',
            'var notes=Check3cImageEvidence(go,original)',
            'var sourceImages=Check3cImageEvidence(source,proof)',
            'var previewImages=Check3cImageEvidence(preview,proof)',
            'sourceLinkedIds.Contains(sourceImage.Key)',
            '3C Image WITHOUT source Sprite binding was changed:',
            'REF04 265 exact Sprite-linked Image subset has',
        ):
            self.assertIn(token,SRC)
        self.assertNotIn('if(notes.Count!=265)',SRC)
        self.assertNotIn('REF04 3C Image inventory changed.',SRC)

    def test_saved_scene_preserves_source_child_layout_and_all_image_fields(self):
        for x in (
            'private static void AuditSaved()',
            'AuditSaved(); // Fail closed and roll back only NEW study outputs.',
            'sourceRects.Count!=copiedRects.Count',
            'a.GetSiblingIndex()!=b.GetSiblingIndex()',
            'Vector2.Distance(a.anchorMin,b.anchorMin)',
            'Vector2.Distance(a.anchorMax,b.anchorMax)',
            'Vector2.Distance(a.pivot,b.pivot)',
            'Vector2.Distance(a.sizeDelta,b.sizeDelta)',
            'Vector2.Distance(a.anchoredPosition,b.anchoredPosition)',
            'Vector3.Distance(a.localScale,b.localScale)',
            'Quaternion.Angle(a.localRotation,b.localRotation)',
            'rendered.type!=original.type',
            'rendered.preserveAspect!=original.preserveAspect',
            'rendered.fillMethod!=original.fillMethod',
            'rendered.fillOrigin!=original.fillOrigin',
            'rendered.fillClockwise!=original.fillClockwise',
            'rendered.fillAmount-original.fillAmount',
            'rendered.enabled!=original.enabled',
            'Source XAPK/REF04 - Audit source Tight Sprite bounds UI study',
        ):
            self.assertIn(x,SRC)

    def test_sprite_import_exact_native_9slice_and_safe_preview_label(self):
        for x in (
            'item.settingsRaw!=64',
            'item.originalBorder',
            'item.originalPixelsPerUnit',
            'importer.spritePixelsPerUnit=item.originalPixelsPerUnit',
            'importer.spriteImportMode=SpriteImportMode.Single',
            'importer.spriteBorder=new Vector4(',
            'sprite.rect.width,item.nativeSize[0]',
            'sourceOffset',
            'PREVIEW',
            'sourceBoundSprites=265',
            'runtime layout NOT authenticated',
        ):
            if x != 'sourceOffset':
                self.assertIn(x,SRC)
        self.assertNotIn('image.SetNativeSize(',SRC)
        self.assertNotIn('image.color =',SRC)


if __name__=="__main__":
    unittest.main()
