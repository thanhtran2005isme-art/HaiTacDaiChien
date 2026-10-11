using UnityEngine;
using UnityEngine.UI;
using pbdson;

public class PanelHistory : MonoEventHandler
{
	[SerializeField]
	private VerticalPoolGroup m_PoolItem;

	[SerializeField]
	private GameObject m_ItemPool;

	[SerializeField]
	private Text m_TextBattleFakeId;

	[SerializeField]
	private GameObject m_GoPanelHistoryDetail;

	[SerializeField]
	private GameObject m_GoPanelResult;

	private DataItemBattleLog m_ItemDetail;

	private DataItemBattleLog m_ItemAvgDetail;

	private int m_BattleFakeId;

	protected override void Awake()
	{
	}

	public void Init(PbListUser lstUser)
	{
	}

	public void ButtonBackClick()
	{
	}

	private DataTeamInfo AddDataTeamInfo(PbListUser lstUser, int index)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
