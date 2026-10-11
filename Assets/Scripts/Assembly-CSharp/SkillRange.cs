using System.Collections.Generic;
using SimpleJSON;

public class SkillRange : BaseSkill
{
	public SkillObjectInfo m_SkillObjInfo;

	public SkillRange(string animationName, float delayCastEffect, SkillObjectInfo skillObjInfo)
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

	public override List<SkillObjectInfo> GetListSpineSkillObjectInfo()
	{
		return null;
	}

	public override void AdjustSkillPath(bool debug = false)
	{
	}

	public override void PerformSkill(BattleHero heroAction, List<BattleHero> listHeroBeImpacted, List<BattleHero> listAllHeroes, BattleChainListEffect chainListEffect, float delayBattleEffect)
	{
	}

	public override void PerformSkillWithOutBattleEffect(BattleHero heroAction, List<BattleHero> listHeroBeImpacted)
	{
	}
}
