using System.Collections.Generic;

public class BattleResultInfo
{
	public BattleResultType m_ResultType;

	public BonusModel m_BonusModel;

	public long m_BattleId;

	public List<object> m_LstRewardSelection;

	public ArenaPointResult m_ArenaPointResult;

	public int m_Team1Score;

	public int m_Team2Score;

	public BattlePlayerInfo m_AttackerInfo;

	public BattlePlayerInfo m_DefenderInfo;

	public BattleResultInfo(ListBattleData lstBattleData)
	{
	}
}
