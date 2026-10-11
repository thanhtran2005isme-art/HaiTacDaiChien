using UnityEngine;

public class PanelDauDanhTrangEvent : MonoEventHandler
{
	public BoxGameEventInfo m_BoxEventInfo;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_ButtonBuy;

	private bool m_InitPool;

	private GameDauDanhTrangEvent m_GameEvent;

	private float timeRefresh;

	private bool stopUpdateBonus;

	public void LoadEventData(GameDauDanhTrangEvent gameEvent, bool isFresh = false)
	{
	}

	public void BuyClickCB()
	{
	}

	public void StartUpdateBonus()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
