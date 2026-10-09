#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// Read-only audit of locally generated Spine 3.8 atlas/skeleton assets.
    /// Uses serialized properties and reflection so that no vendor runtime
    /// is bundled or referenced directly at compile time.
    /// </summary>
    public static class LocalSpineAtlasAudit
    {
        private const string PacksRoot = "Assets/LocalReconstruction/SpinePacks";

        private static string PathOf(UnityEngine.Object asset)
        {
            return asset == null ? "(missing)" : AssetDatabase.GetAssetPath(asset);
        }

        private static List<UnityEngine.Object> FindGenerated(string folder, string typeSuffix)
        {
            var list = new List<UnityEngine.Object>();
            foreach (string guid in AssetDatabase.FindAssets("t:ScriptableObject",
                                                               new[] { folder }))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (!path.StartsWith(folder + "/", StringComparison.Ordinal) ||
                    !path.EndsWith(".asset", StringComparison.OrdinalIgnoreCase))
                    continue;
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
                if (asset != null && asset.GetType().Name.EndsWith(typeSuffix,
                                                                   StringComparison.Ordinal))
                    list.Add(asset);
            }
            return list;
        }

        private static UnityEngine.Object ObjectField(SerializedObject serialized, string field)
        {
            var property = serialized.FindProperty(field);
            return property != null &&
                   property.propertyType == SerializedPropertyType.ObjectReference
                   ? property.objectReferenceValue : null;
        }

        private static void CheckPack(string folder, List<string> problems)
        {
            string id = folder.Substring(PacksRoot.Length + 1);
            string jsonPath = folder + "/skeleton.json";
            string atlasPath = folder + "/skeleton.atlas.txt";
            var json = AssetDatabase.LoadAssetAtPath<TextAsset>(jsonPath);
            var atlasText = AssetDatabase.LoadAssetAtPath<TextAsset>(atlasPath);
            var pages = AssetDatabase.FindAssets("t:Texture2D", new[] { folder })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => path.StartsWith(folder + "/", StringComparison.Ordinal) &&
                               path.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                .Distinct().ToArray();
            var atlasAssets = FindGenerated(folder, "AtlasAsset");
            var skeletonAssets = FindGenerated(folder, "SkeletonDataAsset");
            var issues = new List<string>();

            if (json == null) issues.Add("missing skeleton.json TextAsset");
            if (atlasText == null) issues.Add("missing skeleton.atlas.txt TextAsset");
            if (pages.Length == 0) issues.Add("no PNG Texture2D pages found");
            if (atlasAssets.Count != 1)
                issues.Add("expected exactly one AtlasAsset, found " + atlasAssets.Count);
            if (skeletonAssets.Count != 1)
                issues.Add("expected exactly one SkeletonDataAsset, found " +
                           skeletonAssets.Count);

            foreach (var atlas in atlasAssets)
            {
                var data = new SerializedObject(atlas);
                var textRef = ObjectField(data, "atlasFile");
                if (textRef != atlasText)
                    issues.Add("AtlasAsset.atlasFile does not reference local atlas text: " +
                               PathOf(textRef));
                var materials = data.FindProperty("materials");
                if (materials == null || !materials.isArray ||
                    materials.arraySize == 0)
                {
                    issues.Add("AtlasAsset.materials missing or empty");
                    continue;
                }
                var texturePaths = new HashSet<string>();
                for (int index = 0; index < materials.arraySize; index++)
                {
                    var item = materials.GetArrayElementAtIndex(index);
                    var mat = item.objectReferenceValue as Material;
                    if (mat == null)
                    {
                        issues.Add("AtlasAsset.materials[" + index + "] is null");
                        continue;
                    }
                    var texture = mat.mainTexture as Texture2D;
                    if (texture == null)
                    {
                        issues.Add("material has no main Texture2D: " + PathOf(mat));
                        continue;
                    }
                    string texturePath = PathOf(texture);
                    texturePaths.Add(texturePath);
                    if (!texturePath.StartsWith(folder + "/", StringComparison.Ordinal))
                        issues.Add("material texture points outside its pack: " + texturePath);
                }
                foreach (string page in pages)
                    if (!texturePaths.Contains(page))
                        issues.Add("PNG page not represented by atlas materials: " +
                                   Path.GetFileName(page));
            }

            foreach (var skeleton in skeletonAssets)
            {
                var data = new SerializedObject(skeleton);
                var skeletonRef = ObjectField(data, "skeletonJSON");
                if (skeletonRef != json)
                    issues.Add("SkeletonDataAsset.skeletonJSON does not reference local JSON: " +
                               PathOf(skeletonRef));
                var refs = data.FindProperty("atlasAssets");
                if (refs == null || !refs.isArray || refs.arraySize == 0)
                {
                    issues.Add("SkeletonDataAsset.atlasAssets missing or empty");
                    continue;
                }
                for (int i = 0; i < refs.arraySize; i++)
                {
                    var asset = refs.GetArrayElementAtIndex(i).objectReferenceValue;
                    if (asset == null || !atlasAssets.Contains(asset))
                        issues.Add("SkeletonDataAsset.atlasAssets[" + i +
                                   "] missing or does not match this pack: " + PathOf(asset));
                }
            }

            string summary = "[HaiTac Spine atlas audit] " + id +
                ": atlasAssets=" + atlasAssets.Count +
                ", skeletonAssets=" + skeletonAssets.Count +
                ", pngPages=" + pages.Length +
                ", issues=" + issues.Count;
            if (issues.Count == 0)
                Debug.Log(summary + " [LINKS VERIFIED; PLAYBACK NOT TESTED]");
            else
            {
                problems.Add(id + ": " + string.Join("; ", issues.ToArray()));
                Debug.LogWarning(summary + "\n - " +
                                 string.Join("\n - ", issues.ToArray()));
            }
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Spine 3.8/Audit all local AtlasAsset links (read only)")]
        public static void AuditAll()
        {
            if (!AssetDatabase.IsValidFolder(PacksRoot))
            {
                Debug.LogError("[HaiTac Spine atlas audit] Missing " + PacksRoot +
                               ". Reconstruct/import the authorized local pack first.");
                return;
            }

            string[] folders = AssetDatabase.GetSubFolders(PacksRoot);
            var problems = new List<string>();
            foreach (string folder in folders.OrderBy(f => f, StringComparer.Ordinal))
                CheckPack(folder, problems);
            Debug.Log("[HaiTac Spine atlas audit] COMPLETE: packs=" + folders.Length +
                      ", packsWithIssues=" + problems.Count +
                      ", allLinksVerified=" + (problems.Count == 0) +
                      ". This does NOT verify animation, rendering or original game UI.");
        }
    }
}
#endif
