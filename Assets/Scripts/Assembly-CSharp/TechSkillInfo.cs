public class TechSkillInfo
{
	public int m_Level;

	public int m_MaxLevel;

	public int m_GoldCost;

	public int m_GuildCoinCost;

	public string m_TitleTech;

	public string m_UpgradeDesc;

	public string m_UpgradeRequire;

	public int m_TechIndex;

	public int techLevel;

	public TechSkillInfo(int techIndex, int level, int maxLevel, int goldCost, int guidCoinCost, string titleTech, string upgradeDesc, string upgradeRequirement, int techLevel)
	{
	}

	public void UpdateInfo(int level, int maxLevel, int goldCost, int guidCoinCost, string titleTech, string upgradeDesc, string upgradeRequirement)
	{
	}

	public bool IsEnable()
	{
		return false;
	}
}
