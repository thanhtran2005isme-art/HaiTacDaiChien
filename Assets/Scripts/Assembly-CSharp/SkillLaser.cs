using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

public class SkillLaser : BaseSkill
{
	public SkillObjectInfo m_SkillObjInfo;

	public SkillObjectInfo m_SkillObjInfoHit;

	public SkillLaser(string animationName, float delayCastEffect, SkillObjectInfo skillObjInfo, SkillObjectInfo skillObjInfoHit)
		: base(null, 0f)
	{
	}

	public override JSONClass ToJSONObject()
	{
		return null;
	}

	public override void ImportFromJSONObject(JSONClass jsonObject)
	{
	}

	public override void AdjustSkillPath(bool debug = false)
	{
	}

	public override List<SkillObjectInfo> GetListSpineSkillObjectInfo()
	{
		return null;
	}

	public static Quaternion getRotation(Transform trans, Vector2 pointFrom, Vector2 pointTo)
	{
		return default;
	}

	public override void PerformSkill(BattleHero heroAction, List<BattleHero> listHeroBeImpacted, List<BattleHero> listAllHeroes, BattleChainListEffect chainListEffect, float delayBattleEffect)
	{
	}

	public override void PerformSkillWithOutBattleEffect(BattleHero heroAction, List<BattleHero> listHeroBeImpacted)
	{
	}
}
