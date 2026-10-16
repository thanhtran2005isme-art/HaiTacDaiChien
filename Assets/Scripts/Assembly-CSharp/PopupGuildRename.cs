using System;
using UnityEngine.UI;

public class PopupGuildRename : BasePopup
{
	public InputField m_InputName;

	public Text m_TextCost;

	public Text m_TextTitle;

	public Action m_RenameSuccessCb;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void SetData(int costRename)
	{
	}

	public void ButtonSaveClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupGuildRename ShowUp(int costRename)
	{
		return null;
	}

	private void Update()
	{
	}
}
