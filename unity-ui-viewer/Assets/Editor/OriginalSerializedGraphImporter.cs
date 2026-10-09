#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HaiTac.OfflineViewer;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// Builds an EDITABLE GRAPH STUDY from verified serialized XAPK pointers.
    /// These are NOT recovered original .prefab/.unity sources.
    ///
    /// IMPORTANT: unlike the legacy rendered Canvas reconstruction, this
    /// does NOT add a provisional root Canvas, unknown Images, Mask, camera,
    /// Sprite, Spine or layout components. Transform values, even root zero
    /// scale, are kept as serialized where they were actually readable.
    /// </summary>
    public static class OriginalSerializedGraphImporter
    {
        private const string OutputFolder = "Assets/LocalReconstruction";
        private const string PrefabFolder = OutputFolder + "/SourceGraphPrefabs";
        private const string SceneFolder = OutputFolder + "/SourceGraphScenes";
        private const string ExpectedClass =
            "SERIALIZED_HIERARCHY_NOT_VERIFIED_EDITOR_PREFAB_OR_SCENE";

        [Serializable] private sealed class GraphDatabase
        {
            public int version;
            public string classification;
            public GraphScene[] scenes;
        }

        [Serializable] private sealed class GraphScene
        {
            public string sceneId;
            public string sourceSerializedFile;
            public int candidateRootTransform;
            public GraphNode[] nodes;
            public GraphComponent[] components;
        }

        [Serializable] private sealed class Ref
        {
            public int fileId;
            public int pathId;
        }

        [Serializable] private sealed class SourceRect
        {
            public float[] anchorMin;
            public float[] anchorMax;
            public float[] pivot;
            public float[] sizeDelta;
            public float[] anchoredPosition;
            public float[] localScale;
            public float[] localRotation;
        }

        [Serializable] private sealed class GraphNode
        {
            public int rectTransformId;
            public int gameObjectId;
            public Ref parent;
            public string name;
            public int active;
            public int[] componentIds;
            public int[] childTransformIds;
            public SourceRect rect;
        }

        [Serializable] private sealed class GraphComponent
        {
            public int pathId;
            public string kind;
            public string fieldStatus;
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/Build evidence-only serialized graph prefabs")]
        public static void Build()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;
            string root = Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
            string sourceFile = Path.Combine(root, "output", "original-unity-graph.json");
            if (!File.Exists(sourceFile))
            {
                EditorUtility.DisplayDialog("No verified source graph",
                    "Run in repo CMD first:\npy -3 tools\\audit_original_unity_graph.py\n" +
                    "This command reads the local XAPK. No guessing or 1600x900 Canvas.",
                    "OK");
                return;
            }

            try
            {
                var db = JsonUtility.FromJson<GraphDatabase>(
                    File.ReadAllText(sourceFile, Encoding.UTF8));
                if (db == null || db.version != 1 ||
                    db.classification != ExpectedClass ||
                    db.scenes == null || db.scenes.Length != 5)
                    throw new InvalidDataException("Not a supported original XAPK graph.");
                if (db.scenes.Select(s => s.sceneId).Distinct().Count() != 5 ||
                    db.scenes.Any(s => s == null || !System.Text.RegularExpressions.Regex.IsMatch(
                        s.sceneId ?? "", @"^REF[0-9A-Za-z-]+$")))
                    throw new InvalidDataException("Invalid candidate scene IDs.");

                EnsureFolder("Assets", "LocalReconstruction");
                EnsureFolder(OutputFolder, "SourceGraphPrefabs");
                EnsureFolder(OutputFolder, "SourceGraphScenes");

                foreach (var scene in db.scenes)
                    BuildScene(scene);
                AssetDatabase.SaveAssets();
                Debug.Log("[HaiTac original source graph] PASS 5/5 evidence-only " +
                    "hierarchy prefabs/scenes built in LocalReconstruction. " +
                    "NO original editor Prefab/Scene source was proven. " +
                    "Original Sprite/UI MonoBehaviour fields not invented.");
                EditorUtility.DisplayDialog("Source graph imported",
                    "Built 5 editable EVIDENCE-ONLY serialized object hierarchies.\n" +
                    "They are NOT the original game's .prefab/.unity scenes.\n" +
                    "Open Assets/LocalReconstruction/SourceGraphScenes.\n" +
                    "Missing managed UI attributes stay unresolved.", "OK");
            }
            catch (Exception exc)
            {
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("Source graph blocked",
                    exc.GetBaseException().Message +
                    "\nNo legacy UI scenes were overwritten.", "OK");
            }
        }

        private static void EnsureFolder(string parent, string child)
        {
            string path = parent + "/" + child;
            if (!AssetDatabase.IsValidFolder(path))
                AssetDatabase.CreateFolder(parent, child);
        }

        private static Vector2? Two(float[] array)
        {
            return array != null && array.Length == 2 &&
                array.All(x => !float.IsNaN(x) && !float.IsInfinity(x))
                ? new Vector2(array[0], array[1]) : (Vector2?)null;
        }

        private static void ApplyRect(RectTransform transform, SourceRect rect)
        {
            if (rect == null) return;
            var min = Two(rect.anchorMin);
            var max = Two(rect.anchorMax);
            var pivot = Two(rect.pivot);
            var delta = Two(rect.sizeDelta);
            var position = Two(rect.anchoredPosition);
            if (min.HasValue) transform.anchorMin = min.Value;
            if (max.HasValue) transform.anchorMax = max.Value;
            if (pivot.HasValue) transform.pivot = pivot.Value;
            if (delta.HasValue) transform.sizeDelta = delta.Value;
            if (position.HasValue) transform.anchoredPosition = position.Value;
            if (rect.localScale != null && rect.localScale.Length == 3 &&
                rect.localScale.All(x => !float.IsNaN(x) && !float.IsInfinity(x)))
                transform.localScale = new Vector3(rect.localScale[0],
                    rect.localScale[1], rect.localScale[2]);
            if (rect.localRotation != null && rect.localRotation.Length == 4 &&
                rect.localRotation.All(x => !float.IsNaN(x) && !float.IsInfinity(x)))
                transform.localRotation = new Quaternion(rect.localRotation[0],
                    rect.localRotation[1], rect.localRotation[2],
                    rect.localRotation[3]);
        }

        private static void BuildScene(GraphScene source)
        {
            if (source.nodes == null || source.components == null ||
                source.nodes.Length < 2 || source.nodes.Length > 3000)
                throw new InvalidDataException("Invalid source graph length " + source.sceneId);
            var ids = source.nodes.Select(n => n.rectTransformId).ToArray();
            if (ids.Distinct().Count() != ids.Length ||
                !ids.Contains(source.candidateRootTransform))
                throw new InvalidDataException("Duplicate or missing root transform.");
            var original = source.nodes.ToDictionary(n => n.rectTransformId);
            var components = source.components.ToDictionary(c => c.pathId);
            var mapped = new Dictionary<int, RectTransform>();
            var sceneRoot = source.nodes.Single(n =>
                n.rectTransformId == source.candidateRootTransform);
            GameObject candidateRoot = null;
            try
            {
                // Real transforms first; parent pointers and sibling order second.
                foreach (var n in source.nodes)
                {
                    if (n.componentIds == null || n.childTransformIds == null ||
                        n.componentIds.Distinct().Count() != n.componentIds.Length ||
                        !n.componentIds.Contains(n.rectTransformId))
                        throw new InvalidDataException("Corrupt GameObject components " +
                            source.sceneId + ":" + n.rectTransformId);
                    var go = new GameObject(n.name ?? "Unnamed", typeof(RectTransform));
                    var tr = go.GetComponent<RectTransform>();
                    ApplyRect(tr, n.rect);
                    var note = go.AddComponent<OriginalSerializedEvidence>();
                    note.serializedFile = source.sourceSerializedFile;
                    note.gameObjectPathId = n.gameObjectId;
                    note.rectTransformPathId = n.rectTransformId;
                    note.originalComponentPathIds = n.componentIds;
                    note.sourceActiveKnown = n.active == 0 || n.active == 1;
                    note.parentWasOutsideCandidate = n.parent != null &&
                        n.parent.pathId != 0 &&
                        !original.ContainsKey(n.parent.pathId);
                    var info = n.componentIds.Select(id =>
                    {
                        if (!components.TryGetValue(id, out var c))
                            return id + " MISSING FROM SERIALIZED GRAPH";
                        return id + ": " + c.kind + " (" + c.fieldStatus + ")";
                    }).ToArray();
                    note.originalComponentTypes = string.Join("\n", info);
                    note.missingComponentDetails = string.Join("\n",
                        info.Where(x => x.Contains("MISSING") ||
                           x.Contains("unavailable") || x.Contains("head_only")));
                    mapped.Add(n.rectTransformId, tr);
                    if (n.rectTransformId == source.candidateRootTransform)
                        candidateRoot = go;
                }
                foreach (var n in source.nodes)
                {
                    if (n.rectTransformId == source.candidateRootTransform) continue;
                    if (n.parent == null || n.parent.fileId != 0 ||
                        !mapped.TryGetValue(n.parent.pathId, out var parent))
                        throw new InvalidDataException(
                            "Cannot prove original parent for node " + n.rectTransformId);
                    mapped[n.rectTransformId].SetParent(parent, false);
                    // Parent assignment changes local transform; restore serialized values.
                    ApplyRect(mapped[n.rectTransformId], n.rect);
                }
                foreach (var n in source.nodes)
                {
                    int next = 0;
                    foreach (int child in n.childTransformIds)
                    {
                        if (!mapped.TryGetValue(child, out var tr)) continue;
                        if (tr.parent != mapped[n.rectTransformId])
                            throw new InvalidDataException(
                                "Original child list conflicts with parent pointer " + child);
                        tr.SetSiblingIndex(next++);
                    }
                }
                foreach (var n in source.nodes)
                {
                    if (n.active == 0 || n.active == 1)
                        mapped[n.rectTransformId].gameObject.SetActive(n.active == 1);
                }
                var saved = PrefabUtility.SaveAsPrefabAsset(candidateRoot,
                    PrefabFolder + "/" + source.sceneId + "_SERIALIZED_GRAPH.prefab");
                if (saved == null)
                    throw new IOException("Cannot save source hierarchy prefab " + source.sceneId);
            }
            finally
            {
                foreach (var tr in mapped.Values.Where(x => x != null && x.parent == null))
                    UnityEngine.Object.DestroyImmediate(tr.gameObject);
            }

            var empty = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                     NewSceneMode.Single);
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                PrefabFolder + "/" + source.sceneId + "_SERIALIZED_GRAPH.prefab");
            if (prefab == null || PrefabUtility.InstantiatePrefab(prefab) == null)
                throw new IOException("Cannot instantiate verified graph " + source.sceneId);
            if (!EditorSceneManager.SaveScene(empty,
                    SceneFolder + "/" + source.sceneId + "_SERIALIZED_GRAPH.unity"))
                throw new IOException("Cannot save graph-only scene " + source.sceneId);
            Debug.Log("[HaiTac original source graph] " + source.sceneId +
                ": " + source.nodes.Length + " original pointer-verified nodes, " +
                source.components.Length + " serialized component records; " +
                "NO UI canvas/rendering assumptions used.");
        }
    }
}
#endif
