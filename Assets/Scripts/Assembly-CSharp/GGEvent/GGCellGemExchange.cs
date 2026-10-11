using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGCellGemExchange : MonoBehaviour
	{
		public Text m_TextCost;

		public Text m_TextRemain;

		public Text m_TextAction;

		public Image m_IconItemCost;

		public Action<GGGemExchangeElement> m_ItemActionClickCb;

		public RectTransform m_TfRewards;

		public ScrollRect m_ScrollRect;

		private GGGemExchangeElement _gemExchangeElm;

		private bool _isEnoughMatterial;

		public void SetCellData(GGGemExchangeElement exchangeItem)
		{
		}

		public void ButtonActionClick()
		{
		}
	}
}
