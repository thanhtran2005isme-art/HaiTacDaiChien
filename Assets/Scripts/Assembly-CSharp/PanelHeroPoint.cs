using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroPoint : MonoEventHandler
{
	public Text m_TextPower;

	public Text m_TextLevel;

	public Text m_TextTierCaption;

	public Text m_TextAttack;

	public Text m_TextHp;

	public Text m_TextArmor;

	public Text m_TextSpeed;

	public Image m_ImageClass;

	public Text m_TextClass;

	public Text m_TextGoldUpgradeCost;

	public Text m_TextSpiritUpgradeCost;

	public HorizontalSlotPoint m_SlotTier;

	public HeroSpineInfo m_HeroSpineInfo;

	public GameObject m_GoBoxMaxLevel;

	public GameObject m_GoBoxUpgradeCost;

	public GameObject m_GoBtnUpTier;

	public List<GameObject> m_LstSkillButton;

	public GameObject m_GoBoxSkillDesc;

	private HeroModel m_HeroModel;

	public GameObject m_GoPanelPointDetail;

	public RectTransform m_TransfromGroupButtonSkill;

	public List<GameObject> m_ListGoBtnHoldUpLevel;

	public AutoWarningResource m_WarningSpirit;

	public AutoWarningResource m_WarningGold;

	private UpgradeHeroData upLevelData;

	private bool m_ReceivedHeroUpLevelData;

	protected override void Start()
	{
	}

	private void testData()
	{
	}

	public void ResetPanel()
	{
	}

	private void ResetHoldToUpLevel()
	{
	}

	public void SetHeroModel(HeroModel model)
	{
	}

	public void ButtonPointDetailClick()
	{
	}

	public void ButtonUpLevelClick()
	{
	}

	public void ButtonUpLevelX10Click()
	{
	}

	private void ShowInfoDropPositionIfNeed()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ButtonUpTierClick()
	{
	}

	private void Update()
	{
	}
}
