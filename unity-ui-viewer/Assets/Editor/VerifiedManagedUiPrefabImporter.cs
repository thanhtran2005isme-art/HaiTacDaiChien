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
    /// OPT-IN: makes separate study copies from verified serialized graph prefabs,
    /// applying ONLY fields proven equal by two strict IL2CPP decoding backends.
    /// This never changes the original source-graph prefabs or gameplay scenes.
    /// Unity-initialized component defaults are NOT claimed as recovered fields.
    /// </summary>
    public static class VerifiedManagedUiPrefabImporter
    {
        private const string Root = "Assets/LocalReconstruction";
        private const string GraphPrefabs = Root + "/SourceGraphPrefabs";
        private const string StudyPrefabs = Root + "/VerifiedManagedFieldPrefabs";
        private const string StudyScenes = Root + "/VerifiedManagedFieldScenes";
        private const string Classification =
            "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS";

        [Serializable] private sealed class FilePointer
        {
            public int fileId;
            public int pathId;
        }
        [Serializable] private sealed class SourceGraph
        {
            public int version;
            public string classification;
            public SourceScene[] scenes;
        }
        [Serializable] private sealed class SourceScene
        {
            public string sceneId;
            public SourceNode[] nodes;
            public SourceComponent[] components;
        }
        [Serializable] private sealed class SourceNode
        {
            public int gameObjectId;
            public int rectTransformId;
            public int[] componentIds;
        }
        [Serializable] private sealed class SourceComponent
        {
            public int pathId;
            public string kind;
            public FilePointer gameObjectPointer;
            public FilePointer monoScriptPointer;
            public int m_Enabled;
        }
        [Serializable] private sealed class ImportPlan
        {
            public int schemaVersion;
            public string classification;
            public string unityVersion;
            public string sourceGraphSha256;
            public string sourceLibrarySha256;
            public string sourceMetadataSha256;
            public int verifiedComponents;
            public int verifiedFieldValues;
            public int singleBackendExcludedComponents;
            public int singleBackendExcludedFieldValues;
            public ImportScene[] scenes;
        }
        [Serializable] private sealed class ImportScene
        {
            public string sceneId;
            public ImportComponent[] components;
        }
        [Serializable] private sealed class ImportComponent
        {
            public int componentPathId;
            public int gameObjectPathId;
            public int rectTransformPathId;
            public string className;
            public string rawObjectSha256;
            public bool enabled;
            public ImportField[] fields;
        }
        [Serializable] private sealed class ImportField
        {
            public string name;
            public string kind;
            public bool boolValue;
            public int intValue;
            public float floatValue;
            public float[] floatValues;
        }

        private static readonly Dictionary<string, string> ImageFields =
            new Dictionary<string, string> {
                {"m_Color", "color"}, {"m_Type", "int"},
                {"m_PreserveAspect", "bool"}, {"m_FillMethod", "int"},
                {"m_FillAmount", "float"}, {"m_FillOrigin", "int"},
                {"m_FillClockwise", "bool"},
            };
        private static readonly Dictionary<string, string> ScalerFields =
            new Dictionary<string, string> {
                {"m_UiScaleMode", "int"}, {"m_ScreenMatchMode", "int"},
                {"m_ReferenceResolution", "vector2"},
                {"m_MatchWidthOrHeight", "float"}, {"m_ScaleFactor", "float"},
                {"m_ReferencePixelsPerUnit", "float"},
            };
        private static readonly Dictionary<string, string> MaskFields =
            new Dictionary<string, string> {{"m_ShowMaskGraphic", "bool"}};
        private static readonly Dictionary<string, string> FitterFields =
            new Dictionary<string, string> {
                {"m_HorizontalFit", "int"}, {"m_VerticalFit", "int"},
            };

        private static string RepoRoot =>
            Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));

        private static string Sha256(byte[] bytes)
        {
            using (var sha = SHA256.Create())
                return BitConverter.ToString(sha.ComputeHash(bytes))
                    .Replace("-", "").ToLowerInvariant();
        }
        private static bool ValidSha(string value) =>
            !string.IsNullOrEmpty(value) && value.Length == 64 &&
            value.All(c => (c >= '0' && c <= '9') ||
                           (c >= 'a' && c <= 'f'));

        private static string SourcePath(string scene) =>
            GraphPrefabs + "/" + scene + "_SERIALIZED_GRAPH.prefab";
        private static string TargetPath(string scene) =>
            StudyPrefabs + "/" + scene + "_VERIFIED_FIELDS_STUDY.prefab";
        private static string TargetScene(string scene) =>
            StudyScenes + "/" + scene + "_VERIFIED_FIELDS_STUDY.unity";

        private static Dictionary<string, string> Expected(string cls)
        {
            switch (cls)
            {
                case "UnityEngine.UI.Image": return ImageFields;
                case "UnityEngine.UI.CanvasScaler": return ScalerFields;
                case "UnityEngine.UI.Mask": return MaskFields;
                case "UnityEngine.UI.ContentSizeFitter": return FitterFields;
                default: throw new InvalidDataException("Unrecognized UI class: " + cls);
            }
        }

        private static bool Finite(float f) =>
            !float.IsNaN(f) && !float.IsInfinity(f);

        private static void CheckField(ImportField field, string cls)
        {
            var allowed = Expected(cls);
            if (field == null || !allowed.TryGetValue(field.name ?? "", out var kind)
                || field.kind != kind)
                throw new InvalidDataException("Field name/type not source allowlisted.");
            if (kind == "float" && !Finite(field.floatValue))
                throw new InvalidDataException("Non-finite numeric field.");
            if (kind == "vector2" || kind == "color")
            {
                int n = kind == "vector2" ? 2 : 4;
                if (field.floatValues == null || field.floatValues.Length != n ||
                    field.floatValues.Any(x => !Finite(x)))
                    throw new InvalidDataException("Malformed verified vector/color field.");
            }
            if (kind == "int")
            {
                int cap = field.name == "m_Type" ? 3 :
                          field.name == "m_FillMethod" ? 4 :
                          field.name == "m_FillOrigin" ? 3 : 2;
                if (field.intValue < 0 || field.intValue > cap)
                    throw new InvalidDataException("Invalid source UI enum.");
            }
            if (field.name == "m_FillAmount" ||
                field.name == "m_MatchWidthOrHeight")
            {
                if (field.floatValue < 0 || field.floatValue > 1)
                    throw new InvalidDataException("Invalid fill/width-height ratio.");
            }
            if ((field.name == "m_ScaleFactor" ||
                 field.name == "m_ReferencePixelsPerUnit") &&
                field.floatValue <= 0)
                throw new InvalidDataException("Invalid native scale factor.");
            if (field.name == "m_ReferenceResolution" &&
                (field.floatValues[0] <= 0 || field.floatValues[1] <= 0))
                throw new InvalidDataException("Invalid reference resolution.");
        }

        private static (ImportPlan, SourceGraph) ReadAndValidate()
        {
            string graphPath = Path.Combine(RepoRoot, "output",
                "original-unity-graph.json");
            string planPath = Path.Combine(RepoRoot, "output",
                "verified-ui-prefab-plan.json");
            if (!File.Exists(graphPath) || !File.Exists(planPath))
                throw new FileNotFoundException(
                    "Run original graph audit and build_verified_ui_prefab_plan.py first.");
            byte[] graphBytes = File.ReadAllBytes(graphPath);
            var graph = JsonUtility.FromJson<SourceGraph>(
                Encoding.UTF8.GetString(graphBytes));
            var plan = JsonUtility.FromJson<ImportPlan>(
                File.ReadAllText(planPath, Encoding.UTF8));
            if (graph == null || graph.version != 1 || graph.scenes == null ||
                graph.scenes.Length != 5 || graph.classification !=
                "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE" ||
                plan == null || plan.schemaVersion != 1 ||
                plan.classification != Classification ||
                plan.unityVersion != "2022.3.51f1" ||
                plan.scenes == null || plan.scenes.Length != 5 ||
                plan.verifiedComponents != 1108 ||
                plan.verifiedFieldValues != 7451 ||
                plan.singleBackendExcludedComponents != 93 ||
                plan.singleBackendExcludedFieldValues != 651 ||
                Sha256(graphBytes) != plan.sourceGraphSha256 ||
                !ValidSha(plan.sourceLibrarySha256) ||
                !ValidSha(plan.sourceMetadataSha256))
                throw new InvalidDataException(
                    "Missing cross-backend evidence, wrong Unity version or source graph changed.");

            var graphs = graph.scenes.ToDictionary(s => s.sceneId);
            var seen = new HashSet<string>();
            var countClasses = new Dictionary<string, int>();
            int count = 0, fields = 0;
            foreach (var candidate in plan.scenes)
            {
                if (candidate == null ||
                    !System.Text.RegularExpressions.Regex.IsMatch(
                        candidate.sceneId ?? "", @"^REF[0-9A-Za-z-]+$") ||
                    !seen.Add(candidate.sceneId) ||
                    !graphs.TryGetValue(candidate.sceneId, out var original) ||
                    original.nodes == null || original.components == null ||
                    candidate.components == null)
                    throw new InvalidDataException("Invalid source scene mapping.");
                var nodes = original.nodes.ToDictionary(n => n.rectTransformId);
                var components = original.components.ToDictionary(c => c.pathId);
                var usedComponents = new HashSet<int>();
                var usedClasses = new HashSet<string>();
                foreach (var target in candidate.components)
                {
                    if (target == null ||
                        !usedComponents.Add(target.componentPathId) ||
                        !nodes.TryGetValue(target.rectTransformPathId, out var node) ||
                        node.gameObjectId != target.gameObjectPathId ||
                        node.componentIds == null ||
                        !node.componentIds.Contains(target.componentPathId) ||
                        !components.TryGetValue(target.componentPathId, out var src) ||
                        src.kind != "MonoBehaviour" ||
                        src.gameObjectPointer == null ||
                        src.gameObjectPointer.fileId != 0 ||
                        src.gameObjectPointer.pathId != target.gameObjectPathId ||
                        src.monoScriptPointer == null ||
                        src.monoScriptPointer.pathId == 0 ||
                        !ValidSha(target.rawObjectSha256) ||
                        target.fields == null)
                        throw new InvalidDataException("Original component ownership not proven.");
                    var types = Expected(target.className);
                    if (target.fields.Length != types.Count ||
                        target.fields.Select(x => x.name).Distinct().Count() != types.Count)
                        throw new InvalidDataException("Partial/unexpected target managed fields.");
                    foreach (var field in target.fields)
                        CheckField(field, target.className);
                    var recordClasses = target.rectTransformPathId + "/" + target.className;
                    if (!usedClasses.Add(recordClasses))
                        throw new InvalidDataException(
                            "Multiple same-class source uGUI components on one GameObject.");
                    var native = new HashSet<string>(node.componentIds
                        .Where(id => components.ContainsKey(id))
                        .Select(id => components[id].kind));
                    if (target.className == "UnityEngine.UI.Image" &&
                        !native.Contains("CanvasRenderer"))
                        throw new InvalidDataException("Image CanvasRenderer source missing.");
                    if (target.className == "UnityEngine.UI.CanvasScaler" &&
                        !native.Contains("Canvas"))
                        throw new InvalidDataException("CanvasScaler native Canvas source missing.");
                    count++;
                    fields += target.fields.Length;
                    countClasses[target.className] =
                        countClasses.TryGetValue(target.className, out int old)
                        ? old + 1 : 1;
                }
                if (AssetDatabase.LoadAssetAtPath<GameObject>(
                    SourcePath(candidate.sceneId)) == null)
                    throw new FileNotFoundException(
                        "Build source serialized graph prefabs first: " +
                        SourcePath(candidate.sceneId));
                if (AssetDatabase.LoadAssetAtPath<GameObject>(
                        TargetPath(candidate.sceneId)) != null ||
                    File.Exists(TargetScene(candidate.sceneId)))
                    throw new IOException("Study Prefab/Scene already exists; " +
                        "refusing to overwrite local edits: " + candidate.sceneId);
            }
            if (count != 1108 || fields != 7451 ||
                countClasses.GetValueOrDefault("UnityEngine.UI.Image") != 1052 ||
                countClasses.GetValueOrDefault("UnityEngine.UI.CanvasScaler") != 4 ||
                countClasses.GetValueOrDefault("UnityEngine.UI.Mask") != 41 ||
                countClasses.GetValueOrDefault("UnityEngine.UI.ContentSizeFitter") != 11 ||
                countClasses.Count != 4)
                throw new InvalidDataException("Recovered UI class counts changed.");
            return (plan, graph);
        }

        private static Dictionary<string, ImportField> Fields(ImportComponent item) =>
            item.fields.ToDictionary(f => f.name);

        private static Image ApplyImage(GameObject gameObject,
            Dictionary<string, ImportField> f)
        {
            var image = gameObject.AddComponent<Image>();
            image.type = (Image.Type)f["m_Type"].intValue;
            image.preserveAspect = f["m_PreserveAspect"].boolValue;
            image.fillMethod = (Image.FillMethod)f["m_FillMethod"].intValue;
            image.fillOrigin = f["m_FillOrigin"].intValue;
            image.fillAmount = f["m_FillAmount"].floatValue;
            image.fillClockwise = f["m_FillClockwise"].boolValue;
            var color = f["m_Color"].floatValues;
            image.color = new Color(color[0], color[1], color[2], color[3]);
            return image;
        }

        private static CanvasScaler ApplyScaler(GameObject gameObject,
            Dictionary<string, ImportField> f)
        {
            // The source graph preflight proved this GameObject has native Canvas.
            // Its other native render settings remain unverified in THIS plan.
            if (gameObject.GetComponent<Canvas>() == null)
                gameObject.AddComponent<Canvas>();
            var scaler = gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = (CanvasScaler.ScaleMode)f["m_UiScaleMode"].intValue;
            scaler.screenMatchMode = (CanvasScaler.ScreenMatchMode)
                f["m_ScreenMatchMode"].intValue;
            var resolution = f["m_ReferenceResolution"].floatValues;
            scaler.referenceResolution = new Vector2(resolution[0], resolution[1]);
            scaler.matchWidthOrHeight = f["m_MatchWidthOrHeight"].floatValue;
            scaler.scaleFactor = f["m_ScaleFactor"].floatValue;
            scaler.referencePixelsPerUnit = f["m_ReferencePixelsPerUnit"].floatValue;
            return scaler;
        }

        private static Behaviour ApplyManaged(GameObject go, ImportComponent source)
        {
            var f = Fields(source);
            Behaviour result;
            switch (source.className)
            {
                case "UnityEngine.UI.Image":
                    result = ApplyImage(go, f);
                    break;
                case "UnityEngine.UI.CanvasScaler":
                    result = ApplyScaler(go, f);
                    break;
                case "UnityEngine.UI.Mask":
                    var mask = go.AddComponent<Mask>();
                    mask.showMaskGraphic = f["m_ShowMaskGraphic"].boolValue;
                    result = mask;
                    break;
                case "UnityEngine.UI.ContentSizeFitter":
                    var fit = go.AddComponent<ContentSizeFitter>();
                    fit.horizontalFit = (ContentSizeFitter.FitMode)
                        f["m_HorizontalFit"].intValue;
                    fit.verticalFit = (ContentSizeFitter.FitMode)
                        f["m_VerticalFit"].intValue;
                    result = fit;
                    break;
                default:
                    throw new InvalidDataException("Unsupported managed class.");
            }
            result.enabled = source.enabled;
            return result;
        }

        private static void ApplySceneToCopy(ImportPlan plan, ImportScene scene)
        {
            string source = SourcePath(scene.sceneId);
            var copy = PrefabUtility.LoadPrefabContents(source);
            if (copy == null) throw new IOException("Cannot open source study prefab.");
            try
            {
                var map = copy.GetComponentsInChildren<OriginalSerializedEvidence>(true)
                    .ToDictionary(note => note.rectTransformPathId);
                foreach (var record in scene.components.OrderBy(x =>
                    x.className == "UnityEngine.UI.CanvasScaler" ? 0 :
                    x.className == "UnityEngine.UI.Image" ? 1 :
                    x.className == "UnityEngine.UI.ContentSizeFitter" ? 2 : 3))
                {
                    if (!map.TryGetValue(record.rectTransformPathId, out var original) ||
                        original.gameObjectPathId != record.gameObjectPathId ||
                        original.originalComponentPathIds == null ||
                        !original.originalComponentPathIds.Contains(record.componentPathId))
                        throw new InvalidDataException(
                            "Local prefab object differs from original source identity.");
                    var go = original.gameObject;
                    ApplyManaged(go, record);
                    var note = go.AddComponent<ManagedUiSourceEvidence>();
                    note.sourceSceneId = scene.sceneId;
                    note.originalClassName = record.className;
                    note.sourceGameObjectPathId = record.gameObjectPathId;
                    note.sourceRectTransformPathId = record.rectTransformPathId;
                    note.sourceMonoBehaviourPathId = record.componentPathId;
                    note.sourceObjectSha256 = record.rawObjectSha256;
                    note.graphSha256 = plan.sourceGraphSha256;
                    note.libil2cppSha256 = plan.sourceLibrarySha256;
                    note.metadataSha256 = plan.sourceMetadataSha256;
                    note.exactTwoBackendFieldAgreement = true;
                    note.verifiedFieldNames = record.fields.Select(x => x.name).ToArray();
                }
                var saved = PrefabUtility.SaveAsPrefabAsset(copy, TargetPath(scene.sceneId));
                if (saved == null)
                    throw new IOException("Failed to save study prefab: " + scene.sceneId);
            }
            finally
            {
                PrefabUtility.UnloadPrefabContents(copy);
            }
            var preview = EditorSceneManager.NewScene(
                NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(TargetPath(scene.sceneId));
            if (asset == null || PrefabUtility.InstantiatePrefab(asset) == null ||
                !EditorSceneManager.SaveScene(preview, TargetScene(scene.sceneId)))
                throw new IOException("Failed to save verified study scene.");
        }

        private static void EnsureFolder(string parent, string name)
        {
            if (!AssetDatabase.IsValidFolder(parent + "/" + name))
                AssetDatabase.CreateFolder(parent, name);
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/Apply cross-verified fields to study prefab copies")]
        public static void Build()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;
            var created = new List<string>();
            try
            {
                var source = ReadAndValidate();
                EnsureFolder("Assets", "LocalReconstruction");
                EnsureFolder(Root, "VerifiedManagedFieldPrefabs");
                EnsureFolder(Root, "VerifiedManagedFieldScenes");
                foreach (var scene in source.Item1.scenes)
                {
                    // Track only newly created study destinations for rollback;
                    // never delete the original source-graph evidence prefabs.
                    created.Add(TargetPath(scene.sceneId));
                    created.Add(TargetScene(scene.sceneId));
                    ApplySceneToCopy(source.Item1, scene);
                }
                AssetDatabase.SaveAssets();
                Debug.Log("[HaiTac 3C] PASS: 5 independent STUDY prefab copies, " +
                    "1108 source UI components, 7451 dual-backend matched fields. " +
                    "Excluded 93 single-backend components / 651 fields. " +
                    "No original source-graph Prefabs changed; Play Mode not proven.");
                EditorUtility.DisplayDialog("Cross-verified UI field study copies",
                    "Created 5 NEW study prefabs and scenes under LocalReconstruction. " +
                    "Applied 7451 exact two-backend field values. " +
                    "651 single-backend fields intentionally excluded. " +
                    "Review native Canvas settings, sprites, layouts and Play Mode " +
                    "in Unity Editor before using these for a viewer.", "OK");
            }
            catch (Exception exc)
            {
                foreach (string path in created)
                    AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("Strict UI field import BLOCKED",
                    exc.GetBaseException().Message +
                    "\nAny newly generated study outputs were rolled back. " +
                    "Source graph Prefabs were not modified.", "OK");
            }
        }
    }
}
#endif
