using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupEditGuild : BasePopup
{
	public InputField m_InputMotto;

	public ButtonGuildAvatar m_ButtonAvatar;

	public Text m_TextGuildName;

	private int m_GuildAvatar;

	public GameObject m_GoButtonRename;

	public Action m_EditSuccessCb;

	protected override void Start()
	{
	}

	public void SetGuildInfo(string guildName, int avatarId, string motto)
	{
	}

	public void ButtonRenameClick()
	{
	}

	public void ButtonSaveClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupEditGuild ShowUp(string guildName, int guildAvatarId, string guildMotto)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
