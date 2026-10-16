using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// NEW_PROJECT_DESIGN: local, reversible UI responses for AssetRipper PanelHeroInfo.
// Never synthesizes HeroModel, inventory, currency, server responses, or purchases.
// Existing serialized Button.OnClick handlers remain untouched.
[DisallowMultipleComponent]
public sealed class PanelHeroInfoOfflineBridge : MonoBehaviour
{
    public enum ResponseKind
    {
        Unavailable = 0,
        OpenShare = 1,
        CloseShare = 2,
        CloseHeroPanel = 3,
        ToggleFilter = 4,
        ClosePointDetail = 5,
        CloseSkillDetail = 6,
        CloseSkillDescription = 7,
        ShowExistingTab = 8
    }

    [Serializable]
    public sealed class ButtonResponse
    {
        public Button button;
        public ResponseKind kind;
        public GameObject target;
        public string title;
        public string message;
    }

    public ButtonResponse[] responses;
    public GameObject shareBox;
    public GameObject filterPanel;
    public GameObject pointDetail;
    public GameObject skillDetail;
    public GameObject skillDescription;
    public GameObject[] originalTabPanels;

    private readonly List<Button> subscribedButtons = new List<Button>();
    private readonly List<UnityAction> subscribedActions = new List<UnityAction>();
    private GameObject openNoticeLayer;

    private void Awake()
    {
        var registered = new HashSet<Button>();
        if (responses == null) return;

        foreach (ButtonResponse response in responses)
        {
            if (response == null || response.button == null || !registered.Add(response.button))
            {
                UnityEngine.Debug.LogWarning("[HTDC Hero Info] Incomplete/duplicate offline button binding.", this);
                continue;
            }

            ButtonResponse captured = response;
            UnityAction listener = () => Handle(captured);
            captured.button.onClick.AddListener(listener);
            subscribedButtons.Add(captured.button);
            subscribedActions.Add(listener);
        }
    }

    private void Handle(ButtonResponse response)
    {
        switch (response.kind)
        {
            case ResponseKind.OpenShare:
                SetVisible(shareBox, true);
                break;
            case ResponseKind.CloseShare:
                SetVisible(shareBox, false);
                break;
            case ResponseKind.CloseHeroPanel:
                CloseNotice();
                gameObject.SetActive(false); // Preserve the imported prefab for its owner to reopen.
                break;
            case ResponseKind.ToggleFilter:
                if (filterPanel != null) filterPanel.SetActive(!filterPanel.activeSelf);
                break;
            case ResponseKind.ClosePointDetail:
                SetVisible(pointDetail, false);
                break;
            case ResponseKind.CloseSkillDetail:
                SetVisible(skillDetail, false);
                break;
            case ResponseKind.CloseSkillDescription:
                SetVisible(skillDescription, false);
                break;
            case ResponseKind.ShowExistingTab:
                if (!ShowOriginalTab(response.target))
                    return;
                // The visual tab can open, but no loaded HeroModel or calculated values exist.
                ShowNotice(response.title, response.message);
                break;
            default:
                ShowNotice(response.title, response.message);
                break;
        }
    }

    private static void SetVisible(GameObject panel, bool visible)
    {
        if (panel != null) panel.SetActive(visible);
    }

    private bool ShowOriginalTab(GameObject selected)
    {
        if (selected == null || originalTabPanels == null || originalTabPanels.Length != 7)
        {
            UnityEngine.Debug.LogWarning("[HTDC Hero Info] Missing source tab references.", this);
            return false;
        }
        bool matched = false;
        foreach (GameObject tabPanel in originalTabPanels)
        {
            if (tabPanel == null)
            {
                UnityEngine.Debug.LogWarning("[HTDC Hero Info] Original tab panel was not loaded.", this);
                return false;
            }
            if (tabPanel == selected) matched = true;
        }
        if (!matched)
        {
            UnityEngine.Debug.LogWarning("[HTDC Hero Info] Refusing unrelated tab panel.", this);
            return false;
        }
        foreach (GameObject tabPanel in originalTabPanels) tabPanel.SetActive(tabPanel == selected);
        // Never invent level, rank, equipment, skill or state in the opened panel.
        return true;
    }

    private void ShowNotice(string title, string message)
    {
        if (openNoticeLayer != null) return;
        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(message))
        {
            UnityEngine.Debug.LogWarning("[HTDC Hero Info] Offline blocker is not described.", this);
            return;
        }
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            UnityEngine.Debug.LogWarning("[HTDC Hero Info] No parent Canvas for PopupMessage: " + title, this);
            return;
        }
        const string resourcePath = "prefabs/common/PopupMessage";
        GameObject prefab = Resources.Load<GameObject>(resourcePath);
        if (prefab == null)
        {
            UnityEngine.Debug.LogError("[HTDC Hero Info] Missing original PopupMessage prefab.", this);
            return;
        }

        GameObject layer = null;
        try
        {
            layer = new GameObject("HTDC Hero Info Offline Notice",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            layer.transform.SetParent(canvas.transform, false);
            var rect = layer.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            Image blocker = layer.GetComponent<Image>();
            blocker.color = Color.clear;
            blocker.raycastTarget = true;
            layer.transform.SetAsLastSibling();

            GameObject popup = Instantiate(prefab, layer.transform, false);
            popup.SetActive(true);
            bool hasTitle = false;
            bool hasMessage = false;
            foreach (Text label in popup.GetComponentsInChildren<Text>(true))
            {
                if (label.name == "TextTitle")
                {
                    label.text = title;
                    hasTitle = true;
                }
                else if (label.name == "TextMessage")
                {
                    label.text = message;
                    hasMessage = true;
                }
                else if (label.name == "Text1" || label.name == "Text2")
                    label.text = "Đóng";
            }
            if (!hasTitle || !hasMessage)
                UnityEngine.Debug.LogWarning("[HTDC Hero Info] PopupMessage missing expected text nodes.", this);
            foreach (Button button in popup.GetComponentsInChildren<Button>(true))
                if (button.name == "ButtonAction1" || button.name == "ButtonAction2"
                    || button.name == "ButtonClose" || button.name == "ButtonBack")
                    button.onClick.AddListener(CloseNotice);

            openNoticeLayer = layer;
        }
        catch (Exception error)
        {
            if (layer != null) Destroy(layer);
            UnityEngine.Debug.LogException(error, this);
        }
    }

    private void CloseNotice()
    {
        if (openNoticeLayer == null) return;
        Destroy(openNoticeLayer);
        openNoticeLayer = null;
    }

    private void OnDisable()
    {
        CloseNotice();
    }

    private void OnDestroy()
    {
        CloseNotice();
        for (int i = 0; i < subscribedButtons.Count; i++)
            if (subscribedButtons[i] != null)
                subscribedButtons[i].onClick.RemoveListener(subscribedActions[i]);
    }

    private void Update()
    {
        // This bridge owns only notices for this prefab; other Scenes keep their
        // existing Escape behavior. Avoid a global/duplicate navigation service.
        if (openNoticeLayer != null && Input.GetKeyDown(KeyCode.Escape))
            CloseNotice();
    }
}
