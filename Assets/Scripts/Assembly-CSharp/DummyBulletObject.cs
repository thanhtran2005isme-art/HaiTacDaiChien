using UnityEngine;

public class DummyBulletObject : DummyBattleObject
{
	private SkillObject m_SkillObject;

	private Vector2 m_vBullet;

	private DummyShotSkillInfo m_SkillInfo;

	private float m_DmgPoint;

	private float _countTime;

	private bool _causeDamage;

	private bool _showCastEff;

	private bool _showHitEff;

	private float _countHitTime;

	private Vector2 _posBullet;

	public DummyBulletObject(DummyBattleArena arena, DummyHeroBattle heroCast, Vector2 posBullet, Vector2 velocity, float damagePoint, DummyShotSkillInfo skillInfo)
		: base(null, null)
	{
	}

	public override void DestroyObject()
	{
	}

	private bool CheckIfBulletExplode(Vector2 posBullet)
	{
		return false;
	}

	private void ApplyBulletDamage(Vector2 posBullet)
	{
	}

	private void AddHeroEffect(EffectInfo effInfo)
	{
	}

	private void AddHitEffect(EffectInfo effInfo, Vector2 posBullet)
	{
	}

	public override void UpdateObject(float deltaTime)
	{
	}
}
