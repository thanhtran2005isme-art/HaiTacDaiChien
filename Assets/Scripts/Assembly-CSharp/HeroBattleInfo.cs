using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using pbdson;

[Serializable]
public class HeroBattleInfo
{
	public int m_HeroKey;

	[HideInInspector]
	public bool m_IsSkin;

	public int m_SkinKey;

	[HideInInspector]
	public string m_HeroName;

	public List<BaseSkill> m_ListHeroSkill;

	[HideInInspector]
	public HeroGender m_Gender;

	[HideInInspector]
	public int m_BattleSlotIndex;

	[HideInInspector]
	public int m_TeamId;

	public Vector2 m_SizeBody;

	public float m_AttackRange;

	public float m_AttackRangeSkill;

	public float m_Scale;

	[HideInInspector]
	public GameDirection m_SpineFacingDirection;

	[HideInInspector]
	public float m_HpMax;

	[HideInInspector]
	public float m_Hp;

	[HideInInspector]
	public float m_RageMax;

	[HideInInspector]
	public float m_Rage;

	[HideInInspector]
	public long mp_Attack;

	[HideInInspector]
	public long mp_Armor;

	[HideInInspector]
	public long mp_Speed;

	[HideInInspector]
	public float mp_SkillDamage;

	[HideInInspector]
	public float mp_Precision;

	[HideInInspector]
	public float mp_Block;

	[HideInInspector]
	public float mp_Crit;

	[HideInInspector]
	public float mp_CritDamage;

	[HideInInspector]
	public float mp_ArmorBreak;

	[HideInInspector]
	public float mp_ControlImmune;

	[HideInInspector]
	public float mp_ReduceDamage;

	[HideInInspector]
	public float mp_HolyDamage;

	[HideInInspector]
	public int m_HeroLevel;

	[HideInInspector]
	public float m_AttackFk;

	[HideInInspector]
	public float m_HpFk;

	[HideInInspector]
	public bool m_IsBoss;

	public HeroBattleInfo(JSONClass jsonObject)
	{
	}

	public string GetKeyForSpineModel()
	{
		return null;
	}

	public HeroBattleInfo(int heroKey, string heroName)
	{
	}

	public HeroBattleInfo(int heroKey)
	{
	}

	public HeroBattleInfo(int heroKey, int battleSlotIndex)
	{
	}

	public JSONClass ToJSONObject()
	{
		return null;
	}

	public void ImportFromJSONObject(JSONClass jsonObj)
	{
	}

	public HeroBattleInfo ShallowClone()
	{
		return null;
	}

	public void ImportFromPbBattleHero(PbBattleHero pbHero)
	{
	}

	public bool IsBoss()
	{
		return false;
	}

	public void AdjustHeroSkillPath()
	{
	}

	public List<EffectInfo> GetHeroSpineSkillEffectInfo()
	{
		return null;
	}

	public List<SkillObjectInfo> GetHeroSpineSkillObjectInfo()
	{
		return null;
	}

	public void DeepClone()
	{
	}
}
