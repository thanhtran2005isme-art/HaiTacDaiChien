using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGCellItemExchange : MonoBehaviour
	{
		public Text m_TextNumberCost;

		public Image m_ImgIconCost;

		public GameObject m_GoLimited;

		public GameObject m_GoOutOfStock;

		public GameItem m_ItemReward;

		public Action<GGItemExchangeElement> m_ItemActionClickCb;

		private GGItemExchangeElement _itemExchange;

		public void SetCellData(GGItemExchangeElement itemExchange)
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
