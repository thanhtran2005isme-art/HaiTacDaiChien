using UnityEngine;

public class HeroActionSetPos : HeroActionBattle
{
	private Vector2 m_PositionSet;

	public HeroActionSetPos(DummyBattleArena arena, DummyHeroBattle heroBattle, Vector2 position)
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
}
