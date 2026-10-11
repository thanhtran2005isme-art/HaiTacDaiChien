using System.Collections.Generic;

public class TowerPlayerData
{
	public int m_TypeTower;

	public int m_CurrentLevelId;

	public int m_NumberAtkLeft;

	public int m_MaxNumberAtk;

	public bool m_HasDailyReward;

	public bool m_CollectedDailyReward;

	public List<TowerHistoryLog> m_LstTowerLog;

	public int m_CountDownBuyTurn;

	public TowerPlayerData(int typeTower)
	{
	}
}
