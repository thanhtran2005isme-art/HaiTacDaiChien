using System.Collections.Generic;

public class GuildBossStage
{
	public int m_StageId;

	public List<int> m_LstMonsterId;

	public int m_GuildExp;

	public int m_GoldPerBattle;

	public int m_CoinPerBattle;

	public int m_GoldKill;

	public int m_CoinKill;

	public int m_GoldReward1;

	public int m_CoinReward1;

	public int m_GoldReward2;

	public int m_CoinReward2;

	public int m_GoldReward3;

	public int m_CoinReward3;

	public int m_GoldReward4_10;

	public int m_CoinReward4_10;

	public int m_GoldReward11_15;

	public int m_CoinReward11_15;

	public int m_GoldReward16_20;

	public int m_CoinReward16_20;

	public int m_GoldReward21_max;

	public int m_CoinReward21_max;

	public int[] GetRewardByRank(int rank)
	{
		return null;
	}

	public GuildBossStage(int stageId, List<int> lstMonsterId)
	{
	}
}
