public abstract class ActionTimer
{
	protected bool mDone;

	public bool IsDone()
	{
		return false;
	}

	public abstract void UpdateTimer(float deltaTime);
}
