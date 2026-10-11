using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSpecialEvent2 : BasePopup, ITabListener
{
	[SerializeField]
	private TabManager m_TabManager;

	[SerializeField]
	private GameObject m_PrefabTabButton;

	[SerializeField]
	private RectTransform m_TfTabContainer;

	[SerializeField]
	private RectTransform m_TfEventContainer;

	[SerializeField]
	private Text m_TextTitle;

	[SerializeField]
	private TextCountdown m_TextCountdown;

	private List<SpEventInfo> m_ListEventInfo;

	private Dictionary<int, GameObject> m_DictGoEvent;

	private ActionRepeatTimer m_TimerCountDownTime;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void Update()
	{
	}

	public void LoadEventList(string urlListEvent)
	{
	}

	private void ShowTabListEvent(List<SpEventInfo> lstEventInfo)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public override void ProcessKEvent(int serviceId, object data)
	{
	}

	public static PopupSpecialEvent2 ShowUp(string urlListEvent)
	{
		return null;
	}

	public static PopupSpecialEvent2 ShowUp()
	{
		return null;
	}

	public static PopupSpecialEvent2 ShowUp(int eventGroupType)
	{
		return null;
	}
}
