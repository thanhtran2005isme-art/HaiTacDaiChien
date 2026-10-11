public class BotFindTargetState : DummyBotState
{
	public DummyPlayerHeroControl m_HeroPlayerControl;

	public const float DISTANCE_STOP_BEFORE_TARGET = 1f;

	public BotFindTargetState(DummyBotHeroAI botAI, DummyPlayerHeroControl heroPlayerControl)
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
