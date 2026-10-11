using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap2
{
	public class TapShopItem : MonoBehaviour
	{
		public Image m_ImageItem;

		public Text m_TextAmount;

		public Text m_TextPrice;

		private TapShopItemInfo _itemInfo;

		public Action<TapShopItemInfo> m_ItemClickCb;

		private void Start()
		{
		}

		public void SetItemInfo(TapShopItemInfo itemInfo)
		{
		}

		public bool IsProduct(string productId)
		{
			return false;
		}

		public void SetPrice(string strPrice)
		{
		}

		public void OnClick()
		{
		}

		private void Update()
		{
		}
	}
}
