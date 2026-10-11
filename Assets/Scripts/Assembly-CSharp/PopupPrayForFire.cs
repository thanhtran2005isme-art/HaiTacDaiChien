using UnityEngine;

public class PopupPrayForFire : BasePopup, ITabListener
{
	public const int TAB_BUTTON_BATTLE = 1;

	public const int TAB_BUTTON_TOP_DMG = 2;

	public TabManager m_TabManager;

	public GameObject m_PanelStart;

	public GameObject m_PanelTopDmg;

	public static PopupPrayForFire instance;

	protected override void Start()
	{
	}

	public static void ShowUp()
	{
	}

	private void Update()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}
}
