using System.Collections.Generic;
using UnityEngine.UI;

public class PopupGuildPromote : BasePopup
{
	public PlayerAvatar m_Avatar;

	public Text m_TextRole;

	public Text m_TextPlayerName;

	public List<Text> m_LstTextNewRole;

	private GuildMemberInfo m_MemberInfo;

	private List<GuildRole> m_LstGuildRoleNew;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void SetData(GuildMemberInfo memberInfo)
	{
	}

	public void ButtonRoleNew1Click()
	{
	}

	public void ButtonRoleNew2Click()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupGuildPromote ShowUp(GuildMemberInfo memberInfo)
	{
		return null;
	}

	private void Update()
	{
	}
}
