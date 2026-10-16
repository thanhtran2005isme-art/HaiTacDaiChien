using System.Collections.Generic;
using UnityEngine;

namespace GGEvent
{
	public class GGGrowthFundEventV2 : GGGameEventBehavior
	{
		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_LOAD_SUB_EVENT_DETAIL;

		public RectTransform m_TfScrollContent;

		public GameObject m_PrefabBanner;

		public GameObject m_PrefabCellReward;

		private List<GrowthFundSubEventInfo> _listSubEventInfo;

		private GGBannerGrowthFund _banner;

		private List<GameObject> _listCellReward;

		private int _currentTabIdx;

		protected override void Start()
		{
		}

		public override void SetEventInfo(GGEventHeadlineInfo eventHeadlineInfo)
		{
		}

		private void InitContent()
		{
		}

		private void LoadEvent()
		{
		}

		private void ParseEventDetail(string strJson)
		{
		}

		private void ReloadSubEvent()
		{
		}

		private void ParseSubEventDetail(string strJson)
		{
		}

		private void OnTabSelect(int tabIdx)
		{
		}

		private void ShowListRewardItem(List<GGGrowthFundRewardInfo> lstRewardInfo)
		{
		}

		private void OnCellActionClick(GGGrowthFundRewardInfo rewardInfo)
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}

		private void BuyIAP(string productId, string hash)
		{
		}
	}
}
