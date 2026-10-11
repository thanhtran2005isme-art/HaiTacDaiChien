using System.Collections.Generic;
using UnityEngine.UI;

public class PanelExchangeEquipEvent : MonoEventHandler
{
	public VerticalPoolGroup m_PoolGroup;

	public ScrollRect m_ScrollRect;

	private bool m_InitPool;

	private GameExchangeEquipEvent m_GameEvent;

	public BoxGameEventInfo m_BoxEventInfo;

	private string m_UrlEventDetail;

	private List<long> dataSetItemChoose;

	public void ButtonHelpClick()
	{
	}

	public void LoadEventData(string urlEventDetail, GameExchangeEquipEvent gameEvent)
	{
	}

	private void LoadEvent(GameExchangeEquipEvent gameEvent, bool reload = false)
	{
	}

	public void ChooseSet(List<long> data)
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
