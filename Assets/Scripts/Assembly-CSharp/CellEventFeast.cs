using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellEventFeast : MonoBehaviour
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
	private Image m_ImageAdd;

	private int m_id;

	private int limit;

	public Action<int, int> m_TabItemClick;

	private List<GameItem> m_ListGoGameItemFrom;

	private List<GameItem> m_ListGoGameItemTo;

	private int m_Remain;

	public void SetData(DefineDataType2 data)
	{
	}

	public void ExchangeButtonClick()
	{
	}
}
