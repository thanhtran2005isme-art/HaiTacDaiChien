#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using HaiTac.OfflineViewer;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HaiTac.OfflineViewer.Editor
{
    /// <summary>
    /// P6.2 creates a DISTINCT new-client Game View study scene. It copies the
    /// already-proven REF04 source study scene, then overrides ONLY that scene
    /// instance's CanvasScaler via a NEW_PROJECT_DESIGN responsive fit script.
    /// Does not overwrite the existing native-bounds study, 3C, sprites, XAPK
    /// or any immutable evidence. This is NOT a pixel-perfect restoration.
    /// </summary>
    public static class Ref04P62ClientSceneBuilder
    {
        private const string OriginalStudy =
            "Assets/LocalReconstruction/Ref04NativeBoundsStudyScenes/" +
            "REF04-home-crew_NATIVE_BOUNDS_STUDY.unity";
        private const string Folder =
            "Assets/LocalReconstruction/Ref04OfflineClientScenes";
        public const string ClientScene =
            Folder + "/REF04-home-crew_NEW_CLIENT_FIT_STUDY.unity";

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/P6.2 - Dung REF04 new-client fit scene")]
        public static void Build()
        {
            if (EditorApplication.isPlaying)
                throw new InvalidOperationException(
                    "P6.2 BLOCKED: Exit Play Mode before creating a client study.");

            // Verifies all 299 source Images, 265 Sprite pointers,
            // 503 RectTransforms, P5 source-report file SHAs first.
            Ref04P6OfflineWorkspace.ValidateSourceStudyForClientDesign();

            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(ClientScene) != null ||
                File.Exists(Path.GetFullPath(
                    Path.Combine(Application.dataPath,
                        ClientScene.Substring("Assets/".Length)))))
                throw new IOException(
                    "P6.2 NEW CLIENT study already exists. Refusing to overwrite.");

            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(OriginalStudy) == null)
                throw new FileNotFoundException(
                    "P6.2 original REF04 source study scene is missing.");

            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;

            string previousPath = SceneManager.GetActiveScene().path;
            if (!AssetDatabase.IsValidFolder(Folder))
                AssetDatabase.CreateFolder(
                    "Assets/LocalReconstruction", "Ref04OfflineClientScenes");
            bool copied = false;
            try
            {
                if (!AssetDatabase.CopyAsset(OriginalStudy, ClientScene))
                    throw new IOException(
                        "Could not copy the source study into independent P6.2 scene.");
                copied = true;

                var study = EditorSceneManager.OpenScene(
                    ClientScene, OpenSceneMode.Single);
                var roots = study.GetRootGameObjects()
                    .Where(obj =>
                    {
                        var proof = obj.GetComponent<VerifiedVisualPreviewEvidence>();
                        return proof != null &&
                               proof.sourceSceneId == "REF04-home-crew";
                    }).ToArray();
                if (roots.Length != 1)
                    throw new InvalidDataException(
                        "P6.2 blocked: expected exactly one REF04 study UI root.");

                var root = roots[0];
                if (root.GetComponent<Canvas>() == null ||
                    root.GetComponent<CanvasScaler>() == null ||
                    root.GetComponent<RectTransform>() == null ||
                    root.GetComponent<Ref04ClientViewportFit>() != null)
                    throw new InvalidDataException(
                        "P6.2 blocked: Canvas/CanvasScaler/root is inconsistent.");

                var fit = root.AddComponent<Ref04ClientViewportFit>();
                fit.designOnlyReferenceResolution = new Vector2(1600f, 900f);
                fit.newProjectDesignNotOriginalXapk = true;
                if (!EditorSceneManager.SaveScene(study))
                    throw new IOException("Could not save independent P6.2 scene.");

                AssetDatabase.SaveAssets();
                Debug.Log("[P6.2 NEW_PROJECT_DESIGN] New REF04 client fit scene " +
                    "created from proven source Study. Root CanvasScaler " +
                    "adapts to Game View ratio. 299 source Image components " +
                    "and all original child RectTransforms remain in the " +
                    "original Study unchanged. Character Spine, dynamic Text, " +
                    "original layout fidelity and backend are NOT recovered.");
                EditorUtility.DisplayDialog(
                    "P6.2 new client study ready",
                    "Created a separate scene under Ref04OfflineClientScenes. " +
                    "The original P6.1 Study is untouched. " +
                    "This is a NEW_PROJECT_DESIGN viewport fit, " +
                    "NOT authenticated original Canvas/SafeArea. " +
                    "Open Game View to check clipping.", "OK");
            }
            catch
            {
                // Roll back ONLY the brand-new P6.2 scene. Never touch the
                // original REF04 source study or decoded Sprite assets.
                if (copied)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(previousPath) &&
                            AssetDatabase.LoadAssetAtPath<SceneAsset>(
                                previousPath) != null)
                            EditorSceneManager.OpenScene(
                                previousPath, OpenSceneMode.Single);
                        else
                            EditorSceneManager.NewScene(
                                NewSceneSetup.EmptyScene, NewSceneMode.Single);
                    }
                    finally
                    {
                        AssetDatabase.DeleteAsset(ClientScene);
                        AssetDatabase.SaveAssets();
                    }
                }
                throw;
            }
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Source XAPK/P6.2 - Mo REF04 new-client fit scene")]
        public static void Open()
        {
            if (EditorApplication.isPlaying)
                throw new InvalidOperationException(
                    "Exit Play Mode before switching Scene.");
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(ClientScene) == null)
                throw new FileNotFoundException(
                    "P6.2 client scene is missing. Use Build first.");
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;
            EditorSceneManager.OpenScene(ClientScene, OpenSceneMode.Single);
        }
    }
}
#endif
