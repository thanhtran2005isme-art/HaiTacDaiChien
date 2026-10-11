using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectMaterial : MonoBehaviour
{
	public GridPoolGroup m_PoolMaterial;

	public VerticalPoolGroup m_PoolSetMaterial;

	public Action<object> m_ItemSelectedCb;

	private List<long> listItemSelected;

	public GameObject m_GroupNoItems;

	private int currItemSelected;

	private List<List<long>> listSet;

	public void InitPool()
	{
	}

	public void FillDataSetItem(List<long> listItem, int currSelect)
	{
	}

	public void SelectClick()
	{
	}

	public static PopupSelectMaterial ShowUpSetItem(List<long> listItem, int currIndexSelect = -1)
	{
		return null;
	}
}
