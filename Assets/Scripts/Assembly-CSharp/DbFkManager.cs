using System.Collections.Generic;

public class DbFkManager
{
	private static DbFkManager _instance;

	public List<HeroBossStage> m_ListBossStage;

	public List<StageCampaignBotInfo> m_ListBotCampaignInfo;

	public List<HeroFkUpgradeCost> m_ListHeroUpgradeCost;

	public List<DummyHeroModel> m_ListDummyHero;

	public static DbFkManager Instance => null;

	private DbFkManager()
	{
	}

	private void InitBossStage()
	{
	}

	public void InitBotCampaignInfo()
	{
	}

	public DummyHeroModel GetHero(int heroId)
	{
		return null;
	}

	public StageCampaignBotInfo GetBotCampaignInfo(int stageIdx)
	{
		return null;
	}

	public void InitCostUpgrade()
	{
	}

	public void InitHeroes()
	{
	}
}
