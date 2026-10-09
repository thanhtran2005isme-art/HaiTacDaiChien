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

        // Optional evidence file is generated locally from the real XAPK,
        // never from screenshot positions. JsonUtility ignores unknown fields.
        [Serializable] private class LayoutDatabase
        {
            public int version;
            public LayoutScene[] scenes;
        }

        [Serializable] private class LayoutScene
        {
            public string sceneId;
            public LayoutNode[] nodes;
            public LayoutCanvas[] canvases;
            public LayoutImage[] images;
        }

        [Serializable] private class LayoutNode
        {
            public int nodeId;
            public float[] rotation;
            public float[] localScale;
            public float localPositionZ;
            public bool hasLocalPositionZ;
        }

        [Serializable] private class LayoutCanvas
        {
            public int nodeId;
            public bool hasSortingOrder;
            public int sortingOrder;
            public bool hasOverrideSorting;
            public bool overrideSorting;
            public bool hasPixelPerfect;
            public bool pixelPerfect;
        }

        [Serializable] private class LayoutImage
        {
            public int nodeId;
            public bool hasType;
            public int type;
            public bool hasPreserveAspect;
            public bool preserveAspect;
            public bool hasFillMethod;
            public int fillMethod;
            public bool hasFillAmount;
            public float fillAmount;
            public bool hasFillOrigin;
            public int fillOrigin;
            public bool hasFillClockwise;
            public bool fillClockwise;
            public bool hasRaycastTarget;
            public bool raycastTarget;
            public bool hasColor;
            public float[] color;
        }

        [Serializable] private class DeepUiDatabase
        {
            public int version;
            public DeepUiScene[] scenes;
        }

        [Serializable] private class DeepUiScene
        {
            public string sceneId;
            public DeepSprite[] spriteGeometry;
            public DeepUiComponent[] components;
        }

        [Serializable] private class DeepSprite
        {
            public int nodeId;
            public int spriteId;
            public float[] border;
            public float pixelsPerUnit;
            public float[] sourceRectSize;
        }

        [Serializable] private class DeepUiComponent
        {
            public int nodeId;
            public int componentId;
            public string className;
            public string status;
        }

        [Serializable] private class SpineLinkDatabase
        {
            public int version;
            public SpineLink[] records;
        }

        [Serializable] private class SpineLink
        {
            public string sceneId;
            public int nodeId;
            public string componentId;
            public string status;
            public string skeletonName;
            public string atlasName;
            public string spineVersion;
            public string localPackId;
            public string[] animationNames;
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
                // Strict validation: do not silently bind stale evidence to
                // GameObjects whose IDs have changed in a different XAPK.
                var verifiedLayout = ReadLayoutEvidence(root, scenes);
                var componentEvidence = ReadDeepUiEvidence(root, scenes);
                var spineEvidence = ReadSpineEvidence(root, plan);
                EnsureFolders();
                var sprites = ImportSprites(plan.sprites, artFolder);
                ApplyOriginalSpriteGeometry(plan.sprites, componentEvidence, sprites);
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
                    verifiedLayout.TryGetValue(scene.id, out var nativeLayout);
                    componentEvidence.TryGetValue(scene.id, out var sceneComponents);
                    BuildPrefabAndScene(scene, sceneArt, sceneSpine, nativeLayout,
                                        sceneComponents, spineEvidence);
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
                    string.Join("\n", issues.ToArray()));
            if (issues.Count > 0) Debug.LogError("[HaiTac scene audit] " + summary);
            EditorUtility.DisplayDialog("HaiTac reconstructed scene audit", summary, "OK");
        }

        private static bool HasWorkingCamera(Scene scene)
        {
            return scene.GetRootGameObjects().SelectMany(root =>
                root.GetComponentsInChildren<Camera>(true)).Any(camera =>
                camera.enabled && camera.gameObject.activeInHierarchy);
        }

        private static Camera EnsurePreviewCamera(Scene scene)
        {
            var existing = scene.GetRootGameObjects().SelectMany(root =>
                root.GetComponentsInChildren<Camera>(true)).FirstOrDefault(camera =>
                camera.enabled && camera.gameObject.activeInHierarchy);
            if (existing != null) return existing;

            var cameraObject = new GameObject("Local Preview Camera", typeof(Camera));
            if (cameraObject.scene != scene)
                SceneManager.MoveGameObjectToScene(cameraObject, scene);
            cameraObject.tag = "MainCamera";
            cameraObject.transform.position = new Vector3(0f, 0f, -10f);
            cameraObject.transform.rotation = Quaternion.identity;
            cameraObject.transform.localScale = Vector3.one;
            cameraObject.SetActive(true);
            var camera = cameraObject.GetComponent<Camera>();
            camera.enabled = true;
            camera.orthographic = true;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(.045f, .075f, .115f);
            camera.nearClipPlane = .1f;
            camera.farClipPlane = 100f;
            return camera;
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Repair existing 5 scenes (Camera + Canvas scale)")]
        public static void RepairExistingScenes()
        {
            if (EditorApplication.isPlaying)
            {
                EditorUtility.DisplayDialog("HaiTac repair",
                    "Stop Play Mode before repairing generated scenes.", "OK");
                return;
            }
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            var originalPath = EditorSceneManager.GetActiveScene().path;
            var errors = new List<string>();
            int repaired = 0;
            try
            {
                string metadata = Path.Combine(Application.dataPath, "StreamingAssets",
                                               "ui-scenes.json");
                var db = JsonUtility.FromJson<ViewerDatabase>(File.ReadAllText(metadata));
                if (db == null || db.scenes == null || db.scenes.Length != 5)
                    throw new InvalidDataException("Expected 5 Unity scene candidates.");
                foreach (var candidate in db.scenes)
                {
                    try
                    {
                        if (!SafeSceneId.IsMatch(candidate.id))
                            throw new InvalidDataException("Invalid candidate scene ID.");
                        string path = SceneFolder + "/" + candidate.id + ".unity";
                        if (!File.Exists(path))
                            throw new FileNotFoundException("Generated Unity scene missing: " + path);
                        var opened = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                        var canvas = opened.GetRootGameObjects().SelectMany(root =>
                            root.GetComponentsInChildren<Canvas>(true))
                            .FirstOrDefault(x => x.GetComponent<ReconstructionEvidence>() != null);
                        if (canvas == null)
                            throw new InvalidDataException("No reconstructed Canvas found.");
                        // Old generated scenes contain a duplicate zero-scale Canvas
                        // child, which hides all 4 affected scenes. Correct only the
                        // direct child whose name matches the serialized root.
                        var root = canvas.transform;
                        root.localScale = Vector3.one;
                        if (candidate.nodes != null && candidate.nodes.Length > 0)
                        {
                            string sourceRoot = candidate.nodes[0].name;
                            foreach (Transform child in root)
                            {
                                if (child.name == sourceRoot &&
                                    (Mathf.Approximately(child.localScale.x, 0f) ||
                                     Mathf.Approximately(child.localScale.y, 0f)))
                                {
                                    child.localScale = Vector3.one;
                                }
                            }
                        }
                        EnsurePreviewCamera(opened);
                        if (!HasWorkingCamera(opened))
                            throw new InvalidDataException("Could not create active preview Camera.");
                        EditorSceneManager.MarkSceneDirty(opened);
                        if (!EditorSceneManager.SaveScene(opened, path))
                            throw new IOException("Failed to save scene " + path);
                        repaired++;
                        Debug.Log("[HaiTac repair] " + candidate.id +
                            ": Canvas root scale normalized, preview Camera present.");
                    }
                    catch (Exception error)
                    {
                        errors.Add(candidate.id + ": " + error.Message);
                        Debug.LogException(error);
                    }
                }
            }
            catch (Exception error)
            {
                errors.Add(error.Message);
                Debug.LogException(error);
            }
            finally
            {
                if (!string.IsNullOrEmpty(originalPath) && File.Exists(originalPath))
                    EditorSceneManager.OpenScene(originalPath, OpenSceneMode.Single);
            }
            var result = "Saved " + repaired + "/5 repaired scenes." +
                (errors.Count > 0 ? "\n" + string.Join("\n", errors.ToArray()) :
                "\nAll saved scenes now have a Camera and nonzero preview Canvas root.");
            EditorUtility.DisplayDialog("HaiTac scene repair", result, "OK");
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

        private static Dictionary<string, DeepUiScene> ReadDeepUiEvidence(
            string root, ViewerDatabase source)
        {
            var output = new Dictionary<string, DeepUiScene>();
            string file = Path.Combine(root, "output", "local-ui-components.json");
            if (!File.Exists(file))
            {
                Debug.LogWarning("[HaiTac] No deep UI component evidence. " +
                    "Run py -3 tools/audit_local_ui_components.py.");
                return output;
            }
            var evidence = JsonUtility.FromJson<DeepUiDatabase>(File.ReadAllText(file));
            if (evidence == null || evidence.version != 1 || evidence.scenes == null ||
                evidence.scenes.Length != source.scenes.Length)
                throw new InvalidDataException("Deep UI evidence version/scene count mismatch.");
            foreach (var scene in evidence.scenes)
            {
                var expected = source.scenes.FirstOrDefault(x => x.id == scene.sceneId);
                if (expected == null || scene.spriteGeometry == null || scene.components == null ||
                    output.ContainsKey(scene.sceneId))
                    throw new InvalidDataException("Unknown or duplicate deep evidence scene.");
                var ids = new HashSet<int>(expected.nodes.Select(x => x.id));
                if (scene.spriteGeometry.Any(x => !ids.Contains(x.nodeId)) ||
                    scene.components.Any(x => !ids.Contains(x.nodeId)))
                    throw new InvalidDataException("Stale deep evidence node ID: " + scene.sceneId);
                output.Add(scene.sceneId, scene);
            }
            return output;
        }

        private static Dictionary<string, SpineLink> ReadSpineEvidence(
            string root, Plan plan)
        {
            var output = new Dictionary<string, SpineLink>(StringComparer.Ordinal);
            string file = Path.Combine(root, "output", "local-spine-link-evidence.json");
            if (!File.Exists(file)) return output;
            var evidence = JsonUtility.FromJson<SpineLinkDatabase>(File.ReadAllText(file));
            if (evidence == null || evidence.version != 1 || evidence.records == null)
                throw new InvalidDataException("Spine link evidence invalid.");
            var possible = new HashSet<string>(plan.spine.Select(s =>
                s.sceneId + "/" + s.nodeId + "/" + s.componentId));
            foreach (var item in evidence.records)
            {
                var key = item.sceneId + "/" + item.nodeId + "/" + item.componentId;
                if (!possible.Contains(key) || output.ContainsKey(key))
                    throw new InvalidDataException("Stale or duplicate Spine component evidence: " + key);
                output.Add(key, item);
            }
            Debug.Log("[HaiTac] Read " + output.Count +
                " exact GameObject-linked Spine evidence rows. " +
                "Binding and default animation remain unverified.");
            return output;
        }

        private static void ApplyOriginalSpriteGeometry(
            SpriteEntry[] plan, Dictionary<string, DeepUiScene> evidence,
            Dictionary<string, Sprite> sprites)
        {
            var names = plan.ToDictionary(row => row.sceneId + "/" + row.nodeId,
                                          row => row.spriteFile);
            var patches = new Dictionary<string, List<DeepSprite>>();
            foreach (var scene in evidence.Values)
            {
                foreach (var record in scene.spriteGeometry)
                {
                    if (!names.TryGetValue(scene.sceneId + "/" + record.nodeId,
                                           out var filename)) continue;
                    if (!patches.TryGetValue(filename, out var list))
                    {
                        list = new List<DeepSprite>();
                        patches.Add(filename, list);
                    }
                    list.Add(record);
                }
            }
            int changed = 0;
            foreach (var entry in patches)
            {
                if (!sprites.TryGetValue(entry.Key, out var sprite)) continue;
                var variants = entry.Value;
                if (variants.Count == 0 || variants.Any(v => v.border == null ||
                    v.border.Length != 4 || v.sourceRectSize == null ||
                    v.sourceRectSize.Length != 2))
                    continue;
                var sample = variants[0];
                if (variants.Any(v => !v.border.SequenceEqual(sample.border) ||
                      !v.sourceRectSize.SequenceEqual(sample.sourceRectSize) ||
                      Mathf.Abs(v.pixelsPerUnit - sample.pixelsPerUnit) > 0.001f))
                    continue;
                if (Mathf.Abs(sprite.rect.width - sample.sourceRectSize[0]) > 1f ||
                    Mathf.Abs(sprite.rect.height - sample.sourceRectSize[1]) > 1f)
                    continue; // PNG geometry differs from serialized Sprite rectangle.
                var importer = AssetImporter.GetAtPath(SpriteFolder + "/" + entry.Key)
                               as TextureImporter;
                if (importer == null) continue;
                var border = new Vector4(sample.border[0], sample.border[1],
                                         sample.border[2], sample.border[3]);
                if (sample.pixelsPerUnit < 0.001f || sample.pixelsPerUnit > 10000f ||
                    border.x + border.z > sprite.rect.width ||
                    border.y + border.w > sprite.rect.height)
                    continue;
                importer.spriteBorder = border;
                importer.spritePixelsPerUnit = sample.pixelsPerUnit;
                importer.SaveAndReimport();
                sprites[entry.Key] =
                    AssetDatabase.LoadAssetAtPath<Sprite>(SpriteFolder + "/" + entry.Key);
                changed++;
            }
            Debug.Log("[HaiTac] Applied original verified Sprite border/PPU to " +
                changed + " local imported Sprite(s); unavailable variants untouched.");
        }

        private static Dictionary<string, LayoutScene> ReadLayoutEvidence(
            string root, ViewerDatabase source)
        {
            var evidence = new Dictionary<string, LayoutScene>();
            string filename = Path.Combine(root, "output", "local-ui-layout.json");
            if (!File.Exists(filename))
            {
                Debug.LogWarning("[HaiTac] Exact serialized layout evidence not present. " +
                    "Run py -3 tools/export_local_ui_layout.py from the repo root. " +
                    "Falling back to known 2D metadata without guessed rotation.");
                return evidence;
            }
            var db = JsonUtility.FromJson<LayoutDatabase>(File.ReadAllText(filename));
            if (db == null || db.version != 1 || db.scenes == null ||
                db.scenes.Length != source.scenes.Length)
                throw new InvalidDataException("Layout evidence has wrong version or scene count.");
            var expected = source.scenes.ToDictionary(scene => scene.id);
            foreach (var item in db.scenes)
            {
                if (item == null || !expected.TryGetValue(item.sceneId, out var original) ||
                    item.nodes == null || item.nodes.Length != original.nodes.Length ||
                    item.canvases == null || item.images == null ||
                    evidence.ContainsKey(item.sceneId))
                    throw new InvalidDataException("Stale or incomplete layout evidence: " +
                                                   (item == null ? "(null)" : item.sceneId));
                var ids = new HashSet<int>(original.nodes.Select(node => node.id));
                if (item.nodes.Select(node => node.nodeId).Distinct().Count() != ids.Count ||
                    item.nodes.Any(node => !ids.Contains(node.nodeId)) ||
                    item.canvases.Any(canvas => !ids.Contains(canvas.nodeId)) ||
                    item.images.Any(image => !ids.Contains(image.nodeId)))
                    throw new InvalidDataException("Layout evidence transform ID mismatch: " +
                                                   item.sceneId);
                evidence.Add(item.sceneId, item);
            }
            Debug.Log("[HaiTac] Verified XAPK layout evidence: " + evidence.Count +
                      " scenes; CanvasScaler runtime resolution still unknown.");
            return evidence;
        }

        private static Quaternion ReadRotation(float[] value)
        {
            if (value == null || value.Length != 4 ||
                value.Any(x => float.IsNaN(x) || float.IsInfinity(x)))
                return Quaternion.identity;
            var rotation = new Quaternion(value[0], value[1], value[2], value[3]);
            return rotation.normalized;
        }

        private static void ApplyCanvasSettings(Canvas canvas, LayoutCanvas record)
        {
            if (record == null) return;
            if (record.hasOverrideSorting) canvas.overrideSorting = record.overrideSorting;
            if (record.hasSortingOrder) canvas.sortingOrder = record.sortingOrder;
            if (record.hasPixelPerfect) canvas.pixelPerfect = record.pixelPerfect;
            // No guess at original render mode, CanvasScaler or camera reference.
        }

        private static void ApplyImageSettings(Image image, LayoutImage record)
        {
            if (record == null) return;
            if (record.hasColor && record.color != null && record.color.Length == 4)
                image.color = new Color(record.color[0], record.color[1],
                                        record.color[2], record.color[3]);
            if (record.hasPreserveAspect) image.preserveAspect = record.preserveAspect;
            if (record.hasRaycastTarget) image.raycastTarget = record.raycastTarget;
            // Sliced/Tiled need original sprite border/pixels-per-unit metadata,
            // which PNG extraction does not preserve. Never simulate their sizes.
            if (record.hasType && (record.type == 0 || record.type == 3))
                image.type = (Image.Type)record.type;
            if (image.type == Image.Type.Filled)
            {
                if (record.hasFillMethod && record.fillMethod >= 0 && record.fillMethod <= 4)
                    image.fillMethod = (Image.FillMethod)record.fillMethod;
                if (record.hasFillOrigin && record.fillOrigin >= 0 && record.fillOrigin <= 3)
                    image.fillOrigin = record.fillOrigin;
                if (record.hasFillAmount && record.fillAmount >= 0f && record.fillAmount <= 1f)
                    image.fillAmount = record.fillAmount;
                if (record.hasFillClockwise) image.fillClockwise = record.fillClockwise;
            }
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
            ViewerScene scene, Dictionary<int, Sprite> art, SpineEntry[] spine,
            LayoutScene layout, DeepUiScene deepUi,
            Dictionary<string, SpineLink> spineLinks)
        {
            var go = new GameObject("ReconstructedCandidate_" + scene.id,
                typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler),
                typeof(GraphicRaycaster));
            try
            {
                var rect = go.GetComponent<RectTransform>();
                // Explicitly set an identity root transform. A newly constructed
                // Unity UI RectTransform can serialize scale=(0,0,0) on some
                // Editor versions, even if source-root duplication is removed.
                // The 4 source Canvas roots with zero XY scale are NOT usable
                // directly as a standalone preview Canvas.
                rect.localScale = Vector3.one;
                rect.localRotation = Quaternion.identity;
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
                var mappedLayout = layout == null
                    ? new Dictionary<int, LayoutNode>()
                    : layout.nodes.ToDictionary(record => record.nodeId);
                var mappedImage = layout == null
                    ? new Dictionary<int, LayoutImage>()
                    : layout.images.GroupBy(record => record.nodeId)
                        .Where(group => group.Count() == 1)
                        .ToDictionary(group => group.Key, group => group.First());
                var mappedCanvas = layout == null
                    ? new Dictionary<int, LayoutCanvas>()
                    : layout.canvases.GroupBy(record => record.nodeId)
                        .Where(group => group.Count() == 1)
                        .ToDictionary(group => group.Key, group => group.First());
                if (mappedCanvas.TryGetValue(scene.rootTransform, out var originalCanvas))
                    ApplyCanvasSettings(canvas, originalCanvas);
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
                            if (mappedImage.TryGetValue(node.id, out var originalRootImage))
                                ApplyImageSettings(image, originalRootImage);
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
                    // Only apply a full quaternion when read directly from Unity
                    // serialized RectTransform; quaternion Z alone is not an angle.
                    if (mappedLayout.TryGetValue(node.id, out var sourceNode))
                    {
                        transform.localRotation = ReadRotation(sourceNode.rotation);
                        if (sourceNode.hasLocalPositionZ)
                            transform.anchoredPosition3D = new Vector3(
                                transform.anchoredPosition.x, transform.anchoredPosition.y,
                                sourceNode.localPositionZ);
                        if (sourceNode.localScale != null && sourceNode.localScale.Length == 3)
                            transform.localScale = new Vector3(sourceNode.localScale[0],
                                sourceNode.localScale[1], sourceNode.localScale[2]);
                    }
                    child.SetActive(node.active);
                    if (mappedCanvas.TryGetValue(node.id, out var nestedEvidence))
                        ApplyCanvasSettings(child.AddComponent<Canvas>(), nestedEvidence);
                    map.Add(node.id, transform);
                    if (art != null && art.TryGetValue(node.id, out var sprite))
                    {
                        var image = child.AddComponent<Image>();
                        image.sprite = sprite;
                        image.color = Color.white;
                        image.raycastTarget = false;
                        image.type = Image.Type.Simple; // Original 9-slice unknown.
                        if (mappedImage.TryGetValue(node.id, out var originalImage))
                            ApplyImageSettings(image, originalImage);
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
                if (deepUi != null)
                {
                    foreach (var component in deepUi.components)
                    {
                        if (!map.TryGetValue(component.nodeId, out var target))
                            throw new InvalidDataException("UI evidence node missing: " +
                                                           component.nodeId);
                        var note = target.gameObject.AddComponent<UiComponentEvidence>();
                        note.originalComponentId = component.componentId.ToString();
                        note.originalClass = component.className;
                        note.evidenceStatus = component.status;
                        note.serializedFields = component.status == "serialized_fields_available"
                            ? "Original fields available in local JSON; runtime reproduction " +
                              "must respect type-specific material and layout dependencies."
                            : "Original fields unavailable in the IL2CPP typetree; no layout guessed.";
                    }
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
                    var key = scene.id + "/" + row.nodeId + "/" + row.componentId;
                    if (spineLinks.TryGetValue(key, out var link))
                    {
                        note.contentEvidenceStatus = link.status;
                        note.candidateSkeletonName = link.skeletonName;
                        note.candidateAtlasName = link.atlasName;
                        note.candidateSpineVersion = link.spineVersion;
                        note.localPackId = link.localPackId;
                        note.availableAnimationCount =
                            link.animationNames == null ? 0 : link.animationNames.Length;
                        note.knownAnimationNames =
                            link.animationNames == null ? "" : string.Join(", ", link.animationNames);
                    }
                }
                // Ensure the root identity survives all intermediate UI
                // component creation and is serialized into the prefab asset.
                rect.localScale = Vector3.one;
                string filename = scene.id;
                string prefabPath = PrefabFolder + "/" + filename + ".prefab";
                var prefab = PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
                if (prefab == null)
                    throw new IOException("Could not save local prefab " + prefabPath);
                var newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                            NewSceneMode.Single);
                // Create Camera in the destination scene rather than assuming
                // the last-open scene will receive new GameObjects.
                EnsurePreviewCamera(newScene);
                var instance = PrefabUtility.InstantiatePrefab(prefab, newScene) as GameObject;
                if (instance == null || instance.GetComponent<Canvas>() == null)
                    throw new InvalidDataException("Canvas prefab not instantiated: " + filename);
                // The prefab and instance may be independently normalized when
                // Unity restores serialized scene references.
                instance.transform.localScale = Vector3.one;
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
                if (!HasWorkingCamera(newScene))
                    throw new InvalidDataException("Missing working preview Camera: " + filename);
                if (!EditorSceneManager.SaveScene(newScene, SceneFolder + "/" + filename + ".unity"))
                    throw new IOException("Could not save local scene " + filename);
                Debug.Log("[HaiTac] Saved local scene " + filename + " with " +
                    spriteCount + " source-linked Sprite Images and 1 preview Camera. " +
                    (layout == null ? "No optional XAPK layout evidence; using legacy XY." :
                    "Verified source transforms=" + layout.nodes.Length +
                    ", Canvas records=" + layout.canvases.Length +
                    ", Image typetrees=" + layout.images.Length + "."));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(go);
            }
        }
    }
}
#endif
