using System;
using UnityEngine;
using UnityEngine.UI;

public class CellExchangeItemEvent : MonoBehaviour
{
	public Text m_TextCost;

	public Image m_ImgFeat;

	public GameObject m_GoLimited;

	public GameObject m_GoOutOfStock;

	public GameItem m_RewardController;

	public Action<long, int> itemCb;

	private ExchangeGemItem m_ItemExchange;

	private int m_EventId;

	public void ShowData(int eventId, ExchangeGemItem itemExchange)
	{
	}

	public void ButtonActionClick()
	{
	}

	private void Update()
	{
	}
}
