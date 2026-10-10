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
using UnityEngine.UI;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// P6 offline REF04 operator workflow. Uses ONLY private XAPK-proven
    /// source reports and existing separate native-Sprite study importer.
    /// Never claims original runtime Canvas, SafeArea, Text or pixel parity.
    /// It does not edit any original source Prefab/Scene/PNG.
    /// </summary>
    public sealed class Ref04P6OfflineWorkspace : EditorWindow
    {
        private const string ReportName = "ref04-p6-offline-ui-gaps.json";
        private const string SceneId = "REF04-home-crew";
        private const string LocalRoot = "Assets/LocalReconstruction";
        private const string ThreeCPrefabs = LocalRoot + "/VerifiedManagedFieldPrefabs";
        private const string ThreeCScenes = LocalRoot + "/VerifiedManagedFieldScenes";
        private const string ThreeC = LocalRoot +
            "/VerifiedManagedFieldPrefabs/REF04-home-crew_VERIFIED_FIELDS_STUDY.prefab";
        private const string NativeManifest =
            "output/ref04-source-logical-sprite-previews/manifest.json";
        private const string PreviewPrefab = LocalRoot +
            "/Ref04NativeBoundsStudyPrefabs/REF04-home-crew_NATIVE_BOUNDS_STUDY.prefab";
        private const string PreviewScene = LocalRoot +
            "/Ref04NativeBoundsStudyScenes/REF04-home-crew_NATIVE_BOUNDS_STUDY.unity";
        private string status = "Chưa kiểm tra dữ liệu P6 offline.";
        private MessageType severity = MessageType.Info;
        private Vector2 scroll;

        [Serializable]
        private sealed class Counts
        {
            public int spriteLinkedImages;
            public int sourceGeometryVerified;
            public int sourceGeometryBlocked;
            public int allDualVerifiedImages;
            public int verifiedImagesWithoutSourceSprite;
            public int layoutSerializedFieldsTwoSchemaVerified;
            public int originalTextSourceComponents;
            public int P2RawShaBackedComponents;
            public int P2IdentityOnlyComponents;
            public int P2ExcludedFieldNamesWithoutSha;
        }

        [Serializable]
        private sealed class ImageProof
        {
            public int originalImageComponentPathId;
            public int originalGameObjectPathId;
            public int originalRectTransformPathId;
            public string originalImageObjectSha256;
            public string spriteFile;
            public string spriteGeometrySourceStatus;
            public bool geometryMayBeAppliedToSourceOnlyStudy;
        }

        [Serializable]
        private sealed class Report
        {
            public string classification;
            public string sceneId;
            public string mode;
            public string p5ReportSha256;
            public string inventoryReportSha256;
            public string geometryReportSha256;
            public Counts counts;
            public ImageProof[] spriteGeometryAudit;
            public bool sourceOnlyFieldPromotionAllowed;
            public bool requiresOriginalAndroidGameToRun;
            public bool requiresOriginalGameServer;
            public bool newBackendIsAuthoritativeForNewGameplay;
            public bool originalGameBackendBehaviorVerified;
            public bool originalXapkPixelPerfectMatchProven;
            public bool originalUiAssetsChanged;
            public bool unityAssetsChanged;
        }

        private static string RepositoryRoot =>
            Path.GetFullPath(Path.Combine(Application.dataPath, "..", ".."));

        private static string OutputPath(string filename) =>
            Path.Combine(RepositoryRoot, "output", filename);

        private static void Require(bool condition, string description)
        {
            if (!condition) throw new InvalidDataException(
                "P6 OFFLINE BLOCKED: " + description);
        }

        private static string Digest(string path)
        {
            Require(File.Exists(path), "Thiếu dữ liệu nguồn: " +
                Path.GetFileName(path));
            using (var hash = SHA256.Create())
            using (var input = File.OpenRead(path))
                return BitConverter.ToString(hash.ComputeHash(input))
                    .Replace("-", "").ToLowerInvariant();
        }

        private static bool ValidSha(string value) =>
            !string.IsNullOrEmpty(value) &&
            System.Text.RegularExpressions.Regex.IsMatch(
                value, @"\A[0-9a-f]{64}\z");
        private static bool ValidSpriteFile(string value) =>
            !string.IsNullOrEmpty(value) &&
            System.Text.RegularExpressions.Regex.IsMatch(
                value, @"\A[0-9a-f]{32}\.png\z");

        private static Report VerifyOfflineReport()
        {
            string path = OutputPath(ReportName);
            Require(File.Exists(path) && new FileInfo(path).Length <=
                80L * 1024 * 1024, "Chưa có báo cáo P6 riêng tư hoặc tệp quá lớn");
            var report = JsonUtility.FromJson<Report>(
                File.ReadAllText(path, Encoding.UTF8));
            Require(report != null &&
                report.classification ==
                    "REF04_P6_OFFLINE_XAPK_TO_UNITY_SOURCE_GAP_AUDIT" &&
                report.sceneId == SceneId &&
                report.mode == "OFFLINE_XAPK_SOURCE_TO_NEW_UNITY_CLIENT" &&
                report.counts != null &&
                report.spriteGeometryAudit != null &&
                report.counts.spriteLinkedImages == 265 &&
                report.counts.allDualVerifiedImages == 299 &&
                report.counts.verifiedImagesWithoutSourceSprite == 34 &&
                report.counts.layoutSerializedFieldsTwoSchemaVerified == 168 &&
                report.counts.originalTextSourceComponents == 62 &&
                report.counts.P2RawShaBackedComponents +
                    report.counts.P2IdentityOnlyComponents == 15 &&
                report.counts.spriteLinkedImages ==
                    report.spriteGeometryAudit.Length &&
                report.counts.sourceGeometryVerified +
                    report.counts.sourceGeometryBlocked == 265 &&
                !report.sourceOnlyFieldPromotionAllowed &&
                !report.requiresOriginalAndroidGameToRun &&
                !report.requiresOriginalGameServer &&
                report.newBackendIsAuthoritativeForNewGameplay &&
                !report.originalGameBackendBehaviorVerified &&
                !report.originalXapkPixelPerfectMatchProven &&
                !report.originalUiAssetsChanged && !report.unityAssetsChanged,
                "Báo cáo P6 không khớp các điều kiện phục dựng offline");
            Require(ValidSha(report.p5ReportSha256) &&
                ValidSha(report.inventoryReportSha256) &&
                ValidSha(report.geometryReportSha256) &&
                Digest(OutputPath("ref04-p5-cross-phase-source-integrity.json")) ==
                    report.p5ReportSha256 &&
                Digest(OutputPath("ref04-full-source-inventory.json")) ==
                    report.inventoryReportSha256 &&
                Digest(OutputPath("ref04-static-image-geometry.json")) ==
                    report.geometryReportSha256,
                "SHA nguồn P5/Inventory/Geometry thay đổi; chạy lại audit P6");

            var ids = new HashSet<int>();
            var sprites = new HashSet<string>();
            int geometryVerified = 0;
            foreach (var item in report.spriteGeometryAudit)
            {
                Require(item != null &&
                    item.originalImageComponentPathId > 0 &&
                    item.originalGameObjectPathId > 0 &&
                    item.originalRectTransformPathId > 0 &&
                    ids.Add(item.originalImageComponentPathId) &&
                    ValidSha(item.originalImageObjectSha256) &&
                    ValidSpriteFile(item.spriteFile),
                    "Image owner, component hoặc Sprite nguồn không hợp lệ");
                sprites.Add(item.spriteFile);
                bool verified = item.spriteGeometrySourceStatus ==
                    "ORIGINAL_NATIVE_GEOMETRY_VERIFIED";
                Require(verified == item.geometryMayBeAppliedToSourceOnlyStudy &&
                    (verified || item.spriteGeometrySourceStatus ==
                        "BLOCKED_NATIVE_GEOMETRY_UNVERIFIED"),
                    "Trạng thái geometry không khớp bằng chứng gốc");
                if (verified) geometryVerified++;
            }
            Require(geometryVerified == report.counts.sourceGeometryVerified &&
                sprites.Count == 88,
                "Báo cáo Sprite/geometry REF04 không nhất quán");
            return report;
        }

        private static bool AnyExistingThreeCOutput()
        {
            // The legacy 3C builder processes FIVE scenes and can overwrite
            // existing generated prefab copies. Guard the *entire* destination
            // set before calling it, not only the REF04 destination.
            return (AssetDatabase.IsValidFolder(ThreeCPrefabs) &&
                    AssetDatabase.FindAssets("t:Prefab",
                        new[] { ThreeCPrefabs }).Length > 0) ||
                   (AssetDatabase.IsValidFolder(ThreeCScenes) &&
                    AssetDatabase.FindAssets("t:Scene",
                        new[] { ThreeCScenes }).Length > 0);
        }

        /// <summary>
        /// Checks the GENERATED copy, without opening a scene or changing assets.
        /// The original serialized hierarchy still does not prove runtime HUD.
        /// </summary>
        private static void VerifyGeneratedStudy(Report source)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PreviewPrefab);
            Require(prefab != null &&
                AssetDatabase.LoadAssetAtPath<SceneAsset>(PreviewScene) != null,
                "Prefab/Scene REF04 Study chưa được tạo");
            var note = prefab.GetComponent<VerifiedVisualPreviewEvidence>();
            Require(note != null &&
                note.sourceSceneId == SceneId &&
                note.sourceBoundSprites == 265 &&
                note.provisionalPreviewReferenceResolution ==
                    new Vector2(1600, 900) &&
                note.limitations != null &&
                note.limitations.Contains("NOT authenticated"),
                "Thiếu nhãn rõ ràng về Canvas PREVIEW không phải gốc");
            var notes = prefab.GetComponentsInChildren<ManagedUiSourceEvidence>(true)
                .Where(x => x.originalClassName == "UnityEngine.UI.Image")
                .ToArray();
            Require(notes.Length == 299, "Không đủ 299 Image component đã xác minh");
            var byId = new Dictionary<int, ManagedUiSourceEvidence>();
            foreach (var item in notes)
            {
                Require(item.sourceSceneId == SceneId &&
                    item.exactTwoBackendFieldAgreement &&
                    item.GetComponent<Image>() != null &&
                    byId.TryAdd(item.sourceMonoBehaviourPathId, item),
                    "Component Image trùng ID hoặc không có bằng chứng 3C");
            }
            foreach (var src in source.spriteGeometryAudit)
            {
                Require(byId.TryGetValue(src.originalImageComponentPathId,
                    out var item) &&
                    item.sourceGameObjectPathId ==
                        src.originalGameObjectPathId &&
                    item.sourceRectTransformPathId ==
                        src.originalRectTransformPathId &&
                    item.sourceObjectSha256 ==
                        src.originalImageObjectSha256 &&
                    item.GetComponent<Image>().sprite != null,
                    "Mất Image/Sprite gốc theo Component PathID");
            }
            // The 34 source Images WITHOUT Sprite pointers still exist as
            // independent Image components: never silently delete them.
            Require(byId.Count - source.spriteGeometryAudit.Length == 34,
                "34 Image không có Sprite nguồn đã bị mất khỏi bản làm việc");
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/P6 - REF04 offline Unity workspace")]
        public static void Open()
        {
            var window = GetWindow<Ref04P6OfflineWorkspace>(
                "P6 REF04 Offline Workspace");
            window.minSize = new Vector2(500, 340);
            window.Show();
        }

        private void OnGUI()
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            EditorGUILayout.LabelField("REF04 — P6 Unity Study từ XAPK",
                EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "Game gốc không cần hoạt động. XAPK là nguồn UI; " +
                "Unity Study là bản làm việc. Backend mới là thiết kế riêng.",
                MessageType.Info);
            EditorGUILayout.HelpBox(
                "265 Image/Sprite geometry đã xác minh ≠ tọa độ runtime. " +
                "Canvas 1600×900 của Study chỉ là PREVIEW, không phải cấu hình gốc.",
                MessageType.Warning);
            EditorGUILayout.HelpBox(status, severity);
            if (GUILayout.Button("1. Kiểm tra dữ liệu P5/P6 offline (không sửa file)"))
                InvokeSafe(() =>
                {
                    var doc = VerifyOfflineReport();
                    return "Bằng chứng gốc PASS: " +
                        doc.counts.spriteLinkedImages + " Image có Sprite; " +
                        doc.counts.sourceGeometryVerified + " geometry đã chứng minh; " +
                        doc.counts.verifiedImagesWithoutSourceSprite +
                        " Image không có Sprite; " +
                        doc.counts.originalTextSourceComponents + " Text nguồn.";
                });
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("2. Chuẩn bị Prefab 3C Study", EditorStyles.boldLabel);
            bool has3c = AssetDatabase.LoadAssetAtPath<GameObject>(ThreeC) != null;
            bool any3c = AnyExistingThreeCOutput();
            EditorGUILayout.LabelField(has3c ? "3C source-study: có" :
                any3c ? "3C study khác tồn tại: KHÔNG tự ghi đè" :
                "3C source-study: chưa có");
            using (new EditorGUI.DisabledScope(any3c || EditorApplication.isPlaying))
            {
                if (GUILayout.Button("Dựng 3C Prefabs từ field đã kiểm chứng"))
                    InvokeSafe(() =>
                    {
                        VerifyOfflineReport();
                        Require(!AnyExistingThreeCOutput(),
                            "3C study outputs đã tồn tại, từ chối ghi đè");
                        if (!EditorUtility.DisplayDialog("Tạo bản Study riêng?",
                            "Công cụ 3C sẽ dựng 5 prefab/scenes trong " +
                            "LocalReconstruction; không sửa Prefab nguồn. " +
                            "Không coi Canvas runtime là đã khôi phục.", "Tiếp tục", "Hủy"))
                            return "Đã hủy";
                        VerifiedManagedUiPrefabImporter.Build();
                        Require(AssetDatabase.LoadAssetAtPath<GameObject>(
                            ThreeC) != null,
                            "3C Build chưa tạo Prefab. Xem Console và dữ liệu nguồn.");
                        return "Đã tạo bản 3C study; chưa dựng REF04 native Sprite.";
                    });
            }
            bool nativeReady = File.Exists(OutputPath(
                "ref04-source-logical-sprite-previews/manifest.json"));
            EditorGUILayout.LabelField(nativeReady ?
                "Tight Sprite source manifest: có" :
                "Tight Sprite source manifest: thiếu. Chạy Python chuẩn bị.");
            EditorGUILayout.SelectableLabel(
                "py -3 tools/prepare_ref04_native_bounds_preview.py",
                EditorStyles.textField, GUILayout.Height(22));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("3. Dựng bản REF04 Study", EditorStyles.boldLabel);
            bool hasStudy =
                AssetDatabase.LoadAssetAtPath<GameObject>(PreviewPrefab) != null ||
                AssetDatabase.LoadAssetAtPath<SceneAsset>(PreviewScene) != null;
            using (new EditorGUI.DisabledScope(
                !has3c || !nativeReady || hasStudy || EditorApplication.isPlaying))
            {
                if (GUILayout.Button("Dựng REF04 Study — từ Sprite nguồn"))
                    InvokeSafe(() =>
                    {
                        var doc = VerifyOfflineReport();
                        Require(doc.counts.sourceGeometryVerified == 265 &&
                            doc.counts.sourceGeometryBlocked == 0,
                            "Chưa đủ 265 geometry gốc; không dựng từ trường thiếu proof");
                        if (!EditorUtility.DisplayDialog("REF04 SOURCE STUDY",
                            "Tạo Prefab/Scene mới trong LocalReconstruction. " +
                            "Các RectTransform con giữ nguyên field 3C đã kiểm chứng. " +
                            "Canvas 1600×900 và camera vẫn chỉ là PREVIEW. " +
                            "Không chạm source art hoặc gameplay.", "Dựng", "Hủy"))
                            return "Đã hủy";
                        Ref04NativeBoundsPreviewImporter.Build();
                        VerifyGeneratedStudy(doc);
                        return "Đã tạo và đối chiếu 299 Image (265 Sprite + " +
                            "34 không Sprite) trong Prefab Study. " +
                            "Canvas/viewport gốc vẫn BLOCKED.";
                    });
            }
            using (new EditorGUI.DisabledScope(
                !hasStudy || EditorApplication.isPlaying))
            {
                if (GUILayout.Button("4. Kiểm tra Prefab REF04 đã dựng"))
                    InvokeSafe(() =>
                    {
                        var doc = VerifyOfflineReport();
                        VerifyGeneratedStudy(doc);
                        return "Prefab source-study PASS: 299 Image/265 " +
                            "Sprite; bản gốc không được sửa. Chưa chứng minh HUD runtime.";
                    });
                if (GUILayout.Button("5. Mở REF04 Study Scene trong Unity"))
                    InvokeSafe(() =>
                    {
                        var doc = VerifyOfflineReport();
                        VerifyGeneratedStudy(doc);
                        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                            return "Đã hủy mở Scene";
                        EditorSceneManager.OpenScene(PreviewScene);
                        return "Đã mở REF04 Study. Đây là PREVIEW; chưa có " +
                            "backend và chưa có xác minh bố cục runtime.";
                    });
            }
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(
                "Tiếp theo: xem Scene/Game View, ghi nhận khoảng trống Canvas/" +
                "SafeArea/Text; mọi chức năng và API cho backend mới phải gắn nhãn " +
                "NEW_PROJECT_DESIGN, không được giả nhận là XAPK gốc.",
                MessageType.Info);
            EditorGUILayout.EndScrollView();
        }

        private void InvokeSafe(Func<string> action)
        {
            try
            {
                status = action();
                severity = MessageType.Info;
            }
            catch (Exception exception)
            {
                status = exception.GetBaseException().Message;
                severity = MessageType.Error;
                Debug.LogException(exception);
            }
            Repaint();
        }
    }
}
#endif
