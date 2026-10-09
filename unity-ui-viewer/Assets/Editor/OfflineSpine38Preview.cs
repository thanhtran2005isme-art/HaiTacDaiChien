#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HaiTac.OfflineViewer;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// Opt-in REAL Spine-Unity playback preview. Uses reflection so this
    /// GitHub repository can compile with NO proprietary runtime installed.
    /// Never attaches SkeletonGraphic to the original reconstructed scene.
    /// </summary>
    public sealed class OfflineSpine38Preview : EditorWindow
    {
        private const string Root = "Assets/LocalReconstruction";
        private const string PreviewScenes = Root + "/SpinePreviews";
        private readonly List<SpineReferenceEvidence> matches =
            new List<SpineReferenceEvidence>();
        private int selectedPack;
        private int selectedAnimation;
        private bool experimentalUnityVersion;
        private Vector2 scroll;
        private string lastDiagnostic = "Not checked yet.";
        private const string Verified = "content_chain_verified_field_unverified";

        [MenuItem("Tools/HaiTac Offline UI Viewer/Spine 3.8/Diagnose and preview real animations")]
        public static void Open()
        {
            GetWindow<OfflineSpine38Preview>("Local Spine 3.8 test");
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Spine 3.8/Log runtime and asset status")]
        public static void DiagnoseFromMenu()
        {
            var window = GetWindow<OfflineSpine38Preview>("Local Spine 3.8 test");
            window.RefreshReferences();
            window.Diagnose();
        }

        private static Type FindRuntimeType(string fullName)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = assembly.GetType(fullName, false);
                if (type != null) return type;
            }
            return null;
        }

        private static bool OfficiallySupportedUnity
        {
            get
            {
                string version = Application.unityVersion;
                int dot = version.IndexOf('.');
                if (dot < 0 || !int.TryParse(version.Substring(0, dot),
                        out int major)) return false;
                if (major > 2020) return false;
                if (major < 2017) return false;
                if (major == 2020)
                    return version.StartsWith("2020.1") ||
                           version.StartsWith("2020.2") ||
                           version.StartsWith("2020.3");
                return true;
            }
        }

        private void OnEnable() { RefreshReferences(); }

        private void RefreshReferences()
        {
            matches.Clear();
            var loaded = SceneManager.GetActiveScene();
            if (!loaded.IsValid() || !loaded.path.Replace('\\', '/')
                    .StartsWith(Root + "/Scenes/", StringComparison.Ordinal))
                return;

            foreach (var root in loaded.GetRootGameObjects())
            {
                foreach (var note in root.GetComponentsInChildren<SpineReferenceEvidence>(true))
                {
                    if (note.contentEvidenceStatus == Verified &&
                        !string.IsNullOrEmpty(note.localPackId) &&
                        note.sourceSkeletonJson != null &&
                        note.sourceAtlasText != null &&
                        note.sourceAtlasTextures != null &&
                        note.sourceAtlasTextures.Length > 0 &&
                        note.sourceAtlasTextures.All(texture => texture != null) &&
                        !string.IsNullOrWhiteSpace(note.knownAnimationNames))
                        matches.Add(note);
                }
            }
            matches.Sort((a, b) => string.CompareOrdinal(a.localPackId, b.localPackId));
            selectedPack = Mathf.Clamp(selectedPack, 0, Math.Max(0, matches.Count - 1));
            selectedAnimation = 0;
        }

        private static string[] AnimationChoices(SpineReferenceEvidence note)
        {
            return note.knownAnimationNames.Split(',')
                .Select(x => x.Trim()).Where(x => x.Length > 0)
                .Distinct(StringComparer.Ordinal).OrderBy(x => x, StringComparer.Ordinal)
                .ToArray();
        }

        private static string FolderFor(SpineReferenceEvidence note)
        {
            if (note == null || note.localPackId == null ||
                !System.Text.RegularExpressions.Regex.IsMatch(note.localPackId,
                    @"^[0-9a-f]{24}$"))
                throw new InvalidDataException("Unverified local Spine pack ID.");
            string folder = Root + "/SpinePacks/" + note.localPackId;
            string actual = AssetDatabase.GetAssetPath(note.sourceSkeletonJson);
            if (!actual.StartsWith(folder + "/", StringComparison.Ordinal))
                throw new InvalidDataException("Spine JSON is not inside its exact local pack.");
            string atlas = AssetDatabase.GetAssetPath(note.sourceAtlasText);
            if (!atlas.StartsWith(folder + "/", StringComparison.Ordinal))
                throw new InvalidDataException("Atlas text is not inside its exact pack.");
            if (note.sourceAtlasTextures.Any(t => !AssetDatabase.GetAssetPath(t)
                    .StartsWith(folder + "/", StringComparison.Ordinal)))
                throw new InvalidDataException("Atlas texture is outside original pack.");
            return folder;
        }

        private static UnityEngine.Object FindMatchingSkeletonData(
            SpineReferenceEvidence note, Type dataType)
        {
            string folder = FolderFor(note);
            var verified = new List<UnityEngine.Object>();
            foreach (string guid in AssetDatabase.FindAssets("t:ScriptableObject",
                                                               new[] { folder }))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath(path, dataType);
                if (asset == null) continue;
                var serialized = new SerializedObject(asset);
                var skeleton = serialized.FindProperty("skeletonJSON");
                if (skeleton == null ||
                    skeleton.propertyType != SerializedPropertyType.ObjectReference ||
                    skeleton.objectReferenceValue != note.sourceSkeletonJson)
                    continue;

                var atlases = serialized.FindProperty("atlasAssets");
                if (atlases == null || !atlases.isArray || atlases.arraySize == 0)
                    continue;
                bool matchingAtlas = false;
                for (int i = 0; i < atlases.arraySize; i++)
                {
                    var atlas = atlases.GetArrayElementAtIndex(i);
                    if (atlas.propertyType != SerializedPropertyType.ObjectReference ||
                        atlas.objectReferenceValue == null)
                        continue;
                    var atlasData = new SerializedObject(atlas.objectReferenceValue);
                    var atlasFile = atlasData.FindProperty("atlasFile");
                    if (atlasFile != null &&
                        atlasFile.propertyType == SerializedPropertyType.ObjectReference &&
                        atlasFile.objectReferenceValue == note.sourceAtlasText)
                        matchingAtlas = true;
                }
                if (matchingAtlas) verified.Add(asset);
            }
            if (verified.Count != 1)
                throw new InvalidDataException("Expected exactly one generated " +
                    "SkeletonDataAsset whose skeletonJSON and atlasFile match " +
                    note.candidateSkeletonName + "; found " + verified.Count +
                    ". Import licensed Spine-Unity 3.8, then Reimport this pack.");
            return verified[0];
        }

        private static void ValidateAnimation(UnityEngine.Object skeletonAsset,
                                              string animation)
        {
            MethodInfo method = skeletonAsset.GetType().GetMethod(
                "GetSkeletonData", new[] { typeof(bool) });
            if (method == null)
                throw new MissingMethodException("Runtime GetSkeletonData(bool) not found.");
            var data = method.Invoke(skeletonAsset, new object[] { false });
            if (data == null)
                throw new InvalidDataException("Spine could not parse the skeleton data.");
            var find = data.GetType().GetMethod("FindAnimation", new[] { typeof(string) });
            if (find == null || find.Invoke(data, new object[] { animation }) == null)
                throw new InvalidDataException("Animation is missing from parsed " +
                    "SkeletonDataAsset: " + animation);
        }

        private void Diagnose()
        {
            var graphicType = FindRuntimeType("Spine.Unity.SkeletonGraphic");
            var dataType = FindRuntimeType("Spine.Unity.SkeletonDataAsset");
            int eligible = 0;
            int assetsReady = 0;
            var failures = new List<string>();
            foreach (var note in matches)
            {
                eligible++;
                if (graphicType == null || dataType == null) continue;
                try
                {
                    var asset = FindMatchingSkeletonData(note, dataType);
                    ValidateAnimation(asset, AnimationChoices(note)[0]);
                    assetsReady++;
                }
                catch (Exception error)
                {
                    failures.Add(note.candidateSkeletonName + ": " +
                        error.GetBaseException().Message);
                }
            }
            lastDiagnostic = "Unity " + Application.unityVersion +
                " | Spine-Unity 3.8 officially supported: " + OfficiallySupportedUnity +
                " | SkeletonGraphic found: " + (graphicType != null) +
                " | SkeletonDataAsset found: " + (dataType != null) +
                " | verified source packs in current scene: " + eligible +
                " | matching generated assets and parsable animations: " + assetsReady +
                (failures.Count > 0 ? "\n" + string.Join("\n", failures.ToArray()) : "");
            Debug.Log("[HaiTac Spine 3.8 diagnostic] " + lastDiagnostic);
            Repaint();
        }

        private static void Reimport(SpineReferenceEvidence note)
        {
            string folder = FolderFor(note);
            AssetDatabase.ImportAsset(folder,
                ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
            Debug.Log("[HaiTac Spine 3.8] Reimported local, evidence-matched " + folder);
        }

        private void OnGUI()
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);
            EditorGUILayout.HelpBox(
                "REAL Spine-Unity 3.8 diagnostic only. No third-party runtime or game " +
                "art is stored in this Git repository. Only an explicitly chosen, " +
                "content-verified skeleton/atlas + animation gets a separate test scene. " +
                "Original REF04 and player lineup are NEVER modified.", MessageType.Info);
            EditorGUILayout.HelpBox(
                "Spine-Unity 3.8 officially supports Unity 2017.1–2020.3, not " +
                "this project's Unity 2022.3. A compatible 2020.3 test project " +
                "is recommended. Never silently downgrade the existing 2022 project.",
                MessageType.Warning);

            if (GUILayout.Button("Refresh exact scene references"))
            {
                RefreshReferences();
                Diagnose();
            }
            if (GUILayout.Button("Diagnose licensed runtime and generated assets"))
                Diagnose();
            EditorGUILayout.LabelField("Diagnostic", EditorStyles.boldLabel);
            EditorGUILayout.SelectableLabel(lastDiagnostic,
                EditorStyles.wordWrappedLabel,
                GUILayout.MinHeight(68));

            if (matches.Count == 0)
            {
                EditorGUILayout.HelpBox(
                    "Open Assets/LocalReconstruction/Scenes/REF04-home-crew.unity " +
                    "and rebuild local canvases after CHUAN_BI_DO_HOA.bat PASS.",
                    MessageType.Warning);
                EditorGUILayout.EndScrollView();
                return;
            }

            string[] descriptions = matches.Select(n =>
                n.candidateSkeletonName + "  |  " + n.name +
                "  |  component " + n.originalComponentId).ToArray();
            int next = EditorGUILayout.Popup("Verified asset (original node)",
                                             selectedPack, descriptions);
            if (next != selectedPack) { selectedPack = next; selectedAnimation = 0; }
            var selected = matches[selectedPack];
            var animations = AnimationChoices(selected);
            var choices = new[] { "-- Select a real source animation --" }
                .Concat(animations).ToArray();
            selectedAnimation = EditorGUILayout.Popup("Explicit animation",
                                                      selectedAnimation, choices);
            EditorGUILayout.LabelField("Original skeleton source",
                selected.candidateSkeletonName);
            EditorGUILayout.LabelField("Spine export version",
                selected.candidateSpineVersion);
            EditorGUILayout.LabelField("Evidence",
                "Content chain verified; original C# field and runtime state UNVERIFIED.");
            EditorGUILayout.LabelField("Source GameObject", selected.gameObject.name);
            if (!OfficiallySupportedUnity)
                experimentalUnityVersion = EditorGUILayout.ToggleLeft(
                    "I accept UNSUPPORTED Unity Editor version for a LOCAL test only",
                    experimentalUnityVersion);

            var graphicType = FindRuntimeType("Spine.Unity.SkeletonGraphic");
            var dataType = FindRuntimeType("Spine.Unity.SkeletonDataAsset");
            bool runtimeInstalled = graphicType != null && dataType != null;
            if (!runtimeInstalled)
                EditorGUILayout.HelpBox(
                    "Licensed Spine-Unity 3.8 not installed or unable to compile. " +
                    "Get the official 3.8 unitypackage; don't use the Web JS Player.",
                    MessageType.Error);
            if (GUILayout.Button("Official spine-unity download / license"))
                Application.OpenURL("https://esotericsoftware.com/spine-unity-download");
            using (new EditorGUI.DisabledScope(!runtimeInstalled))
            {
                if (GUILayout.Button("Reimport selected pack after installing runtime"))
                {
                    try { Reimport(selected); Diagnose(); }
                    catch (Exception e) { Debug.LogException(e); }
                }
            }
            bool canPreview = runtimeInstalled && selectedAnimation > 0 &&
                              (OfficiallySupportedUnity || experimentalUnityVersion) &&
                              !EditorApplication.isPlaying;
            using (new EditorGUI.DisabledScope(!canPreview))
            {
                if (GUILayout.Button("Create ISOLATED real-animation test scene"))
                    CreatePreview(selected, choices[selectedAnimation],
                                  graphicType, dataType);
            }
            EditorGUILayout.HelpBox("Press Play in the generated scene. " +
                "Console reports TRACK_ADVANCING only when a real Spine track " +
                "progresses. Inspect Game view separately for visible artwork. " +
                "This test does not establish the player's selected skin or " +
                "the correct live-game character.", MessageType.Info);
            EditorGUILayout.EndScrollView();
        }

        private static void CreatePreview(SpineReferenceEvidence note,
                                          string chosenAnimation,
                                          Type graphicType, Type dataType)
        {
            try
            {
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
                if (note.contentEvidenceStatus != Verified ||
                    !AnimationChoices(note).Contains(chosenAnimation))
                    throw new InvalidDataException("Unverified or unselected animation.");
                UnityEngine.Object data = FindMatchingSkeletonData(note, dataType);
                ValidateAnimation(data, chosenAnimation);
                var sourcePackId = note.localPackId;
                string name = note.candidateSkeletonName;
                string safeAnimation = System.Text.RegularExpressions.Regex.Replace(
                    chosenAnimation, @"[^A-Za-z0-9_-]", "_");
                // Never mutate the original REF04 or its source-linked GameObjects.
                var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                                                        NewSceneMode.Single);
                var cameraGo = new GameObject("Preview Camera", typeof(Camera));
                cameraGo.tag = "MainCamera";
                cameraGo.transform.position = new Vector3(0, 0, -10);
                var camera = cameraGo.GetComponent<Camera>();
                camera.orthographic = true;
                camera.clearFlags = CameraClearFlags.SolidColor;
                camera.backgroundColor = new Color(.06f, .08f, .12f);

                var canvasGo = new GameObject("SPINE TEST ONLY - NOT ORIGINAL GAME UI",
                    typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler),
                    typeof(GraphicRaycaster));
                canvasGo.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGo.GetComponent<RectTransform>().localScale = Vector3.one;
                var scaler = canvasGo.GetComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1600, 900);
                var testGo = new GameObject("Test " + name, typeof(RectTransform));
                var rt = testGo.GetComponent<RectTransform>();
                rt.SetParent(canvasGo.transform, false);
                rt.anchorMin = rt.anchorMax = new Vector2(.5f, .5f);
                rt.anchoredPosition = Vector2.zero;
                rt.sizeDelta = new Vector2(800, 800);
                rt.localScale = Vector3.one;
                var graphic = testGo.AddComponent(graphicType);
                var so = new SerializedObject(graphic);
                var skeleton = so.FindProperty("skeletonDataAsset");
                var anim = so.FindProperty("startingAnimation");
                var loop = so.FindProperty("startingLoop");
                if (skeleton == null || anim == null || loop == null ||
                    skeleton.propertyType != SerializedPropertyType.ObjectReference ||
                    anim.propertyType != SerializedPropertyType.String ||
                    loop.propertyType != SerializedPropertyType.Boolean)
                    throw new InvalidDataException("Unknown Spine-Unity API/fields; " +
                        "preview cannot safely bind SkeletonGraphic.");
                skeleton.objectReferenceValue = data;
                anim.stringValue = chosenAnimation; // explicit user selection ONLY
                loop.boolValue = true; // explicitly looping preview, not game state
                so.ApplyModifiedPropertiesWithoutUndo();
                var probe = testGo.AddComponent<SpinePreviewProbe>();
                probe.spineGraphic = graphic;
                probe.expectedAnimation = chosenAnimation;
                if (!AssetDatabase.IsValidFolder(PreviewScenes))
                    AssetDatabase.CreateFolder(Root, "SpinePreviews");
                string target = PreviewScenes + "/" + sourcePackId + "_" +
                    safeAnimation + ".unity";
                if (!EditorSceneManager.SaveScene(scene, target))
                    throw new IOException("Failed to save isolated preview.");
                Debug.Log("[HaiTac Spine preview] CREATED " + target +
                    "; real Spine SkeletonDataAsset verified against skeletonJSON + atlasFile." +
                    " Press Play and check TRACK_ADVANCING. Scene does not modify REF04.");
                EditorUtility.DisplayDialog("Real Spine 3.8 test prepared",
                    "Opened a separate test scene for " + name + " / " +
                    chosenAnimation + ". Press Play and monitor Console.\n" +
                    "Your original REF04 remains unchanged.", "OK");
            }
            catch (Exception exc)
            {
                Debug.LogException(exc);
                EditorUtility.DisplayDialog("Spine preview blocked",
                    exc.GetBaseException().Message +
                    "\nOriginal REF04 scene data was not changed.", "OK");
            }
        }
    }
}
#endif
