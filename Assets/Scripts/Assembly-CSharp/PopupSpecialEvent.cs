using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSpecialEvent : BasePopup
{
	public VerticalPoolGroup m_GoScrollTabContent;

	private int m_CurrentEventId;

	public Image m_Bgr_0;

	public Image m_Bgr_1;

	public Image m_ImgTitle;

	public Text m_TitleEvent;

	public Text m_TimeCountDown;

	public GameObject m_PanelContent;

	public GameObject m_PrefabSpecialCheckin;

	public GameObject m_PrefabTravel;

	public GameObject m_PrefabSpecialPack;

	private GameObject goPanelEvent;

	private bool isRefresh;

	private GameEventInfo eventInfo;

	private List<GameEventInfo> m_LstEventInfo;

	private int templateId;

	private string urlEventDetail;

	private TimeStruct timeRemain;

	protected override void Start()
	{
	}

	public void GetListEvent()
	{
	}

	private void InitTabEvent(List<GameEventInfo> lstEventInfo)
	{
	}

	private void TabEventClick(int eventId)
	{
	}

	public void RefreshEvent()
	{
	}

	private void FillInfoEvent(GameEventData baseData)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}

	public static void ShowUp()
	{
	}
}
