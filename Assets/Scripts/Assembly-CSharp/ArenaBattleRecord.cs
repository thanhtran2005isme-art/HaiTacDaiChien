public class ArenaBattleRecord
{
	public string m_OpponentName;

	public int m_OpponentId;

	public int m_Avatar;

	public int m_TypeAvatar;

	public int m_OpponentLevel;

	public int m_BattleId;

	public bool m_Win;

	public int m_Point;

	public int m_BattleTimePassed;

	public string m_PointCaption;

	public int m_NewRank;

	public int m_OldRank;

	public bool m_IsAttacker;

	public ArenaType m_ArenaType;

	public ArenaBattleRecord(int opponentId, string opponentName, int typeAvt, int avatar, int opponentLevel, int battleId, bool win, int timePassed = 0)
	{
	}
}
