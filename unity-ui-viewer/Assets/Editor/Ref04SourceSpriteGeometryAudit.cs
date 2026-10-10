#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HaiTac.OfflineViewer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// REF04 STATIC UI ONLY. Source-authenticated Sprite rectangle, border and
    /// PPU audit/repair. Does not touch Canvas, RectTransform, managed 3C fields,
    /// Spine or Text. The original decoded PNG bytes are never changed.
    /// </summary>
    public static class Ref04SourceSpriteGeometryAudit
    {
        private const string Ref = "REF04-home-crew";
        private const string SpriteRoot = "Assets/LocalReconstruction/Sprites";
        private const string PrefabRoot =
            "Assets/LocalReconstruction/RootCanvasViewportPrefabs/" +
            Ref + "_SOURCE_ROOT_CANVAS_PREVIEW.prefab";
        [Serializable] private sealed class Source
        {
            public int schemaVersion;
            public string classification;
            public string sceneId;
            public string sourceGraphSha256;
            public string verifiedUiPlanSha256;
            public string verifiedVisualPlanSha256;
            public string nativeGeometryEvidenceSha256;
            public int sourceBindings;
            public SourceImage[] images;
        }
        [Serializable] private sealed class SourceImage
        {
            public int rectTransformPathId;
            public int componentPathId;
            public int gameObjectPathId;
            public string sourceObjectSha256;
            public string spriteFile;
            public int verifiedImageType;
            public string nativeSpriteGeometryStatus;
            public bool applyGeometry;
            public float[] border;
            public float[] sourceRectSize;
            public float pixelsPerUnit;
        }
        private sealed class Geometry
        {
            public string filename;
            public string assetPath;
            public Vector4 border;
            public float ppu;
            public float width;
            public float height;
            public TextureImporter importer;
        }
        private static string RepoRoot =>
            Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));
        private static string Sha(byte[] bytes)
        {
            using (var sha = SHA256.Create())
                return BitConverter.ToString(sha.ComputeHash(bytes))
                    .Replace("-", "").ToLowerInvariant();
        }
        private static byte[] ReadOutput(string path) =>
            File.ReadAllBytes(Path.Combine(RepoRoot, "output", path));
        private static bool Eq(float a, float b) => Mathf.Abs(a-b) <= .001f;
        private static bool Eq(Vector4 a, Vector4 b) =>
            Eq(a.x,b.x) && Eq(a.y,b.y) && Eq(a.z,b.z) && Eq(a.w,b.w);
        private static bool Finite(float x) =>
            !float.IsNaN(x) && !float.IsInfinity(x);
        private static bool SourcePngName(string file) =>
            !string.IsNullOrEmpty(file) &&
            System.Text.RegularExpressions.Regex.IsMatch(file,
                @"^[0-9a-f]{32}\.png$");
        private static Source ReadSource()
        {
            var src = JsonUtility.FromJson<Source>(
                Encoding.UTF8.GetString(ReadOutput("ref04-static-image-geometry.json")));
            if (src == null || src.schemaVersion != 1 ||
                src.classification != "REF04_EXACT_SOURCE_IMAGE_SPRITE_GEOMETRY" ||
                src.sceneId != Ref || src.sourceBindings != 265 ||
                src.images == null || src.images.Length != 265 ||
                Sha(ReadOutput("original-unity-graph.json")) != src.sourceGraphSha256 ||
                Sha(ReadOutput("verified-ui-prefab-plan.json")) != src.verifiedUiPlanSha256 ||
                Sha(ReadOutput("verified-visual-preview-plan.json")) !=
                    src.verifiedVisualPlanSha256 ||
                Sha(ReadOutput("local-ui-components.json")) !=
                    src.nativeGeometryEvidenceSha256)
                throw new InvalidDataException(
                    "REF04 265 Image geometry source manifest/fingerprints invalid.");

            var sourcePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabRoot);
            if (sourcePrefab == null)
                throw new FileNotFoundException(
                    "3E REF04 preview Prefab is missing. Build and Audit 3E first.");
            var ids = sourcePrefab.GetComponentsInChildren<ManagedUiSourceEvidence>(true)
                .ToDictionary(n => n.sourceMonoBehaviourPathId);
            var seen = new HashSet<int>();
            foreach (var item in src.images)
            {
                if (item == null || !seen.Add(item.componentPathId) ||
                    !SourcePngName(item.spriteFile) ||
                    !ids.TryGetValue(item.componentPathId, out var marker) ||
                    marker.originalClassName != "UnityEngine.UI.Image" ||
                    marker.sourceSceneId != Ref ||
                    marker.sourceGameObjectPathId != item.gameObjectPathId ||
                    marker.sourceRectTransformPathId != item.rectTransformPathId ||
                    marker.sourceObjectSha256 != item.sourceObjectSha256 ||
                    !marker.exactTwoBackendFieldAgreement)
                    throw new InvalidDataException("REF04 owner/Component PathID differs from original.");
                var image = marker.GetComponent<Image>();
                if (image == null || image.sprite == null ||
                    (int)image.type != item.verifiedImageType ||
                    AssetDatabase.GetAssetPath(image.sprite) !=
                    SpriteRoot + "/" + item.spriteFile)
                    throw new InvalidDataException(
                        "REF04 Image type/source Sprite pointer differs on component " +
                        item.componentPathId);
                if (!item.applyGeometry)
                    continue;
                if (item.nativeSpriteGeometryStatus !=
                        "NATIVE_SPRITE_GEOMETRY_VERIFIED" ||
                    item.border == null || item.border.Length != 4 ||
                    item.sourceRectSize == null || item.sourceRectSize.Length != 2 ||
                    item.border.Any(b => !Finite(b) || b < 0) ||
                    item.sourceRectSize.Any(n => !Finite(n) || n <= 0 || n > 16384) ||
                    !Finite(item.pixelsPerUnit) ||
                    item.pixelsPerUnit <= 0 || item.pixelsPerUnit > 10000 ||
                    item.border[0] + item.border[2] > item.sourceRectSize[0] ||
                    item.border[1] + item.border[3] > item.sourceRectSize[1])
                    throw new InvalidDataException("Untrusted native Sprite geometry.");
            }
            return src;
        }

        private static List<Geometry> Resolve(Source src)
        {
            var result = new Dictionary<string, Geometry>();
            foreach (var row in src.images)
            {
                if (!row.applyGeometry) continue;
                var path = SpriteRoot + "/" + row.spriteFile;
                var importer = AssetImporter.GetAtPath(path) as TextureImporter;
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                if (importer == null || sprite == null ||
                    importer.textureType != TextureImporterType.Sprite)
                    throw new FileNotFoundException(
                        "Original imported Sprite missing: " + row.spriteFile);
                var raw = Path.Combine(RepoRoot,"output","local-ui-art",row.spriteFile);
                var local = Path.Combine(Application.dataPath,
                    "LocalReconstruction","Sprites",row.spriteFile);
                if (!File.Exists(raw) || !File.Exists(local) ||
                    Sha(File.ReadAllBytes(raw)) != Sha(File.ReadAllBytes(local)))
                    throw new InvalidDataException(
                        "Sprite PNG content differs from XAPK export: " + row.spriteFile);
                if (!Eq(sprite.rect.width,row.sourceRectSize[0]) ||
                    !Eq(sprite.rect.height,row.sourceRectSize[1]))
                    throw new InvalidDataException(
                        "Sprite imported rectangle differs from original native rectangle: " +
                        row.spriteFile + " source=" + row.sourceRectSize[0] + "x" +
                        row.sourceRectSize[1] + " imported=" + sprite.rect.size);
                var original = new Geometry {
                    filename=row.spriteFile, assetPath=path, importer=importer,
                    border=new Vector4(row.border[0],row.border[1],
                                       row.border[2],row.border[3]),
                    ppu=row.pixelsPerUnit, width=row.sourceRectSize[0],
                    height=row.sourceRectSize[1],
                };
                if (result.TryGetValue(row.spriteFile,out var existing))
                {
                    if (!Eq(existing.border,original.border) ||
                        !Eq(existing.ppu,original.ppu) ||
                        !Eq(existing.width,original.width) ||
                        !Eq(existing.height,original.height))
                        throw new InvalidDataException(
                            "Conflicting source geometry for one Sprite: " + row.spriteFile);
                    continue;
                }
                result.Add(row.spriteFile,original);
            }
            return result.Values.OrderBy(g=>g.filename).ToList();
        }

        private static int CountWrong(IEnumerable<Geometry> geometries)
        {
            return geometries.Count(g => !Eq(g.importer.spriteBorder,g.border) ||
                !Eq(g.importer.spritePixelsPerUnit,g.ppu));
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/REF04 - Audit static icon native Sprite borders")]
        public static void Audit()
        {
            try
            {
                var src = ReadSource();
                var geometries = Resolve(src);
                int wrong = CountWrong(geometries);
                int unverified = src.images.Count(x=>!x.applyGeometry);
                int sliced = src.images.Count(x=>x.verifiedImageType==1);
                Debug.Log("[REF04 SOURCE ICONS] AUDIT: " +
                    src.sourceBindings + " exact original Images; " +
                    geometries.Count + " unique native-geometry Sprite files; " +
                    wrong + " imported border/PPU mismatches; " +
                    unverified + " Image geometry records not proven; " +
                    sliced + " original Sliced Images. " +
                    "No Canvas/Spine/Text/Root RectTransforms changed. " +
                    "0 mismatch does NOT prove original runtime visual layout.");
                EditorUtility.DisplayDialog("REF04 static icon audit",
                    "Original Images: " + src.sourceBindings +
                    "\nSprite native geometry records: " + geometries.Count +
                    "\nBorder/PPU importer mismatches: " + wrong +
                    "\nUnverified Image geometries: " + unverified +
                    "\nThis is NOT a full original-runtime layout test.", "OK");
            }
            catch (Exception exc)
            {
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("REF04 audit BLOCKED",
                    exc.GetBaseException().Message, "OK");
            }
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/REF04 - Restore verified icon Sprite border and PPU")]
        public static void Restore()
        {
            if (EditorApplication.isPlaying) return;
            try
            {
                var src = ReadSource();
                var geometries = Resolve(src);
                var toFix = geometries.Where(g =>
                    !Eq(g.importer.spriteBorder,g.border) ||
                    !Eq(g.importer.spritePixelsPerUnit,g.ppu)).ToArray();
                if (toFix.Length == 0)
                {
                    Debug.Log("[REF04 SOURCE ICONS] No original border/PPU mismatches; " +
                        "not changing any asset. Remaining UI mismatch is elsewhere.");
                    return;
                }
                // Save local importer settings and revert on any failure.
                var touched = new List<(Geometry item, Vector4 border, float ppu)>();
                try
                {
                    foreach (var item in toFix)
                    {
                        touched.Add((item,item.importer.spriteBorder,
                                    item.importer.spritePixelsPerUnit));
                        item.importer.spriteBorder=item.border;
                        item.importer.spritePixelsPerUnit=item.ppu;
                        item.importer.SaveAndReimport();
                        var sprite=AssetDatabase.LoadAssetAtPath<Sprite>(item.assetPath);
                        if (sprite == null || !Eq(sprite.border,item.border) ||
                            !Eq(sprite.pixelsPerUnit,item.ppu))
                            throw new InvalidDataException(
                                "REF04 Sprite did not retain original border/PPU: " +
                                item.filename);
                    }
                }
                catch
                {
                    foreach (var change in touched)
                    {
                        change.item.importer.spriteBorder=change.border;
                        change.item.importer.spritePixelsPerUnit=change.ppu;
                        change.item.importer.SaveAndReimport();
                    }
                    throw;
                }
                Debug.Log("[REF04 SOURCE ICONS] RESTORE PASS: " +
                    toFix.Length + " source-derived local Sprite border/PPU import settings " +
                    "corrected. Original PNG bytes untouched; 3C fields, Canvas, Spine, Text " +
                    "and RectTransforms unchanged. REF04 Game View needs visual review.");
            }
            catch (Exception exc)
            {
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("REF04 restore BLOCKED",
                    exc.GetBaseException().Message, "OK");
            }
        }
    }
}
#endif
