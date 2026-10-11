public class QuestRequirement
{
	public const int REQUIREMENT_STAR = 1;

	public const int REQUIREMENT_FACTION = 2;

	public const int REQUIREMENT_CLASS = 3;

	public const int REQUIREMENT_NONE = 4;

	public int m_Star;

	public HeroClass m_HeroClass;

	public HeroFaction m_Faction;

	public int m_HeroKey;

	public int m_HeroId;

	public int m_RequirementType;

	public QuestRequirement()
	{
	}

	public QuestRequirement(int star)
	{
	}

	public QuestRequirement(HeroClass heroClass)
	{
	}

	public QuestRequirement(HeroFaction heroFaction)
	{
	}

	public bool IsHeroModelSatisfied(HeroModel hm)
	{
		return false;
	}
}
