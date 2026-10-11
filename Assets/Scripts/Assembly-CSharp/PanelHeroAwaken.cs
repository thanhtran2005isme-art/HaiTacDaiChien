using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroAwaken : MonoEventHandler
{
	public RectTransform m_TransformGroupMaterial;

	public HeroStars m_StarCurrent;

	public HeroStars m_StarNext;

	public ButtonSkillInfo m_ButtonSkill1;

	public ButtonSkillInfo m_ButtonSkill2;

	public List<HeroMaterialSlot> m_LstHeroMaterialSlot;

	public Text m_TextPromotionStoneCost;

	public GameObject m_GoBoxSkillDesc;

	public HeroModel m_HeroModel;

	public static PanelHeroAwaken Instance;

	public Text m_TextPowerUp;

	public Text m_TextLevelCap;

	public AutoWarningResource m_WarningPromotionStone;

	private HeroModel m_HeroModelBeforeAwaken;

	public GameObject m_GoBoxAwaken;

	public GameObject m_GoBoxMaxStar;

	protected override void Start()
	{
	}

	private void OnEnable()
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

	private void LoadAwakenBox()
	{
	}

	public void AutoFillHeroToAllSlot()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ButtonAwakenClick()
	{
	}

	public List<object> DeleteMaterialHeroFromInventory()
	{
		return null;
	}

	public void ShowAwakenResultIfNeed(HeroModel heroModelNew)
	{
	}
}
