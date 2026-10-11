using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelAddGroup : MonoBehaviour
{
	[SerializeField]
	private Text m_TextListId;

	[SerializeField]
	private InputField m_TextID;

	[SerializeField]
	private InputField m_TextDesc;

	public Action ButtonCloseAddGrCB;

	private List<long> listId;

	private void OnEnable()
	{
	}

	public void ButtonHelpClicl()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonAddIdClick()
	{
	}

	public void ButtonAddGroupClick()
	{
	}

	public void ButtonDelIdClick()
	{
	}

	private void LoadListId()
	{
	}
}
