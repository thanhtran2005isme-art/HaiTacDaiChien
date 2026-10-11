using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// NEW_PROJECT_DESIGN: editor navigation pilot, NOT recovered original IL2CPP logic.
// Existing serialized Button.onClick callbacks are intentionally preserved.
[DisallowMultipleComponent]
public sealed class HomeSceneNavigationBridge : MonoBehaviour
{
    [Serializable]
    public sealed class Route
    {
        public Button button;
        public string scenePath;
    }

    public Route[] routes;
    public string homeScenePath = "Assets/Scenes/HomeScene.unity";

    private readonly List<Button> subscribedButtons = new List<Button>();
    private readonly List<UnityAction> subscribedActions = new List<UnityAction>();

    private void Awake()
    {
        // Keep only the keyboard return helper alive across scene transitions,
        // never the original Scripts GameObject with its recovered components.
        HomeSceneReturnHotkey returnHotkey = FindObjectOfType<HomeSceneReturnHotkey>();
        if (returnHotkey == null)
        {
            GameObject holder = new GameObject("HTDC Offline Navigation Return");
            DontDestroyOnLoad(holder);
            returnHotkey = holder.AddComponent<HomeSceneReturnHotkey>();
        }

        returnHotkey.SetHomeScene(homeScenePath);

        if (routes == null)
            return;

        HashSet<Button> seen = new HashSet<Button>();
        foreach (Route route in routes)
        {
            if (route == null || route.button == null || string.IsNullOrEmpty(route.scenePath))
            {
                Debug.LogWarning("[HTDC Navigation] Skipped incomplete HomeScene route.", this);
                continue;
            }

            if (!seen.Add(route.button))
            {
                Debug.LogWarning("[HTDC Navigation] Duplicate Button mapping: " + route.button.name, route.button);
                continue;
            }

            string destination = route.scenePath;
            UnityAction callback = () => NavigateTo(destination);
            route.button.onClick.AddListener(callback);
            subscribedButtons.Add(route.button);
            subscribedActions.Add(callback);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < subscribedButtons.Count; i++)
        {
            if (subscribedButtons[i] != null)
                subscribedButtons[i].onClick.RemoveListener(subscribedActions[i]);
        }
    }

    public static void NavigateTo(string scenePath)
    {
        if (string.IsNullOrEmpty(scenePath)
            || !scenePath.StartsWith("Assets/Scenes/", StringComparison.Ordinal)
            || !scenePath.EndsWith(".unity", StringComparison.Ordinal))
        {
            Debug.LogError("[HTDC Navigation] Refusing unknown scene path: " + scenePath);
            return;
        }

        // Works in player builds only when the scene is in Build Settings.
        // SceneManager expects the Build Settings path WITHOUT the .unity suffix.
        // EditorSceneManager.LoadSceneInPlayMode instead needs the full asset path.
        string buildScenePath = scenePath.Substring(0, scenePath.Length - ".unity".Length);
        if (Application.CanStreamedLevelBeLoaded(buildScenePath))
        {
            SceneManager.LoadScene(buildScenePath, LoadSceneMode.Single);
            return;
        }

#if UNITY_EDITOR
        // Play Mode recovery workflow: EditorBuildSettings.asset is stored via
        // Git LFS, so never rewrite or infer its scene list automatically.
        if (UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.SceneAsset>(scenePath) != null)
        {
            UnityEditor.SceneManagement.EditorSceneManager.LoadSceneInPlayMode(
                scenePath, new LoadSceneParameters(LoadSceneMode.Single));
            return;
        }
#endif

        Debug.LogError("[HTDC Navigation] Scene unavailable: " + scenePath
            + ". Restore the scene asset and add it to Build Settings for player builds.");
    }
}
