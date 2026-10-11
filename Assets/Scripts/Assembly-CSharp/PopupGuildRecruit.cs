using System;
using UnityEngine.UI;

public class PopupGuildRecruit : BasePopup
{
	public Text m_TextTitle;

	public Text m_TextCharLeft;

	public InputField m_InputText;

	private int m_MinLength;

	private int m_MaxCharAllow;

	private Action<string> m_ActionCb;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonActionClick()
	{
	}

	public void OnTextChanged()
	{
	}

	public static PopupGuildRecruit ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
