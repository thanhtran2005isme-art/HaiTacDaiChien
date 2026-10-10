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
        // These plans were independently validated against the real XAPK in
        // Phase 3C/3D. Their private SHA256s already appear in the geometry
        // plan. Recheck exact per-Image identity and Image.Type here, without
        // loading or rebuilding any disposable 3E preview Scene/Prefab.
        [Serializable] private sealed class VerifiedPlan
        {
            public string classification;
            public int verifiedComponents;
            public int verifiedFieldValues;
            public int singleBackendExcludedFieldValues;
            public VerifiedScene[] scenes;
        }
        [Serializable] private sealed class VerifiedScene
        {
            public string sceneId;
            public VerifiedComponent[] components;
        }
        [Serializable] private sealed class VerifiedComponent
        {
            public int componentPathId;
            public int rectTransformPathId;
            public int gameObjectPathId;
            public string rawObjectSha256;
            public string className;
            public VerifiedField[] fields;
        }
        [Serializable] private sealed class VerifiedField
        {
            public string name;
            public string kind;
            public int intValue;
        }
        [Serializable] private sealed class VisualPlan
        {
            public string classification;
            public int sourceBindings;
            public VisualScene[] scenes;
        }
        [Serializable] private sealed class VisualScene
        {
            public string sceneId;
            public VisualBinding[] bindings;
        }
        [Serializable] private sealed class VisualBinding
        {
            public int rectTransformPathId;
            public int imageComponentPathId;
            public int gameObjectPathId;
            public string sourceObjectSha256;
            public string spriteFile;
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

            var verified = JsonUtility.FromJson<VerifiedPlan>(
                Encoding.UTF8.GetString(ReadOutput("verified-ui-prefab-plan.json")));
            var visual = JsonUtility.FromJson<VisualPlan>(
                Encoding.UTF8.GetString(ReadOutput("verified-visual-preview-plan.json")));
            if (verified == null || visual == null ||
                verified.classification !=
                    "TWO_BACKEND_STRICT_SOURCE_VERIFIED_UI_FIELDS" ||
                verified.verifiedComponents != 1108 ||
                verified.verifiedFieldValues != 7451 ||
                verified.singleBackendExcludedFieldValues != 651 ||
                visual.classification !=
                    "EXACT_SOURCE_SPRITES_ON_DUAL_VERIFIED_IMAGE_COMPONENTS" ||
                visual.sourceBindings != 963 ||
                verified.scenes == null || visual.scenes == null)
                throw new InvalidDataException(
                    "Original REF04 source Image plans are incomplete.");
            var fieldScenes = verified.scenes.Where(x => x.sceneId == Ref).ToArray();
            var visualScenes = visual.scenes.Where(x => x.sceneId == Ref).ToArray();
            if (fieldScenes.Length != 1 || visualScenes.Length != 1 ||
                fieldScenes[0].components == null ||
                visualScenes[0].bindings == null ||
                visualScenes[0].bindings.Length != 265)
                throw new InvalidDataException(
                    "REF04 source Image inventory does not contain 265 unique bindings.");

            var originalImages = fieldScenes[0].components
                .Where(x => x.className == "UnityEngine.UI.Image")
                .ToDictionary(x => x.componentPathId);
            var originalSprites = visualScenes[0].bindings
                .ToDictionary(x => x.imageComponentPathId);
            var seen = new HashSet<int>();
            foreach (var item in src.images)
            {
                if (item == null || !seen.Add(item.componentPathId) ||
                    !SourcePngName(item.spriteFile) ||
                    !originalImages.TryGetValue(item.componentPathId, out var owner) ||
                    !originalSprites.TryGetValue(item.componentPathId, out var pointer) ||
                    owner.gameObjectPathId != item.gameObjectPathId ||
                    owner.rectTransformPathId != item.rectTransformPathId ||
                    owner.rawObjectSha256 != item.sourceObjectSha256 ||
                    pointer.gameObjectPathId != item.gameObjectPathId ||
                    pointer.rectTransformPathId != item.rectTransformPathId ||
                    pointer.sourceObjectSha256 != item.sourceObjectSha256 ||
                    pointer.spriteFile != item.spriteFile)
                    throw new InvalidDataException(
                        "REF04 original Image/Sprite PathID or hash differs: " +
                        (item == null ? "null Image" : item.componentPathId.ToString()));
                var type = owner.fields == null ? null :
                    owner.fields.Where(x => x.name == "m_Type").ToArray();
                if (type == null || type.Length != 1 ||
                    type[0].kind != "int" ||
                    type[0].intValue != item.verifiedImageType ||
                    item.verifiedImageType < 0 || item.verifiedImageType > 3)
                    throw new InvalidDataException(
                        "REF04 source Image Type is not independently verified: " +
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
            if (seen.Count != 265 || originalSprites.Count != 265)
                throw new InvalidDataException(
                    "REF04 exact source Sprite/Image inventory contains gaps.");
            return src;
        }

        private sealed class GeometryReport
        {
            public List<Geometry> compatible = new List<Geometry>();
            public List<string> incompatible = new List<string>();
        }

        private static GeometryReport Resolve(Source src)
        {
            var all = new Dictionary<string, Geometry>();
            // Any native-Rect/PNG disagreement disqualifies the *entire Sprite
            // file*, even when referenced by several source Image components.
            // Never apply original 9-slice border to an exported image whose
            // pixel rectangle is materially smaller/different than the source.
            var unsafeFiles = new SortedDictionary<string, string>(
                StringComparer.Ordinal);
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
                var original = new Geometry {
                    filename=row.spriteFile, assetPath=path, importer=importer,
                    border=new Vector4(row.border[0],row.border[1],
                                       row.border[2],row.border[3]),
                    ppu=row.pixelsPerUnit, width=row.sourceRectSize[0],
                    height=row.sourceRectSize[1],
                };
                if (all.TryGetValue(row.spriteFile,out var existing))
                {
                    if (!Eq(existing.border,original.border) ||
                        !Eq(existing.ppu,original.ppu) ||
                        !Eq(existing.width,original.width) ||
                        !Eq(existing.height,original.height))
                        throw new InvalidDataException(
                            "Conflicting source geometry for one Sprite: " + row.spriteFile);
                }
                else
                    all.Add(row.spriteFile,original);

                // <=1px allows fractional Unity m_Rect roundoff. 84x92 versus
                // 84x86 is a genuine source/export dimension disagreement.
                // It may indicate atlas trimming, but DO NOT infer padding or
                // sprite offsets without separately verified source metadata.
                if (Mathf.Abs(sprite.rect.width-row.sourceRectSize[0]) > 1f ||
                    Mathf.Abs(sprite.rect.height-row.sourceRectSize[1]) > 1f)
                    unsafeFiles[row.spriteFile] =
                        row.spriteFile + " | native=" + row.sourceRectSize[0] +
                        "x" + row.sourceRectSize[1] + " | imported=" +
                        sprite.rect.width + "x" + sprite.rect.height +
                        " | SKIPPED (no 9-slice/PPU change)";
            }
            var report = new GeometryReport();
            report.compatible = all.Values
                .Where(g => !unsafeFiles.ContainsKey(g.filename))
                .OrderBy(g => g.filename).ToList();
            report.incompatible = unsafeFiles.Values.ToList();
            return report;
        }

        private static void LogUnsafeFiles(GeometryReport report)
        {
            if (report.incompatible.Count == 0) return;
            Debug.LogWarning("[REF04 SOURCE ICONS] EXPORT_RECT_MISMATCH: " +
                report.incompatible.Count + " original Sprite files have " +
                "materially different PNG dimensions. Their importer metadata " +
                "was NOT modified. Source image export must be investigated.\n" +
                string.Join("\n", report.incompatible.Take(50).ToArray()) +
                (report.incompatible.Count > 50 ?
                    "\n... additional mismatch files omitted from Console." : ""));
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
                var report = Resolve(src);
                LogUnsafeFiles(report);
                var geometries = report.compatible;
                int wrong = CountWrong(geometries);
                int unverified = src.images.Count(x=>!x.applyGeometry);
                int sliced = src.images.Count(x=>x.verifiedImageType==1);
                Debug.Log("[REF04 SOURCE ICONS] AUDIT: " +
                    src.sourceBindings + " exact original Images; " +
                    geometries.Count + " unique native-geometry Sprite files; " +
                    wrong + " compatible imported border/PPU mismatches; " +
                    report.incompatible.Count + " exported Sprite RECT mismatches BLOCKED; " +
                    unverified + " Image geometry records not proven; " +
                    sliced + " original Sliced Images. " +
                    "No Canvas/Spine/Text/Root RectTransforms changed. " +
                    "0 mismatch does NOT prove original runtime visual layout.");
                EditorUtility.DisplayDialog("REF04 static icon audit",
                    "Original Images: " + src.sourceBindings +
                    "\nSprite native geometry records: " + geometries.Count +
                    "\nCompatible border/PPU mismatches: " + wrong +
                    "\nExported Sprite RECT mismatches (blocked): " +
                    report.incompatible.Count +
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
                var report = Resolve(src);
                LogUnsafeFiles(report);
                var geometries = report.compatible;
                var toFix = geometries.Where(g =>
                    !Eq(g.importer.spriteBorder,g.border) ||
                    !Eq(g.importer.spritePixelsPerUnit,g.ppu)).ToArray();
                if (toFix.Length == 0)
                {
                    Debug.Log("[REF04 SOURCE ICONS] No COMPATIBLE border/PPU mismatches; " +
                        report.incompatible.Count + " source/export RECT mismatches " +
                        "remain BLOCKED. No asset changed.");
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
                    "corrected; " + report.incompatible.Count +
                    " source/export RECT mismatch files SKIPPED. " +
                    "Original PNG bytes untouched; 3C fields, Canvas, Spine, Text " +
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
