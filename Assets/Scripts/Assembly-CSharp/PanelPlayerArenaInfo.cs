using System;
using UnityEngine.UI;

public class PanelPlayerArenaInfo : MonoEventHandler
{
	public PlayerAvatar m_Avatar;

	public Text m_TextPower;

	public Text m_TextRanking;

	public Text m_TextPoint;

	public Text m_TextName;

	public Text m_TextTicket;

	public Action m_RecordClickCb;

	public Action m_DefensiveClickCb;

	public Action m_BattleClickCb;

	public Action m_RewardViewClick;

	private int m_PriceTicket;

	protected override void Start()
	{
	}

	public void SetPlayerInfo(int ranking, int power, int crystalPoint, int prizeTicket)
	{
	}

	public void ButttonRewoardClick()
	{
	}

	public void ButtonDefensiveClick()
	{
	}

	public void ButtonBattleRecordClick()
	{
	}

	public void ButtonBattleClick()
	{
	}

	public void ButtonBuyTicketClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
