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

    [Serializable]
    public sealed class ToggleRoute
    {
        public Button button;
        public GameObject target;
    }

    [Serializable]
    public sealed class NoticeRoute
    {
        public Button button;
        public string title;
        public string message;
    }

    public Route[] routes;
    public PopupRoute[] popupRoutes;
    public ToggleRoute[] toggleRoutes;
    public NoticeRoute[] noticeRoutes;
    public string homeScenePath = "Assets/Scenes/HomeScene.unity";

    private readonly List<Button> subscribedButtons = new List<Button>();
    private readonly List<UnityAction> subscribedActions = new List<UnityAction>();
    private static GameObject activePopupLayer;
    private static GameObject activeToggleTarget;

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
                    UnityEngine.Debug.LogWarning("[HTDC Navigation] Skipped incomplete Scene route.", this);
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
                    UnityEngine.Debug.LogWarning("[HTDC Navigation] Skipped incomplete Popup route.", this);
                    continue;
                }
                string destination = route.resourcePath;
                Register(route.button, () => OpenPopup(destination), seen);
            }
        }
        if (toggleRoutes != null)
        {
            foreach (ToggleRoute route in toggleRoutes)
            {
                if (route == null || route.button == null || route.target == null)
                {
                    UnityEngine.Debug.LogWarning("[HTDC Navigation] Skipped incomplete Toggle route.", this);
                    continue;
                }
                GameObject target = route.target;
                Register(route.button, () => TogglePanel(target), seen);
            }
        }
        if (noticeRoutes != null)
        {
            foreach (NoticeRoute route in noticeRoutes)
            {
                if (route == null || route.button == null
                    || string.IsNullOrEmpty(route.title)
                    || string.IsNullOrEmpty(route.message))
                {
                    UnityEngine.Debug.LogWarning("[HTDC Navigation] Skipped incomplete Notice route.", this);
                    continue;
                }
                string title = route.title;
                string message = route.message;
                Register(route.button, () => OpenNotice(title, message), seen);
            }
        }
    }

    // Existing scene objects such as Home2Scene's PanelMenu and BoxSelect
    // start inactive. Do not synthesize new menus or change their layout.
    private static void TogglePanel(GameObject target)
    {
        if (target == null)
            return;

        bool wasOpen = target.activeSelf;
        if (activeToggleTarget != null && activeToggleTarget != target)
            activeToggleTarget.SetActive(false);
        target.SetActive(!wasOpen);
        activeToggleTarget = wasOpen ? null : target;
    }

    public static bool CloseOpenToggle()
    {
        if (activeToggleTarget == null || !activeToggleTarget.activeSelf)
            return false;

        activeToggleTarget.SetActive(false);
        activeToggleTarget = null;
        return true;
    }

    // Replace only the six source ClickArea offline notices AFTER a real
    // offline hero adapter is bound; keep every persistent original onClick.
    public bool RemoveOfflineNoticeForButton(Button button)
    {
        if (button == null || noticeRoutes == null) return false;
        bool isNotice = false;
        foreach (NoticeRoute route in noticeRoutes)
            if (route != null && route.button == button) { isNotice = true; break; }
        if (!isNotice) return false;
        int index = subscribedButtons.IndexOf(button);
        if (index < 0) return false;
        button.onClick.RemoveListener(subscribedActions[index]);
        subscribedButtons.RemoveAt(index);
        subscribedActions.RemoveAt(index);
        return true;
    }

    private void Register(Button button, UnityAction callback, HashSet<Button> seen)
    {
        if (!seen.Add(button))
        {
            UnityEngine.Debug.LogWarning("[HTDC Navigation] Duplicate route for Button: " + button.name, button);
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

    private void OpenNotice(string title, string message)
    {
        UnityEngine.Debug.LogWarning(
            "[HTDC Offline] " + title + ": " + message, this);
        OpenPopup("prefabs/common/PopupMessage", title, message);
    }

    private void OpenPopup(string resourcePath, string noticeTitle = null, string noticeMessage = null)
    {
        // Only one popup is open at a time; clicking behind it cannot trigger another route.
        if (activePopupLayer != null)
            return;
        if (string.IsNullOrEmpty(resourcePath)
            || !resourcePath.StartsWith("prefabs/", StringComparison.Ordinal)
            || resourcePath.Contains("..")
            || resourcePath.IndexOf('\\') >= 0)
        {
            UnityEngine.Debug.LogError("[HTDC Navigation] Refusing unknown Popup resource: " + resourcePath, this);
            return;
        }

        // The imported HomeScene contains a root named Canvas.
        GameObject canvasObject = GameObject.Find("Canvas");
        Canvas canvas = canvasObject != null ? canvasObject.GetComponent<Canvas>() : null;
        if (canvas == null)
        {
            UnityEngine.Debug.LogError("[HTDC Navigation] HomeScene Canvas not found; cannot open " + resourcePath, this);
            return;
        }

        GameObject prefab = Resources.Load<GameObject>(resourcePath);
        if (prefab == null)
        {
            UnityEngine.Debug.LogError("[HTDC Navigation] Missing Resources prefab: " + resourcePath, this);
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

            if (noticeTitle != null)
            {
                // Reuse the game's own PopupMessage prefab, not a new UI mock.
                // Its decompiled setters are empty, so assign Text components.
                bool titleFound = false;
                bool messageFound = false;
                foreach (Text label in instance.GetComponentsInChildren<Text>(true))
                {
                    if (label.name == "TextTitle")
                    {
                        label.text = noticeTitle;
                        titleFound = true;
                    }
                    else if (label.name == "TextMessage")
                    {
                        label.text = noticeMessage;
                        messageFound = true;
                    }
                    else if (label.name == "Text1" || label.name == "Text2")
                    {
                        label.text = "Đóng";
                    }
                }
                if (!titleFound || !messageFound)
                {
                    UnityEngine.Debug.LogWarning(
                        "[HTDC Offline] PopupMessage missing title/message Text.", this);
                }
            }

            // The exported C# close handlers are stubs. Add only the closing
            // callback (not gameplay/data actions) to existing close controls.
            foreach (Button button in instance.GetComponentsInChildren<Button>(true))
            {
                if (button.name == "ButtonClose"
                    || button.name == "ButtonBack"
                    || button.name == "CloseButton"
                    || (noticeTitle != null
                        && (button.name == "ButtonAction1" || button.name == "ButtonAction2"))
                    || (resourcePath == "prefabs/home/PopupCheckin" && button.name == "BgrPopup"))
                {
                    button.onClick.AddListener(() => { CloseOpenPopup(); });
                }
            }

            activePopupLayer = modalLayer;
            UnityEngine.Debug.Log("[HTDC Navigation] Opened offline Popup preview: " + resourcePath, this);
        }
        catch (Exception error)
        {
            if (modalLayer != null)
                Destroy(modalLayer);
            UnityEngine.Debug.LogException(error, this);
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
            UnityEngine.Debug.LogError("[HTDC Navigation] Refusing unknown scene path: " + scenePath);
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

        UnityEngine.Debug.LogError("[HTDC Navigation] Scene unavailable: " + scenePath
            + ". Restore the scene asset and add it to Build Settings for player builds.");
    }
}
