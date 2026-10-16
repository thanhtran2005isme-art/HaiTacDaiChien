using System.Collections.Generic;
using UnityEngine;

public class PanelHeroEquipment : MonoBehaviour
{
	public List<HeroSlotEquipment> m_LstSlotEquipment;

	public HeroSlotArtifact m_SlotArtifact;

	public HeroSlotStone m_SlotStone;

	private HeroModel m_HeroModel;

	private int m_CurrentHeroId;

	private const int HERO_LEVEL_UNLOCK_STONE = 40;

	private void Start()
	{
	}

	public void DisableSlotEquipment()
	{
	}

	public void ButtonQuickEquipClick()
	{
	}

	public void ButtonUnequipClick()
	{
	}

	public void OnSlotEquipmentClick(HeroSlotEquipment slotEquipment)
	{
	}

	public void OnSlotArtifactClick(HeroSlotArtifact slotArtifact)
	{
	}

	public void LoadHeroItemsAndStone(HeroModel heroModel)
	{
	}

	public HeroSlotEquipment GetSlotEquipment(EquipmentType type)
	{
		return null;
	}

	public void SlotArtifactClick()
	{
	}

	private void Update()
	{
	}
}
