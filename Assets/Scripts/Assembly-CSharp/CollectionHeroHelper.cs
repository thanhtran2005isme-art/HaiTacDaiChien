using System.Collections.Generic;

public class CollectionHeroHelper
{
	public static void SortListHeroesByLevel(List<HeroModel> lstHeroModel)
	{
	}

	public static void SortListHeroes(List<HeroModel> lstHeroModel)
	{
	}

	public static void ResetHeroEpicEquiByHeroId(List<HeroModel> lstHeroModel, int heroId)
	{
	}

	public static HeroModel GetHeroModelById(List<HeroModel> lstHeroModel, int heroId, bool cloneCopy = false)
	{
		return null;
	}

	public static HeroModel GetHeroModelByKey(List<HeroModel> lstHeroModel, int heroKey, bool cloneCopy = false)
	{
		return null;
	}

	public static List<HeroModel> RemoveByFaction(List<HeroModel> lstHeroModel, HeroFaction factionRemove)
	{
		return null;
	}

	public static List<HeroModel> FilterByFaction(List<HeroModel> lstHeroModel, HeroFaction faction, bool cloneCopy = false)
	{
		return null;
	}

	public static List<HeroModel> FilterByQuality(List<HeroModel> lstHeroModel, int rank, bool cloneCopy = false)
	{
		return null;
	}

	public static List<HeroModel> FilterByClass(List<HeroModel> lstHeroModel, HeroClass heroClass, bool cloneCopy = false)
	{
		return null;
	}

	public static List<HeroModel> FilterByLevel(List<HeroModel> lstHeroModel, int minLevel)
	{
		return null;
	}

	public static List<HeroModel> FilterByStar(List<HeroModel> lstHeroModel, int heroStar)
	{
		return null;
	}

	public static List<HeroModel> FilterFromStarToStar(List<HeroModel> lstHeroModel, int fromStar, int toStar)
	{
		return null;
	}

	public static List<HeroModel> RemoveHero10Star(List<HeroModel> lstHeroModel)
	{
		return null;
	}

	public static List<HeroModel> RemoveUnReleasedHero(List<HeroModel> lstHeroModel)
	{
		return null;
	}

	public static List<HeroModel> FilterByKey(List<HeroModel> lstHeroModel, int heroKey)
	{
		return null;
	}

	public static HeroFactionFusableStatus GetStatusFusable(List<HeroModel> lstHeroModel, bool returnIfFoundAny = false)
	{
		return null;
	}

	private static bool CheckCanFuseAnyHeroInList(List<HeroModel> lstHeroModel)
	{
		return false;
	}

	public static HeroFaction ConvertIntToHeroFaction(int value)
	{
		return (HeroFaction)0;
	}
}
