using System;
using UnityEngine.UI;

public class PopupShareBattle : BasePopup
{
	private const int POS_SHARE_WORLD = 0;

	private const int POS_SHARE_GUILD = 1;

	public Text m_TextCharLeft;

	public InputField m_InputText;

	public Dropdown m_DropDown;

	public Action<string, int> m_ActionCb;

	private int m_MaxCharAllow;

	private int m_PosShare;

	protected override void Start()
	{
	}

	public void ButtonActionClick()
	{
	}

	public void OnValueDropDownChanged()
	{
	}

	public void OnTextChanged()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupShareBattle ShowUp()
	{
		return null;
	}
}
