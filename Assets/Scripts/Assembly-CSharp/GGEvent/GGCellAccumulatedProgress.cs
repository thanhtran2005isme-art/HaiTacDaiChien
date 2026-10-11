using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGCellAccumulatedProgress : GGCellAccumulatedBase
	{
		public Slider m_SliderProgress;

		public Text m_TextProgress;

		public Text m_TextStatus;

		public GameObject m_GoButtonAction;

		public Text m_TextAction;

		public Action<GGAccumulatedReward> m_ActionClickCb;

		public override void SetCellData(GGAccumulatedReward accReward)
		{
		}

		public void ButtonActionClick()
		{
		}
	}
}
