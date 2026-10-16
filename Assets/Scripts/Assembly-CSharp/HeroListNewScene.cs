using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroListNewScene : BaseScene, ITabListener
{
	public GridPoolGroup m_PoolGrid;

	public GameObject m_GoScrollView;

	public GameObject m_GoMiscInfo;

	public Text m_TextHeroCapacity;

	public Text m_TextTitle;

	public PanelHeroMenuSort m_PanelMenuSort;

	public TabManager m_TabFaction;

	private const int MODE_HERO_INVENTORY = 1;

	private const int MODE_HERO_DICTIONARY = 2;

	private int m_ModeList;

	private const int TAB_BUTTON_FORTRESS = 1;

	private const int TAB_BUTTON_ABYSS = 2;

	private const int TAB_BUTTON_FOREST = 3;

	private const int TAB_BUTTON_SHADOW = 4;

	private const int TAB_BUTTON_DARK = 5;

	private const int TAB_BUTTON_LIGHT = 6;

	private bool m_SortByLevel;

	private int m_CurrentTabIdFaction;

	private List<HeroModel> m_ListHeroInventory;

	private List<HeroModel> m_ListHeroDictionary;

	private List<HeroModel> m_ListHeroDisplay;

	private List<HeroModel> m_ListHeroFilter;

	public GameObject m_GoButtonDictionary;

	public GameObject m_GoPrefabCell;

	private PanelHeroInfo m_PanelHeroInfo;

	public Button m_BtnBack;

	public GameObject mHand;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
		base.Start();
		// AssetRipper restored an empty Start. Attach the new offline UI
		// presenter here as a deterministic fallback when entering Play Mode
		// directly in HeroListNewScene (Editor domain reload / sceneLoaded differences).
		if (GetComponent<OfflineHeroListPresenter>() == null)
			gameObject.AddComponent<OfflineHeroListPresenter>();
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	private void LoadHeroInventory()
	{
	}

	private void LoadHeroDictionary()
	{
	}

	public void ButtonDictionaryClick()
	{
	}

	public void ButtonHelpClick()
	{
	}

	public void ButtonBuyHeroCapacityClick()
	{
	}

	public void ButtonSortClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void SortByLevel()
	{
	}

	public void SortByStar()
	{
	}

	public void ButtonBagClick()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	private void ReloadListHero()
	{
	}

	public void OnHeroHeadClick(HeroModel heroModel)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
