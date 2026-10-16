using UnityEngine;
using UnityEngine.UI;

public class PopupSettings : BasePopup, ITabListener
{
	private const int TAB_INFO = 0;

	private const int TAB_FEEDBACK = 1;

	private const int TAB_HELP = 2;

	public TabManager m_TabManager;

	public Text m_Title;

	public GameObject m_GoPanelInfo;

	public GameObject m_GoPanelFeedback;

	public GameObject m_GoPanelHelp;

	private ActionRepeatTimer m_TimerCheckNotify;

	protected override void Start()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public static void ShowUp()
	{
	}

	private void Update()
	{
	}
}
