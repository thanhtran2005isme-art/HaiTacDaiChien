using System.Collections.Generic;
using SimpleJSON;

public class SkillMelee : BaseSkill
{
	public SkillMelee(string animationName, float delayCastEffect)
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

	public override void PerformSkill(BattleHero heroAction, List<BattleHero> listHeroBeImpacted, List<BattleHero> listAllHeroes, BattleChainListEffect chainListEffect, float delayBattleEffect)
	{
	}

	public override void PerformSkillWithOutBattleEffect(BattleHero heroAction, List<BattleHero> listHeroBeImpacted)
	{
	}
}
