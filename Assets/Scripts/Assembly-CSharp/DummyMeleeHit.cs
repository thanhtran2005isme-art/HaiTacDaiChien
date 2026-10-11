using UnityEngine;

public class DummyMeleeHit : DummyBattleObject
{
	public float m_DamagePoint;

	public Vector2 m_PositionDamage;

	public DummyMeleeSkillInfo m_SkillInfo;

	private float _countTime;

	private bool _causeDamage;

	private bool _playSfx;

	private bool _playEffect;

	public DummyMeleeHit(DummyBattleArena arena, DummyHeroBattle heroCast, Vector2 positionDamage, float damagePoint, DummyMeleeSkillInfo skillInfo)
		: base(null, null)
	{
	}

	public override void DestroyObject()
	{
	}

	public override void UpdateObject(float deltaTime)
	{
	}
}
