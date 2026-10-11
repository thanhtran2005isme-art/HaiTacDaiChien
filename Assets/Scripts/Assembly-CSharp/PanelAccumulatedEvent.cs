using UnityEngine;

public class PanelAccumulatedEvent : MonoEventHandler
{
	public const int TYPE_ACCUMULATED_1 = 0;

	public const int TYPE_ACCUMULATED_2 = 1;

	public const int TYPE_ACCUMULATED_3 = 2;

	public BoxGameEventInfo m_BoxEventInfo;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_BgrPopup;

	private bool m_InitPool;

	private GameAccumulatedEvent m_GameEvent;

	private new void Start()
	{
	}

	public void LoadEventData(GameAccumulatedEvent gameEvent, int typeAccumulate, bool isPopup = false)
	{
	}

	public void CloseClick()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
