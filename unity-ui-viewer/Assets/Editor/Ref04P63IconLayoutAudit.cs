#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HaiTac.OfflineViewer;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// P6.3 READ-ONLY audit of actual icons, sprite bindings and hierarchy in
    /// the NEW-CLIENT study. The row of fists and the empty character shadows
    /// must NOT be dragged into a guessed original HUD. Produce enough local
    /// evidence to decide whether these are character slot indicators, HUD
    /// icons or separate original source objects before moving anything.
    /// The report is private and ignored by git under output/.
    /// </summary>
    public static class Ref04P63IconLayoutAudit
    {
        private const string Classification =
            "REF04_P63_LOCAL_NEW_CLIENT_ICON_OWNER_LAYOUT_AUDIT";
        private const string SceneName =
            "REF04-home-crew_NEW_CLIENT_FIT_STUDY.unity";
        private const string OutputName =
            "ref04-p63-client-icon-layout-audit.json";

        [Serializable]
        private sealed class IconRecord
        {
            public string hierarchyPath;
            public string sourceRootGroup;
            public string spriteName;
            public string spriteAssetPath;
            public bool activeInHierarchy;
            public bool imageEnabled;
            public bool hasSprite;
            public bool raycastTarget;
            public bool sourceManagedImageVerified;
            public int originalImageComponentPathId;
            public int originalGameObjectPathId;
            public int originalRectTransformPathId;
            public string sourceImageObjectSha256;
            public int sourceSpineEvidenceInAncestor;
            public int sourceSpineEvidenceInDescendants;
            public string parentName;
            public float[] anchorMin;
            public float[] anchorMax;
            public float[] pivot;
            public float[] anchoredPosition;
            public float[] sizeDelta;
            public float[] scale;
            public float[] worldCorners;
        }

        [Serializable]
        private sealed class GroupRecord
        {
            public string rootGroup;
            public int totalImages;
            public int visibleSpriteImages;
            public int spriteLessImages;
            public int originalImageReferences;
            public int spineEvidenceRefs;
        }

        [Serializable]
        private sealed class Report
        {
            public string classification;
            public string sceneName;
            public string originalGameRuntimeLayoutProven;
            public string authoringPolicy;
            public bool modifiesUnityAssets;
            public bool changesOriginalSourceEvidence;
            public string[] limitations;
            public int totalImages;
            public int activeSpriteImages;
            public int spriteLessImages;
            public int managedSourceImageProofs;
            public int objectsWithSpineEvidence;
            public GroupRecord[] groups;
            public IconRecord[] images;
        }

        private static string HierarchyPath(Transform transform, Transform root)
        {
            var chunks = new List<string>();
            for (var node = transform; node != null; node = node.parent)
            {
                chunks.Add(node.name);
                if (node == root) break;
            }
            chunks.Reverse();
            return string.Join("/", chunks.ToArray());
        }

        private static string TopGroup(Transform node, Transform root)
        {
            if (node == root) return "[ROOT]";
            while (node.parent != null && node.parent != root)
                node = node.parent;
            return node.name;
        }

        private static float[] Values(Vector2 source) =>
            new[] { source.x, source.y };

        private static float[] Values(Vector3 source) =>
            new[] { source.x, source.y, source.z };

        private static string RepositoryRoot =>
            Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));

        public static string BuildReport()
        {
            // Static source proof first, and exact expected client Scene.
            Ref04P6OfflineWorkspace.ValidateSourceStudyForClientDesign();
            var scene = SceneManager.GetActiveScene();
            if (!scene.IsValid() || !scene.isLoaded ||
                !scene.path.Replace('\\', '/').EndsWith("/" + SceneName,
                    StringComparison.Ordinal))
                throw new InvalidDataException(
                    "P6.3 BLOCKED: Open the NEW_CLIENT_FIT_STUDY Scene, " +
                    "not the source or 3C Study Scene.");

            var roots = scene.GetRootGameObjects()
                .Where(go => {
                    var n = go.GetComponent<VerifiedVisualPreviewEvidence>();
                    return n != null && n.sourceSceneId == "REF04-home-crew";
                }).ToArray();
            if (roots.Length != 1 ||
                roots[0].GetComponent<Ref04ClientViewportFit>() == null)
                throw new InvalidDataException(
                    "P6.3 BLOCKED: not exactly one verified P6.2 client Canvas.");
            var root = roots[0].transform;
            var images = root.GetComponentsInChildren<Image>(true);
            var records = new List<IconRecord>();
            var byGroup = new SortedDictionary<string, GroupRecord>(
                StringComparer.Ordinal);
            int active = 0, spriteLess = 0, sourceProofs = 0;
            var allSpine = root.GetComponentsInChildren<SpineReferenceEvidence>(
                true);
            foreach (var image in images)
            {
                var trans = image.rectTransform;
                if (trans == null)
                    throw new InvalidDataException(
                        "P6.3 BLOCKED: Image has no RectTransform.");
                var note = image.GetComponent<ManagedUiSourceEvidence>();
                var owner = image.GetComponent<OriginalSerializedEvidence>();
                var group = TopGroup(trans, root);
                if (!byGroup.TryGetValue(group, out var g))
                {
                    g = new GroupRecord { rootGroup = group };
                    byGroup.Add(group, g);
                }
                g.totalImages++;
                var visible = image.sprite != null &&
                    image.enabled && image.gameObject.activeInHierarchy;
                if (visible) { active++; g.visibleSpriteImages++; }
                if (image.sprite == null) { spriteLess++; g.spriteLessImages++; }
                if (note != null &&
                    note.originalClassName == "UnityEngine.UI.Image" &&
                    note.exactTwoBackendFieldAgreement)
                {
                    sourceProofs++;
                    g.originalImageReferences++;
                }
                var parentSpine = image.GetComponentsInParent<SpineReferenceEvidence>(
                    true).Length;
                var childSpine = image.GetComponentsInChildren<SpineReferenceEvidence>(
                    true).Length;
                g.spineEvidenceRefs += childSpine;
                var corners = new Vector3[4];
                trans.GetWorldCorners(corners);
                var flatCorners = new float[12];
                for (int i = 0; i < 4; i++)
                {
                    flatCorners[3 * i] = corners[i].x;
                    flatCorners[3 * i + 1] = corners[i].y;
                    flatCorners[3 * i + 2] = corners[i].z;
                }
                records.Add(new IconRecord {
                    hierarchyPath = HierarchyPath(trans, root),
                    sourceRootGroup = group,
                    parentName = trans.parent == null ? "" : trans.parent.name,
                    spriteName = image.sprite == null ? "" : image.sprite.name,
                    spriteAssetPath = image.sprite == null ? "" :
                        AssetDatabase.GetAssetPath(image.sprite),
                    activeInHierarchy = image.gameObject.activeInHierarchy,
                    imageEnabled = image.enabled,
                    hasSprite = image.sprite != null,
                    raycastTarget = image.raycastTarget,
                    sourceManagedImageVerified =
                        note != null &&
                        note.originalClassName == "UnityEngine.UI.Image" &&
                        note.exactTwoBackendFieldAgreement,
                    originalImageComponentPathId =
                        note == null ? 0 : note.sourceMonoBehaviourPathId,
                    originalGameObjectPathId = note != null ?
                        note.sourceGameObjectPathId :
                        owner == null ? 0 : owner.gameObjectPathId,
                    originalRectTransformPathId = note != null ?
                        note.sourceRectTransformPathId :
                        owner == null ? 0 : owner.rectTransformPathId,
                    sourceImageObjectSha256 =
                        note == null ? "" : note.sourceObjectSha256,
                    sourceSpineEvidenceInAncestor = parentSpine,
                    sourceSpineEvidenceInDescendants = childSpine,
                    anchorMin = Values(trans.anchorMin),
                    anchorMax = Values(trans.anchorMax),
                    pivot = Values(trans.pivot),
                    anchoredPosition = Values(trans.anchoredPosition),
                    sizeDelta = Values(trans.sizeDelta),
                    scale = Values(trans.localScale),
                    worldCorners = flatCorners,
                });
            }

            var report = new Report {
                classification = Classification,
                sceneName = SceneName,
                originalGameRuntimeLayoutProven = "NO",
                authoringPolicy =
                    "ORIGINAL_XAPK_SOURCE_VERIFIED_VS_NEW_PROJECT_DESIGN",
                modifiesUnityAssets = false,
                changesOriginalSourceEvidence = false,
                limitations = new[] {
                    "World corners are Unity CLIENT-STUDY observations, NOT original runtime coordinates.",
                    "SpineReferenceEvidence is a pointer candidate, NOT a rendered character.",
                    "A round fist icon is not identified as hero/HUD solely by its image content.",
                    "Missing dynamic text, button handlers and original Canvas runtime are not recovered.",
                    "Do not re-parent or move source-backed Icons until their current parent/source PathIDs are classified.",
                },
                totalImages = records.Count,
                activeSpriteImages = active,
                spriteLessImages = spriteLess,
                managedSourceImageProofs = sourceProofs,
                objectsWithSpineEvidence = allSpine.Length,
                groups = byGroup.Values.ToArray(),
                images = records.OrderBy(x => x.hierarchyPath,
                    StringComparer.Ordinal).ToArray(),
            };
            if (report.totalImages < 299 ||
                report.managedSourceImageProofs != 299)
                throw new InvalidDataException(
                    "P6.3 BLOCKED: source Image inventory differs from validated 299.");
            var destination = Path.Combine(
                RepositoryRoot, "output", OutputName);
            if (!Directory.Exists(Path.GetDirectoryName(destination)))
                throw new DirectoryNotFoundException(
                    "P6.3 output/ directory missing; rerun P6 offline source reports.");
            if (File.Exists(destination))
                throw new IOException(
                    "P6.3 report already exists; preserving previous evidence. " +
                    "Rename the old report locally before another capture.");
            File.WriteAllText(destination, JsonUtility.ToJson(report, true),
                new UTF8Encoding(false));
            Debug.Log("[P6.3 ICON AUDIT] READ-ONLY: " + report.totalImages +
                " Image components, " + report.activeSpriteImages +
                " active Sprites, " + report.managedSourceImageProofs +
                " source-verified Image IDs across " + report.groups.Length +
                " top-level root groups. Private JSON: " + destination +
                ". No icons moved or renamed; runtime positions NOT proven.");
            foreach (var g in report.groups)
                Debug.Log("[P6.3 ICON GROUP] " + g.rootGroup +
                    ": image=" + g.totalImages +
                    ", visibleSprite=" + g.visibleSpriteImages +
                    ", noSprite=" + g.spriteLessImages +
                    ", spineEvidenceRefs=" + g.spineEvidenceRefs);
            return destination;
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/P6.3 - Audit icon owners and layout (read only)")]
        public static void RunFromMenu()
        {
            try
            {
                var location = BuildReport();
                EditorUtility.DisplayDialog(
                    "P6.3 icon ownership audit",
                    "Read-only Scene image+icon mapping saved privately:\n" +
                    location + "\n\nNo HUD or character positions changed. " +
                    "Use the report to distinguish the PlayerHeroes " +
                    "slots, Background and HUD before design changes.", "OK");
            }
            catch (Exception error)
            {
                Debug.LogError("[P6.3 ICON AUDIT] " +
                    error.GetBaseException().Message);
                EditorUtility.DisplayDialog(
                    "P6.3 audit BLOCKED",
                    error.GetBaseException().Message, "OK");
            }
        }
    }
}
#endif
