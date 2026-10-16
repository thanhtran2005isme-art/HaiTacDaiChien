public class ESkillModel : SkillModel
{
	public int m_HeroStarToUnlock;

	public int m_SkillOrder;

	public ESkillModel(int skillId, string skillName, string skillDesc)
		: base(0, null, null)
	{
	}

	public ESkillModel()
		: base(0, null, null)
	{
	}
}
