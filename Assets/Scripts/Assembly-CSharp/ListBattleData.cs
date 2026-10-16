using System.Collections.Generic;
using pbdson;

public class ListBattleData
{
	public long battleID;

	public List<BattleData> m_LstBattleData;

	public BonusModel m_BonusModel;

	public List<object> m_LstRewardSelection;

	public BattleResultType m_ResultType;

	public int m_Team1Score;

	public int m_Team2Score;

	public BattlePlayerInfo m_PlayerAttack;

	public BattlePlayerInfo m_PlayerDefense;

	public bool m_AllowSkipBattle;

	public ArenaPointResult m_ArenaPointResult;

	public static ListBattleData Parse(PbListBattleResult pbListBattleResult, bool addBonusToInventory = true)
	{
		return null;
	}

	public void SetShowBattleTotalDamage()
	{
	}

	public BattlePointDetail GetBattlePointDetail(int gameIndex)
	{
		return null;
	}
}
