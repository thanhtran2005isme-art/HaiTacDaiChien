using UnityEngine;
using UnityEngine.UI;

public class PopupGuildMill : BasePopup, ITabListener
{
	private const int TAB_BUTTON_QUEST = 1;

	private const int TAB_BUTTON_DONATE = 2;

	private const int TAB_BUTTON_TOP = 3;

	public Text m_TextTitle;

	public TabManager m_TabManager;

	public GameObject m_GoBoxQuest;

	public GameObject m_GoBoxDonate;

	public GameObject m_GoBoxTop;

	protected override void Start()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public static void ShowUp()
	{
	}

	public void ButtonCloseClick()
	{
	}

	private void Update()
	{
	}
}
