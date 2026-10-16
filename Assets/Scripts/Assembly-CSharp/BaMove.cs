using UnityEngine;

public class BaMove : BattleAction
{
	private Vector2 m_PointMoveTo;

	public BaMove(BattleHero heroScript, float duration, Vector2 pointDest)
		: base(null, 0f)
	{
	}

	public override void PerformAction()
	{
	}
}
