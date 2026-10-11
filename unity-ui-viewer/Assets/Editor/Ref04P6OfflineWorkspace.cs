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
        private string status = "Đang xác minh P6 offline...";
        private MessageType severity = MessageType.Info;
        private Vector2 scroll;

        private void OnEnable()
        {
            // Domain reload / opening EditorWindow must show an ACTUAL audit
            // result, not a persistent misleading "not checked" placeholder.
            // Only reads the ignored source JSON reports. No Unity asset edits.
            RefreshSourceStatus();
        }

        private void RefreshSourceStatus()
        {
            try
            {
                var proof = VerifyOfflineReport();
                status = "P6 nguồn PASS: " +
                    proof.counts.sourceGeometryVerified + "/265 geometry; " +
                    proof.counts.allDualVerifiedImages + " Image; " +
                    proof.counts.originalTextSourceComponents +
                    " Text nguồn. Có thể kiểm tra Prefab ở bước 4. " +
                    "Chưa chứng minh Canvas/runtime.";
                severity = MessageType.Info;
            }
            catch (Exception exception)
            {
                status = exception.GetBaseException().Message;
                severity = MessageType.Error;
                // A failed proof check is a BLOCKED state, not an Editor
                // compilation failure. The operator can regenerate the JSON
                // locally and press step 1; no source assets are modified.
            }
            Repaint();
        }

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
                ValidSha(report.geometryReportSha256),
                "Báo cáo P6 thiếu SHA dạng phẳng. Chạy lại: " +
                "py -3 tools/ref04_p6_offline_ui_plan.py");
            Require(Digest(OutputPath("ref04-p5-cross-phase-source-integrity.json")) ==
                report.p5ReportSha256,
                "SHA P5 khác báo cáo P6. Chạy lại audit P6 offline");
            Require(Digest(OutputPath("ref04-full-source-inventory.json")) ==
                report.inventoryReportSha256,
                "SHA inventory khác báo cáo P6. Chạy lại audit P6 offline");
            Require(Digest(OutputPath("ref04-static-image-geometry.json")) ==
                report.geometryReportSha256,
                "SHA Sprite geometry khác báo cáo P6. Chạy lại audit P6 offline");

            var ids = new HashSet<int>();
            var sprites = new HashSet<string>();
            int geometryVerified = 0;
            foreach (var item in report.spriteGeometryAudit)
            {
                Require(item != null &&
                    item.originalImageComponentPathId != 0 &&
                    item.originalGameObjectPathId != 0 &&
                    item.originalRectTransformPathId != 0 &&
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

        // P6.2 may consume this verified private Study. It must not
        // mutate the source/P5 reports or any existing Unity prefab.
        public static void ValidateSourceStudyForClientDesign()
        {
            var source = VerifyOfflineReport();
            Require(source.counts.sourceGeometryVerified == 265 &&
                source.counts.sourceGeometryBlocked == 0,
                "Thiếu 265 geometry; không thể dựng client từ dữ liệu thiếu");
            VerifyGeneratedStudy(source);
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
            // The imported source study is allowed to adjust ONLY its
            // provisional root Canvas; every child transform/sibling must
            // remain equal to the verified 3C study prefab.
            var original3c = AssetDatabase.LoadAssetAtPath<GameObject>(ThreeC);
            Require(original3c != null, "Thiếu 3C verified field Prefab gốc");
            var fromSource = original3c
                .GetComponentsInChildren<OriginalSerializedEvidence>(true)
                .ToDictionary(x => x.rectTransformPathId);
            var inStudy = prefab
                .GetComponentsInChildren<OriginalSerializedEvidence>(true)
                .ToDictionary(x => x.rectTransformPathId);
            Require(fromSource.Count == 503 && inStudy.Count == 503,
                "Thiếu 503 RectTransform/owner nguồn REF04");
            int rootId = original3c
                .GetComponent<OriginalSerializedEvidence>().rectTransformPathId;
            foreach (var src in fromSource)
            {
                Require(inStudy.TryGetValue(src.Key, out var copy) &&
                    copy.gameObjectPathId == src.Value.gameObjectPathId,
                    "GameObject/RectTransform PathID của bản Study không khớp");
                if (src.Key == rootId) continue; // provisional preview root
                var a = src.Value.GetComponent<RectTransform>();
                var b = copy.GetComponent<RectTransform>();
                Require(a != null && b != null &&
                    Vector2.Distance(a.anchorMin, b.anchorMin) < .0001f &&
                    Vector2.Distance(a.anchorMax, b.anchorMax) < .0001f &&
                    Vector2.Distance(a.pivot, b.pivot) < .0001f &&
                    Vector2.Distance(a.sizeDelta, b.sizeDelta) < .0001f &&
                    Vector2.Distance(a.anchoredPosition, b.anchoredPosition) < .0001f &&
                    Vector3.Distance(a.localScale, b.localScale) < .0001f &&
                    Quaternion.Angle(a.localRotation, b.localRotation) < .01f &&
                    a.GetSiblingIndex() == b.GetSiblingIndex() &&
                    a.gameObject.activeSelf == b.gameObject.activeSelf,
                    "Bản Study đã thay đổi giá trị RectTransform con gốc");
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
            if (GUILayout.Button("1. Kiểm tra lại dữ liệu nguồn P5/P6 (chỉ đọc)"))
                RefreshSourceStatus();
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
            bool nativeReady = File.Exists(Path.Combine(RepositoryRoot, NativeManifest));
            EditorGUILayout.LabelField(nativeReady ?
                "Tight Sprite source manifest: có (chưa kiểm SHA ở màn này)" :
                "Tight Sprite source manifest: thiếu. Chạy Python chuẩn bị.");
            EditorGUILayout.SelectableLabel(
                "py -3 tools/prepare_ref04_native_bounds_preview.py",
                EditorStyles.textField, GUILayout.Height(22));
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("3. Dựng bản REF04 Study", EditorStyles.boldLabel);
            bool hasStudyPrefab =
                AssetDatabase.LoadAssetAtPath<GameObject>(PreviewPrefab) != null;
            bool hasStudyScene =
                AssetDatabase.LoadAssetAtPath<SceneAsset>(PreviewScene) != null;
            bool hasAnyStudy = hasStudyPrefab || hasStudyScene;
            bool hasCompleteStudy = hasStudyPrefab && hasStudyScene;
            if (hasCompleteStudy)
                EditorGUILayout.LabelField(
                    "REF04 Study đã có Prefab + Scene. Dùng bước 4 để kiểm tra, " +
                    "bước 5 để mở. Dựng lại bị khóa để bảo vệ dữ liệu.",
                    EditorStyles.wordWrappedLabel);
            else if (hasAnyStudy)
                EditorGUILayout.HelpBox(
                    "REF04 Study CHƯA ĐẦY ĐỦ: " +
                    (hasStudyPrefab ? "chỉ có Prefab, thiếu Scene." :
                        "chỉ có Scene, thiếu Prefab.") +
                    " Đã khóa Dựng/Kiểm tra/Mở để tránh mất dữ liệu. " +
                    "Kiểm tra Console và thư mục LocalReconstruction, " +
                    "không xóa hay ghi đè tự động.",
                    MessageType.Error);
            else if (!has3c || !nativeReady)
                EditorGUILayout.HelpBox(
                    "Chưa thể dựng: " +
                    (!has3c ? "thiếu Prefab 3C. " : "") +
                    (!nativeReady ? "thiếu Tight Sprite manifest." : ""),
                    MessageType.Warning);
            else
                EditorGUILayout.HelpBox(
                    "Chưa có REF04 Study; hãy xác nhận trạng thái nguồn PASS " +
                    "bên trên trước khi dựng bản Study riêng.",
                    MessageType.Info);
            using (new EditorGUI.DisabledScope(
                !has3c || !nativeReady || hasAnyStudy || EditorApplication.isPlaying))
            {
                if (GUILayout.Button("Dựng REF04 Study — từ Sprite nguồn"))
                    InvokeSafe(() =>
                    {
                        var doc = VerifyOfflineReport();
                        Require(!AssetDatabase.LoadAssetAtPath<GameObject>(PreviewPrefab) &&
                            !AssetDatabase.LoadAssetAtPath<SceneAsset>(PreviewScene),
                            "Study đã tồn tại; từ chối ghi đè. Kiểm tra bước 4.");
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
                !hasCompleteStudy || EditorApplication.isPlaying))
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
            EditorGUILayout.LabelField(
                "P6.2 - Game View cua game moi (NEW_PROJECT_DESIGN)",
                EditorStyles.boldLabel);
            bool hasClientScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(
                Ref04P62ClientSceneBuilder.ClientScene) != null;
            EditorGUILayout.HelpBox(hasClientScene
                ? "Da co Scene P6.2 rieng. Mo Scene moi de kiem tra co con " +
                  "bi cat giao dien trong Game View khong."
                : "P6.2 tao MOT Scene moi, tu ban REF04 Study da kiem chung. " +
                  "Scene moi fit viewport theo Game View. Khong chinh sua " +
                  "Prefab/Scene goc; khong phuc dung nhan vat, Text hoac server.",
                MessageType.Info);
            using (new EditorGUI.DisabledScope(
                !hasCompleteStudy || hasClientScene || EditorApplication.isPlaying))
            {
                if (GUILayout.Button("6. Tao P6.2 NEW CLIENT viewport Study"))
                    InvokeSafe(() =>
                    {
                        Ref04P62ClientSceneBuilder.Build();
                        Require(AssetDatabase.LoadAssetAtPath<SceneAsset>(
                            Ref04P62ClientSceneBuilder.ClientScene) != null,
                            "P6.2 Scene chua duoc tao hoac da huy");
                        return "Da tao Scene P6.2 rieng. Hay bam buoc 7 mo Game View.";
                    });
            }
            using (new EditorGUI.DisabledScope(
                !hasClientScene || EditorApplication.isPlaying))
            {
                if (GUILayout.Button("7. Mo P6.2 NEW CLIENT Scene"))
                    InvokeSafe(() =>
                    {
                        Ref04P62ClientSceneBuilder.Open();
                        return "Da mo P6.2 NEW_CLIENT_FIT_STUDY: fit theo viewport " +
                            "la NEW_PROJECT_DESIGN, khong phai runtime goc.";
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
