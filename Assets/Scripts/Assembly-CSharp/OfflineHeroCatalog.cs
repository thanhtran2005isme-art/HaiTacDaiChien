using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

// NEW_PROJECT_DESIGN: local, read-only catalog from the AssetRipper-exported JSON.
// It is NOT a player inventory, a game-server response, or recovered IL2CPP behavior.
public enum OfflineHeroFaction
{
    All = 0,
    Fortress = 1,
    Abyss = 2,
    Forest = 3,
    Shadow = 4,
    Dark = 5,
    Light = 6,
    Unknown = 7
}

public enum OfflineHeroReleaseFilter
{
    All = 0,
    Released = 1,
    Unreleased = 2
}

public sealed class OfflineHeroRecord
{
    public int Id { get; private set; }
    public int AvatarId { get; private set; }
    public string Name { get; private set; }
    public string SourceFaction { get; private set; }
    public OfflineHeroFaction Faction { get; private set; }
    public string ClassName { get; private set; }
    public int Star { get; private set; }
    public int Rank { get; private set; }
    public int ReleaseFlag { get; private set; }
    public bool IsReleased { get { return ReleaseFlag == 1; } }

    internal OfflineHeroRecord(int id, int avatarId, string name, string sourceFaction,
        OfflineHeroFaction faction, string className, int star, int rank, int releaseFlag)
    {
        Id = id;
        AvatarId = avatarId;
        Name = name;
        SourceFaction = sourceFaction;
        Faction = faction;
        ClassName = className;
        Star = star;
        Rank = rank;
        ReleaseFlag = releaseFlag;
    }
}

public sealed class OfflineHeroCatalog
{
    public const string ResourcePath = "_pack/db/db_heroes";

    [Serializable]
    private sealed class HeroDbEnvelope
    {
        public HeroDbRow[] rows;
    }

    [Serializable]
    private sealed class HeroDbRow
    {
        public string id;
        public string avatar;
        public string name;
        public string type;
        public string @class;
        public string star;
        public string rank;
        public int release;
    }

    private readonly List<OfflineHeroRecord> entries;
    private readonly Dictionary<int, OfflineHeroRecord> byId;
    private readonly IReadOnlyList<OfflineHeroRecord> readOnlyEntries;

    public IReadOnlyList<OfflineHeroRecord> Entries { get { return readOnlyEntries; } }
    public int Count { get { return entries.Count; } }

    private OfflineHeroCatalog(List<OfflineHeroRecord> rows,
        Dictionary<int, OfflineHeroRecord> index)
    {
        entries = rows;
        byId = index;
        readOnlyEntries = rows.AsReadOnly();
    }

    public static OfflineHeroCatalog LoadFromResources()
    {
        TextAsset asset = Resources.Load<TextAsset>(ResourcePath);
        if (asset == null)
        {
            throw new InvalidOperationException(
                "Missing Assets/Resources/_pack/db/db_heroes.json (Resources path: " +
                ResourcePath + ").");
        }

        return FromJson(asset.text);
    }

    // Public so edit-mode code can validate data without changing scenes or gameplay.
    public static OfflineHeroCatalog FromJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json) || json.TrimStart()[0] != '[')
        {
            throw new FormatException("db_heroes.json must be a nonempty JSON array.");
        }

        HeroDbEnvelope envelope;
        try
        {
            // Unity JsonUtility does not support a top-level JSON array.
            envelope = JsonUtility.FromJson<HeroDbEnvelope>("{\"rows\":" + json + "}");
        }
        catch (Exception ex)
        {
            throw new FormatException("Unable to parse db_heroes.json.", ex);
        }

        if (envelope == null || envelope.rows == null)
        {
            throw new FormatException("db_heroes.json has no hero rows.");
        }

        List<OfflineHeroRecord> result = new List<OfflineHeroRecord>(envelope.rows.Length);
        Dictionary<int, OfflineHeroRecord> index =
            new Dictionary<int, OfflineHeroRecord>(envelope.rows.Length);

        foreach (HeroDbRow row in envelope.rows)
        {
            if (row == null)
                throw new FormatException("Null hero row in db_heroes.json.");

            int id = ParseNonNegativeInt(row.id, "id");
            if (id == 0)
                throw new FormatException("Hero id must be positive.");
            if (index.ContainsKey(id))
                throw new FormatException("Duplicate hero id: " + id);
            if (string.IsNullOrWhiteSpace(row.name))
                throw new FormatException("Hero " + id + " has an empty name.");

            int avatarId = ParseNonNegativeInt(row.avatar, "avatar");
            int star = ParseNonNegativeInt(row.star, "star");
            int rank = ParseNonNegativeInt(row.rank, "rank");
            if (row.release != 0 && row.release != 1)
                throw new FormatException("Hero " + id + " has an invalid release flag.");

            OfflineHeroFaction faction = ParseFaction(row.type);
            if (faction == OfflineHeroFaction.Unknown)
                throw new FormatException("Hero " + id + " has an unknown faction: " + row.type);

            OfflineHeroRecord hero = new OfflineHeroRecord(
                id, avatarId, row.name, row.type, faction, row.@class,
                star, rank, row.release);
            result.Add(hero);
            index.Add(id, hero);
        }

        result.Sort((left, right) => left.Id.CompareTo(right.Id));
        return new OfflineHeroCatalog(result, index);
    }

    public bool TryGetById(int id, out OfflineHeroRecord hero)
    {
        return byId.TryGetValue(id, out hero);
    }

    // faction=All, star=-1 and release=All mean no filter.
    // A record with release=1 is NOT proof that the player owns that hero.
    public List<OfflineHeroRecord> Query(OfflineHeroFaction faction = OfflineHeroFaction.All,
        int star = -1, OfflineHeroReleaseFilter release = OfflineHeroReleaseFilter.All)
    {
        if (star < -1)
            throw new ArgumentOutOfRangeException("star");
        if (!Enum.IsDefined(typeof(OfflineHeroFaction), faction))
            throw new ArgumentOutOfRangeException("faction");
        if (!Enum.IsDefined(typeof(OfflineHeroReleaseFilter), release))
            throw new ArgumentOutOfRangeException("release");

        List<OfflineHeroRecord> matches = new List<OfflineHeroRecord>();
        foreach (OfflineHeroRecord hero in entries)
        {
            if (faction != OfflineHeroFaction.All && hero.Faction != faction)
                continue;
            if (star != -1 && hero.Star != star)
                continue;
            if (release == OfflineHeroReleaseFilter.Released && !hero.IsReleased)
                continue;
            if (release == OfflineHeroReleaseFilter.Unreleased && hero.IsReleased)
                continue;
            matches.Add(hero);
        }
        return matches;
    }

    private static int ParseNonNegativeInt(string value, string field)
    {
        int parsed;
        if (!int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out parsed))
            throw new FormatException("Hero field " + field + " is not an unsigned integer: " + value);
        return parsed;
    }

    private static OfflineHeroFaction ParseFaction(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return OfflineHeroFaction.Unknown;

        switch (value.Trim().ToLowerInvariant())
        {
            case "fortress": return OfflineHeroFaction.Fortress;
            case "abyss": return OfflineHeroFaction.Abyss;
            case "forest": return OfflineHeroFaction.Forest;
            case "shadow": return OfflineHeroFaction.Shadow;
            case "dark": return OfflineHeroFaction.Dark;
            case "light": return OfflineHeroFaction.Light;
            default: return OfflineHeroFaction.Unknown;
        }
    }
}

#if UNITY_EDITOR
// Diagnostic only: does not mutate the scene, the JSON file, or saved player data.
public static class OfflineHeroCatalogEditorAudit
{
    [UnityEditor.MenuItem("Tools/HTDC/Offline Heroes/Validate db_heroes.json")]
    public static void Validate()
    {
        try
        {
            OfflineHeroCatalog catalog = OfflineHeroCatalog.LoadFromResources();
            StringBuilder summary = new StringBuilder();
            summary.AppendLine("[HTDC Offline Heroes] Records: " + catalog.Count);
            summary.AppendLine("release=1: " +
                catalog.Query(release: OfflineHeroReleaseFilter.Released).Count +
                "; release=0: " +
                catalog.Query(release: OfflineHeroReleaseFilter.Unreleased).Count);

            foreach (OfflineHeroFaction faction in new[] {
                OfflineHeroFaction.Fortress, OfflineHeroFaction.Abyss,
                OfflineHeroFaction.Forest, OfflineHeroFaction.Shadow,
                OfflineHeroFaction.Dark, OfflineHeroFaction.Light
            })
            {
                summary.AppendLine(faction + ": " + catalog.Query(faction: faction).Count);
            }

            foreach (int star in new[] { 0, 1, 2, 3, 4, 5, 6, 9, 10 })
                summary.AppendLine(star + " stars: " + catalog.Query(star: star).Count);

            Debug.Log(summary.ToString());
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
#endif
