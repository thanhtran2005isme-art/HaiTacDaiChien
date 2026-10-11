using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupSellItem : BasePopup
{
	public GameObject m_GoButtonClose;

	public GameObject m_GoButtonMinus;

	public GameObject m_GoButtonPlus;

	public GameItem m_GameItem;

	public Text m_TextTitle;

	public InputField m_InputNumberItem;

	public Text m_TextSellValue;

	public Image m_ImgSellCurrency;

	private int m_UnitPrice;

	public Action m_CloseClickCb;

	private int m_Limit;

	private ItemModel m_SellItem;

	protected override void Start()
	{
	}

	public void NumberItemChanged()
	{
	}

	public void ButtonPlusClick()
	{
	}

	public void ButtonMinusClick()
	{
	}

	private void SetItemNumber(int numItem)
	{
	}

	public void ButtonSellClick()
	{
	}

	public void SetData(ItemModel itemSell, long numberItem, ShopCurrencyType currencyType, int unitPrice)
	{
	}

	public static PopupSellItem ShowUp(ItemModel itemSell, long number, ShopCurrencyType currencyType, int unitPrice)
	{
		return null;
	}

	public override void ClosePopup()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
