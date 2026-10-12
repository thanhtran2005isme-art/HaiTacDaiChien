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
        UnityEngine.Debug.Log("[HTDC Heroes] HeroList presenter Start; loading offline source catalog.", this);
        source = GetComponent<HeroListNewScene>();
        if (source == null || source.m_GoPrefabCell == null || source.m_GoScrollView == null)
        {
            UnityEngine.Debug.LogError("[HTDC Heroes] Original hero scene template/scroll references missing.", this);
            return;
        }

        try { catalog = OfflineHeroCatalog.LoadFromResources(); }
        catch (Exception ex) { UnityEngine.Debug.LogException(ex, this); return; }

        // AssetRipper serialized this Scene Canvas localScale as (0,0,0).
        // Fix only the runtime instance so the original scene file stays unchanged.
        Canvas ownerCanvas = source.m_GoScrollView.GetComponentInParent<Canvas>(true);
        if (ownerCanvas != null && ownerCanvas.transform.localScale == Vector3.zero)
            ownerCanvas.transform.localScale = Vector3.one;

        ScrollRect scroll = source.m_GoScrollView.GetComponent<ScrollRect>();
        if (scroll == null || scroll.content == null)
        {
            UnityEngine.Debug.LogError("[HTDC Heroes] Original ScrollRect content unavailable.", this);
            return;
        }
        content = scroll.content;

        // HeroListNewScene now stores eight ORIGINAL-catalog, EditorOnly
        // preview cards so the design is visible with Play OFF. They must
        // never appear in live party/catalog rendering or inflate counts.
        // Disable immediately, then destroy the runtime instances.
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Transform child = content.GetChild(i);
            if (child.name.StartsWith("HTDC EDIT PREVIEW HERO ", StringComparison.Ordinal)
                && child.gameObject.CompareTag("EditorOnly"))
            {
                child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
        }

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
        // Labels stay Vietnamese after source I2 localization initializes.
        SetVietnameseControls();
        if (source.m_TextTitle != null) source.m_TextTitle.text = "Tướng";
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
            if (cell.m_TextLevel != null) cell.m_TextLevel.text = "★" + hero.Star;
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
        }

        UpdateCaption();
    }

    private void ShowInfo(OfflineHeroRecord hero)
    {
        Canvas canvas = GetComponent<HeroListNewScene>() != null
            ? GetComponent<HeroListNewScene>().m_GoScrollView.GetComponentInParent<Canvas>()
            : null;
        // Selection is a secondary action inside the original detail popup,
        // never a large button obscuring the source hero-card artwork.
        OfflineHeroDetailModal.Show(hero, canvas, catalog, () =>
        {
            status = "";
            Render();
        });
    }

    private void UpdateCaption()
    {
        if (source == null || source.m_TextHeroCapacity == null || catalog == null) return;
        // The original narrow capacity counter cannot fit a sentence:
        // keep it numeric, without claiming source account inventory.
        source.m_TextHeroCapacity.text =
            OfflineHeroParty.Read(catalog).Count + "/" + OfflineHeroParty.Capacity;
        if (!string.IsNullOrEmpty(status))
            UnityEngine.Debug.LogWarning("[HTDC Heroes] " + status, this);
    }
    private void SetVietnameseControls()
    {
        if (source == null || source.m_TabFaction == null) return;
        Dictionary<string, string> labels = new Dictionary<string, string>
        {
            { "ButtonFortress", "Nhanh nhẹn" },
            { "ButtonAbyss", "Bền bỉ" },
            { "ButtonForest", "Chính xác" },
            { "ButtonShadow", "Sinh khí" },
            { "ButtonDark", "Thông minh" },
            { "ButtonLight", "Minh mẫn" }
        };

        foreach (Button button in source.m_TabFaction.GetComponentsInChildren<Button>(true))
        {
            string translation;
            if (!labels.TryGetValue(button.name, out translation)) continue;
            Text text = button.GetComponentInChildren<Text>(true);
            if (text != null) text.text = translation;
        }

        // Source ButtonSort is a child of the same original root Canvas.
        Canvas canvas = source.m_GoScrollView.GetComponentInParent<Canvas>(true);
        Transform sort = canvas != null ? FindChildRecursive(canvas.transform, "ButtonSort") : null;
        if (sort != null)
        {
            Text sortText = sort.GetComponentInChildren<Text>(true);
            if (sortText != null) sortText.text = "Sắp xếp";
        }
    }

    private static Transform FindChildRecursive(Transform root, string name)
    {
        if (root == null) return null;
        if (root.name == name) return root;
        foreach (Transform child in root)
        {
            Transform found = FindChildRecursive(child, name);
            if (found != null) return found;
        }
        return null;
    }

}
