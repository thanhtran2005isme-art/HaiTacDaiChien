using UnityEngine;

public class DummyBotHeroAI
{
	public DummyHeroBattle m_HeroBot;

	public float m_Speed;

	public float m_Attack;

	public float m_Hp;

	private float m_HpMax;

	public float m_DistanceStartAttack;

	public DummyHealthBar m_HealthBar;

	private DummyBotState m_AiState;

	private DummySkillInfo m_SkillInfo;

	private bool m_Death;

	private const float DURATION_HURT = 1f;

	public DummyBotHeroAI(DummyHeroBattle heroBot, BotCampaignInfo botInfo)
	{
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public void Idle()
	{
	}

	public void FindTarget()
	{
	}

	public void AttackTarget()
	{
	}

	public void BeHurt()
	{
	}

	public void Die()
	{
	}

	public void DestroyBot()
	{
	}

	public void HideHealthBar()
	{
	}

	public void CastAttack()
	{
	}

	public void SetAIState(DummyBotState aiState)
	{
	}

	public Vector2 GetBotPosition()
	{
		return default;
	}

	public void ApplyDamage(float damagePoint)
	{
	}

	private Vector2 ConvertWordPointToCanvasPoint(Vector2 pointWord)
	{
		return default;
	}

	public bool IsDeath()
	{
		return false;
	}

	public void Update(float deltaTime)
	{
	}
}
