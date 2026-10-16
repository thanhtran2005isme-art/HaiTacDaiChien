using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelectGameItem : BasePopup
{
	public GridPoolGroup m_PoolGrid;

	public GameObject m_GoScrollView;

	public Action<int> m_ItemPickedCb;

	public GameObject m_PrefabItem;

	private List<GameItemTickData> m_LstGameItemTick;

	public Text m_TextTitle;

	private int number;

	private int m_ItemIndexPicked;

	protected override void Awake()
	{
	}

	private void ItemPickChanged(int indexPicked)
	{
	}

	protected override void Start()
	{
	}

	public void LoadItems(List<object> lstItemObject, int number = -1)
	{
	}

	public void SetTitle(string title)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonOkClick()
	{
	}

	public static PopupSelectGameItem ShowUp(List<object> lstItemObject, int number = -1)
	{
		return null;
	}
}
