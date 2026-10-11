public class ActionOnceTimer : ActionTimer
{
	public float mDelay;

	private float mCountTime;

	private MyActionDelegate.MyAction mActionJob;

	public ActionOnceTimer(float delay, MyActionDelegate.MyAction job)
	{
	}

	public void Reset()
	{
	}

	public void Stop()
	{
	}

	public override void UpdateTimer(float deltaTime)
	{
	}
}
