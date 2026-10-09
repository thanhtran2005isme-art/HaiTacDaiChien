#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HaiTac.OfflineViewer.Editor
{
    public static class OfflineDemoScene
    {
        private const string ScenePath = "Assets/Scenes/OfflineDemo.unity";

        [MenuItem("Tools/HaiTac Offline UI Viewer/Create or Open Demo Scene")]
        public static void CreateOrOpen()
        {
            if (File.Exists(ScenePath))
            {
                EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
                Debug.Log("Opened offline viewer scene: " + ScenePath);
                return;
            }

            if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
                AssetDatabase.CreateFolder("Assets", "Scenes");

            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects,
                NewSceneMode.Single);
            scene.name = "OfflineDemo";
            var mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.clearFlags = CameraClearFlags.SolidColor;
                mainCamera.backgroundColor = new Color(.055f, .075f, .11f);
            }
            if (!EditorSceneManager.SaveScene(scene, ScenePath))
                throw new IOException("Could not save " + ScenePath);

            AssetDatabase.Refresh();
            Debug.Log("Created offline viewer scene. Press Play to start the metadata viewer.");
        }

        [MenuItem("Tools/HaiTac Offline UI Viewer/Check Offline Data")]
        public static void CheckData()
        {
            const string data = "Assets/StreamingAssets/ui-scenes.json";
            if (File.Exists(data))
                EditorUtility.DisplayDialog("Offline UI Viewer",
                    "Found UI metadata at " + data +
                    "\n\nChoose the Demo Scene and press Play.", "OK");
            else
                EditorUtility.DisplayDialog("Offline UI Viewer",
                    "Missing " + data +
                    "\n\nRegenerate from repository root using:\n" +
                    "py tools\\build_unity_viewer_data.py --repo-root .", "OK");
        }
    }
}
#endif
