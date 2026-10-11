using System.Collections.Generic;

namespace GGEvent
{
	public class GGDauDanhTrangRewardInfo
	{
		public int m_ItemId;

		public List<long> m_ListDataReward1;

		public bool m_Collected1;

		public List<long> m_ListDataReward2;

		public bool m_Collected2;

		public string m_StrTitle;

		public int m_FeatValue;

		public int m_FeatThreshold;

		public bool m_HasPurchaseIap;
	}
}
