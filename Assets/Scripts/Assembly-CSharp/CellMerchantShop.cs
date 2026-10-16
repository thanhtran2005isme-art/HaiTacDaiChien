using System;
using UnityEngine;
using UnityEngine.UI;

public class CellMerchantShop : MonoBehaviour
{
	private MerchantShopItem m_ShopItem;

	public Action<MerchantShopItem> m_BuyClickCb;

	public GameItem m_GameItem;

	public Image m_IconPrice;

	public Text m_TextPrice;

	private void Start()
	{
	}

	public void SetData(MerchantShopItem item)
	{
	}

	public void ButtonBuyClick()
	{
	}

	private void Update()
	{
	}
}
