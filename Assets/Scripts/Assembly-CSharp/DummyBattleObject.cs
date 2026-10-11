public abstract class DummyBattleObject
{
	private bool m_Finished;

	public DummyHeroBattle m_HeroCast;

	public DummyBattleArena m_BattleArena;

	public DummyBattleObject(DummyBattleArena arena, DummyHeroBattle heroCast)
	{
	}

	public bool IsFinished()
	{
		return false;
	}

	public void MarkFinished()
	{
	}

	public abstract void DestroyObject();

	public abstract void UpdateObject(float deltaTime);
}
