using System;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGCellAccumulatedPurchase : GGCellAccumulatedBase
	{
		public Text m_TextLimitCap;

		public Text m_TextLimit;

		public Text m_TextAction;

		public Action m_PopupPurchaseOpenSuccessCb;

		public override void SetCellData(GGAccumulatedReward accReward)
		{
		}

		public void ButtonActionClick()
		{
		}
	}
}
