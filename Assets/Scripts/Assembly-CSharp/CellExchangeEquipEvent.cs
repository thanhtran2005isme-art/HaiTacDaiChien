using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellExchangeEquipEvent : MonoBehaviour
{
	public Text m_TextReq_0;

	public Text m_TextReq_1;

	public Text m_TextLimit;

	public Text m_TextLabelAction;

	public Image m_ImgFeat_0;

	public Image m_ImgFeat_1;

	public List<SlotExchangeEquip> m_ListFromItem;

	public List<SlotExchangeEquip> m_ListToItem;

	public Action<string> actionCb;

	public Action<List<long>> chooseSetCb;

	private ExchangeEquipItem m_Reward;

	private int m_EventId;

	private List<ItemModel> lstFromItemModel;

	private List<ItemModel> lstToItemModel;

	private string listItemSelected;

	private int currSetSelect;

	public void ShowData(ExchangeEquipItem exchangeEquip, List<long> dataSetChose = null)
	{
	}

	private void ChooseSetCallback(object obj)
	{
	}

	public void ButtonActionClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public long CheckNumbItemReq(ItemEquipment itemEquip)
	{
		return 0L;
	}
}
