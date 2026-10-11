public class ActionRepeatTimer : ActionTimer
{
	public float mInterval;

	private float mCountTime;

	public float mDuration;

	private MyActionDelegate.MyAction mActionJob;

	private MyActionDelegate.MyAction mActionJobActionFinish;

	private float mTotalTime;

	public bool m_IgnoreTimeScale;

	public ActionRepeatTimer(float interval, MyActionDelegate.MyAction job)
	{
	}

	public ActionRepeatTimer(float interval, float duration, MyActionDelegate.MyAction job, MyActionDelegate.MyAction jobActionFinish = null)
	{
	}

	public void CallTheDelegate()
	{
	}

	public void ResetCounter()
	{
	}

	public void Stop()
	{
	}

	public override void UpdateTimer(float deltaTime)
	{
	}
}
