using UnityEngine.UI;

public class ReplaceHeroScene : BaseScene, ITabListener
{
	public BoxReplaceHero m_BoxReplaceHero;

	public BoxRegressHero m_BoxRegressHero;

	public TabManager m_TabManager;

	public Text m_TextTitle;

	private const int BUTTON_TAB_REPLACE = 1;

	private const int BUTTON_TAB_REGRESS = 2;

	private int m_CurrentTabActive;

	protected override void Start()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void ButtonHelpClick()
	{
	}

	private void Update()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}
}
