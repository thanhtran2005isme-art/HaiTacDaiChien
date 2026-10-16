using UnityEngine;

public class DummyPlayerHeroControl : ActionTimerManager
{
	private DummyHeroState m_State;

	public DummyHeroBattle m_HeroPlayer;

	private DummySkillInfo m_SkillInfo;

	private bool m_Death;

	private float m_Hp;

	private float m_HpMax;

	private float m_Speed;

	private float m_Attack;

	private float ATTACK_DURATION;

	public DummyHealthBar m_HealthBar;

	private bool _onMoving;

	private float _angleMoving;

	private bool _waitForActionFinish;

	public DummyPlayerHeroControl(DummyHeroBattle playerHero, JoystickController joyStickController)
	{
	}

	public void SetHeroPlayerData(float attack, float hp, float speed)
	{
	}

	public void LoadHeroModel(int spineId)
	{
	}

	public void ApplyDamage(float damagePoint)
	{
	}

	private Vector2 ConvertWordPointToCanvasPoint(Vector2 pointWord)
	{
		return default;
	}

	public void Die()
	{
	}

	public float GetFPercentHpLeft()
	{
		return 0f;
	}

	public bool IsDeath()
	{
		return false;
	}

	private void OnJoyStickMove(float angleRad)
	{
	}

	private void OnJoyStickHalt()
	{
	}

	private void OnAttackClick()
	{
	}

	public void Update(float deltaTime)
	{
	}
}
