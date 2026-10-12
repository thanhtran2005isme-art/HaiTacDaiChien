using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        ir.sizeDelta = new Vector2(860f, 56f);
        Text labelData = info.GetComponent<Text>();
        labelData.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        labelData.fontSize = 16;
        labelData.alignment = TextAnchor.MiddleCenter;
        labelData.color = new Color(1f, 0.85f, 0.42f);
        labelData.raycastTarget = false;
        labelData.text = hero.Name + " | ID " + hero.Id + " | " +
            hero.Faction + " | " + hero.Star + " sao | " + hero.ClassName +
            "\nID kỹ năng gốc: " + hero.ActiveSkillId + " / " +
            hero.Passive1SkillId + " / " + hero.Passive2SkillId + " / " +
            hero.Passive3SkillId;
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
