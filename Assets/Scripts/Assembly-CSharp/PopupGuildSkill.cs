using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGuildSkill : BasePopup, ITabListener
{
	private const int TAB_BUTTON_WARRIOR = 1;

	private const int TAB_BUTTON_MAGE = 2;

	private const int TAB_BUTTON_RANGER = 3;

	private const int TAB_BUTTON_ASSASSIN = 4;

	private const int TAB_BUTTON_PRIEST = 5;

	private List<ClassTechSkill> m_LstClassTechSkill;

	private List<ClassTechSkill> m_LstClassTechSkillNewLevel;

	private List<string> m_LstWarriorTechName;

	private List<string> m_LstMageTechName;

	private List<string> m_LstRangerTechName;

	private List<string> m_LstAssassinTechName;

	private List<string> m_LstPriestTechName;

	private List<string> m_LstWarriorNewTechName;

	private List<string> m_LstMageNewTechName;

	private List<string> m_LstRangerNewTechName;

	private List<string> m_LstAssassinNewTechName;

	private List<string> m_LstPriestNewTechName;

	public Text m_TextTitle;

	public TabManager m_TabManager;

	public List<GameObject> m_LstTechButton;

	public List<bool> lstCheckMaxLevel;

	public Text m_TxtLevelTech;

	public Image m_ImageTech;

	public Text m_TextTechName;

	public Text m_TextUpgradeDesc;

	public Text m_TextUpgradeRequire;

	public Text m_TextGoldCost;

	public Text m_TextGuildCoinCost;

	public GameObject m_GoButtonUpgrade;

	public GameObject m_GoGroupUpgrade;

	public GameObject m_GoGroupMaxed;

	public GameObject m_GoBoxTech;

	public GameObject m_GoBoxDetail;

	public Image m_IconTechClass;

	public SpineObject m_SpineTechUp;

	public SpineObject m_SpineEffectTech;

	public GameObject m_GoSelection;

	private TechSkillInfo m_CurrentTechInfo;

	private ClassTechSkill m_CurrentClassTech;

	private string m_CurrentPathTechOn;

	private string m_CurrentPathTechOff;

	private List<int> m_ListHeroIdNeedUpdateInfo;

	private List<int> m_ListCostGemToReset;

	public AutoWarningResource m_WarningGold;

	public AutoWarningResource m_WarningGuildCoin;

	public static bool checkGoToNextLevelGuild;

	public Action m_CloseClickCb;

	private int m_CurrentTabButton;

	protected override void Start()
	{
	}

	public override void ClosePopup()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	private void InitTestData()
	{
	}

	public void ButtonHelpClick()
	{
	}

	private void LoadIconForTechButton(int tabButtonId)
	{
	}

	private void LoadTechGroup(ClassTechSkill classTech)
	{
	}

	private void LoadListTechInfoToCircleTech(ClassTechSkill classTech)
	{
	}

	private void ShowTechDetail(TechSkillInfo tech, string pathOn, string pathOff)
	{
	}

	public static PopupGuildSkill ShowUp()
	{
		return null;
	}

	public void ButtonResetClick()
	{
	}

	public void ButtonUpgardeTechClick()
	{
	}

	private void GetListHeroIdOfCurrentClassToUpdate()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void GoToNextLevelGuildSkills()
	{
	}

	private void LoadIconForTechButtonNextLevel(int tabButtonId)
	{
	}
}
