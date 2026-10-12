using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// NEW_PROJECT_DESIGN: reuses the ORIGINAL HeroListNewScene CellHeroThumb and ScrollView.
// Never changes original serialization or claims that the selected party is an account inventory.
public sealed class OfflineHeroListPresenter : MonoBehaviour
{
    private HeroListNewScene source;
    private OfflineHeroCatalog catalog;
    private RectTransform content;
    private OfflineHeroFaction faction = OfflineHeroFaction.All;
    private readonly List<GameObject> generated = new List<GameObject>();
    private readonly List<OfflineHeroRecord> visible = new List<OfflineHeroRecord>();
    private string status = "";

    private void Start()
    {
        source = GetComponent<HeroListNewScene>();
        if (source == null || source.m_GoPrefabCell == null || source.m_GoScrollView == null)
        {
            UnityEngine.Debug.LogError("[HTDC Heroes] Original hero scene template/scroll references missing.", this);
            return;
        }

        try { catalog = OfflineHeroCatalog.LoadFromResources(); }
        catch (Exception ex) { UnityEngine.Debug.LogException(ex, this); return; }

        ScrollRect scroll = source.m_GoScrollView.GetComponent<ScrollRect>();
        if (scroll == null || scroll.content == null)
        {
            UnityEngine.Debug.LogError("[HTDC Heroes] Original ScrollRect content unavailable.", this);
            return;
        }
        content = scroll.content;
        content.anchorMin = new Vector2(0f, 1f);
        content.anchorMax = new Vector2(1f, 1f);
        content.pivot = new Vector2(0.5f, 1f);
        content.sizeDelta = new Vector2(0f, 0f);

        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        if (grid == null) grid = content.gameObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(190f, 260f); // original GridPoolGroup serialized CellSize
        grid.spacing = new Vector2(10f, 10f);    // original serialized Spacing
        grid.padding = new RectOffset(10, 10, 10, 10);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 4;                 // original serialized ConstraintCount
        grid.childAlignment = TextAnchor.UpperLeft;
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;

        ContentSizeFitter fitter = content.GetComponent<ContentSizeFitter>();
        if (fitter == null) fitter = content.gameObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // Original TabManager is an AssetRipper stub; only add source-named UI events.
        foreach (Button button in source.m_TabFaction != null
            ? source.m_TabFaction.GetComponentsInChildren<Button>(true)
            : new Button[0])
        {
            OfflineHeroFaction target;
            switch (button.name)
            {
                case "ButtonFortress": target = OfflineHeroFaction.Fortress; break;
                case "ButtonAbyss": target = OfflineHeroFaction.Abyss; break;
                case "ButtonForest": target = OfflineHeroFaction.Forest; break;
                case "ButtonShadow": target = OfflineHeroFaction.Shadow; break;
                case "ButtonDark": target = OfflineHeroFaction.Dark; break;
                case "ButtonLight": target = OfflineHeroFaction.Light; break;
                default: continue;
            }
            OfflineHeroFaction captured = target;
            button.onClick.AddListener(() => { faction = captured; Render(); });
        }

        if (source.m_GoButtonDictionary != null)
        {
            Button all = source.m_GoButtonDictionary.GetComponent<Button>();
            if (all != null) all.onClick.AddListener(() => { faction = OfflineHeroFaction.All; Render(); });
        }

        if (source.m_BtnBack != null)
            source.m_BtnBack.onClick.AddListener(() =>
                HomeSceneNavigationBridge.NavigateTo("Assets/Scenes/Home2Scene.unity"));

        // Display dictionary entries, not a forged player collection.
        if (source.m_TextTitle != null) source.m_TextTitle.text = "Tướng · Offline";
        Render();
    }

    private void Render()
    {
        if (catalog == null || content == null || source.m_GoPrefabCell == null) return;

        foreach (GameObject clone in generated)
            if (clone != null) Destroy(clone);
        generated.Clear();
        visible.Clear();

        // Show only released mainline heroes. Never infer inventory from release=1.
        // Use one highest-star released variant per source avatar, retain actual row ID.
        Dictionary<int, OfflineHeroRecord> byAvatar = new Dictionary<int, OfflineHeroRecord>();
        foreach (OfflineHeroRecord record in catalog.Query(
            faction: faction, release: OfflineHeroReleaseFilter.Released))
        {
            if (!OfflineHeroParty.IsEligible(record)) continue;
            OfflineHeroRecord old;
            if (!byAvatar.TryGetValue(record.AvatarId, out old)
                || record.Star > old.Star || (record.Star == old.Star && record.Id > old.Id))
                byAvatar[record.AvatarId] = record;
        }

        visible.AddRange(byAvatar.Values);
        visible.Sort((a, b) => a.Id.CompareTo(b.Id));
        HashSet<int> selected = new HashSet<int>();
        foreach (OfflineHeroRecord hero in OfflineHeroParty.Read(catalog))
            selected.Add(hero.AvatarId);

        foreach (OfflineHeroRecord hero in visible)
        {
            OfflineHeroRecord captured = hero;
            GameObject card = Instantiate(source.m_GoPrefabCell, content, false);
            card.name = "HTDC Hero " + hero.Id;
            card.SetActive(true);
            generated.Add(card);

            CellHeroThumb cell = card.GetComponent<CellHeroThumb>();
            if (cell == null) continue;
            if (cell.m_TextName != null) cell.m_TextName.text = hero.Name;
            if (cell.m_TextLevel != null) cell.m_TextLevel.text = hero.Star + " sao";
            if (cell.m_ImageThumb != null)
            {
                Sprite portrait = OfflineHeroArt.LoadPortrait(hero.AvatarId);
                cell.m_ImageThumb.sprite = portrait;
                cell.m_ImageThumb.color = portrait == null ? Color.clear : Color.white;
                cell.m_ImageThumb.preserveAspect = true;
            }
            if (cell.m_ImgCheck != null)
                cell.m_ImgCheck.gameObject.SetActive(selected.Contains(hero.AvatarId));

            Button info = card.GetComponent<Button>();
            if (info != null) info.onClick.AddListener(() => ShowInfo(captured));
            CreateTeamButton(card.transform, cell.m_TextName, captured, selected.Contains(hero.AvatarId));
        }

        UpdateCaption();
    }

    private void ShowInfo(OfflineHeroRecord hero)
    {
        Canvas canvas = GetComponent<HeroListNewScene>() != null
            ? GetComponent<HeroListNewScene>().m_GoScrollView.GetComponentInParent<Canvas>()
            : null;
        OfflineHeroDetailModal.Show(hero, canvas);
    }

    private void CreateTeamButton(Transform card, Text fontSource,
        OfflineHeroRecord hero, bool selected)
    {
        GameObject go = new GameObject("OfflineTeamSelect",
            typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        go.transform.SetParent(card, false);
        RectTransform rect = go.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.20f, 0f);
        rect.anchorMax = new Vector2(0.80f, 0f);
        rect.pivot = new Vector2(0.5f, 0f);
        rect.anchoredPosition = new Vector2(0f, 2f);
        rect.sizeDelta = new Vector2(0f, 34f);

        Image back = go.GetComponent<Image>();
        back.color = selected ? new Color(0.25f, 0.48f, 0.20f, 0.95f)
            : new Color(0.56f, 0.33f, 0.11f, 0.95f);
        Button button = go.GetComponent<Button>();
        button.targetGraphic = back;
        button.onClick.AddListener(() =>
        {
            string message;
            if (OfflineHeroParty.Toggle(catalog, hero.Id, out message))
            {
                status = "";
                Render();
            }
            else
            {
                status = message;
                UpdateCaption();
            }
        });

        GameObject label = new GameObject("Text", typeof(RectTransform), typeof(CanvasRenderer),
            typeof(Text));
        label.transform.SetParent(go.transform, false);
        RectTransform labelRect = label.GetComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;
        Text text = label.GetComponent<Text>();
        text.font = fontSource != null ? fontSource.font : Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 20;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
        text.text = selected ? "Bỏ đội" : "Chọn";
        text.raycastTarget = false;
    }

    private void UpdateCaption()
    {
        if (source == null || source.m_TextHeroCapacity == null || catalog == null) return;
        source.m_TextHeroCapacity.text = string.IsNullOrEmpty(status)
            ? "Đội hình offline: " + OfflineHeroParty.Read(catalog).Count + "/6"
            : status;
    }
}
