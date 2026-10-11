using System.Collections.Generic;
using GGEvent;
using UnityEngine;

public class GGEventScene : BaseScene
{
	public const int TAB_EVENT_WEEK = 0;

	public const int TAB_EVENT_MONTH = 1;

	public const int TAB_EVENT_COOL = 2;

	private static readonly int SV_ID_STARTER;

	private static readonly int SV_GET_LIST_EVENTS;

	public TabManager m_TabMng;

	public RectTransform m_TfEventDetail;

	public VerticalPoolGroup m_PoolEventHeadline;

	private ActionRepeatTimer m_ActionCountdownTimeEvent;

	public static int ResumeTabId;

	public static int ResumeEventId;

	private List<GGEventHeadlineInfo> _lstEventHeadlineInfo;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private bool IsTabIdValid(int tabId)
	{
		return false;
	}

	private void OnTabEventClick(int tabId)
	{
	}

	private void OnButtonEventClick(int eventIdSelected)
	{
	}

	private void LoadEventDetail(GGEventHeadlineInfo eventHeadlineInfo)
	{
	}

	private string GetPrefabEventDetail(int templateId)
	{
		return null;
	}

	public void ButtonShopFeatherClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public override void ProcessKEvent(int serviceId, object data)
	{
	}

	private void Update()
	{
	}
}
