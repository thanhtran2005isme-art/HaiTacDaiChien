using System;
using UnityEngine.UI;

public class PopupBuyConfirm : BasePopup
{
	public Image m_ImageCurrency;

	public Text m_TextCost;

	public Text m_TextAction;

	public Text m_TextMessage;

	private Action m_ConfirmClickCb;

	private ShopCurrencyType m_CurrencyType;

	public Text m_TextTitle;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void SetTitle(string title)
	{
	}

	public void ButtonOkClick()
	{
	}

	public void SetData(string message, string action, long totalCost, ShopCurrencyType currencyType)
	{
	}

	public void SetConfirmCallback(Action confirmCb)
	{
	}

	public static PopupBuyConfirm ShowUp(string msg, string action, long totalCost, ShopCurrencyType currencyType)
	{
		return null;
	}

	private void Update()
	{
	}
}
