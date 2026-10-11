using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using pbdson;

public class PanelHeroInfo : MonoEventHandler, ITabListener
{
	private const int TAB_HERO_EQUIPMENT = 6;

	private const int TAB_HERO_POINT = 1;

	private const int TAB_HERO_AWAKEN = 2;

	private const int TAB_HERO_BREAKTHOUGH = 3;

	private const int TAB_HERO_ENABLE = 4;

	private const int TAB_HERO_E_SKILL = 5;

	private const int TAB_SPECIAL_EQUIP = 7;

	public Image m_ImageBgr;

	public GameObject m_GoPanelEquipment;

	public GameObject m_GoPanelSpecialEquip;

	public GameObject m_GoPanelPoint;

	public GameObject m_GoPanelAwaken;

	public GameObject m_GoPanelBreakthrough;

	public GameObject m_GoPanelEnable;

	public GameObject m_GoPanelESkill;

	public PanelHeroList m_PanelHeroList;

	public HeroSpineInfo m_HeroSpineInfo;

	public PanelHeroPoint m_PanelHeroPoint;

	public PanelHeroAwaken m_PanelHeroAwaken;

	public PanelHeroBreakthrough m_PanelHeroBreakthrough;

	public PanelHeroEnable m_PanelHeroEnable;

	public PanelHeroESkill m_PanelHeroESkill;

	public PanelHeroEquipment m_PanelHeroEquipment;

	public PanelSpecialEquip m_PanelSpecialEqui;

	public GameObject m_GoRightPart;

	public TabManager m_Tab;

	private bool m_HasAwakenHero;

	private HeroModel m_CurrentHeroModelClone;

	private List<HeroModel> m_LstHeroModel;

	[HideInInspector]
	public bool m_DestroyWhenClickBack;

	[HideInInspector]
	public Action<bool> m_ClickBackCallback;

	public GameObject m_GoBoxShare;

	public GameObject m_GoButtonNoLock;

	public GameObject m_GoButtonLocked;

	public GameObject m_GoButtonSkin;

	public GameObject m_GoButtonNext;

	public GameObject m_GoButtonPrev;

	public static PanelHeroInfo Instance;

	public Button m_BtnHeroInfor;

	public Button m_BtnHeroEquip;

	public Button m_BtnHeroLevelUp;

	public Button m_BtnHeroesQuickEquip;

	public Button m_BtnBack;

	public Button m_BtnHeroRankUp;

	private Dictionary<HeroFaction, Sprite> m_DickSpriteBgr;

	public HeroListNewScene mScene;

	private HeroModel currentHeroModel;

	private int m_CurrentTabActive;

	protected override void Start()
	{
	}

	private void Update()
	{
	}

	public void ButtonNextClick()
	{
	}

	public void CheckButtonArrowVisibility()
	{
	}

	public void ButtonPrevClick()
	{
	}

	public void ButtonSkinClick()
	{
	}

	public void ButtonShareClick()
	{
	}

	public void ButtonLockedClick()
	{
	}

	public void ButtonNoLockClick()
	{
	}

	public void ButtonShareToGuildClick()
	{
	}

	public void ButtonShareToWorldClick()
	{
	}

	public void BoxShareBackgroundClick()
	{
	}

	public override void OnDestroy()
	{
	}

	public void SetListHeroModel(List<HeroModel> lstHeroModel)
	{
	}

	private void OnHeroHeadClick(HeroModel model)
	{
	}

	private void CheckForPointChange(HeroModel hm1, PbHero pbHeroNew)
	{
	}

	private void AddPointChangeToList(int pointDiff, string attributeName, List<string> lstPointUp, List<string> lstPointDown, bool appendPecent = false)
	{
	}

	public void LoadHeroModel(HeroModel model, bool loadSpine = true)
	{
	}

	public void ButtonBagClick()
	{
	}

	public void OnButtonBackClick()
	{
	}

	private void HideAllTabContent()
	{
	}

	private void ShowPanelHeroPoint()
	{
	}

	private void ShowPanelHeroAwaken()
	{
	}

	private void ShowPanelBreakThrough()
	{
	}

	private void ShowPanelESkill()
	{
	}

	private void ShowPanelEnable()
	{
	}

	private void ShowPanelEquipment()
	{
	}

	private void ShowPanelSpecialEquip()
	{
	}

	public static PanelHeroInfo ShowUp(HeroModel heroModel, List<HeroModel> lstHeroModel = null)
	{
		return null;
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void ReloadHeroInfo()
	{
	}

	private void ParseDataEpicEquip(ListCommonVector lstCommonVector)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
