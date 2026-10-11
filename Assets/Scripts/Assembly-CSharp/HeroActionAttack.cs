using System.Collections.Generic;

public class HeroActionAttack : HeroActionBattle
{
	public int m_SkillIndex;

	private List<QueuingSpineEffect> m_ListQueueSpineEffect;

	private bool _animationAttackCompleted;

	private string _attackAnimationName;

	public HeroActionAttack(DummyBattleArena arena, DummyHeroBattle heroBattle, int skillIndex)
		: base(null, null)
	{
	}

	public override void StartAction()
	{
	}

	public override void InteruptAction()
	{
	}

	public override void UpdateAction(float deltaTime)
	{
	}

	public override void AnimationCompleted(string animationName)
	{
	}
}
