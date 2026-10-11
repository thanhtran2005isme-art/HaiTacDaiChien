using System.Collections.Generic;

public class FusingHeroSlotData
{
	public int m_HeroKey;

	public int m_HeroStar;

	public HeroFaction m_HeroFaction;

	public int m_NumberRequire;

	public FusingHeroSlotData(int heroKey, int heroStar, int numberRequire)
	{
	}

	public FusingHeroSlotData(int heroStar, HeroFaction heroFaction, int numberRequire)
	{
	}

	public List<int> FindHeroIdMeetRequirementInList(List<HeroModel> lstHeroModel, List<int> lstHeroIdExclude)
	{
		return null;
	}
}
