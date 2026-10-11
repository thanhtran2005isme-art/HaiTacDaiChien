using UnityEngine;

public class PanelPackValueEvent : MonoEventHandler
{
	public BoxGameEventInfo m_BoxEventInfo;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_BgrPopup;

	private bool m_InitPool;

	private GamePackValueEvent m_GameEvent;

	private float timeRefresh;

	private bool stopUpdateBonus;

	private new void Start()
	{
	}

	public void LoadEventData(GamePackValueEvent gameEvent, bool isRefresh = false)
	{
	}

	public void UpdateEventData(GamePackValueEvent gameEvent)
	{
	}

	public void StartUpdateBonus()
	{
	}

	public void CloseClick()
	{
	}

	public void ItemBuyClick(object data)
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
