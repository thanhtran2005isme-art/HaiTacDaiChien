using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelHeroBreakthrough : MonoEventHandler
{
	public RectTransform m_TransformGroupMaterial;

	public HeroStars m_StarCurrent;

	public HeroStars m_StarNext;

	public List<HeroMaterialSlot> m_LstHeroMaterialSlot;

	public HeroModel m_HeroModel;

	public static PanelHeroBreakthrough Instance;

	public Text m_TextLevelCap;

	public Text m_TextSkill;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void OnEnable()
	{
	}

	public void SetShowNameTag(bool showNameTag)
	{
	}

	private List<int> GetListHeroIdFillInSlots(int slotIndexNotCountIn)
	{
		return null;
	}

	public void SetHeroModel(HeroModel model)
	{
	}

	public void AutoFillHeroToAllSlot()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public List<object> DeleteMaterialHeroFromInventory()
	{
		return null;
	}

	public void ButtonBreakthroughClick()
	{
	}
}
