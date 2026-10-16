using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupBuyItem : BasePopup
{
	public Image m_ImageItem;

	public Image m_ImageCurrency;

	public Text m_TextCost;

	public Text m_TextNumberItem;

	public Text m_TextLimit;

	public Text m_TextBuy;

	public GameItem m_IconItemBuy;

	private Action<int> m_BuyClickCb;

	private ShopCurrencyType m_CurrencyType;

	private long m_UnitPrice;

	private long m_TotalMoney;

	private int m_Limit;

	public Text m_TextTitle;

	[HideInInspector]
	public int m_NumItemPerPurchase;

	public InputField m_InputNumberItem;

	private string m_StringMsgExceedLimit;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void SetTitle(string title)
	{
	}

	public void SetMessgeWhenExceedLimit(string msg)
	{
	}

	public void ButtonPlusClick()
	{
	}

	public void ButtonMinusClick()
	{
	}

	public void SetNumberPurchase(int numPurchase)
	{
	}

	public void NumberItemChanged()
	{
	}

	public void OnEndEdit()
	{
	}

	public void ButtonBuyClick()
	{
	}

	public void SetData(Sprite spriteItem, long unitPrice, long totalMoney, ShopCurrencyType currencyType, int limit = 999, string txtLimit = "")
	{
	}

	public void SetBuyClickCallback(Action<int> buyClickCb)
	{
	}

	public void SetBuyClickCallback(string strBuyCaption, Action<int> buyClickCb)
	{
	}

	public static PopupBuyItem ShowUp(Sprite spriteItem, long unitPrice, long totalMoney, ShopCurrencyType currencyType, int limit = 999, string txtLimt = "")
	{
		return null;
	}

	private void SetData(BonusModel toItem, long unitPrice, long totalMoney, Sprite spriteItemCost, int limit = 999)
	{
	}

	public static PopupBuyItem ShowUp(BonusModel toItem, long unitPrice, long totalMoney, Sprite spriteItemCost, int limit = 999)
	{
		return null;
	}

	private void Update()
	{
	}
}
