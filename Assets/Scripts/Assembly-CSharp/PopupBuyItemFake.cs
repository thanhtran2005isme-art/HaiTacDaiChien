using UnityEngine;
using UnityEngine.UI;

public class PopupBuyItemFake : BasePopup
{
	public Image m_ImgItemBuy;

	public Text m_TextCost;

	public InputField m_InputNumberItem;

	private int m_Limit;

	private string m_StringMsgExceedLimit;

	[HideInInspector]
	public int m_NumItemPerPurchase;

	private TypeCurrency typeCurrency;

	protected override void Start()
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

	public void OnEndEdit()
	{
	}

	public void ButtonBuyClick()
	{
	}

	private void SetImageItemBuy(TypeCurrency type)
	{
	}

	public static void ShowPopup(TypeCurrency type)
	{
	}
}
