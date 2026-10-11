using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelPlayerAllStarsLeagueInfo : MonoEventHandler
{
	public PlayerAvatar m_Avatar;

	public Text m_TextPower;

	public Text m_TextRanking;

	public Text m_TextName;

	public Text m_TextNumAttack;

	public Text m_TextPoint;

	public Action m_RecordClickCb;

	public Action m_DefensiveClickCb;

	public Action m_BattleClickCb;

	public Action m_RewardViewClick;

	public Action m_KnockOutViewClick;

	public Action m_BetHistoryViewClick;

	public GameObject m_GoButtonKnockOut;

	public GameObject m_GoButtonBetHistory;

	public GameObject m_GoButtonBattle;

	public GameObject m_GoGroupNumAttack;

	protected override void Start()
	{
	}

	public void SetPlayerInfo(int ranking, int point, int power, int numAtk, int maxAtk)
	{
	}

	public void SetAttackTurn(int numAtk, int maxAtk)
	{
	}

	public void ButtonBuyTurnClick()
	{
	}

	public void SetModeRoundTable()
	{
	}

	public void SetModeRoundKnockOut()
	{
	}

	public void ButtonShowBoardKnockOutClick()
	{
	}

	public void ButtonShowBetHistory()
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

	private void Update()
	{
	}
}
