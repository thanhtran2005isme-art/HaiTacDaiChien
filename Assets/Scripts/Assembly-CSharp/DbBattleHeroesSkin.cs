using System.Collections.Generic;

public class DbBattleHeroesSkin
{
	private static DbBattleHeroesSkin m_Instance;

	private List<HeroBattleInfo> m_LstHeroInfo;

	public static DbBattleHeroesSkin GetInstance()
	{
		return null;
	}

	private DbBattleHeroesSkin()
	{
	}

	public void SetListBattleHeroInfo(List<HeroBattleInfo> lst)
	{
	}

	public HeroBattleInfo GetHeroSkinInfo(int skinKey)
	{
		return null;
	}

	public int Count()
	{
		return 0;
	}

	private void InitDb()
	{
	}
}
