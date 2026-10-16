using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroEnable : MonoEventHandler
{
	private HeroModel m_HeroModel;

	public HeroStars m_StarNext;

	public List<HeroMaterialSlot> m_LstHeroMaterialSlot;

	public Text m_TextHpUp;

	public Text m_TextLevelMax;

	public Text m_TextAttackUp;

	public GameObject m_GroupHeroMaterial;

	public GameObject m_GroupPromoStone;

	public GameObject m_GroupBlockEnable;

	public GameObject m_GroupAction;

	public GameObject m_GroupActionBlockEnable;

	public GameObject m_GoBoxEnable;

	public GameObject m_GoBoxMaxStar;

	public GameObject m_GoBoxBeyondMaxStar;

	public RectTransform m_TransformGroupMaterial;

	public Image m_ImageSkillEnable;

	public GameObject m_GoTextMaterial;

	public Text m_TextPromotionStoneCost;

	public AutoWarningResource m_WarningPromotionStone;

	public TouchAndReleaseHandler m_TouchReleaseSkill;

	public List<GameObject> m_ListGoRegressionButton;

	public GameObject m_GoBoxSkillDesc;

	protected override void Start()
	{
	}

	private List<int> GetListHeroIdFillInSlots(int slotIndexNotCountIn)
	{
		return null;
	}

	public void SetHeroModel(HeroModel model)
	{
	}

	private void LoadMaxStarBox()
	{
	}

	private void LoadBeyondMaxStarBox()
	{
	}

	private void LoadEnableBox()
	{
	}

	public void AutoFillHeroToAllSlot()
	{
	}

	public List<object> DeleteMaterialHeroFromInventory()
	{
		return null;
	}

	public void ButtonRegressClick()
	{
	}

	public void ButtonBeyondMaxSkillClick()
	{
	}

	public void ButtonAwakenClick()
	{
	}
}
