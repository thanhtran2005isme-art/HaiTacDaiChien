using System;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

// NEW_PROJECT_DESIGN: explicitly user-selected OFFLINE party, never server inventory.
public static class OfflineHeroParty
{
    public const int Capacity = 6;
    private const string PrefsKey = "htdc.offline.party.v1.ids";

    public static List<OfflineHeroRecord> Read(OfflineHeroCatalog catalog)
    {
        List<OfflineHeroRecord> result = new List<OfflineHeroRecord>();
        if (catalog == null) return result;
        string stored = PlayerPrefs.GetString(PrefsKey, "");
        if (string.IsNullOrEmpty(stored)) return result;

        HashSet<int> avatars = new HashSet<int>();
        foreach (string part in stored.Split(','))
        {
            int id;
            OfflineHeroRecord row;
            if (!int.TryParse(part, out id) ||
                !catalog.TryGetById(id, out row) ||
                !IsEligible(row) || !avatars.Add(row.AvatarId))
                continue;
            result.Add(row);
            if (result.Count == Capacity) break;
        }
        return result;
    }

    public static bool IsEligible(OfflineHeroRecord hero)
    {
        // Keep NPC/boss/test IDs out of the offline selectable hero dictionary.
        return hero != null && hero.Id >= 1 && hero.Id <= 530 &&
            hero.Star >= 5 && hero.IsReleased && hero.AvatarId > 0;
    }

    public static bool Toggle(OfflineHeroCatalog catalog, int id, out string notice)
    {
        notice = "";
        OfflineHeroRecord hero;
        if (catalog == null || !catalog.TryGetById(id, out hero) || !IsEligible(hero))
        {
            notice = "Không thể chọn ID không nằm trong danh sách tướng phát hành.";
            return false;
        }

        List<OfflineHeroRecord> party = Read(catalog);
        int selectedIndex = party.FindIndex(item => item.AvatarId == hero.AvatarId);
        if (selectedIndex >= 0)
        {
            if (party[selectedIndex].Id == id)
                party.RemoveAt(selectedIndex);
            else
                party[selectedIndex] = hero; // Switch star variant, not duplicate avatar.
        }
        else
        {
            if (party.Count >= Capacity)
            {
                notice = "Đội hình offline chỉ có 6 vị trí. Bỏ một tướng trước khi chọn.";
                return false;
            }
            party.Add(hero);
        }

        List<string> ids = new List<string>();
        foreach (OfflineHeroRecord item in party) ids.Add(item.Id.ToString());
        PlayerPrefs.SetString(PrefsKey, string.Join(",", ids.ToArray()));
        PlayerPrefs.Save();
        notice = "Đội hình offline: " + party.Count + "/6";
        return true;
    }
}

// Asset identity is proven by Resources path + avatar ID. Never borrow art from another hero.
public static class OfflineHeroArt
{
    private static readonly Dictionary<int, Sprite> headCache = new Dictionary<int, Sprite>();
    private static readonly HashSet<int> missingHeads = new HashSet<int>();
    private static readonly Dictionary<int, SkeletonDataAsset> skeletonCache =
        new Dictionary<int, SkeletonDataAsset>();
    private static readonly HashSet<int> missingSkeletons = new HashSet<int>();

    public static Sprite LoadPortrait(int avatarId)
    {
        if (avatarId <= 0 || missingHeads.Contains(avatarId)) return null;
        Sprite sprite;
        if (headCache.TryGetValue(avatarId, out sprite)) return sprite;
        string path = "_pack/herohead/" + avatarId;
        sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>(path);
            if (sprites != null && sprites.Length > 0) sprite = sprites[0];
        }
        if (sprite == null) missingHeads.Add(avatarId);
        else headCache.Add(avatarId, sprite);
        return sprite;
    }

    public static SkeletonDataAsset LoadSkeleton(int avatarId)
    {
        if (avatarId <= 0 || missingSkeletons.Contains(avatarId)) return null;
        SkeletonDataAsset asset;
        if (skeletonCache.TryGetValue(avatarId, out asset)) return asset;

        // Verified repository directories; existence remains checked by Resources at runtime.
        for (int folder = 2; folder <= 5; folder++)
        {
            string path = "_pack/heroes" + folder + "/" + avatarId +
                "/Hero_" + avatarId + "_SkeletonData";
            asset = Resources.Load<SkeletonDataAsset>(path);
            if (asset != null)
            {
                skeletonCache.Add(avatarId, asset);
                return asset;
            }
        }
        missingSkeletons.Add(avatarId);
        return null;
    }
}
