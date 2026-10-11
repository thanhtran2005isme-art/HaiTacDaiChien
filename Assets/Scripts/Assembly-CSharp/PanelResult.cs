using System;
using System.Collections.Generic;
using UnityEngine;
using pbdson;

public class PanelResult : MonoEventHandler
{
	[SerializeField]
	private GameObject m_GoReplay;

	[SerializeField]
	private GameObject m_GoPanelResultOneHero;

	[SerializeField]
	private GameObject m_ItemPool;

	[SerializeField]
	private GameObject m_Team1Win;

	[SerializeField]
	private GameObject m_Team1Lost;

	[SerializeField]
	private GameObject m_Team2Win;

	[SerializeField]
	private GameObject m_Team2Lost;

	[SerializeField]
	private VerticalPoolGroup m_poolItem;

	public Action m_ButtonBackCB;

	public Action m_ButtonShowOneHeroCB;

	private long m_BattleId;

	private ListCommonVector lstInfo;

	private int curTypeClick;

	private Dictionary<int, List<long>> DataListHero;

	protected override void Awake()
	{
	}

	public void Init(int type, ListCommonVector lstInfo, DataItemBattleLog dataTeam, int battleId = 0)
	{
	}

	private void CaculatorData(int typeClick)
	{
	}

	private void SetStatus(int type)
	{
	}

	public void ButtonBackClick()
	{
	}

	public void ButtonReplayClick()
	{
	}

	public void ButtotToggeClick(int id)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
