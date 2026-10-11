using System.Collections.Generic;

public class DummyDbHeroSkill
{
	private static DummyDbHeroSkill _instance;

	private List<DummyHeroSkillData> m_ListHeroSkillData;

	public static DummyDbHeroSkill Instance => null;

	private DummyDbHeroSkill()
	{
	}

	private void InitDb()
	{
	}

	public DummyHeroSkillData GetHeroSkillData(int heroSpineId)
	{
		return null;
	}
}
