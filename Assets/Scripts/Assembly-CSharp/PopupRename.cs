using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupRename : BasePopup
{
	public InputField m_InputName;

	public Text m_TextCost;

	public Text m_TextTitle;

	public GameObject m_ButtonClose;

	public GameObject m_GoButtonSaveFree;

	public GameObject m_GoButtonSave;

	public Action<string> m_RenameSuccessCb;

	protected override void Start()
	{
	}

	public void ButtonGenNameClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void SetData(int costRename, bool allowClose)
	{
	}

	public void ButtonSaveClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupRename ShowUp(int costRename, bool allowClose = true)
	{
		return null;
	}

	private void Update()
	{
	}
}
