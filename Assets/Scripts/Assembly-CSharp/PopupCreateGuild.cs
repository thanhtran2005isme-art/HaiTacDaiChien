using UnityEngine;
using UnityEngine.UI;

public class PopupCreateGuild : BasePopup, ITabListener
{
	private const int TAB_BUTTON_CREATE = 1;

	private const int TAB_BUTTON_SEARCH = 2;

	public TabManager m_TabMng;

	public GameObject m_GoBoxSearch;

	public GameObject m_GoBoxCreate;

	public InputField m_InputCreate;

	public InputField m_InputSearch;

	public ButtonGuildAvatar m_ButtonAvatar;

	public Text m_TextCostCreateGuild;

	public GameObject m_PrefabCellGuild;

	public GameObject m_GoScrollViewSearch;

	private int m_LastGuildIdApply;

	private int m_GuildAvatar;

	public VerticalPoolGroup m_PoolGuild;

	protected override void Start()
	{
	}

	private void ApplyGuildCallback(int guildIdApply)
	{
	}

	public void SearchForRandomGuild()
	{
	}

	public void SearchForSpecificGuild(int guildId)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void ButtonCreateClick()
	{
	}

	public void ButtonSearchClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupCreateGuild ShowUp()
	{
		return null;
	}

	public static PopupCreateGuild ShowUp(int guildIdSearch)
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
