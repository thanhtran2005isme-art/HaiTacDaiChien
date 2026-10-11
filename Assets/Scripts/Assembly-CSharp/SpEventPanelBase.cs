public abstract class SpEventPanelBase : MonoEventHandler
{
	public virtual void GetEventId(int eventId)
	{
	}

	public abstract void LoadEventDetail(string url);

	public virtual void OnEventResume()
	{
	}
}
