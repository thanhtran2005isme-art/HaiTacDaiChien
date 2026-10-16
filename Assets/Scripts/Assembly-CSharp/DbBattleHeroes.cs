using System.Collections.Generic;

public class DbBattleHeroes
{
	private static DbBattleHeroes m_Instance;

	private List<HeroBattleInfo> m_LstHeroInfo;

	public static DbBattleHeroes GetInstance()
	{
		return null;
	}

	private DbBattleHeroes()
	{
	}

	public void SetListBattleHeroInfo(List<HeroBattleInfo> lst)
	{
	}

	public void UpdateHeroName(List<HeroModel> lstHm)
	{
	}

	public int Count()
	{
		return 0;
	}

	public List<HeroBattleInfo> GetAllHero()
	{
		return null;
	}

	public static int GetHeroPack(int heroKey)
	{
		return 0;
	}

	public bool IsHeroHasSpine(int heroKey)
	{
		return false;
	}

	public void addHero(HeroBattleInfo mHero)
	{
	}

	public int LookUpHeroSpineReplacement(int heroKeyInput)
	{
		return 0;
	}

	public HeroBattleInfo GetHeroInfo(int heroKey, bool warningIfNoSpine = true)
	{
		return null;
	}

	public HeroBattleInfo GetHeroInfoSameFaction(HeroFaction faction)
	{
		return null;
	}

	public HeroBattleInfo GetRandomHeroInfo()
	{
		return null;
	}

	private void InitDb()
	{
	}
}
