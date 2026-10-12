using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Bind original imported scene references at RUNTIME. No edits to original serialized
// Scenes, prefabs, original OnClick functions or recovered AssetRipper stub classes.
public static class OfflineHeroSceneRuntime
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Install()
    {
        SceneManager.sceneLoaded -= OnLoaded;
        SceneManager.sceneLoaded += OnLoaded;
        OnLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private static void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "HeroListNewScene")
        {
            HeroListNewScene original = UnityEngine.Object.FindObjectOfType<HeroListNewScene>();
            if (original != null && original.GetComponent<OfflineHeroListPresenter>() == null)
                original.gameObject.AddComponent<OfflineHeroListPresenter>();
        }
        else if (scene.name == "Home2Scene")
        {
            Home2Scene original = UnityEngine.Object.FindObjectOfType<Home2Scene>();
            if (original != null && original.GetComponent<OfflineHeroHomePresenter>() == null)
                original.gameObject.AddComponent<OfflineHeroHomePresenter>();
        }
    }
}

public sealed class OfflineHeroHomePresenter : MonoBehaviour
{
    private OfflineHeroCatalog catalog;
    private Home2Scene original;

    private void Start()
    {
        original = GetComponent<Home2Scene>();
        if (original == null || original.m_ListHeroHome == null
            || original.m_ListHeroHome.Count != OfflineHeroParty.Capacity)
        {
            UnityEngine.Debug.LogError("[HTDC Heroes] Original six Home2 hero references missing.", this);
            return;
        }
        try { catalog = OfflineHeroCatalog.LoadFromResources(); }
        catch (Exception ex) { UnityEngine.Debug.LogException(ex, this); return; }

        List<OfflineHeroRecord> party = OfflineHeroParty.Read(catalog);
        HomeSceneNavigationBridge nav = GetComponent<HomeSceneNavigationBridge>();

        for (int i = 0; i < original.m_ListHeroHome.Count; i++)
        {
            HeroHome2 slot = original.m_ListHeroHome[i];
            if (slot == null) continue;
            OfflineHeroRecord hero = i < party.Count ? party[i] : null;

            // The 6 original click areas carried "missing HeroModel" notices.
            // Remove only that known offline notice; preserve serialized callbacks.
            Button click = slot.m_CanvasHero != null && slot.m_CanvasHero.m_GoClickArea != null
                ? slot.m_CanvasHero.m_GoClickArea.GetComponent<Button>() : null;
            if (click == null && slot.m_CanvasHero != null
                && slot.m_CanvasHero.m_GoClickArea != null)
                click = slot.m_CanvasHero.m_GoClickArea.GetComponentInChildren<Button>(true);
            if (click != null)
            {
                if (nav != null) nav.RemoveOfflineNoticeForButton(click);
                OfflineHeroRecord captured = hero;
                click.onClick.AddListener(() =>
                {
                    if (captured != null)
                        OfflineHeroDetailModal.Show(captured, GetComponentInParent<Canvas>()
                            ?? UnityEngine.Object.FindObjectOfType<Canvas>());
                    else
                        HomeSceneNavigationBridge.NavigateTo("Assets/Scenes/HeroListNewScene.unity");
                });
            }
            Display(slot, hero);
        }
        UnityEngine.Debug.Log("[HTDC Heroes] Offline party " + party.Count
            + "/6 loaded from local selection. No account inventory synthesized.", this);
    }

    private static void Display(HeroHome2 slot, OfflineHeroRecord hero)
    {
        if (slot.m_TextHeroName != null)
            slot.m_TextHeroName.text = hero != null ? hero.Name : "";
        if (slot.m_TextLevel != null)
            slot.m_TextLevel.text = hero != null ? hero.Star + " sao" : "";

        if (slot.m_CanvasHero == null) return;
        CanvasHero sourceCanvas = slot.m_CanvasHero;
        SkeletonGraphic graphic = sourceCanvas.GetComponentInChildren<SkeletonGraphic>(true);
        // Source skeleton references often have no runtime model assigned yet.
        SkeletonDataAsset skeleton = hero != null
            ? OfflineHeroArt.LoadSkeleton(hero.AvatarId) : null;
        if (graphic != null)
        {
            graphic.enabled = skeleton != null;
            if (skeleton != null)
            {
                try
                {
                    Transform ancestor = graphic.transform;
                    while (ancestor != null && ancestor != sourceCanvas.transform)
                    {
                        ancestor.gameObject.SetActive(true);
                        ancestor = ancestor.parent;
                    }
                    sourceCanvas.gameObject.SetActive(true);
                    graphic.skeletonDataAsset = skeleton;
                    graphic.Initialize(true);
                    if (graphic.Skeleton != null &&
                        graphic.Skeleton.Data.FindAnimation("idle") != null &&
                        graphic.AnimationState != null)
                        graphic.AnimationState.SetAnimation(0, "idle", true);
                }
                catch (Exception ex)
                {
                    graphic.enabled = false;
                    UnityEngine.Debug.LogWarning(
                        "[HTDC Heroes] Spine import/runtime unavailable for avatar " +
                        hero.AvatarId + ": " + ex.Message);
                }
            }
        }

        // Fallback: only the exact source portrait, never a stand-in character.
        Image fallback = sourceCanvas.transform.Find("HTDC Offline Hero Portrait")
            ? sourceCanvas.transform.Find("HTDC Offline Hero Portrait").GetComponent<Image>()
            : null;
        Sprite portrait = hero != null && (graphic == null || !graphic.enabled)
            ? OfflineHeroArt.LoadPortrait(hero.AvatarId) : null;
        if (portrait != null && fallback == null)
        {
            GameObject obj = new GameObject("HTDC Offline Hero Portrait",
                typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            obj.transform.SetParent(sourceCanvas.transform, false);
            fallback = obj.GetComponent<Image>();
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = new Vector2(90f, 125f);
        }
        if (fallback != null)
        {
            fallback.sprite = portrait;
            fallback.enabled = portrait != null;
            fallback.preserveAspect = true;
            fallback.raycastTarget = false;
        }
        if (hero != null && skeleton == null && portrait == null)
            UnityEngine.Debug.LogWarning("[HTDC Heroes] No exported Spine/portrait for "
                + hero.Name + " (avatar " + hero.AvatarId + "); showing only source name.");
    }
}

public sealed class OfflineHeroDetailModal : MonoBehaviour
{
    private static GameObject activeLayer;
    private GameObject originalPanel;

    public static void Show(OfflineHeroRecord hero, Canvas canvas)
    {
        if (hero == null || canvas == null)
        {
            UnityEngine.Debug.LogWarning("[HTDC Heroes] Cannot show hero info: hero or Canvas missing.");
            return;
        }
        CloseActive();
        GameObject prefab = Resources.Load<GameObject>("prefabs/heroinfo/PanelHeroInfo");
        if (prefab == null)
        {
            UnityEngine.Debug.LogWarning("[HTDC Heroes] Original PanelHeroInfo prefab missing.");
            return;
        }
        GameObject layer = new GameObject("HTDC Offline Hero Details",
            typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        layer.transform.SetParent(canvas.transform, false);
        RectTransform rect = layer.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
        Image blocker = layer.GetComponent<Image>();
        blocker.color = new Color(0f, 0f, 0f, 0.45f);
        blocker.raycastTarget = true;
        layer.transform.SetAsLastSibling();
        OfflineHeroDetailModal monitor = layer.AddComponent<OfflineHeroDetailModal>();
        GameObject panel = Instantiate(prefab, layer.transform, false);
        panel.SetActive(true);
        monitor.originalPanel = panel;
        activeLayer = layer;

        HeroSpineInfo sourceInfo = panel.GetComponentInChildren<HeroSpineInfo>(true);
        if (sourceInfo != null)
        {
            if (sourceInfo.m_TextHeroName != null)
                sourceInfo.m_TextHeroName.text = hero.Name;
            if (sourceInfo.m_ImageHero != null)
            {
                Sprite portrait = OfflineHeroArt.LoadPortrait(hero.AvatarId);
                sourceInfo.m_ImageHero.sprite = portrait;
                sourceInfo.m_ImageHero.color = portrait == null ? Color.clear : Color.white;
                sourceInfo.m_ImageHero.preserveAspect = true;
            }
        }
        // Multiple original subpanels contain TextHeroName; correct all displayed copies.
        foreach (Text label in panel.GetComponentsInChildren<Text>(true))
            if (label.name == "TextHeroName") label.text = hero.Name;

        // Raw configuration data only. No claim about actual account level, power or skills.
        GameObject info = new GameObject("OfflineHeroSourceData",
            typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
        info.transform.SetParent(panel.transform, false);
        RectTransform ir = info.GetComponent<RectTransform>();
        ir.anchorMin = new Vector2(0.5f, 0f);
        ir.anchorMax = new Vector2(0.5f, 0f);
        ir.pivot = new Vector2(0.5f, 0f);
        ir.anchoredPosition = new Vector2(0f, 8f);
        ir.sizeDelta = new Vector2(760f, 35f);
        Text labelData = info.GetComponent<Text>();
        labelData.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        labelData.fontSize = 18;
        labelData.alignment = TextAnchor.MiddleCenter;
        labelData.color = new Color(1f, 0.85f, 0.42f);
        labelData.raycastTarget = false;
        labelData.text = hero.Name + " | ID " + hero.Id + " | " +
            hero.Faction + " | " + hero.Star + " sao | " + hero.ClassName;
    }

    public static bool CloseActive()
    {
        if (activeLayer == null) return false;
        Destroy(activeLayer);
        activeLayer = null;
        return true;
    }

    private void Update()
    {
        // Original PanelHeroInfoOfflineBridge hides its root on Back instead of destroying.
        if (originalPanel == null || !originalPanel.activeSelf) CloseActive();
    }

    private void OnDestroy()
    {
        if (activeLayer == gameObject) activeLayer = null;
    }
}
