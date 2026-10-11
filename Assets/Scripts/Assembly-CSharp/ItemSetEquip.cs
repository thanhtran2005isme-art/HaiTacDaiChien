using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSetEquip : MonoBehaviour
{
	public Text m_NameSet;

	public List<SlotExchangeEquip> m_ListToItem;

	public Toggle m_CheckSelected;

	public Action<List<long>> itemClickCB;

	private List<long> listItem;

	public void FillData(int index, List<long> info, bool isSelected)
	{
	}

	public void ItemClick()
	{
	}

	private string GetNameSet(int id)
	{
		return null;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
