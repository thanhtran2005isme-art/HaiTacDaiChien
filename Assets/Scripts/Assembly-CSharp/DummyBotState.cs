public abstract class DummyBotState : ActionTimerManager
{
	protected DummyBotHeroAI m_BotHeroAI;

	public DummyBotState(DummyBotHeroAI botHeroAI)
	{
	}

	public abstract void StateStart();

	public abstract void UpdateState(float deltaTime);
}
