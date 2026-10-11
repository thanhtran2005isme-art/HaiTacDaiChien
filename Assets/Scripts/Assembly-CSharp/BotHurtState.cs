public class BotHurtState : DummyBotState
{
	private float m_DurationHurt;

	private float _countTime;

	public BotHurtState(DummyBotHeroAI botAI, float durationHurt)
		: base(null)
	{
	}

	public override void StateStart()
	{
	}

	public override void UpdateState(float deltaTime)
	{
	}
}
