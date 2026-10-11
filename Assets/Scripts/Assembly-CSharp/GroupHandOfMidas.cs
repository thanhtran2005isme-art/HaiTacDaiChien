using System;
using UnityEngine;
using UnityEngine.UI;

public class GroupHandOfMidas : MonoBehaviour
{
	public Text m_TextValue;

	public Text m_TextFee;

	public Text m_LabelButton;

	public GameObject m_ButtonReceive;

	public GameObject m_ButtonGray;

	public Action<int> itemClickCB;

	private int indexItem;

	public void SetData(int indexItem, int value = 0, int fee = 0, bool enable = true)
	{
	}

	public void SetEnable(bool enable = true)
	{
	}

	public void ReceiveClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
