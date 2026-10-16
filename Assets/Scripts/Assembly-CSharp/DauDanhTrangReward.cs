using System.Collections.Generic;

public class DauDanhTrangReward
{
	public List<int> m_RewardData1;

	public List<int> m_RewardData2;

	public bool m_received1;

	public bool m_received2;

	public int m_LvlTarget;

	public int m_CurrLvl;

	public string m_SumDesc;

	public string m_thresholdDesc;

	public string m_Status;

	public int m_IndexReward;

	public bool checkBuy;

	public DauDanhTrangReward(int indexReward, string thresholdDesc, List<int> rewardData1, bool received1, List<int> rewardData2, bool received2, string sumDesc, int lvlTarget, string status, int currLvl)
	{
	}
}
