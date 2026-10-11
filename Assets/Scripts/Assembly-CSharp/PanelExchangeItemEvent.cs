using UnityEngine.UI;

public class PanelExchangeItemEvent : MonoEventHandler
{
	public GridPoolGroup m_PoolGroup;

	public ScrollRect m_ScrollRect;

	private bool m_InitPool;

	private GameExchangeGemEvent m_GameEvent;

	public BoxGameEventInfo m_BoxEventInfo;

	private string m_UrlEventDetail;

	public void ButtonHelpClick()
	{
	}

	public void LoadEventData(string urlEventDetail, GameExchangeGemEvent gameEvent)
	{
	}

	private void LoadEvent(GameExchangeGemEvent gameEvent)
	{
	}

	public void ReloadEvent()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
