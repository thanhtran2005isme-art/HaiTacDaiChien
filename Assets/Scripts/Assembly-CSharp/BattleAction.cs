public abstract class BattleAction
{
	public float m_Duration;

	public BattleHero m_HeroAction;

	public BattleAction(BattleHero heroAction, float duration)
	{
	}

	public abstract void PerformAction();
}
