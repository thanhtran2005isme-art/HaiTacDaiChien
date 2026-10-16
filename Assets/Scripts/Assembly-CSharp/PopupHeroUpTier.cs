using UnityEngine;
using UnityEngine.UI;

public class PopupHeroUpTier : BasePopup
{
	public RectTransform m_TransformBoxInfo;

	public GameObject m_GoBoxSkill;

	public Text m_TextLevelFrom;

	public Text m_TextLevelTo;

	public Text m_TextAtkFrom;

	public Text m_TextAtkTo;

	public Text m_TextHpFrom;

	public Text m_TextHpTo;

	public Text m_TextArmorFrom;

	public Text m_TextArmorTo;

	public Text m_TextGoldCost;

	public Text m_TextPromotionCost;

	public ButtonSkillInfo m_ButtonSkillUnlock;

	private HeroModel m_HeroModel;

	private UpgradeHeroData upLevelDataRequire;

	public AutoWarningResource m_WarningGold;

	public AutoWarningResource m_WarningPromotionStone;

	protected override void Start()
	{
	}

	public void LoadUpgradeInfo(HeroModel hm)
	{
	}

	public void ButtonUpgradeClick()
	{
	}

	public void ShowInfoDropPosition()
	{
	}

	public static PopupHeroUpTier ShowUp(HeroModel hm)
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
