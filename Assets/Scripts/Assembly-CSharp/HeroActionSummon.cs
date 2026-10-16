using UnityEngine;

public class HeroActionSummon : HeroActionBattle
{
	public Vector2 m_PositionFrom;

	public Vector2 m_PositionTo;

	public float m_TimeMoving;

	private MovementHandler m_MovementHandler;

	public HeroActionSummon(DummyBattleArena arena, DummyHeroBattle heroBattle, Vector2 positionFrom, Vector2 postionTo, float timeMoving)
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
