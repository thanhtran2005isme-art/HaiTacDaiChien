using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class OfflineHeroHomePresenter : MonoBehaviour
{
    private OfflineHeroCatalog catalog;
    private Home2Scene original;

    private void Start()
    {
        UnityEngine.Debug.Log("[HTDC Heroes] Home2 presenter Start; initializing 6 offline slots.", this);
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
