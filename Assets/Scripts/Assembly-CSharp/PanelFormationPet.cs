using System.Collections.Generic;
using UnityEngine;

public class PanelFormationPet : MonoBehaviour
{
	public List<GameObject> m_LstPetSlot;

	private List<GameObject> m_LstGoPetHead;

	private Transform m_Transform;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void OnSlotClick(int slotIndex)
	{
	}

	public int GetPetIdBySlot(int slotIndex)
	{
		return 0;
	}

	private Vector2 GetSlotPosition(int slotIndex)
	{
		return default;
	}

	public void SetPetToSlot(int petId, int petTier, int slotIndex)
	{
	}

	private GameObject GetPetHeadAtSlot(int slotIdx)
	{
		return null;
	}

	public void OnPetDrop(PetHeadDragDrop headPetDragScript, Vector2 dropPosition)
	{
	}

	private void SwitchPetSlot(int slotIndex1, int slotIndex2)
	{
	}

	public void MovePet1Down()
	{
	}

	public void MovePet2Down()
	{
	}

	public void MovePet2Up()
	{
	}

	public void MovePet3Up()
	{
	}

	private void Update()
	{
	}
}
