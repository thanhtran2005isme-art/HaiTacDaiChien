using UnityEngine;

public class PanelExchangeGemEvent : MonoEventHandler
{
	public BoxGameEventInfo m_BoxEventInfo;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_BgrPopup;

	private bool m_InitPool;

	private GameExchangeGemEvent m_GameEvent;

	private string m_UrlEventDetail;

	public void LoadEventData(string urlEventDetail, GameExchangeGemEvent gameEvent, bool isPopup = false)
	{
	}

	public void ItemBuyClick(object data)
	{
	}

	public void ReloadEvent()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
