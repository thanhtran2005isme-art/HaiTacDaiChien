using System.Collections.Generic;
using SimpleJSON;

public class SkillAoe : BaseSkill
{
	public SkillAoe(string animationName, float delayCastEffect, bool needMoveUp = false)
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

	public override void PerformPetSkill(BattlePet petAction, List<BattleHero> listHeroBeImpacted, List<BattleHero> listAllHeroes, BattleChainListEffect chainListEffect, float delayBattleEffect)
	{
	}

	public override void PerformSkill(BattleHero heroAction, List<BattleHero> listHeroBeImpacted, List<BattleHero> listAllHeroes, BattleChainListEffect chainListEffect, float delayBattleEffect)
	{
	}

	public override void PerformSkillWithOutBattleEffect(BattleHero heroAction, List<BattleHero> listHeroBeImpacted)
	{
	}

	private float PerformAoeDirectHit(GameDirection direction, List<BattleHero> listHeroBeImpacted, bool playAniHurt = false)
	{
		return 0f;
	}
}
