using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupTextInput : BasePopup
{
	public Text m_TextTitle;

	public Text m_TextAction;

	public InputField m_InputText;

	[HideInInspector]
	public int m_MinLength;

	private Action<string> m_ActionCb;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void SetActionCallback(string title, string actionName, Action<string> cb)
	{
	}

	public void ButtonActionClick()
	{
	}

	public static PopupTextInput ShowUp(int minLength = -1)
	{
		return null;
	}

	private void Update()
	{
	}
}
