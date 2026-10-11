using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellExchangeGem : MonoBehaviour
{
	public Text m_TextPrice;

	public Text m_TextNumbBuy;

	public Text m_TextAction;

	public Image m_IconFeat;

	public OnPublicDataCallback itemBuyCB;

	public Transform m_TfRewards;

	public ScrollRect m_ScrollRect;

	private ExchangeGemItem m_Reward;

	private int m_EventId;

	private bool m_IsNotEnougnMaterial;

	private BonusModel bonus;

	private List<object> lstObjModel;

	private GameObject prefabGameItem;

	private void Awake()
	{
	}

	public void ShowData(ExchangeGemItem exchangeItem)
	{
	}

	public void ButtonActionClick()
	{
	}
}
