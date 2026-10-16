using XLua;

public class PointModel
{
	public const int ATTACK = 0;

	public const int ARMOR = 1;

	public const int HP = 2;

	public const int SPEED = 3;

	public const int ANGER = 4;

	public const int SKILL_DAMAGE = 5;

	public const int PRECISION = 6;

	public const int BLOCK = 7;

	public const int CRIT = 8;

	public const int CRIT_DAMAGE = 9;

	public const int ARMOR_BREAK = 10;

	public const int CONTROL_IMMUNE = 11;

	public const int REDUCE_DAMAGE = 12;

	public const int HOLY_DAMAGE = 13;

	public const int BASE_ATTACK = 14;

	public const int BASE_ARMOR = 15;

	public const int BASE_HP = 16;

	public const int BASE_SPEED = 17;

	public const int BASE_ANGER = 18;

	public const int BASE_SKILL_DAMAGE = 19;

	public const int BASE_PRECISION = 20;

	public const int BASE_BLOCK = 21;

	public const int BASE_CRIT = 22;

	public const int BASE_CRIT_DAMAGE = 23;

	public const int BASE_ARMOR_BREAK = 24;

	public const int BASE_CONTROL_IMMUNE = 25;

	public const int BASE_REDUCE_DAMAGE = 26;

	public const int BASE_HOLY_DAMAGE = 27;

	public const int SPEC_ADD_DAMAGE_AGAINST_ASSASIN = 35;

	public const int SPEC_ADD_DAMAGE_AGAINST_MAGE = 36;

	public const int SPEC_ADD_DAMAGE_AGAINST_PRIEST = 37;

	public const int SPEC_ADD_DAMAGE_AGAINST_RANGER = 38;

	public const int SPEC_ADD_DAMAGE_AGAINST_WARRIOR = 39;

	public const int SPEC_IMMUNE_SILENCE = 40;

	public const int SPEC_IMMUNE_STONE = 41;

	public const int SPEC_IMMUNE_STUN = 42;

	public const int SPEC_IMMUNE_FREEZE = 43;

	public const int SPEC_ADD_DAMAGE_AGAINST_BURN_FOE = 44;

	public const int SPEC_ADD_DAMAGE_AGAINST_POISON_FOE = 45;

	public const int SPEC_ADD_DAMAGE_AGAINST_BLOOD_FOE = 46;

	public const int SPEC_ADD_DAMAGE_AGAINST_FREEZE_FOE = 47;

	public const int SPEC_ADD_DAMAGE_AGAINST_STONE_FOE = 48;

	public const int SPEC_ADD_DAMAGE_AGAINST_STUN_FOE = 49;

	public const int SPEC_MORE_HEAL = 52;

	public const int CLASS_MAGE = 1;

	public const int CLASS_WARRIOR = 2;

	public const int CLASS_PRIEST = 3;

	public const int CLASS_RANGER = 4;

	public const int CLASS_ASSASSIN = 5;

	public const int FACTION_SHADOW = 1;

	public const int FACTION_ABYSS = 2;

	public const int FACTION_FORTRESS = 3;

	public const int FACTION_FOREST = 4;

	public const int FACTION_DARK = 5;

	public const int FACTION_LIGHT = 6;

	public int clazz;

	public int faction;

	public int[] points;

	public void PrintPointData()
	{
	}

	public static HeroClass ConvertPointClassToHeroClass(int pointClass)
	{
		return (HeroClass)0;
	}

	public static int ConvertHeroClassToPointClass(HeroClass heroClass)
	{
		return 0;
	}

	public static int ConvertHeroFactionToPointFaction(HeroFaction heroFaction)
	{
		return 0;
	}

	public HeroFaction GetHeroFaction()
	{
		return (HeroFaction)0;
	}

	[Hotfix(HotfixFlag.Stateless)]
	public string getPointDesc(int factionEquip = -1, int classEquip = -1, bool eachPointInSeperatedLine = false)
	{
		return null;
	}

	private string GetSpecAddMoreDamageOnHeroClass(HeroClass heroClazz)
	{
		return null;
	}
}
