using System.Collections.Generic;
using UnityEngine;
using pbdson;

public class PanelHistoryDetail : MonoEventHandler
{
	[SerializeField]
	private VerticalPoolGroup m_VerPool;

	[SerializeField]
	private GameObject m_ItemPool;

	[SerializeField]
	private GameObject m_GoPanelResult;

	private int avgId;

	private ListCommonVector lstInfo;

	private DataItemBattleLog battleLog;

	protected override void Awake()
	{
	}

	public void Init(DataItemBattleLog data, List<long> lstDetail)
	{
	}

	public void ButtonBackClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
