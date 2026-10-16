using System;

public class LuaProcessEventWrapper : MonoEventHandler
{
	private Action<int, object> cbOnReceicedEvent;

	public void SetCallBackReceivedEvent(Action<int, object> cb)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
