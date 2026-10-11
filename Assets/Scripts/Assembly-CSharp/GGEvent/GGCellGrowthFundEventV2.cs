using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGCellGrowthFundEventV2 : MonoBehaviour
	{
		public Text m_TextTitle;

		public RectTransform m_TfRewards;

		public ScrollRect m_ScrollRectReward;

		public Slider m_SliderProgress;

		public Text m_TextProgress;

		public Text m_TextStatus;

		public GameObject m_GoButtonAction;

		public Text m_TextAction;

		public Action<GGGrowthFundRewardInfo> m_ActionClickCb;

		private GGGrowthFundRewardInfo _rewardInfo;

		private void Start()
		{
		}

		public void SetCellData(GGGrowthFundRewardInfo growthReward)
		{
		}

		public void ButtonActionClick()
		{
		}

		private void Update()
		{
		}
	}
}
