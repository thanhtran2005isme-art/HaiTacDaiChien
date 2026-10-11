public class SkillInfo
{
	public bool m_Active;

	public string m_SkillName;

	public string m_SkillDesc;

	public int m_SkillIconId;

	public int m_SkillLevel;

	public int m_SkillIndex;

	public int m_TierUnlockSkill;

	public SkillInfo(int skillIndex, int skillIconId)
	{
	}

	public bool IsSkillUnlockForNewTier(int heroStar, int newTier)
	{
		return false;
	}

	public int GetTierUnlockSkill(int heroStar)
	{
		return 0;
	}

	public void CheckIfSkillActive(int heroStar, int currentTier)
	{
	}

	public void FindSkillLevel(int heroStar)
	{
	}
}
