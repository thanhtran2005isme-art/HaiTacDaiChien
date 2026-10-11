using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupHeroBriefInfo : BasePopup
{
	public Text m_TextPower;

	public Text m_TextTierCaption;

	public Text m_TextAttack;

	public Text m_TextHp;

	public Text m_TextArmor;

	public Text m_TextSpeed;

	public HorizontalSlotPoint m_SlotTier;

	public List<GameObject> m_LstSkillButton;

	public GameObject m_GoBoxSkillDesc;

	private HeroModel m_HeroModel;

	public GameObject m_GoPanelPointDetail;

	public RectTransform m_TransfromGroupButtonSkill;

	public HeroHeadUI m_HeroHeadUI;

	public Text m_TextHeroName;

	public PanelHeroPoint m_PanelHeroPoint;

	protected override void Start()
	{
	}

	public void SetHeroModel(HeroModel model)
	{
	}

	public void ButtonPointDetailClick()
	{
	}

	public static PopupHeroBriefInfo ShowUp(HeroModel hm)
	{
		return null;
	}

	private void Update()
	{
	}
}
