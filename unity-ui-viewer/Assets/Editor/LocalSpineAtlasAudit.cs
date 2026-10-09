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
                if (refs == null || !refs.isArray)
                {
                    issues.Add("SkeletonDataAsset.atlasAssets property missing or incompatible");
                    continue;
                }
                if (refs.arraySize == 0)
                {
                    issues.Add("SkeletonDataAsset.atlasAssets array empty");
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

        /// <summary>
        /// Repair ONLY an empty atlasAssets array, and only when every source
        /// dependency is verified within the SAME private local pack. Never
        /// overwrite any existing non-empty reference or create fake assets.
        /// </summary>
        private static bool TryRepairEmptyAtlasLink(string folder, out string status)
        {
            var atlases = FindGenerated(folder, "AtlasAsset");
            var skeletons = FindGenerated(folder, "SkeletonDataAsset");
            if (atlases.Count != 1 || skeletons.Count != 1)
            {
                status = "BLOCKED: expected exactly one AtlasAsset and SkeletonDataAsset";
                return false;
            }

            var originalJson = AssetDatabase.LoadAssetAtPath<TextAsset>(
                folder + "/skeleton.json");
            var originalAtlasText = AssetDatabase.LoadAssetAtPath<TextAsset>(
                folder + "/skeleton.atlas.txt");
            if (originalJson == null || originalAtlasText == null)
            {
                status = "BLOCKED: source JSON or atlas text missing";
                return false;
            }

            var atlas = atlases[0];
            var skeleton = skeletons[0];
            var atlasSerialized = new SerializedObject(atlas);
            if (ObjectField(atlasSerialized, "atlasFile") != originalAtlasText)
            {
                status = "BLOCKED: AtlasAsset.atlasFile does not match local source";
                return false;
            }
            var materialRefs = atlasSerialized.FindProperty("materials");
            if (materialRefs == null || !materialRefs.isArray ||
                materialRefs.arraySize == 0)
            {
                status = "BLOCKED: AtlasAsset materials missing";
                return false;
            }

            var usedPages = new HashSet<string>(StringComparer.Ordinal);
            for (int i = 0; i < materialRefs.arraySize; i++)
            {
                var materialEntry = materialRefs.GetArrayElementAtIndex(i);
                if (materialEntry.propertyType != SerializedPropertyType.ObjectReference)
                {
                    status = "BLOCKED: atlas material field has wrong type";
                    return false;
                }
                var material = materialEntry.objectReferenceValue as Material;
                var image = material == null ? null : material.mainTexture as Texture2D;
                string materialPath = PathOf(material);
                string pagePath = PathOf(image);
                if (image == null ||
                    !materialPath.StartsWith(folder + "/", StringComparison.Ordinal) ||
                    !pagePath.StartsWith(folder + "/", StringComparison.Ordinal) ||
                    !pagePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                {
                    status = "BLOCKED: atlas material or texture not inside source pack";
                    return false;
                }
                usedPages.Add(pagePath);
            }
            var originalPages = AssetDatabase.FindAssets("t:Texture2D", new[] { folder })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(path => path.StartsWith(folder + "/", StringComparison.Ordinal) &&
                               path.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                .ToArray();
            if (originalPages.Length == 0 ||
                !originalPages.All(page => usedPages.Contains(page)) ||
                !usedPages.SetEquals(originalPages))
            {
                status = "BLOCKED: atlas materials do not cover exact local PNG pages";
                return false;
            }

            var skeletonSerialized = new SerializedObject(skeleton);
            if (ObjectField(skeletonSerialized, "skeletonJSON") != originalJson)
            {
                status = "BLOCKED: SkeletonDataAsset.skeletonJSON points elsewhere";
                return false;
            }
            var atlasRefs = skeletonSerialized.FindProperty("atlasAssets");
            if (atlasRefs == null || !atlasRefs.isArray)
            {
                status = "BLOCKED: incompatible Spine-Unity atlasAssets field";
                return false;
            }
            if (atlasRefs.arraySize != 0)
            {
                status = "UNCHANGED: atlasAssets already has references (never overwrite)";
                return false;
            }

            // Preserve Undo and never touch original atlas, materials or textures.
            Undo.RecordObject(skeleton, "Link verified local Spine atlas");
            atlasRefs.arraySize = 1;
            var slot = atlasRefs.GetArrayElementAtIndex(0);
            if (slot.propertyType != SerializedPropertyType.ObjectReference)
            {
                // No ApplyModifiedProperties means serialized changes are not saved.
                status = "BLOCKED: incompatible atlasAssets array element";
                return false;
            }
            slot.objectReferenceValue = atlas;
            if (!skeletonSerialized.ApplyModifiedProperties())
            {
                status = "BLOCKED: Unity did not accept atlasAssets link";
                return false;
            }
            EditorUtility.SetDirty(skeleton);
            status = "REPAIRED: verified same-pack AtlasAsset linked to empty atlasAssets";
            return true;
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Spine 3.8/Repair EMPTY verified atlas links (local only)")]
        public static void RepairEmptyVerifiedAtlasLinks()
        {
            if (!AssetDatabase.IsValidFolder(PacksRoot))
            {
                Debug.LogError("[HaiTac Spine atlas repair] Missing local SpinePacks folder");
                return;
            }
            if (!EditorUtility.DisplayDialog("Repair only verified empty Spine atlas links",
                "Only SkeletonDataAsset.atlasAssets EMPTY arrays will be linked. " +
                "Each pack must have exactly one matching AtlasAsset, skeleton JSON, " +
                "atlas text and valid material/PNG chain. Nonempty references and " +
                "other assets are never changed. This does not prove animation " +
                "or restore the game's original UI. Continue?", "Repair verified",
                "Cancel"))
                return;

            int repaired = 0;
            int unchanged = 0;
            int blocked = 0;
            foreach (string folder in AssetDatabase.GetSubFolders(PacksRoot)
                         .OrderBy(f => f, StringComparer.Ordinal))
            {
                string id = folder.Substring(PacksRoot.Length + 1);
                if (TryRepairEmptyAtlasLink(folder, out string status))
                    repaired++;
                else if (status.StartsWith("UNCHANGED:", StringComparison.Ordinal))
                    unchanged++;
                else
                    blocked++;

                string message = "[HaiTac Spine atlas repair] " + id + ": " + status;
                if (status.StartsWith("BLOCKED:", StringComparison.Ordinal))
                    Debug.LogWarning(message);
                else
                    Debug.Log(message);
            }

            if (repaired != 0)
                AssetDatabase.SaveAssets();
            Debug.Log("[HaiTac Spine atlas repair] COMPLETE: repaired=" + repaired +
                      ", unchanged=" + unchanged + ", blocked=" + blocked +
                      ". Run read-only audit again; playback NOT tested.");
            AuditAll();
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
