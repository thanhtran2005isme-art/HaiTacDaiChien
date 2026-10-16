using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellTab3LunarEvent : MonoBehaviour
{
	[SerializeField]
	private Text m_TextButton;

	[SerializeField]
	private Text m_TextLimit;

	[SerializeField]
	private Transform m_GoParentTo;

	[SerializeField]
	private Image m_ImgGem;

	[SerializeField]
	private Text m_IndexGem;

	[SerializeField]
	private Image m_ImgButtonAttack;

	[SerializeField]
	private Text m_TextAttack;

	public Action<int> m_TabItemClickCb;

	private int m_id;

	private int m_Remain;

	private List<GameItem> m_ListGoGameItemTo;

	public void SetData(DefineDataType2 data)
	{
	}

	public void ExchangeButtonClick()
	{
	}
}
