using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup7Days : BasePopup
{
	public static DropPositionType m_PositonTarget;

	public HorizontalPoolGroup m_PoolDay;

	public VerticalPoolGroup m_PoolTabMission;

	public VerticalPoolGroup m_PoolMission;

	public Text m_TimeCountDown;

	public GameObject m_PanelDiscount;

	public GameObject m_PanelMission;

	private List<GameEventInfo> m_LstEventInfo;

	private List<GameEventInfo> m_LstTabMission;

	private int m_CurrDay;

	private int m_CurrIndexTab;

	private int m_CurrIdTab;

	private int m_TempId;

	private TimeStruct timeRemain;

	protected override void Start()
	{
	}

	private void InitPool()
	{
	}

	private void FillDataMission(GameEventData data)
	{
	}

	private void ItemDayClick()
	{
	}

	private void ItemTabMissionClick()
	{
	}

	private void MissionGotoClick(string url)
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
