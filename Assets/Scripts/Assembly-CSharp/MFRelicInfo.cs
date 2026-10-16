public class MFRelicInfo
{
	public const int AFFECT_ALL = 0;

	public const int AFFECT_TO_HERO = 1;

	public const int AFFECT_FRONT_LINE = 2;

	public const int AFFECT_BACK_LINE = 3;

	public const int AFFECT_TO_CLASS = 5;

	public const int AFFECT_TO_FACTION = 4;

	public const int RELIC_RARE = 1;

	public const int RELIC_ELITE = 2;

	public const int RELIC_LEGENDARY = 3;

	public HeroFaction m_AffectFaction;

	public HeroClass m_AffectClass;

	public int m_AffectHeroKey;

	public int m_RelicId;

	public int m_RelicImageId;

	public int m_AffectType;

	public int m_RelicQuality;

	public string m_StrName;

	public string m_StrDesc;

	public void SetAffectType(int affectType, string affectData)
	{
	}

	private HeroClass ConvertIntToHeroClass(int value)
	{
		return (HeroClass)0;
	}

	private HeroFaction ConvertIntToHeroFaction(int value)
	{
		return (HeroFaction)0;
	}

	private void RandomRelicInfo()
	{
	}
}
