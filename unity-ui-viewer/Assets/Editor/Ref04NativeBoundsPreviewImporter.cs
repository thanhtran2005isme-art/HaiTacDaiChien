#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HaiTac.OfflineViewer;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// REF04 preview-only reconstruction using native unpacked Tight Sprite
    /// logical pixel bounds from XAPK m_Rect + m_RD.textureRectOffset.
    /// Does NOT overwrite the original 3C, 3D, 3E Prefabs or art.
    /// It is NOT a recreation of the original runtime HUD/Canvas.
    /// </summary>
    public static class Ref04NativeBoundsPreviewImporter
    {
        private const string Id = "REF04-home-crew";
        private const string Local = "Assets/LocalReconstruction";
        private const string Study = Local +
            "/VerifiedManagedFieldPrefabs/REF04-home-crew_VERIFIED_FIELDS_STUDY.prefab";
        private const string Pixels = Local + "/Ref04NativeBoundsSprites";
        private const string Prefabs = Local + "/Ref04NativeBoundsStudyPrefabs";
        private const string Scenes = Local + "/Ref04NativeBoundsStudyScenes";
        private const string SourceSprites = Local + "/Sprites";
        private const string NewPrefab = Prefabs +
            "/REF04-home-crew_NATIVE_BOUNDS_STUDY.prefab";
        private const string NewScene = Scenes +
            "/REF04-home-crew_NATIVE_BOUNDS_STUDY.unity";
        private const string PreviewStatus =
            "UNPACKED_TIGHT_SOURCE_LOGICAL_BOUNDS_PREVIEW";

        [Serializable] private sealed class Plan
        {
            public int schemaVersion;
            public string classification;
            public string nativeGeometryManifestFileSha256;
            public int originalImageBindings;
            public int spriteFiles;
            public bool previewOnly;
            public bool sourceVerifiedPixelPlacement;
            public PlanSprite[] sprites;
        }
        [Serializable] private sealed class PlanSprite
        {
            public string spriteFile;
            public int[] sourceImageComponentPathIds;
            public string sourceExportPngSha256;
            public string previewPngSha256;
            public string status;
            public int[] nativeSize;
            public float[] originalBorder;
            public float originalPixelsPerUnit;
            public int[] placementInPngTopLeft;
            public int settingsRaw;
        }
        [Serializable] private sealed class Source
        {
            public int schemaVersion;
            public string classification;
            public string sceneId;
            public int sourceBindings;
            public string sourceGraphSha256;
            public string verifiedUiPlanSha256;
            public SourceImage[] images;
        }
        [Serializable] private sealed class SourceImage
        {
            public int componentPathId;
            public int rectTransformPathId;
            public int gameObjectPathId;
            public string sourceObjectSha256;
            public string spriteFile;
            public int verifiedImageType;
        }
        [Serializable] private sealed class Verified3cPlan
        {
            public int verifiedComponents;
            public int verifiedFieldValues;
            public string classification;
            public Verified3cScene[] scenes;
        }
        [Serializable] private sealed class Verified3cScene
        {
            public string sceneId;
            public Verified3cComponent[] components;
        }
        [Serializable] private sealed class Verified3cComponent
        {
            public string className;
            public int componentPathId;
            public int rectTransformPathId;
            public int gameObjectPathId;
            public string rawObjectSha256;
        }

        // 265 source Image->Sprite pointers are NOT the entire 3C Image class:
        // additional 3C Images may have no serialized Sprite reference. Never
        // assert every source Image must be Sprite-bound or drop unbound Images.
        private static Dictionary<int,Verified3cComponent> ExpectedAll3cImages(
            string expectedPlanSha256)
        {
            string path=Path.Combine(Repo,"output","verified-ui-prefab-plan.json");
            if(FileDigest(path)!=expectedPlanSha256)
                throw new InvalidDataException(
                    "Original 3C Image verification plan SHA256 differs.");
            var plan=JsonUtility.FromJson<Verified3cPlan>(
                File.ReadAllText(path,Encoding.UTF8));
            if(plan==null ||
                plan.classification!="TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS" ||
                plan.verifiedComponents!=1108 || plan.verifiedFieldValues!=7451 ||
                plan.scenes==null)
                throw new InvalidDataException(
                    "Original 3C source Image component inventory not verified.");
            var ref04=plan.scenes.Where(v=>v.sceneId==Id).ToArray();
            if(ref04.Length!=1 || ref04[0].components==null)
                throw new InvalidDataException("REF04 3C source scene missing.");
            var list=ref04[0].components.Where(v=>
                v.className=="UnityEngine.UI.Image").ToArray();
            if(list.Length<265)
                throw new InvalidDataException(
                    "REF04 3C total Image count is smaller than 265 linked Sprites: "+
                    list.Length);
            return list.ToDictionary(v=>v.componentPathId);
        }

        private static Dictionary<int,ManagedUiSourceEvidence> Check3cImageEvidence(
            GameObject prefab,Source original)
        {
            var expected=ExpectedAll3cImages(original.verifiedUiPlanSha256);
            var actual=prefab.GetComponentsInChildren<ManagedUiSourceEvidence>(true)
                .Where(n=>n.originalClassName=="UnityEngine.UI.Image")
                .ToDictionary(n=>n.sourceMonoBehaviourPathId);
            if(actual.Count!=expected.Count)
                throw new InvalidDataException(
                    "REF04 source 3C all-Image inventory mismatch (includes Image " +
                    "components WITHOUT a Sprite): actual="+actual.Count+
                    ", original plan="+expected.Count);
            foreach(var record in expected)
            {
                if(!actual.TryGetValue(record.Key,out var note) ||
                    note.sourceSceneId!=Id ||
                    note.sourceGameObjectPathId!=record.Value.gameObjectPathId ||
                    note.sourceRectTransformPathId!=record.Value.rectTransformPathId ||
                    note.sourceObjectSha256!=record.Value.rawObjectSha256 ||
                    !note.exactTwoBackendFieldAgreement ||
                    note.GetComponent<Image>()==null)
                    throw new InvalidDataException(
                        "REF04 3C source Image evidence missing/mismatched for " +
                        "Component PathID "+record.Key);
            }
            var seen=new HashSet<int>();
            foreach(var image in original.images)
                if(!seen.Add(image.componentPathId) ||
                    !actual.TryGetValue(image.componentPathId,out var note) ||
                    note.sourceGameObjectPathId!=image.gameObjectPathId ||
                    note.sourceRectTransformPathId!=image.rectTransformPathId ||
                    note.sourceObjectSha256!=image.sourceObjectSha256)
                    throw new InvalidDataException(
                        "REF04 265 exact Sprite-linked Image subset has " +
                        "missing/mismatched Component PathID "+image.componentPathId);
            if(seen.Count!=265)
                throw new InvalidDataException(
                    "REF04 265 source Sprite bindings are not unique.");
            return actual;
        }

        private static string Repo => Path.GetFullPath(
            Path.Combine(Application.dataPath, "..", ".."));
        private static string SourceFile(string file) => Path.Combine(
            Repo,"output","local-ui-art",file);
        private static string PreviewFile(string file) => Path.Combine(
            Repo,"output","ref04-source-logical-sprite-previews",file);
        private static string SourceSpritePath(string file) =>
            SourceSprites + "/" + file;
        private static string PreviewSpritePath(string file) =>
            Pixels + "/" + file;
        private static string NativePlanPath => Path.Combine(
            Repo,"output","ref04-source-logical-sprite-previews","manifest.json");
        private static string NativeGeometryPath => Path.Combine(
            Repo,"output","ref04-static-image-geometry.json");
        private static string Digest(byte[] data)
        {
            using (var sha = SHA256.Create())
                return BitConverter.ToString(sha.ComputeHash(data))
                    .Replace("-","").ToLowerInvariant();
        }
        private static string FileDigest(string file) =>
            Digest(File.ReadAllBytes(file));
        private static bool SafePng(string name) =>
            !string.IsNullOrEmpty(name) &&
            System.Text.RegularExpressions.Regex.IsMatch(
                name, @"^[0-9a-f]{32}\.png$");
        private static bool Eq(float a,float b) => Mathf.Abs(a-b)<=.001f;
        private static bool Eq(Vector4 a,Vector4 b) =>
            Eq(a.x,b.x) && Eq(a.y,b.y) && Eq(a.z,b.z) && Eq(a.w,b.w);
        private static void EnsureFolder(string parent,string child)
        {
            if (!AssetDatabase.IsValidFolder(parent+"/"+child))
                AssetDatabase.CreateFolder(parent,child);
        }

        private static (Plan, Source, Dictionary<string,PlanSprite>) Preflight()
        {
            var plan = JsonUtility.FromJson<Plan>(
                File.ReadAllText(NativePlanPath,Encoding.UTF8));
            var src = JsonUtility.FromJson<Source>(
                File.ReadAllText(NativeGeometryPath,Encoding.UTF8));
            if (plan==null || src==null || plan.schemaVersion!=1 ||
                plan.classification != "REF04_SOURCE_TIGHT_MESH_LOGICAL_BOUNDS_PREVIEW" ||
                !plan.previewOnly || plan.sourceVerifiedPixelPlacement ||
                plan.originalImageBindings!=265 || plan.spriteFiles!=88 ||
                plan.sprites==null || plan.sprites.Length!=88 ||
                src.schemaVersion!=1 ||
                src.classification!="REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY" ||
                src.sceneId!=Id || src.sourceBindings!=265 ||
                src.images==null || src.images.Length!=265 ||
                plan.nativeGeometryManifestFileSha256!=FileDigest(NativeGeometryPath) ||
                src.sourceGraphSha256!=FileDigest(
                    Path.Combine(Repo,"output","original-unity-graph.json")) ||
                src.verifiedUiPlanSha256!=FileDigest(
                    Path.Combine(Repo,"output","verified-ui-prefab-plan.json")))
                throw new InvalidDataException(
                    "REF04 original XAPK geometry/3C manifests or SHA256 changed.");
            var grouped=src.images.GroupBy(i=>i.spriteFile)
                .ToDictionary(g=>g.Key,g=>g.Select(i=>i.componentPathId)
                    .OrderBy(v=>v).ToArray());
            var result=new Dictionary<string,PlanSprite>();
            int changed=0;
            var copiedOwners=new HashSet<int>();
            foreach(var item in plan.sprites)
            {
                if(item==null || !SafePng(item.spriteFile) ||
                    !result.TryAdd(item.spriteFile,item) ||
                    !grouped.TryGetValue(item.spriteFile,out var owners) ||
                    item.sourceImageComponentPathIds==null ||
                    !owners.SequenceEqual(
                        item.sourceImageComponentPathIds.OrderBy(v=>v)) ||
                    FileDigest(SourceFile(item.spriteFile)) != item.sourceExportPngSha256)
                    throw new InvalidDataException(
                        "Source Sprite/Image owner or exported bytes cannot be proved.");
                if(item.status!=PreviewStatus) continue;
                if(item.settingsRaw!=64 || item.nativeSize==null ||
                    item.nativeSize.Length!=2 ||
                    item.nativeSize.Any(v=>v<=0 || v>16384) ||
                    item.originalBorder==null || item.originalBorder.Length!=4 ||
                    item.originalBorder.Any(v=>float.IsNaN(v) ||
                        float.IsInfinity(v) || v<0) ||
                    item.originalPixelsPerUnit<=0 ||
                    float.IsNaN(item.originalPixelsPerUnit) ||
                    item.originalBorder[0]+item.originalBorder[2]>item.nativeSize[0] ||
                    item.originalBorder[1]+item.originalBorder[3]>item.nativeSize[1] ||
                    item.placementInPngTopLeft==null ||
                    item.placementInPngTopLeft.Length!=2 ||
                    item.placementInPngTopLeft.Any(v=>v<0) ||
                    string.IsNullOrEmpty(item.previewPngSha256) ||
                    FileDigest(PreviewFile(item.spriteFile))!=item.previewPngSha256)
                    throw new InvalidDataException(
                        "Original unpacked Tight Sprite pixel placement not proven.");
                changed++;
                foreach(int id in owners)
                    if (!copiedOwners.Add(id))
                        throw new InvalidDataException("Duplicate preview Image PathID");
            }
            if(result.Count!=88 || grouped.Count!=88 ||
                changed<20 || changed>22 || copiedOwners.Count<45)
                throw new InvalidDataException(
                    "REF04 source-derived Sprite/owner inventory unexpected.");
            var source3c=AssetDatabase.LoadAssetAtPath<GameObject>(Study);
            if (source3c==null)
                throw new FileNotFoundException(
                    "Phase 3C original managed-field Study Prefab missing. " +
                    "Do not use an old 3E Preview as the source.");
            Check3cImageEvidence(source3c,src);
            if (AssetDatabase.LoadAssetAtPath<GameObject>(NewPrefab)!=null ||
                File.Exists(NewScene))
                throw new IOException(
                    "REF04 native-bounds study already exists. Refusing to overwrite.");
            foreach(var item in result.Values.Where(v=>v.status==PreviewStatus))
                if(File.Exists(Path.Combine(Application.dataPath,
                    "LocalReconstruction","Ref04NativeBoundsSprites",
                    item.spriteFile)))
                    throw new IOException("Destination preview Sprite exists: " +
                        item.spriteFile);
            return (plan,src,result);
        }

        private static Sprite ImportSourceProvenPreview(
            PlanSprite item,List<string> created)
        {
            var path=PreviewSpritePath(item.spriteFile);
            var destination=Path.Combine(Application.dataPath,
                "LocalReconstruction","Ref04NativeBoundsSprites",
                item.spriteFile);
            File.Copy(PreviewFile(item.spriteFile),destination,false);
            created.Add(path);
            AssetDatabase.ImportAsset(path,ImportAssetOptions.ForceUpdate);
            var importer=AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer==null)
                throw new InvalidDataException("Cannot import source preview Sprite.");
            importer.textureType=TextureImporterType.Sprite;
            importer.spriteImportMode=SpriteImportMode.Single;
            importer.spritePixelsPerUnit=item.originalPixelsPerUnit;
            importer.spriteBorder=new Vector4(
                item.originalBorder[0],item.originalBorder[1],
                item.originalBorder[2],item.originalBorder[3]);
            importer.SaveAndReimport();
            var sprite=AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if(sprite==null ||
                !Eq(sprite.rect.width,item.nativeSize[0]) ||
                !Eq(sprite.rect.height,item.nativeSize[1]) ||
                !Eq(sprite.pixelsPerUnit,item.originalPixelsPerUnit) ||
                !Eq(sprite.border,importer.spriteBorder))
                throw new InvalidDataException(
                    "Native logical bounds preview Sprite import mismatch: "+
                    item.spriteFile);
            return sprite;
        }

        private static void BuildContent(Plan plan,Source original,
            Dictionary<string,PlanSprite> mapping)
        {
            var sourceAsset=AssetDatabase.LoadAssetAtPath<GameObject>(Study);
            var scene=EditorSceneManager.NewScene(
                NewSceneSetup.EmptyScene,NewSceneMode.Single);
            var go=PrefabUtility.InstantiatePrefab(sourceAsset,scene) as GameObject;
            if(go==null)
                throw new InvalidDataException("3C original Prefab instantiate failed.");
            PrefabUtility.UnpackPrefabInstance(go,
                PrefabUnpackMode.Completely,InteractionMode.AutomatedAction);
            var root=go.GetComponent<RectTransform>();
            if(root==null) throw new InvalidDataException("3C original UI root missing.");
            var originalRootScale=sourceAsset.GetComponent<RectTransform>().localScale;
            root.anchorMin=new Vector2(.5f,.5f);
            root.anchorMax=new Vector2(.5f,.5f);
            root.pivot=new Vector2(.5f,.5f);
            root.anchoredPosition=Vector2.zero;
            root.sizeDelta=new Vector2(1600,900);
            root.localScale=Vector3.one;
            var canvas=go.GetComponent<Canvas>();
            if(canvas==null)canvas=go.AddComponent<Canvas>();
            canvas.renderMode=RenderMode.ScreenSpaceOverlay;
            var scaler=go.GetComponent<CanvasScaler>();
            if(scaler==null)
            {
                scaler=go.AddComponent<CanvasScaler>();
                scaler.uiScaleMode=CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution=new Vector2(1600,900);
            }
            if(go.GetComponent<GraphicRaycaster>()==null)
                go.AddComponent<GraphicRaycaster>();
            // The full verified 3C Image inventory can be greater than 265.
            // Validate ALL source Image identities, then bind only the 265
            // Image components with exact original Sprite PPtrs.
            var notes=Check3cImageEvidence(go,original);
            foreach(var item in original.images)
            {
                if(!notes.TryGetValue(item.componentPathId,out var evidence) ||
                    evidence.sourceSceneId!=Id ||
                    evidence.sourceGameObjectPathId!=item.gameObjectPathId ||
                    evidence.sourceRectTransformPathId!=item.rectTransformPathId ||
                    evidence.sourceObjectSha256!=item.sourceObjectSha256 ||
                    !evidence.exactTwoBackendFieldAgreement)
                    throw new InvalidDataException(
                        "Original Image Component PathID evidence changed.");
                var img=evidence.GetComponent<Image>();
                if(img==null || (int)img.type!=item.verifiedImageType)
                    throw new InvalidDataException(
                        "Original XAPK managed Image.Type changed.");
                var path=mapping[item.spriteFile].status==PreviewStatus ?
                    PreviewSpritePath(item.spriteFile) :
                    SourceSpritePath(item.spriteFile);
                img.sprite=AssetDatabase.LoadAssetAtPath<Sprite>(path);
                if(img.sprite==null)
                    throw new FileNotFoundException(
                        "Original or preview Sprite source missing: "+path);
            }
            var marker=go.AddComponent<VerifiedVisualPreviewEvidence>();
            marker.sourceSceneId=Id;
            marker.originalSourceGraphSha256=original.sourceGraphSha256;
            marker.verifiedFieldPlanSha256=original.verifiedUiPlanSha256;
            marker.sourceBoundSprites=265;
            marker.inheritedVerifiedComponents=go.GetComponentsInChildren<
                ManagedUiSourceEvidence>(true).Length;
            marker.excludedSingleBackendFields=651;
            marker.originalRootScale=originalRootScale;
            marker.normalizedPreviewRootScale=Vector3.one;
            marker.provisionalPreviewReferenceResolution=new Vector2(1600,900);
            marker.limitations="REF04 SOURCE TIGHT SPRITE BOUNDS STUDY ONLY. " +
                "Restored native Sprite logical image rectangles only when exact " +
                "source textureRect/offset and unpacked Tight mesh agree. " +
                "Original pixels unchanged; transparent outer area is unrendered " +
                "source mesh space. Preview Canvas 1600x900, HUD/Text, Spine and " +
                "runtime layout NOT authenticated. Original 3C/3E unaffected.";
            var saved=PrefabUtility.SaveAsPrefabAsset(go,NewPrefab);
            if(saved==null)
                throw new IOException("Cannot save REF04 NativeBounds Prefab.");
            UnityEngine.Object.DestroyImmediate(go);
            var view=EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                  NewSceneMode.Single);
            var camera=new GameObject(
                "REF04 PREVIEW CAMERA - NOT ORIGINAL",typeof(Camera));
            camera.transform.position=new Vector3(0,0,-10);
            var cam=camera.GetComponent<Camera>();
            cam.orthographic=true;
            cam.clearFlags=CameraClearFlags.SolidColor;
            cam.backgroundColor=new Color(.045f,.075f,.115f);
            if(PrefabUtility.InstantiatePrefab(saved,view)==null ||
                !EditorSceneManager.SaveScene(view,NewScene))
                throw new IOException("Cannot save REF04 NativeBounds study scene.");
        }


        private static void AuditSaved()
        {
            var source=AssetDatabase.LoadAssetAtPath<GameObject>(Study);
            var preview=AssetDatabase.LoadAssetAtPath<GameObject>(NewPrefab);
            if(source==null || preview==null ||
                AssetDatabase.LoadAssetAtPath<SceneAsset>(NewScene)==null)
                throw new FileNotFoundException("New REF04 source Tight Sprite study missing.");
            var proof=JsonUtility.FromJson<Source>(
                File.ReadAllText(NativeGeometryPath,Encoding.UTF8));
            var plan=JsonUtility.FromJson<Plan>(
                File.ReadAllText(NativePlanPath,Encoding.UTF8));
            if(proof==null || plan==null || proof.images==null ||
                proof.images.Length!=265 || plan.sprites==null ||
                plan.nativeGeometryManifestFileSha256!=FileDigest(NativeGeometryPath))
                throw new InvalidDataException("Native Sprite source proof was modified.");
            var byFile=plan.sprites.ToDictionary(x=>x.spriteFile);
            // Validate the exact COMPLETE set (including Sprite-less 3C
            // Image components) on both saved Prefabs.
            var sourceImages=Check3cImageEvidence(source,proof);
            var previewImages=Check3cImageEvidence(preview,proof);
            var sourceLinkedIds=new HashSet<int>(
                proof.images.Select(i=>i.componentPathId));
            foreach(var sourceImage in sourceImages)
            {
                var originalImage=sourceImage.Value.GetComponent<Image>();
                var previewImage=previewImages[sourceImage.Key].GetComponent<Image>();
                if(originalImage==null || previewImage==null ||
                    originalImage.enabled!=previewImage.enabled ||
                    originalImage.type!=previewImage.type ||
                    originalImage.preserveAspect!=previewImage.preserveAspect ||
                    originalImage.fillMethod!=previewImage.fillMethod ||
                    originalImage.fillOrigin!=previewImage.fillOrigin ||
                    Mathf.Abs(originalImage.fillAmount-previewImage.fillAmount)>.0001f ||
                    originalImage.fillClockwise!=previewImage.fillClockwise ||
                    Vector4.Distance(originalImage.color,previewImage.color)>.0001f)
                    throw new InvalidDataException(
                        "Cross-verified 3C Image field changed for Component PathID "+
                        sourceImage.Key);
                if(!sourceLinkedIds.Contains(sourceImage.Key) &&
                    AssetDatabase.GetAssetPath(originalImage.sprite) !=
                    AssetDatabase.GetAssetPath(previewImage.sprite))
                    throw new InvalidDataException(
                        "3C Image WITHOUT source Sprite binding was changed: "+
                        sourceImage.Key);
            }
            var sourceNotes=source.GetComponentsInChildren<ManagedUiSourceEvidence>(true)
                .ToDictionary(n=>n.sourceMonoBehaviourPathId);
            var copiedNotes=preview.GetComponentsInChildren<ManagedUiSourceEvidence>(true)
                .ToDictionary(n=>n.sourceMonoBehaviourPathId);
            if(sourceNotes.Count!=copiedNotes.Count)
                throw new InvalidDataException("Source-managed component inventory changed.");
            foreach(var sourceEntry in sourceNotes)
            {
                if(!copiedNotes.TryGetValue(sourceEntry.Key,out var copy) ||
                    copy.sourceObjectSha256!=sourceEntry.Value.sourceObjectSha256 ||
                    copy.sourceRectTransformPathId!=
                        sourceEntry.Value.sourceRectTransformPathId ||
                    copy.sourceGameObjectPathId!=
                        sourceEntry.Value.sourceGameObjectPathId ||
                    copy.originalClassName!=sourceEntry.Value.originalClassName)
                    throw new InvalidDataException(
                        "Original 3C source-managed component identity changed.");
            }
            var sourceRects=source.GetComponentsInChildren<OriginalSerializedEvidence>(true)
                .ToDictionary(n=>n.rectTransformPathId);
            var copiedRects=preview.GetComponentsInChildren<OriginalSerializedEvidence>(true)
                .ToDictionary(n=>n.rectTransformPathId);
            if(sourceRects.Count!=copiedRects.Count)
                throw new InvalidDataException(
                    "Original 3C RectTransform/child inventory changed.");
            int rootId=source.GetComponent<OriginalSerializedEvidence>().rectTransformPathId;
            foreach(var item in sourceRects)
            {
                if(!copiedRects.TryGetValue(item.Key,out var other) ||
                    other.gameObjectPathId!=item.Value.gameObjectPathId)
                    throw new InvalidDataException("Original RectTransform owner missing.");
                if(item.Key==rootId) continue; // ONLY preview root may be normalized
                var a=item.Value.GetComponent<RectTransform>();
                var b=other.GetComponent<RectTransform>();
                if(a==null || b==null ||
                    Vector2.Distance(a.anchorMin,b.anchorMin)>.0001f ||
                    Vector2.Distance(a.anchorMax,b.anchorMax)>.0001f ||
                    Vector2.Distance(a.pivot,b.pivot)>.0001f ||
                    Vector2.Distance(a.sizeDelta,b.sizeDelta)>.0001f ||
                    Vector2.Distance(a.anchoredPosition,b.anchoredPosition)>.0001f ||
                    Vector3.Distance(a.localScale,b.localScale)>.0001f ||
                    Quaternion.Angle(a.localRotation,b.localRotation)>.01f ||
                    a.GetSiblingIndex()!=b.GetSiblingIndex() ||
                    a.gameObject.activeSelf!=b.gameObject.activeSelf)
                    throw new InvalidDataException(
                        "Original REF04 child source RectTransform/sibling changed: "+
                        item.Key);
            }
            int previewSpriteOwners=0;
            foreach(var item in proof.images)
            {
                if(!byFile.TryGetValue(item.spriteFile,out var native) ||
                    !sourceNotes.TryGetValue(item.componentPathId,out var a) ||
                    !copiedNotes.TryGetValue(item.componentPathId,out var b) ||
                    a.sourceObjectSha256!=item.sourceObjectSha256 ||
                    b.sourceObjectSha256!=item.sourceObjectSha256)
                    throw new InvalidDataException(
                        "Source original REF04 Image owner changed.");
                var original=a.GetComponent<Image>();
                var rendered=b.GetComponent<Image>();
                if(original==null || rendered==null ||
                    (int)rendered.type!=item.verifiedImageType ||
                    rendered.type!=original.type ||
                    rendered.preserveAspect!=original.preserveAspect ||
                    rendered.fillMethod!=original.fillMethod ||
                    rendered.fillOrigin!=original.fillOrigin ||
                    Mathf.Abs(rendered.fillAmount-original.fillAmount)>.0001f ||
                    rendered.fillClockwise!=original.fillClockwise ||
                    Vector4.Distance(rendered.color,original.color)>.0001f ||
                    rendered.enabled!=original.enabled)
                    throw new InvalidDataException(
                        "Original cross-verified Image fields changed: "+
                        item.componentPathId);
                string path=native.status==PreviewStatus ?
                    PreviewSpritePath(item.spriteFile) :
                    SourceSpritePath(item.spriteFile);
                if(AssetDatabase.GetAssetPath(rendered.sprite)!=path)
                    throw new InvalidDataException(
                        "Original Image Sprite filename binding modified: "+
                        item.componentPathId);
                if(native.status==PreviewStatus)
                    previewSpriteOwners++;
            }
            if(previewSpriteOwners<45 || previewSpriteOwners>53)
                throw new InvalidDataException("Unexpected native Sprite preview owner count.");
            Debug.Log("[REF04 NATIVE BOUNDS STUDY] AUDIT PASS: 265 exact " +
                "original Image IDs, " + previewSpriteOwners +
                " native-offset preview Sprite Image owners, all non-root " +
                "source RectTransforms/sibling indices intact, source Image " +
                "fields identical to original 3C. Runtime visual fidelity " +
                "and full HUD/Text/Spine NOT proven.");
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/REF04 - Audit source Tight Sprite bounds UI study")]
        public static void Audit()
        {
            try { AuditSaved(); }
            catch(Exception exc)
            {
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("REF04 native Sprite UI audit FAILED",
                    exc.GetBaseException().Message,"OK");
            }
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/REF04 - Build source Tight Sprite bounds UI study")]
        public static void Build()
        {
            if(EditorApplication.isPlaying ||
                !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;
            var created=new List<string>();
            string before=EditorSceneManager.GetActiveScene().path;
            try
            {
                var evidence=Preflight();
                EnsureFolder("Assets","LocalReconstruction");
                EnsureFolder(Local,"Ref04NativeBoundsSprites");
                EnsureFolder(Local,"Ref04NativeBoundsStudyPrefabs");
                EnsureFolder(Local,"Ref04NativeBoundsStudyScenes");
                foreach(var item in evidence.Item3.Values.OrderBy(x=>x.spriteFile)
                    .Where(x=>x.status==PreviewStatus))
                    ImportSourceProvenPreview(item,created);
                created.Add(NewPrefab);
                created.Add(NewScene);
                BuildContent(evidence.Item1,evidence.Item2,evidence.Item3);
                AssetDatabase.SaveAssets();
                AuditSaved(); // Fail closed and roll back only NEW study outputs.
                Debug.Log("[REF04 NATIVE BOUNDS STUDY] BUILD PASS: " +
                    evidence.Item3.Values.Count(x=>x.status==PreviewStatus) +
                    " source-offset reconstructed Tight Sprite COPIES; " +
                    "265 original Image Component IDs preserved. " +
                    "New REF04 Study Scene only; runtime visual match UNPROVEN.");
                EditorUtility.DisplayDialog("REF04 source Sprite preview ready",
                    "Open Ref04NativeBoundsStudyScenes/REF04-home-crew_" +
                    "NATIVE_BOUNDS_STUDY.unity in Game tab. " +
                    "All original XAPK-exported PNGs and 3C/3E studies untouched. " +
                    "This is still a preview until XAPK runtime comparison.","OK");
            }
            catch(Exception exc)
            {
                try
                {
                    if(!string.IsNullOrEmpty(before) &&
                        AssetDatabase.LoadAssetAtPath<SceneAsset>(before)!=null &&
                        !created.Contains(before))
                        EditorSceneManager.OpenScene(before,OpenSceneMode.Single);
                    else
                        EditorSceneManager.NewScene(
                            NewSceneSetup.EmptyScene,NewSceneMode.Single);
                }
                catch(Exception restoreError)
                {
                    Debug.LogWarning("Could not restore previous Scene: "+
                                     restoreError.Message);
                }
                foreach(var path in created)
                    AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("REF04 source preview BLOCKED",
                    exc.GetBaseException().Message +
                    "\nOnly NEW REF04 preview assets were rolled back; " +
                    "3C/3D/3E are untouched.","OK");
            }
        }
    }
}
#endif
