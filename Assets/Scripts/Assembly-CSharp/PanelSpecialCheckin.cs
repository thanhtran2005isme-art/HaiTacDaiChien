using UnityEngine;
using UnityEngine.UI;

public class PanelSpecialCheckin : MonoEventHandler
{
	public HorizontalPoolGroup m_PoolGroup;

	public GameObject m_ButtonCheckIn;

	public Image m_Bgr;

	private bool m_InitPool;

	private GameCheckinEvent m_GameEvent;

	private GameEventInfo eventChekinInfo;

	public void LoadEventData(GameCheckinEvent gameEvent)
	{
	}

	public void CheckinClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
