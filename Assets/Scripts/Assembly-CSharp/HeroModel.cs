using System.Collections.Generic;
using pbdson;

public class HeroModel
{
	public EpicEquipment epicEquipment;

	public int m_HeroKey;

	public int m_HeroId;

	public int m_SpineKey;

	public string m_HeroName;

	public string m_HeroDesc;

	public HeroClass m_Class;

	public HeroFaction m_Faction;

	public int m_WeaponKey;

	public int m_ArmorKey;

	public int m_ShoesKey;

	public int m_AccessoryKey;

	public int m_Release;

	public int m_ArtifactKey;

	public int m_ArtifactStar;

	public int m_StoneId;

	public int m_Quality;

	public int m_Star;

	public int m_Level;

	public int m_MaxLevel;

	private int m_Tier;

	public int m_MaxTier;

	public long mp_Attack;

	public long mp_Hp;

	public long mp_Armor;

	public long mp_Speed;

	public long mp_PowerFromServer;

	public float mp_SkillDamage;

	public float mp_Precision;

	public float mp_Block;

	public float mp_Crit;

	public float mp_CritDamage;

	public float mp_ArmorBreak;

	public float mp_ControlImmune;

	public float mp_ReduceDamage;

	public float mp_HolyDamage;

	public int m_ExtraTag;

	public float mp_AttackScale;

	public float mp_HpScale;

	public float mp_ArmorScale;

	public float mp_SpeedScale;

	public long mp_HpMax;

	public long mp_Rage;

	public long mp_RageMax;

	public bool m_LockedInList;

	public bool m_LockedCrystalCrown;

	public bool m_LockedTrialChampion;

	public bool m_LockedAllStarsLeague;

	public List<int> m_ListSkillId;

	public List<float> lstMaxPoint;

	public int m_Number;

	private List<int> m_ListIndexESkillActive;

	public List<int> m_ListSkinKey;

	public int m_EquipedSkinKey;

	public const int HERO_KEY_PUPPET_SHADOW_9_STAR = 310;

	public const int HERO_KEY_PUPPET_FORTRESS_9_STAR = 311;

	public const int HERO_KEY_PUPPET_ABYSS_9_STAR = 312;

	public const int HERO_KEY_PUPPET_FOREST_9_STAR = 313;

	public const int HERO_KEY_PUPPET_DARK_9_STAR = 314;

	public const int HERO_KEY_PUPPET_LIGHT_9_STAR = 315;

	public const int HERO_KEY_PUPPET_SHADOW_10_STAR = 322;

	public const int HERO_KEY_PUPPET_FORTRESS_10_STAR = 323;

	public const int HERO_KEY_PUPPET_ABYSS_10_STAR = 324;

	public const int HERO_KEY_PUPPET_FOREST_10_STAR = 325;

	public const int HERO_KEY_PUPPET_DARK_10_STAR = 326;

	public const int HERO_KEY_PUPPET_LIGHT_10_STAR = 327;

	public List<int> GetListIndexESkill()
	{
		return null;
	}

	public static HeroModel GenNullHeroModel()
	{
		return null;
	}

	public static bool IsPuppetHero(int heroKey)
	{
		return false;
	}

	public void CopyHeroEquipmentAndStone(HeroModel heroModelInput)
	{
	}

	public bool IsModelNull()
	{
		return false;
	}

	public bool IsDeath()
	{
		return false;
	}

	public bool IsHeroLock()
	{
		return false;
	}

	public void UnlockAll()
	{
	}

	public string GetLockDescription()
	{
		return null;
	}

	public void SetHpPercent(int percent)
	{
	}

	public float GetHpRate()
	{
		return 0f;
	}

	public long GetPower()
	{
		return 0L;
	}

	public void CalculateMaxLevelByStarAndTier()
	{
	}

	public void SetStar(int star)
	{
	}

	public void EquipArtifact(int artKey, int artStar)
	{
	}

	public List<object> ReturnEquipmentAndArtifact()
	{
		return null;
	}

	public List<int> GetListEquipmentKey()
	{
		return null;
	}

	public ItemArtifact GetArtifact()
	{
		return null;
	}

	public void SetTier(int tier)
	{
	}

	public static int GetMaxLevelByTier(int tier)
	{
		return 0;
	}

	public static int GetMaxLevelByStar(int star)
	{
		return 0;
	}

	public int GetTier()
	{
		return 0;
	}

	public void SetClass(string className)
	{
	}

	public void SetFaction(string factionName)
	{
	}

	public virtual HeroModel Clone()
	{
		return null;
	}

	public void CloneData()
	{
	}

	public void CalculateMaxHero(List<float> lstMaxPoint)
	{
	}

	public void ImportFromPbHero(PbHero pbHero)
	{
	}

	public static HeroClass ConvertServerHeroClassToClient(int classId)
	{
		return (HeroClass)0;
	}

	public static HeroFaction ConvertServerHeroFactionToClient(int factionId)
	{
		return (HeroFaction)0;
	}
}
