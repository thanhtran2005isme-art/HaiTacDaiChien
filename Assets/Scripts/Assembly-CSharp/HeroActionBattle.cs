public abstract class HeroActionBattle : IActionBattle
{
	public DummyBattleArena m_Arena;

	public DummyHeroBattle m_HeroBattleAction;

	public bool m_IsActionDone;

	public HeroActionBattle(DummyBattleArena arena, DummyHeroBattle heroBattle)
	{
	}

	public virtual void StartAction()
	{
	}

	public abstract void InteruptAction();

	public abstract void UpdateAction(float deltaTime);

	public virtual void AnimationCompleted(string animationName)
	{
	}
}
