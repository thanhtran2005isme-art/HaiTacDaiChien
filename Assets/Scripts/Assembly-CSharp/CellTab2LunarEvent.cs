using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellTab2LunarEvent : MonoEventHandler
{
	[SerializeField]
	private Text m_TextButton;

	[SerializeField]
	private Text m_TextLimit;

	[SerializeField]
	private Transform m_GoParentTo;

	[SerializeField]
	private Transform m_GoParentFrom;

	[SerializeField]
	private Image m_ImgButtonAttack;

	[SerializeField]
	private Text m_TextAttack;

	[SerializeField]
	private Image m_ImageAdd;

	public Action<int> m_TabItemClick;

	private int m_id;

	private int m_Remain;

	private List<GameItem> m_ListGoGameItemFrom;

	private List<GameItem> m_ListGoGameItemTo;

	public void SetData(DefineDataType2 data)
	{
	}

	public void ExchangeButtonClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void ParseDataBuyItem(string json)
	{
	}
}
