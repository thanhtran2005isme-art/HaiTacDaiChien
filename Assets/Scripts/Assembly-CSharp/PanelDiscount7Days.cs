using System;
using UnityEngine;
using UnityEngine.UI;

public class PanelDiscount7Days : MonoBehaviour
{
	public GameItem m_ItemReward;

	public Text m_TitleBasePrice;

	public Text m_TextBasePrice;

	public Text m_TextPrice;

	public Text m_TextAction;

	public Text m_NameReward;

	public Image m_Bgr;

	public Image m_IconPrice;

	public Image m_IconPriceBase;

	public Image m_ButtonAct;

	public RectTransform m_Line;

	public Action<DiscountItemReward> m_BuyCb;

	private DiscountItemReward m_Reward;

	private GameDiscountItemEvent m_DiscountEvent;

	public void ShowData(GameDiscountItemEvent eventData)
	{
	}

	public void BuyClick()
	{
	}

	private void Update()
	{
	}
}
