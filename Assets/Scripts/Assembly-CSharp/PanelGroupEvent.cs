using System.Collections.Generic;
using UnityEngine;

public class PanelGroupEvent : MonoEventHandler
{
	public HorizontalPoolGroup tabSub;

	public GameObject m_GoContent;

	private int m_CurrentEventId;

	private List<GameEventInfo> m_LstEventInfo;

	private GameObject goPanelEvent;

	private bool isRefresh;

	private PopupGameEvents popupGameEvents;

	private int templateId;

	private GameEventInfo eventInfo;

	private string urlEventDetail;

	private GameObject goPanelLoading;

	public void InitGroup(List<GameEventInfo> lstGroup, PopupGameEvents gameEvents)
	{
	}

	public void RefreshEvent(int eventId)
	{
	}

	private void TabSubClick(int eventId)
	{
	}

	private void FillInfoEvent(GameEventData baseData)
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
