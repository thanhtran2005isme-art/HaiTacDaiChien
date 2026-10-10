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
    /// Phase 3D: opt-in visual previews from verified 3C Prefabs + exact source
    /// Sprite->Image pointers. Creates NEW disposable local copies, never
    /// changes the old rendered Canvas or the 3C/serialized-graph Prefabs.
    /// </summary>
    public static class VerifiedVisualStudyImporter
    {
        private const string LocalRoot = "Assets/LocalReconstruction";
        private const string VerifiedFields = LocalRoot + "/VerifiedManagedFieldPrefabs";
        private const string VisualPrefabs = LocalRoot + "/VerifiedVisualPrefabs";
        private const string VisualScenes = LocalRoot + "/VerifiedVisualScenes";
        private const string SpriteFolder = LocalRoot + "/Sprites";
        private const string RootViewPrefabs = LocalRoot + "/RootCanvasViewportPrefabs";
        private const string RootViewScenes = LocalRoot + "/RootCanvasViewportScenes";
        private const string Classification =
            "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS";

        [Serializable] private sealed class Plan
        {
            public int schemaVersion;
            public string classification;
            public string sourceGraphSha256;
            public string verifiedUiPlanSha256;
            public string spritePrefabPlanSha256;
            public string spriteManifestSha256;
            public int sourceBindings;
            public bool previewOnly;
            public PlanScene[] scenes;
        }
        [Serializable] private sealed class PlanScene
        {
            public string sceneId;
            public ImageBinding[] bindings;
        }
        [Serializable] private sealed class ImageBinding
        {
            public int rectTransformPathId;
            public int imageComponentPathId;
            public int gameObjectPathId;
            public string sourceObjectSha256;
            public string spriteFile;
        }
        [Serializable] private sealed class FieldPlan
        {
            public int schemaVersion;
            public string classification;
            public string sourceGraphSha256;
            public int verifiedComponents;
            public int verifiedFieldValues;
            public int singleBackendExcludedFieldValues;
            public FieldScene[] scenes;
        }
        [Serializable] private sealed class FieldScene
        {
            public string sceneId;
            public VerifiedComponent[] components;
        }
        [Serializable] private sealed class VerifiedComponent
        {
            public int componentPathId;
            public int gameObjectPathId;
            public int rectTransformPathId;
            public string className;
            public string rawObjectSha256;
            public bool enabled;
            public FieldValue[] fields;
        }
        [Serializable] private sealed class FieldValue
        {
            public string name;
        }
        private sealed class Sources
        {
            public Plan visual;
            public FieldPlan fields;
        }
        private static string RepoRoot =>
            Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
        private static string SourcePrefab(string id) =>
            VerifiedFields + "/" + id + "_VERIFIED_FIELDS_STUDY.prefab";
        private static string DestPrefab(string id) =>
            VisualPrefabs + "/" + id + "_VERIFIED_VISUAL_PREVIEW.prefab";
        private static string DestScene(string id) =>
            VisualScenes + "/" + id + "_VERIFIED_VISUAL_PREVIEW.unity";
        private static string SpritePath(string file) => SpriteFolder + "/" + file;
        private static string RootViewPrefab(string id) =>
            RootViewPrefabs + "/" + id + "_SOURCE_ROOT_CANVAS_PREVIEW.prefab";
        private static string RootViewScene(string id) =>
            RootViewScenes + "/" + id + "_SOURCE_ROOT_CANVAS_PREVIEW.unity";
        private static string StudyTargetPrefab(string id, bool rootCanvas) =>
            rootCanvas ? RootViewPrefab(id) : DestPrefab(id);
        private static string StudyTargetScene(string id, bool rootCanvas) =>
            rootCanvas ? RootViewScene(id) : DestScene(id);

        private static bool SafeId(string s) =>
            !string.IsNullOrEmpty(s) &&
            System.Text.RegularExpressions.Regex.IsMatch(s, @"^REF[0-9A-Za-z-]+$");
        private static bool SafePng(string s) =>
            !string.IsNullOrEmpty(s) &&
            System.Text.RegularExpressions.Regex.IsMatch(s, @"^[0-9a-f]{32}\.png$");
        private static bool Sha(string s) =>
            !string.IsNullOrEmpty(s) &&
            System.Text.RegularExpressions.Regex.IsMatch(s, @"^[0-9a-f]{64}$");
        private static string Digest(byte[] content)
        {
            using (var hash = SHA256.Create())
                return BitConverter.ToString(hash.ComputeHash(content))
                    .Replace("-", "").ToLowerInvariant();
        }
        private static byte[] ReadOutput(string relative) =>
            File.ReadAllBytes(Path.Combine(RepoRoot, "output", relative));
        private static string DigestedOutput(string relative) =>
            Digest(ReadOutput(relative));

        private static Sources ValidateSources(bool requireFreshOutputs, bool rootCanvas = false)
        {
            var visual = JsonUtility.FromJson<Plan>(
                Encoding.UTF8.GetString(ReadOutput("verified-visual-preview-plan.json")));
            var fields = JsonUtility.FromJson<FieldPlan>(
                Encoding.UTF8.GetString(ReadOutput("verified-ui-prefab-plan.json")));
            if (visual == null || fields == null ||
                visual.schemaVersion != 1 ||
                visual.classification != Classification ||
                !visual.previewOnly || visual.sourceBindings != 963 ||
                visual.scenes == null || visual.scenes.Length != 5 ||
                fields.schemaVersion != 1 ||
                fields.classification !=
                    "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS" ||
                fields.scenes == null || fields.scenes.Length != 5 ||
                fields.verifiedComponents != 1108 ||
                fields.verifiedFieldValues != 7451 ||
                fields.singleBackendExcludedFieldValues != 651 ||
                fields.sourceGraphSha256 != visual.sourceGraphSha256 ||
                DigestedOutput("verified-ui-prefab-plan.json") !=
                    visual.verifiedUiPlanSha256 ||
                DigestedOutput("original-unity-graph.json") !=
                    visual.sourceGraphSha256 ||
                DigestedOutput("unity-prefab-map.json") !=
                    visual.spritePrefabPlanSha256 ||
                DigestedOutput("local-ui-art/manifest.json") !=
                    visual.spriteManifestSha256)
                throw new InvalidDataException(
                    "3D plan, 3C fields, source graph or Sprite manifests changed.");

            var byScene = fields.scenes.ToDictionary(s => s.sceneId);
            var ids = new HashSet<string>();
            var verifiedSpriteFiles = new HashSet<string>(StringComparer.Ordinal);
            int sprites = 0;
            foreach (var scene in visual.scenes)
            {
                if (scene == null || !SafeId(scene.sceneId) ||
                    !ids.Add(scene.sceneId) ||
                    !byScene.TryGetValue(scene.sceneId, out var proof) ||
                    proof.components == null || scene.bindings == null)
                    throw new InvalidDataException("Missing original 3C scene identity.");
                var byImage = proof.components.Where(c =>
                    c.className == "UnityEngine.UI.Image")
                    .ToDictionary(c => (c.rectTransformPathId, c.componentPathId));
                var used = new HashSet<int>();
                foreach (var b in scene.bindings)
                {
                    if (b == null || !used.Add(b.imageComponentPathId) ||
                        !SafePng(b.spriteFile) || !Sha(b.sourceObjectSha256) ||
                        !byImage.TryGetValue(
                            (b.rectTransformPathId, b.imageComponentPathId),
                            out var verified) ||
                        verified.gameObjectPathId != b.gameObjectPathId ||
                        verified.rawObjectSha256 != b.sourceObjectSha256)
                        throw new InvalidDataException(
                            "3D Image binding is not a 3C component identity.");
                    if (AssetDatabase.LoadAssetAtPath<Sprite>(SpritePath(b.spriteFile)) == null)
                        throw new FileNotFoundException(
                            "Source Sprite not imported locally: " + b.spriteFile +
                            ". First run Reconstruct 5 local Canvas prefabs.");
                    if (verifiedSpriteFiles.Add(b.spriteFile))
                    {
                        string sourceImage = Path.Combine(RepoRoot, "output",
                            "local-ui-art", b.spriteFile);
                        string importedImage = Path.Combine(Application.dataPath,
                            "LocalReconstruction", "Sprites", b.spriteFile);
                        if (!File.Exists(sourceImage) || !File.Exists(importedImage) ||
                            Digest(File.ReadAllBytes(sourceImage)) !=
                                Digest(File.ReadAllBytes(importedImage)))
                            throw new InvalidDataException(
                                "Imported Sprite PNG bytes differ from exact XAPK export.");
                    }
                    sprites++;
                }
                if (AssetDatabase.LoadAssetAtPath<GameObject>(SourcePrefab(scene.sceneId))
                    == null)
                    throw new FileNotFoundException(
                        "3C study Prefab missing for " + scene.sceneId);
                if (requireFreshOutputs &&
                    (AssetDatabase.LoadAssetAtPath<GameObject>(
                        StudyTargetPrefab(scene.sceneId, rootCanvas)) != null ||
                     File.Exists(StudyTargetScene(scene.sceneId, rootCanvas))))
                    throw new IOException("3D preview already exists; refusing to overwrite: " +
                        scene.sceneId);
            }
            if (sprites != 963)
                throw new InvalidDataException("Not all 963 original Sprite pointers verified.");
            return new Sources {visual = visual, fields = fields};
        }

        private static void Folder(string parent, string child)
        {
            if (!AssetDatabase.IsValidFolder(parent + "/" + child))
                AssetDatabase.CreateFolder(parent, child);
        }
        private static Camera AddPreviewCamera(Scene destination)
        {
            var go = new GameObject("3D PREVIEW CAMERA - NOT ORIGINAL", typeof(Camera));
            if (go.scene != destination)
                SceneManager.MoveGameObjectToScene(go, destination);
            go.transform.position = new Vector3(0, 0, -10);
            go.transform.rotation = Quaternion.identity;
            var camera = go.GetComponent<Camera>();
            camera.enabled = true;
            camera.orthographic = true;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(.045f, .075f, .115f);
            camera.nearClipPlane = .1f;
            camera.farClipPlane = 100;
            return camera;
        }
        private static void Construct(Sources source, PlanScene scene)
        {
            var stage = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                    NewSceneMode.Single);
            var outer = new GameObject(
                "3D PREVIEW ONLY - " + scene.sceneId,
                typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler),
                typeof(GraphicRaycaster), typeof(VerifiedVisualPreviewEvidence));
            try
            {
                var rect = outer.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;
                rect.sizeDelta = new Vector2(1600, 900); // PROVISIONAL VIEWPORT ONLY.
                var canvas = outer.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                var scaler = outer.GetComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1600, 900);
                var study = AssetDatabase.LoadAssetAtPath<GameObject>(
                    SourcePrefab(scene.sceneId));
                var copy = PrefabUtility.InstantiatePrefab(study, stage) as GameObject;
                if (copy == null)
                    throw new IOException("Cannot instantiate 3C verified source Prefab.");
                copy.transform.SetParent(rect, false);
                var originalScale = copy.transform.localScale;
                // Four source candidates have zero-scale player build roots.
                // Normalize only this local PREVIEW instance, never source 3C.
                if (Mathf.Approximately(originalScale.x, 0f) ||
                    Mathf.Approximately(originalScale.y, 0f))
                    copy.transform.localScale = new Vector3(
                        Mathf.Approximately(originalScale.x, 0f) ? 1f : originalScale.x,
                        Mathf.Approximately(originalScale.y, 0f) ? 1f : originalScale.y,
                        originalScale.z);
                var notes = copy.GetComponentsInChildren<ManagedUiSourceEvidence>(true);
                var byComponent = notes.ToDictionary(n => n.sourceMonoBehaviourPathId);
                var fieldScene = source.fields.scenes.Single(s => s.sceneId == scene.sceneId);
                if (notes.Length != fieldScene.components.Length)
                    throw new InvalidDataException("3C component proof missing from copy.");
                foreach (var record in fieldScene.components)
                {
                    if (!byComponent.TryGetValue(record.componentPathId, out var note) ||
                        !note.exactTwoBackendFieldAgreement ||
                        note.sourceSceneId != scene.sceneId ||
                        note.sourceRectTransformPathId != record.rectTransformPathId ||
                        note.sourceGameObjectPathId != record.gameObjectPathId ||
                        note.originalClassName != record.className ||
                        note.sourceObjectSha256 != record.rawObjectSha256 ||
                        note.verifiedFieldNames == null ||
                        record.fields == null ||
                        !note.verifiedFieldNames.SequenceEqual(
                            record.fields.Select(f => f.name)))
                        throw new InvalidDataException(
                            "3C Prefab contents differ from private 3C manifest.");
                }
                var seen = new HashSet<int>();
                foreach (var record in scene.bindings)
                {
                    if (!seen.Add(record.imageComponentPathId) ||
                        !byComponent.TryGetValue(record.imageComponentPathId,
                                                out var note) ||
                        note.sourceRectTransformPathId != record.rectTransformPathId ||
                        note.sourceGameObjectPathId != record.gameObjectPathId ||
                        note.sourceObjectSha256 != record.sourceObjectSha256 ||
                        note.originalClassName != "UnityEngine.UI.Image")
                        throw new InvalidDataException("Wrong 3C Image on preview GameObject.");
                    var image = note.GetComponent<Image>();
                    if (image == null)
                        throw new InvalidDataException("Verified Image disappeared.");
                    image.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(
                        SpritePath(record.spriteFile));
                    if (image.sprite == null)
                        throw new IOException("Missing original imported Sprite.");
                }
                var evidence = outer.GetComponent<VerifiedVisualPreviewEvidence>();
                evidence.sourceSceneId = scene.sceneId;
                evidence.originalSourceGraphSha256 = source.visual.sourceGraphSha256;
                evidence.verifiedFieldPlanSha256 = source.visual.verifiedUiPlanSha256;
                evidence.exactSpritePlanSha256 = source.visual.spritePrefabPlanSha256;
                evidence.sourceBoundSprites = scene.bindings.Length;
                evidence.inheritedVerifiedComponents = fieldScene.components.Length;
                evidence.inheritedVerifiedFields = fieldScene.components.Sum(c => c.fields.Length);
                evidence.excludedSingleBackendFields = 651;
                evidence.originalRootScale = originalScale;
                evidence.normalizedPreviewRootScale = copy.transform.localScale;
                evidence.provisionalPreviewReferenceResolution = new Vector2(1600, 900);
                var saved = PrefabUtility.SaveAsPrefabAsset(outer, DestPrefab(scene.sceneId));
                if (saved == null)
                    throw new IOException("Could not save new 3D preview Prefab.");
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(outer);
            }
            var preview = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                       NewSceneMode.Single);
            AddPreviewCamera(preview);
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(DestPrefab(scene.sceneId));
            if (asset == null || PrefabUtility.InstantiatePrefab(asset, preview) == null ||
                !EditorSceneManager.SaveScene(preview, DestScene(scene.sceneId)))
                throw new IOException("Could not save 3D scene.");
            Debug.Log("[HaiTac 3D preview] " + scene.sceneId + ": " +
                scene.bindings.Length + " exact source-linked Sprite Images; " +
                "3C field Prefab nested, preview-only Camera and Canvas, NO new Spine guesses.");
        }

        private static bool SameProperty(SerializedProperty lhs, SerializedProperty rhs)
        {
            if (lhs == null || rhs == null || lhs.propertyType != rhs.propertyType)
                return false;
            switch (lhs.propertyType)
            {
                case SerializedPropertyType.Boolean: return lhs.boolValue == rhs.boolValue;
                case SerializedPropertyType.Integer: return lhs.intValue == rhs.intValue;
                case SerializedPropertyType.Enum: return lhs.intValue == rhs.intValue;
                case SerializedPropertyType.Float:
                    return Mathf.Abs(lhs.floatValue - rhs.floatValue) <= .0001f;
                case SerializedPropertyType.Color:
                    return Vector4.Distance(lhs.colorValue, rhs.colorValue) <= .0001f;
                case SerializedPropertyType.Vector2:
                    return Vector2.Distance(lhs.vector2Value, rhs.vector2Value) <= .0001f;
                default: return false;
            }
        }

        private static void AuditScene(Sources source, PlanScene scene, bool rootCanvas = false)
        {
            var basePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                SourcePrefab(scene.sceneId));
            var previewPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                StudyTargetPrefab(scene.sceneId, rootCanvas));
            if (basePrefab == null || previewPrefab == null ||
                !File.Exists(StudyTargetScene(scene.sceneId, rootCanvas)))
                throw new FileNotFoundException("3D preview scene or Prefab missing.");
            var wrapper = previewPrefab.GetComponent<VerifiedVisualPreviewEvidence>();
            if (wrapper == null || wrapper.sourceSceneId != scene.sceneId ||
                wrapper.sourceBoundSprites != scene.bindings.Length ||
                wrapper.originalSourceGraphSha256 != source.visual.sourceGraphSha256 ||
                wrapper.verifiedFieldPlanSha256 != source.visual.verifiedUiPlanSha256 ||
                wrapper.excludedSingleBackendFields != 651 ||
                !wrapper.previewCanvasNotClaimedAsOriginal ||
                !wrapper.previewCameraNotClaimedAsOriginal)
                throw new InvalidDataException("Preview-only evidence removed or changed.");
            var original = basePrefab.GetComponentsInChildren<ManagedUiSourceEvidence>(true);
            var visual = previewPrefab.GetComponentsInChildren<ManagedUiSourceEvidence>(true);
            var a = original.ToDictionary(n => n.sourceMonoBehaviourPathId);
            var b = visual.ToDictionary(n => n.sourceMonoBehaviourPathId);
            var proof = source.fields.scenes.Single(s => s.sceneId == scene.sceneId);
            if (a.Count != proof.components.Length || b.Count != a.Count)
                throw new InvalidDataException("Study components lost from visual Prefab.");
            int compared = 0;
            foreach (var record in proof.components)
            {
                if (!a.TryGetValue(record.componentPathId, out var lhs) ||
                    !b.TryGetValue(record.componentPathId, out var rhs) ||
                    lhs.originalClassName != rhs.originalClassName ||
                    lhs.sourceObjectSha256 != rhs.sourceObjectSha256 ||
                    lhs.sourceGameObjectPathId != rhs.sourceGameObjectPathId ||
                    lhs.sourceRectTransformPathId != rhs.sourceRectTransformPathId)
                    throw new InvalidDataException("3C component identity changed.");
                var sourceComponent = lhs.GetComponents<Behaviour>().FirstOrDefault(
                    x => x.GetType().FullName == record.className);
                var previewComponent = rhs.GetComponents<Behaviour>().FirstOrDefault(
                    x => x.GetType().FullName == record.className);
                if (sourceComponent == null || previewComponent == null ||
                    sourceComponent.enabled != previewComponent.enabled)
                    throw new InvalidDataException("3C UI Component class/enabled changed.");
                var first = new SerializedObject(sourceComponent);
                var second = new SerializedObject(previewComponent);
                foreach (var field in record.fields)
                {
                    if (!SameProperty(first.FindProperty(field.name),
                                      second.FindProperty(field.name)))
                        throw new InvalidDataException(
                            "Preview changed cross-verified 3C property " +
                            record.className + "." + field.name);
                    compared++;
                }
            }
            var byId = b;
            int linked = 0;
            foreach (var record in scene.bindings)
            {
                if (!byId.TryGetValue(record.imageComponentPathId, out var note) ||
                    note.originalClassName != "UnityEngine.UI.Image" ||
                    note.sourceObjectSha256 != record.sourceObjectSha256 ||
                    note.sourceGameObjectPathId != record.gameObjectPathId ||
                    note.sourceRectTransformPathId != record.rectTransformPathId)
                    throw new InvalidDataException("3D Sprite source owner changed.");
                var image = note.GetComponent<Image>();
                var sourceSprite = AssetDatabase.LoadAssetAtPath<Sprite>(
                    SpritePath(record.spriteFile));
                if (image == null || image.sprite == null ||
                    sourceSprite == null || image.sprite != sourceSprite)
                    throw new InvalidDataException("3D Sprite pointer differs from exact source.");
                linked++;
            }
            if (linked != scene.bindings.Length || compared !=
                proof.components.Sum(c => c.fields.Length) ||
                wrapper.inheritedVerifiedFields != compared)
                throw new InvalidDataException("3D preview audit count mismatch.");
        }


        // Phase 3E is an explicit viewport experiment, not a recovered Unity
        // runtime. It removes the extra wrapping Canvas that can double-scale
        // a source CanvasScaler or shift a RectTransform with unknown parent.
        // Every source child RectTransform is retained; only the source-root
        // RectTransform is given a preview viewport.
        private static void ConstructSourceRootCanvas(Sources source, PlanScene scene)
        {
            var stage = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                    NewSceneMode.Single);
            var study = AssetDatabase.LoadAssetAtPath<GameObject>(
                SourcePrefab(scene.sceneId));
            var copy = PrefabUtility.InstantiatePrefab(study, stage) as GameObject;
            if (copy == null)
                throw new InvalidDataException("3C verified study root is unavailable.");
            try
            {
                PrefabUtility.UnpackPrefabInstance(
                    copy, PrefabUnpackMode.Completely,
                    InteractionMode.AutomatedAction);
                var root = copy.GetComponent<RectTransform>();
                var originalRootNote = copy.GetComponent<OriginalSerializedEvidence>();
                if (root == null || originalRootNote == null)
                    throw new InvalidDataException(
                        "Missing exact source RectTransform/owner at preview root.");
                var originalScale = root.localScale;
                // Standalone display hypotheses ONLY. Source 3C root is not
                // modified; descendants keep original anchors and transforms.
                root.anchorMin = new Vector2(.5f, .5f);
                root.anchorMax = new Vector2(.5f, .5f);
                root.pivot = new Vector2(.5f, .5f);
                root.anchoredPosition = Vector2.zero;
                root.sizeDelta = new Vector2(1600f, 900f);
                root.localScale = Vector3.one;
                var canvas = copy.GetComponent<Canvas>();
                if (canvas == null) canvas = copy.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                var scaler = copy.GetComponent<CanvasScaler>();
                if (scaler == null)
                {
                    // There is no source CanvasScaler for this root. Do not
                    // claim these preview-only values as native evidence.
                    scaler = copy.AddComponent<CanvasScaler>();
                    scaler.uiScaleMode =
                        CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    scaler.referenceResolution = new Vector2(1600f, 900f);
                }
                if (copy.GetComponent<GraphicRaycaster>() == null)
                    copy.AddComponent<GraphicRaycaster>();

                var components = source.fields.scenes.Single(
                    x => x.sceneId == scene.sceneId).components;
                var notes = copy.GetComponentsInChildren<ManagedUiSourceEvidence>(true);
                var byId = notes.ToDictionary(x => x.sourceMonoBehaviourPathId);
                if (byId.Count != components.Length)
                    throw new InvalidDataException(
                        "Verified UI components lost in root-Canvas preview.");
                foreach (var verified in components)
                {
                    if (!byId.TryGetValue(verified.componentPathId, out var note) ||
                        note.sourceSceneId != scene.sceneId ||
                        note.sourceGameObjectPathId != verified.gameObjectPathId ||
                        note.sourceRectTransformPathId != verified.rectTransformPathId ||
                        note.sourceObjectSha256 != verified.rawObjectSha256 ||
                        !note.exactTwoBackendFieldAgreement)
                        throw new InvalidDataException(
                            "3C source Component PathID mismatch.");
                }
                foreach (var sprite in scene.bindings)
                {
                    if (!byId.TryGetValue(sprite.imageComponentPathId, out var note) ||
                        note.sourceGameObjectPathId != sprite.gameObjectPathId ||
                        note.sourceRectTransformPathId != sprite.rectTransformPathId ||
                        note.sourceObjectSha256 != sprite.sourceObjectSha256 ||
                        note.originalClassName != "UnityEngine.UI.Image")
                        throw new InvalidDataException(
                            "Original source Image/Sprite ownership differs.");
                    var image = note.GetComponent<Image>();
                    if (image == null)
                        throw new InvalidDataException("Original Image missing.");
                    image.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(
                        SpritePath(sprite.spriteFile));
                    if (image.sprite == null)
                        throw new FileNotFoundException(
                            "Missing source Sprite " + sprite.spriteFile);
                }
                var marker = copy.AddComponent<VerifiedVisualPreviewEvidence>();
                marker.sourceSceneId = scene.sceneId;
                marker.originalSourceGraphSha256 =
                    source.visual.sourceGraphSha256;
                marker.verifiedFieldPlanSha256 =
                    source.visual.verifiedUiPlanSha256;
                marker.exactSpritePlanSha256 =
                    source.visual.spritePrefabPlanSha256;
                marker.sourceBoundSprites = scene.bindings.Length;
                marker.inheritedVerifiedComponents = components.Length;
                marker.inheritedVerifiedFields =
                    components.Sum(c => c.fields.Length);
                marker.excludedSingleBackendFields = 651;
                marker.originalRootScale = originalScale;
                marker.normalizedPreviewRootScale = root.localScale;
                marker.provisionalPreviewReferenceResolution =
                    new Vector2(1600f, 900f);
                marker.limitations =
                    "SOURCE ROOT AS PREVIEW CANVAS (3E). No extra enclosing " +
                    "Canvas or preview-scale nesting. Only root RectTransform " +
                    "anchors/pivot/size/position/scale normalized for 1600x900 " +
                    "preview. All child source RectTransforms kept unchanged. " +
                    "Root Canvas render mode is preview-only. Native runtime " +
                    "ancestor, safe area, Spine animation and 651 LayoutGroup " +
                    "fields remain unverified; not original runtime UI.";
                if (PrefabUtility.SaveAsPrefabAsset(copy,
                        RootViewPrefab(scene.sceneId)) == null)
                    throw new IOException("Root-Canvas preview Prefab not saved.");
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(copy);
            }
            var dest = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                   NewSceneMode.Single);
            AddPreviewCamera(dest);
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(
                RootViewPrefab(scene.sceneId));
            if (asset == null ||
                PrefabUtility.InstantiatePrefab(asset, dest) == null ||
                !EditorSceneManager.SaveScene(dest,
                    RootViewScene(scene.sceneId)))
                throw new IOException("Root-Canvas preview Scene not saved.");
        }

        private static bool RectEqual(RectTransform a, RectTransform b)
        {
            const float eps = .0001f;
            return Vector2.Distance(a.anchorMin, b.anchorMin) <= eps &&
                   Vector2.Distance(a.anchorMax, b.anchorMax) <= eps &&
                   Vector2.Distance(a.pivot, b.pivot) <= eps &&
                   Vector2.Distance(a.sizeDelta, b.sizeDelta) <= eps &&
                   Vector2.Distance(a.anchoredPosition, b.anchoredPosition) <= eps &&
                   Vector3.Distance(a.localScale, b.localScale) <= eps &&
                   Quaternion.Angle(a.localRotation, b.localRotation) <= .01f;
        }

        private static void AuditRootTransforms(PlanScene scene)
        {
            var baseline = AssetDatabase.LoadAssetAtPath<GameObject>(
                SourcePrefab(scene.sceneId));
            var preview = AssetDatabase.LoadAssetAtPath<GameObject>(
                RootViewPrefab(scene.sceneId));
            if (baseline == null || preview == null)
                throw new FileNotFoundException("Preview/source prefab absent.");
            var sourceNotes = baseline.GetComponentsInChildren<OriginalSerializedEvidence>(
                true).ToDictionary(x => x.rectTransformPathId);
            var previewNotes = preview.GetComponentsInChildren<OriginalSerializedEvidence>(
                true).ToDictionary(x => x.rectTransformPathId);
            if (sourceNotes.Count != previewNotes.Count)
                throw new InvalidDataException(
                    "Source RectTransform object inventory altered.");
            var originalRoot = baseline.GetComponent<OriginalSerializedEvidence>();
            var newRoot = preview.GetComponent<OriginalSerializedEvidence>();
            if (originalRoot == null || newRoot == null ||
                originalRoot.rectTransformPathId != newRoot.rectTransformPathId)
                throw new InvalidDataException("Source root PathID changed.");
            foreach (var entry in sourceNotes)
            {
                if (!previewNotes.TryGetValue(entry.Key, out var changed) ||
                    changed.gameObjectPathId != entry.Value.gameObjectPathId ||
                    (changed.transform.parent == null) !=
                        (entry.Value.transform.parent == null))
                    throw new InvalidDataException(
                        "Source hierarchy/object identity changed.");
                if (entry.Key == originalRoot.rectTransformPathId) continue;
                var a = entry.Value.GetComponent<RectTransform>();
                var b = changed.GetComponent<RectTransform>();
                if (a == null || b == null || !RectEqual(a, b) ||
                    changed.transform.GetSiblingIndex() !=
                        entry.Value.transform.GetSiblingIndex() ||
                    changed.gameObject.activeSelf !=
                        entry.Value.gameObject.activeSelf)
                    throw new InvalidDataException(
                        "Source child RectTransform was changed: " + entry.Key);
            }
            var root = preview.GetComponent<RectTransform>();
            var marker = preview.GetComponent<VerifiedVisualPreviewEvidence>();
            var originalRootTransform = baseline.GetComponent<RectTransform>();
            if (root == null || originalRootTransform == null || marker == null ||
                Vector3.Distance(marker.originalRootScale,
                    originalRootTransform.localScale) > .0001f ||
                root.localScale != Vector3.one ||
                !marker.previewCanvasNotClaimedAsOriginal ||
                !marker.previewCameraNotClaimedAsOriginal ||
                marker.provisionalPreviewReferenceResolution !=
                    new Vector2(1600f, 900f) ||
                preview.GetComponent<Canvas>() == null ||
                preview.GetComponent<Canvas>().renderMode !=
                    RenderMode.ScreenSpaceOverlay)
                throw new InvalidDataException(
                    "Provisional root Canvas preview no longer matches its label.");
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/Build 5 source-root Canvas viewport studies")]
        public static void BuildRootCanvas()
        {
            if (EditorApplication.isPlaying ||
                !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;
            var created = new List<string>();
            try
            {
                var source = ValidateSources(true, rootCanvas: true);
                Folder("Assets", "LocalReconstruction");
                Folder(LocalRoot, "RootCanvasViewportPrefabs");
                Folder(LocalRoot, "RootCanvasViewportScenes");
                foreach (var scene in source.visual.scenes)
                {
                    created.Add(RootViewPrefab(scene.sceneId));
                    created.Add(RootViewScene(scene.sceneId));
                    ConstructSourceRootCanvas(source, scene);
                }
                AssetDatabase.SaveAssets();
                foreach (var scene in source.visual.scenes)
                {
                    AuditScene(source, scene, rootCanvas: true);
                    AuditRootTransforms(scene);
                }
                Debug.Log("[HaiTac ROOT CANVAS STUDY] BUILD+AUDIT PASS: 5 " +
                    "separate RootCanvasViewportScenes, 963 exact Sprite bindings, " +
                    "7451 original 3C fields match, source children transforms " +
                    "unchanged. Preview root normalization is NOT runtime evidence.");
                EditorUtility.DisplayDialog("Viewport study complete",
                    "5 new root-Canvas viewport study scenes created. " +
                    "No nested provisional Canvas; 3C 7451 values and all child " +
                    "RectTransforms audited. This is NOT original game layout.",
                    "OK");
            }
            catch (Exception exc)
            {
                foreach (var asset in created)
                    AssetDatabase.DeleteAsset(asset);
                AssetDatabase.SaveAssets();
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("Root Canvas preview BLOCKED",
                    exc.GetBaseException().Message +
                    "\nNo original 3C/3D prefabs overwritten.", "OK");
            }
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/Audit 5 source-root Canvas viewport studies")]
        public static void AuditRootCanvas()
        {
            try
            {
                var source = ValidateSources(false, rootCanvas: true);
                foreach (var scene in source.visual.scenes)
                {
                    AuditScene(source, scene, rootCanvas: true);
                    AuditRootTransforms(scene);
                }
                Debug.Log("[HaiTac ROOT CANVAS STUDY] AUDIT PASS: 963 Sprite " +
                    "links, 7451 managed values, all nonroot source RectTransforms " +
                    "preserved. Original runtime viewport NOT verified.");
            }
            catch (Exception exc)
            {
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("Root Canvas audit FAILED",
                    exc.GetBaseException().Message, "OK");
            }
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/Build 5 exact-source 3D visual previews")]
        public static void Build()
        {
            if (EditorApplication.isPlaying ||
                !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;
            var created = new List<string>();
            try
            {
                var source = ValidateSources(true);
                Folder("Assets", "LocalReconstruction");
                Folder(LocalRoot, "VerifiedVisualPrefabs");
                Folder(LocalRoot, "VerifiedVisualScenes");
                foreach (var scene in source.visual.scenes)
                {
                    created.Add(DestPrefab(scene.sceneId));
                    created.Add(DestScene(scene.sceneId));
                    Construct(source, scene);
                }
                AssetDatabase.SaveAssets();
                foreach (var scene in source.visual.scenes)
                    AuditScene(source, scene);
                Debug.Log("[HaiTac 3D] BUILD + PREFAB FIELD AUDIT PASS: 5 visual " +
                    "study prefabs/scenes, 963 exact source Sprite bindings, " +
                    "7451 inherited 3C managed field values unchanged. " +
                    "Preview Camera/Canvas are PROVISIONAL, not runtime-original.");
                EditorUtility.DisplayDialog("3D visual studies built",
                    "5 NEW visual study scenes created; 963 source Sprite/Image " +
                    "pointers verified. The inherited 7451 field values match 3C. " +
                    "Open VerifiedVisualScenes to inspect Camera, Canvas and image. " +
                    "Runtime Spine, 651 LayoutGroup fields and gameplay remain unresolved.",
                    "OK");
            }
            catch (Exception exc)
            {
                foreach (var path in created)
                    AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("3D strict import BLOCKED",
                    exc.GetBaseException().Message +
                    "\nOnly newly generated 3D preview outputs were rolled back.",
                    "OK");
            }
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/Audit 5 exact-source 3D visual previews")]
        public static void Audit()
        {
            try
            {
                var source = ValidateSources(false);
                foreach (var scene in source.visual.scenes)
                    AuditScene(source, scene);
                Debug.Log("[HaiTac 3D] AUDIT PASS: 5 preview Prefabs, " +
                    "963 exact source Sprite/Image links, 7451 inherited " +
                    "two-backend 3C UI field values unchanged. " +
                    "NOT Unity Play Mode, original runtime art/layout or Spine proof.");
                EditorUtility.DisplayDialog("3D field+Sprite audit PASS",
                    "5 preview Prefabs: all 963 Sprite pointers and 7451 " +
                    "two-backend 3C values remain exact. Preview-only Canvas/Camera " +
                    "and visual appearance are NOT verified as original.", "OK");
            }
            catch (Exception exc)
            {
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("3D audit FAILED",
                    exc.GetBaseException().Message +
                    "\nDo not claim new visuals are source-verified.", "OK");
            }
        }
    }
}
#endif
