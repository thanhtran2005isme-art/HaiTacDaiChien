#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HaiTac.OfflineViewer;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// Restores a hierarchy of Unity uGUI objects from serialized evidence, not
    /// Unity's original prefab or game source. Generated artifacts stay ignored.
    /// No synthetic placeholder rectangles, fallback characters or random skins.
    /// </summary>
    public static class UnityCanvasReconstructor
    {
        private const string LocalRoot = "Assets/LocalReconstruction";
        private const string SpriteFolder = LocalRoot + "/Sprites";
        private const string PrefabFolder = LocalRoot + "/Prefabs";
        private const string SceneFolder = LocalRoot + "/Scenes";
        private static readonly Regex SafePng = new Regex(@"^[0-9a-f]{32}\.png$");
        private static readonly Regex SafeSceneId = new Regex(@"^REF[0-9A-Za-z-]+$");

        [Serializable] private class Plan
        {
            public int schemaVersion;
            public SpriteEntry[] sprites;
            public SpineEntry[] spine;
        }

        [Serializable] private class SpriteEntry
        {
            public string sceneId;
            public int nodeId;
            public string spriteFile;
        }

        [Serializable] private class SpineEntry
        {
            public string sceneId;
            public int nodeId;
            public string componentId;
            public string componentClass;
            public string bindingStatus;
            public int candidateCount;
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Reconstruct 5 local Canvas prefabs")]
        public static void ReconstructFromLocalXapk()
        {
            if (EditorApplication.isPlaying)
            {
                Debug.LogError("Exit Play Mode before rebuilding Canvas prefabs.");
                return;
            }
            try
            {
                var root = Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
                var sceneFile = Path.Combine(Application.dataPath, "StreamingAssets", "ui-scenes.json");
                var planFile = Path.Combine(root, "output", "unity-prefab-map.json");
                var artFolder = Path.Combine(root, "output", "local-ui-art");
                if (!File.Exists(sceneFile) || !File.Exists(planFile))
                    throw new IOException("Missing source scene metadata or local reconstruction plan. " +
                        "Run CHUAN_BI_DO_HOA.bat and then py -3 tools/build_unity_prefab_manifest.py.");
                var scenes = JsonUtility.FromJson<ViewerDatabase>(File.ReadAllText(sceneFile));
                var plan = JsonUtility.FromJson<Plan>(File.ReadAllText(planFile));
                if (scenes == null || scenes.schemaVersion != 1 || scenes.scenes == null ||
                    plan == null || plan.schemaVersion != 1 || plan.sprites == null ||
                    plan.spine == null)
                    throw new InvalidDataException("Invalid reconstruction input schema.");
                EnsureFolders();
                var sprites = ImportSprites(plan.sprites, artFolder);
                var art = new Dictionary<string, Dictionary<int, Sprite>>();
                foreach (var entry in plan.sprites)
                {
                    if (!sprites.TryGetValue(entry.spriteFile, out var sprite)) continue;
                    if (!art.TryGetValue(entry.sceneId, out var nodes))
                    {
                        nodes = new Dictionary<int, Sprite>();
                        art.Add(entry.sceneId, nodes);
                    }
                    if (nodes.ContainsKey(entry.nodeId))
                        throw new InvalidDataException("Duplicate Sprite mapping: " + entry.sceneId +
                                                       "/" + entry.nodeId);
                    nodes.Add(entry.nodeId, sprite);
                }
                int generated = 0;
                foreach (var scene in scenes.scenes)
                {
                    if (!SafeSceneId.IsMatch(scene.id) || scene.nodes == null ||
                        scene.nodes.Length == 0 || scene.nodes[0].id != scene.rootTransform)
                        throw new InvalidDataException("Invalid scene root or ID: " + scene.id);
                    var sceneSpine = plan.spine.Where(e => e.sceneId == scene.id).ToArray();
                    art.TryGetValue(scene.id, out var sceneArt);
                    BuildPrefabAndScene(scene, sceneArt, sceneSpine);
                    generated++;
                }
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("Completed: " + generated + " local Canvas prefabs and scenes, " +
                    sprites.Count + " imported Sprite sources. Real animation bindings remain unverified. " +
                    "All files stay under gitignored " + LocalRoot);
                EditorUtility.DisplayDialog("HaiTac Unity reconstruction",
                    generated + " Canvas prefabs and scenes generated in " + LocalRoot +
                    ".\nSprite image evidence is preserved. No fake Spine animation was attached." +
                    "\nOpen any generated scene to inspect the actual uGUI hierarchy.", "OK");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                EditorUtility.DisplayDialog("Reconstruction blocked", e.Message, "OK");
            }
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Audit 5 generated Canvas scenes")]
        public static void AuditGeneratedScenes()
        {
            if (EditorApplication.isPlaying)
            {
                Debug.LogWarning("Stop Play Mode before auditing local reconstructed scenes.");
                return;
            }
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            var original = EditorSceneManager.GetActiveScene().path;
            int checkedScenes = 0;
            var issues = new List<string>();
            try
            {
                var infoPath = Path.Combine(Application.dataPath, "StreamingAssets", "ui-scenes.json");
                if (!File.Exists(infoPath))
                    throw new FileNotFoundException("Missing original UI scene metadata.", infoPath);
                var database = JsonUtility.FromJson<ViewerDatabase>(File.ReadAllText(infoPath));
                if (database == null || database.scenes == null || database.scenes.Length != 5)
                    throw new InvalidDataException("Expected exactly five source-derived UI scenes.");
                foreach (var sceneInfo in database.scenes)
                {
                    if (!SafeSceneId.IsMatch(sceneInfo.id))
                        throw new InvalidDataException("Invalid scene ID.");
                    var path = SceneFolder + "/" + sceneInfo.id + ".unity";
                    if (!File.Exists(path))
                    {
                        issues.Add(sceneInfo.id + ": generated scene file missing");
                        continue;
                    }
                    var opened = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                    checkedScenes++;
                    var roots = opened.GetRootGameObjects();
                    int cameras = roots.Sum(go => go.GetComponentsInChildren<Camera>(true)
                        .Count(c => c.enabled && c.gameObject.activeInHierarchy));
                    var canvases = roots.SelectMany(go => go.GetComponentsInChildren<Canvas>(true))
                        .Where(c => c.enabled && c.gameObject.activeInHierarchy).ToArray();
                    int art = roots.Sum(go => go.GetComponentsInChildren<Image>(true)
                        .Count(image => image.sprite != null));
                    bool zeroRoot = canvases.Any(c => c.transform.localScale.x == 0f ||
                                                      c.transform.localScale.y == 0f);
                    if (cameras == 0) issues.Add(sceneInfo.id + ": no preview Camera");
                    if (canvases.Length == 0) issues.Add(sceneInfo.id + ": no active Canvas");
                    if (zeroRoot) issues.Add(sceneInfo.id + ": collapsed zero-scale Canvas root");
                    if (art == 0) issues.Add(sceneInfo.id + ": no decoded Sprite Images");
                    Debug.Log("[HaiTac scene audit] " + sceneInfo.id + ": " + cameras +
                        " Cameras, " + canvases.Length + " active Canvases, " + art +
                        " Sprite Images, zero-root=" + zeroRoot);
                }
            }
            catch (Exception error)
            {
                issues.Add(error.Message);
                Debug.LogException(error);
            }
            finally
            {
                if (!string.IsNullOrEmpty(original) && File.Exists(original))
                    EditorSceneManager.OpenScene(original, OpenSceneMode.Single);
            }
            string summary = checkedScenes + "/5 generated scenes audited. " +
                (issues.Count == 0 ? "No structural rendering blockers found." :
                    string.Join("\\n", issues.ToArray()));
            if (issues.Count > 0) Debug.LogError("[HaiTac scene audit] " + summary);
            EditorUtility.DisplayDialog("HaiTac reconstructed scene audit", summary, "OK");
        }

        private static void EnsureFolder(string parent, string name)
        {
            string path = parent + "/" + name;
            if (!AssetDatabase.IsValidFolder(path))
                AssetDatabase.CreateFolder(parent, name);
        }

        private static void EnsureFolders()
        {
            EnsureFolder("Assets", "LocalReconstruction");
            EnsureFolder(LocalRoot, "Sprites");
            EnsureFolder(LocalRoot, "Prefabs");
            EnsureFolder(LocalRoot, "Scenes");
        }

        private static Dictionary<string, Sprite> ImportSprites(SpriteEntry[] entries, string source)
        {
            var output = new Dictionary<string, Sprite>(StringComparer.Ordinal);
            foreach (var name in entries.Select(e => e.spriteFile).Distinct())
            {
                if (!SafePng.IsMatch(name))
                    throw new InvalidDataException("Unapproved Sprite name in plan.");
                var path = Path.Combine(source, name);
                if (!File.Exists(path))
                    throw new FileNotFoundException("Missing decoded local Sprite: " + name);
                var asset = SpriteFolder + "/" + name;
                var destination = Path.Combine(Application.dataPath, "LocalReconstruction", "Sprites", name);
                File.Copy(path, destination, true); // Artwork is kept in ignored local Assets only.
                AssetDatabase.ImportAsset(asset, ImportAssetOptions.ForceSynchronousImport);
                var importer = AssetImporter.GetAtPath(asset) as TextureImporter;
                if (importer == null)
                    throw new InvalidDataException("Sprite importer unavailable: " + name);
                if (importer.textureType != TextureImporterType.Sprite ||
                    importer.spriteImportMode != SpriteImportMode.Single)
                {
                    importer.textureType = TextureImporterType.Sprite;
                    importer.spriteImportMode = SpriteImportMode.Single;
                    importer.alphaIsTransparency = true;
                    importer.SaveAndReimport();
                }
                var loaded = AssetDatabase.LoadAssetAtPath<Sprite>(asset);
                if (loaded == null)
                    throw new InvalidDataException("Failed to import decoded Sprite: " + name);
                output.Add(name, loaded);
            }
            return output;
        }

        private static Vector2 ReadVector(float[] value, Vector2 fallback)
        {
            return value != null && value.Length == 2 &&
                   !float.IsNaN(value[0]) && !float.IsNaN(value[1])
                ? new Vector2(value[0], value[1]) : fallback;
        }

        private static bool HasType(ViewerNode node, string type)
        {
            return node.types != null && Array.IndexOf(node.types, type) >= 0;
        }

        private static void BuildPrefabAndScene(
            ViewerScene scene, Dictionary<int, Sprite> art, SpineEntry[] spine)
        {
            var go = new GameObject("ReconstructedCandidate_" + scene.id,
                typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler),
                typeof(GraphicRaycaster));
            try
            {
                var rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(1600, 900); // Provisional, NOT original runtime resolution.
                var canvas = go.GetComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                var scaler = go.GetComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1600, 900);
                var evidence = go.AddComponent<ReconstructionEvidence>();
                evidence.serializedSource = scene.source + " / root " + scene.rootTransform;
                evidence.limitations =
                    "The serialized root may have localScale=(0,0) because the original Canvas " +
                    "runtime state is unknown. The preview normalizes only the Canvas root " +
                    "to (1,1); its children retain their recorded transforms. " +
                    "Canvas and RectTransforms reconstructed from serialized XAPK metadata. " +
                    "This is not the original prefab. 1600x900 is assumed; original CanvasScaler, " +
                    "UI LayoutGroup resolution, masking, nine-slice and runtime state are unverified. " +
                    "Quaternion z metadata is not a rotation angle; rotation is not guessed.";
                var map = new Dictionary<int, RectTransform>();
                foreach (var node in scene.nodes)
                {
                    if (node.id == scene.rootTransform)
                    {
                        // The standalone Canvas *is* the serialized root. Do not
                        // clone its RectTransform as a child: 4 of the 5 XAPK UI
                        // reference roots report localScale=(0,0), which would
                        // collapse every descendant Sprite in a reconstructed UI.
                        // We normalize only the preview root, never asset children.
                        map.Add(node.id, rect);
                        if (art != null && art.TryGetValue(node.id, out var rootSprite))
                        {
                            var image = go.AddComponent<Image>();
                            image.sprite = rootSprite;
                            image.raycastTarget = false;
                        }
                        continue;
                    }
                    if (!map.TryGetValue(node.parent, out var parent))
                        throw new InvalidDataException("Missing parent transform: " + scene.id +
                                                       " / " + node.id);
                    var child = new GameObject(node.name, typeof(RectTransform));
                    var transform = child.GetComponent<RectTransform>();
                    transform.SetParent(parent, false);
                    transform.anchorMin = ReadVector(node.a0, new Vector2(.5f, .5f));
                    transform.anchorMax = ReadVector(node.a1, new Vector2(.5f, .5f));
                    transform.pivot = ReadVector(node.pivot, new Vector2(.5f, .5f));
                    transform.sizeDelta = ReadVector(node.delta, Vector2.zero);
                    transform.anchoredPosition = ReadVector(node.position, Vector2.zero);
                    Vector2 scale = ReadVector(node.scale, Vector2.one);
                    transform.localScale = new Vector3(scale.x, scale.y, 1f);
                    // No rotation guess: ui-scenes.rotationZ is the source quaternion's Z component.
                    child.SetActive(node.active);
                    map.Add(node.id, transform);
                    if (art != null && art.TryGetValue(node.id, out var sprite))
                    {
                        var image = child.AddComponent<Image>();
                        image.sprite = sprite;
                        image.color = Color.white;
                        image.raycastTarget = false;
                        image.type = Image.Type.Simple; // Original 9-slice setting not confirmed.
                    }
                    if (HasType(node, "RectMask2D"))
                        child.AddComponent<RectMask2D>();
                    // No callbacks or business behavior are inferred for Button/Text.
                }
                // Restore recorded sibling order for each direct parent, not global Z sorting.
                foreach (var group in scene.nodes.GroupBy(n => n.parent))
                {
                    int index = 0;
                    foreach (var node in group.OrderBy(n => n.order).ThenBy(n => n.id))
                        if (node.id != scene.rootTransform &&
                            map.TryGetValue(node.id, out var transform))
                            transform.SetSiblingIndex(index++);
                }
                foreach (var row in spine)
                {
                    if (!map.TryGetValue(row.nodeId, out var target))
                        throw new InvalidDataException("Spine GameObject not found: " + row.nodeId);
                    var note = target.gameObject.AddComponent<SpineReferenceEvidence>();
                    note.originalClass = row.componentClass;
                    note.originalComponentId = row.componentId;
                    note.bindingStatus = row.bindingStatus;
                    note.possibleSkeletonDataAssets = row.candidateCount;
                }
                string filename = scene.id;
                string prefabPath = PrefabFolder + "/" + filename + ".prefab";
                var prefab = PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                if (prefab == null)
                    throw new IOException("Could not save local prefab " + prefabPath);
                var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                            NewSceneMode.Single);
                // Even ScreenSpaceOverlay UI benefits from a scene Camera: without
                // one Unity Game View reports "No cameras rendering" in Edit Mode.
                // It is a LOCAL preview camera, not evidence of the game's camera.
                var cameraObject = new GameObject("Local Preview Camera", typeof(Camera));
                cameraObject.tag = "MainCamera";
                cameraObject.transform.position = new Vector3(0f, 0f, -10f);
                var previewCamera = cameraObject.GetComponent<Camera>();
                previewCamera.orthographic = true;
                previewCamera.clearFlags = CameraClearFlags.SolidColor;
                previewCamera.backgroundColor = new Color(.045f, .075f, .115f);
                previewCamera.nearClipPlane = .1f;
                previewCamera.farClipPlane = 100f;
                var instance = PrefabUtility.InstantiatePrefab(prefab, newScene) as GameObject;
                if (instance == null || instance.GetComponent<Canvas>() == null)
                    throw new InvalidDataException("Canvas prefab not instantiated: " + filename);
                // Fail fast before saving a broken scene; see both inactive and
                // active Image nodes, since original runtime activation is unknown.
                int spriteCount = instance.GetComponentsInChildren<Image>(true)
                    .Count(image => image.sprite != null);
                int expectedSprites = art != null ? art.Count : 0;
                if (spriteCount != expectedSprites)
                    throw new InvalidDataException("Sprite count mismatch for " + filename +
                        ": expected " + expectedSprites + ", found " + spriteCount);
                if (instance.transform.localScale.x == 0f ||
                    instance.transform.localScale.y == 0f)
                    throw new InvalidDataException("Zero-scale reconstructed Canvas: " + filename);
                if (!newScene.GetRootGameObjects().SelectMany(root =>
                      root.GetComponentsInChildren<Camera>(true)).Any(camera =>
                      camera.enabled && camera.gameObject.activeInHierarchy))
                    throw new InvalidDataException("Missing working preview Camera: " + filename);
                if (!EditorSceneManager.SaveScene(newScene, SceneFolder + "/" + filename + ".unity"))
                    throw new IOException("Could not save local scene " + filename);
                Debug.Log("[HaiTac] Saved local scene " + filename + " with " +
                    spriteCount + " source-linked Sprite Images and 1 preview Camera.");
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(go);
            }
        }
    }
}
#endif
