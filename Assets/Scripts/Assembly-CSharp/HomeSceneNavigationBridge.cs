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

    [Serializable]
    public sealed class PopupRoute
    {
        public Button button;
        public string resourcePath;
    }

    public Route[] routes;
    public PopupRoute[] popupRoutes;
    public string homeScenePath = "Assets/Scenes/HomeScene.unity";

    private readonly List<Button> subscribedButtons = new List<Button>();
    private readonly List<UnityAction> subscribedActions = new List<UnityAction>();
    private static GameObject activePopupLayer;

    private void Awake()
    {
        // Preserve the original Scene and its serialized callbacks.
        HomeSceneReturnHotkey returnHotkey = FindObjectOfType<HomeSceneReturnHotkey>();
        if (returnHotkey == null)
        {
            GameObject holder = new GameObject("HTDC Offline Navigation Return");
            DontDestroyOnLoad(holder);
            returnHotkey = holder.AddComponent<HomeSceneReturnHotkey>();
        }
        returnHotkey.SetHomeScene(homeScenePath);

        HashSet<Button> seen = new HashSet<Button>();
        if (routes != null)
        {
            foreach (Route route in routes)
            {
                if (route == null || route.button == null || string.IsNullOrEmpty(route.scenePath))
                {
                    Debug.LogWarning("[HTDC Navigation] Skipped incomplete Scene route.", this);
                    continue;
                }
                string destination = route.scenePath;
                Register(route.button, () => NavigateTo(destination), seen);
            }
        }
        if (popupRoutes != null)
        {
            foreach (PopupRoute route in popupRoutes)
            {
                if (route == null || route.button == null || string.IsNullOrEmpty(route.resourcePath))
                {
                    Debug.LogWarning("[HTDC Navigation] Skipped incomplete Popup route.", this);
                    continue;
                }
                string destination = route.resourcePath;
                Register(route.button, () => OpenPopup(destination), seen);
            }
        }
    }

    private void Register(Button button, UnityAction callback, HashSet<Button> seen)
    {
        if (!seen.Add(button))
        {
            Debug.LogWarning("[HTDC Navigation] Duplicate route for Button: " + button.name, button);
            return;
        }
        button.onClick.AddListener(callback);
        subscribedButtons.Add(button);
        subscribedActions.Add(callback);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < subscribedButtons.Count; i++)
        {
            if (subscribedButtons[i] != null)
                subscribedButtons[i].onClick.RemoveListener(subscribedActions[i]);
        }
    }

    private void OpenPopup(string resourcePath)
    {
        // Only one popup is open at a time; clicking behind it cannot trigger another route.
        if (activePopupLayer != null)
            return;
        if (string.IsNullOrEmpty(resourcePath)
            || !resourcePath.StartsWith("prefabs/", StringComparison.Ordinal)
            || resourcePath.Contains("..")
            || resourcePath.IndexOf('\\') >= 0)
        {
            Debug.LogError("[HTDC Navigation] Refusing unknown Popup resource: " + resourcePath, this);
            return;
        }

        // The imported HomeScene contains a root named Canvas.
        GameObject canvasObject = GameObject.Find("Canvas");
        Canvas canvas = canvasObject != null ? canvasObject.GetComponent<Canvas>() : null;
        if (canvas == null)
        {
            Debug.LogError("[HTDC Navigation] HomeScene Canvas not found; cannot open " + resourcePath, this);
            return;
        }

        GameObject prefab = Resources.Load<GameObject>(resourcePath);
        if (prefab == null)
        {
            Debug.LogError("[HTDC Navigation] Missing Resources prefab: " + resourcePath, this);
            return;
        }

        GameObject modalLayer = null;
        try
        {
            // Invisible raycast blocker: preserve the original HomeScene graphics
            // and prevent click-through while the prefab overlay is visible.
            modalLayer = new GameObject("HTDC Offline Popup Layer",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            modalLayer.transform.SetParent(canvas.transform, false);
            RectTransform rect = modalLayer.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            Image blocker = modalLayer.GetComponent<Image>();
            blocker.color = new Color(0f, 0f, 0f, 0f);
            blocker.raycastTarget = true;
            modalLayer.transform.SetAsLastSibling();

            GameObject instance = Instantiate(prefab, modalLayer.transform, false);
            instance.SetActive(true);

            // The exported C# close handlers are stubs. Add only the closing
            // callback (not gameplay/data actions) to existing close controls.
            foreach (Button button in instance.GetComponentsInChildren<Button>(true))
            {
                if (button.name == "ButtonClose"
                    || button.name == "ButtonBack"
                    || button.name == "CloseButton"
                    || (resourcePath == "prefabs/home/PopupCheckin" && button.name == "BgrPopup"))
                {
                    button.onClick.AddListener(() => { CloseOpenPopup(); });
                }
            }

            activePopupLayer = modalLayer;
            Debug.Log("[HTDC Navigation] Opened offline Popup preview: " + resourcePath, this);
        }
        catch (Exception error)
        {
            if (modalLayer != null)
                Destroy(modalLayer);
            Debug.LogException(error, this);
        }
    }

    public static bool CloseOpenPopup()
    {
        if (activePopupLayer == null)
            return false;
        Destroy(activePopupLayer);
        activePopupLayer = null;
        return true;
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
