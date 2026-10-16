public class BotAttackState : DummyBotState
{
	public DummyPlayerHeroControl m_HeroPlayerControl;

	private bool m_WaitToFinishAttack;

	public BotAttackState(DummyBotHeroAI botAI, DummyPlayerHeroControl heroPlayerControl)
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
