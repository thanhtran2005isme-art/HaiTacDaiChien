using System.Collections.Generic;
using XLua;

[CSharpCallLua]
public class G2UIEventController
{
	public List<IG2UIEventHandler> m_ListHandler;

	private static G2UIEventController m_Instance;

	public static G2UIEventController Instance => null;

	private G2UIEventController()
	{
	}

	public void RegisterHandler(IG2UIEventHandler handler)
	{
	}

	public void RemoveHandler(IG2UIEventHandler handler)
	{
	}

	public void SendG2Event(int eventId, object data)
	{
	}
}
