using System;
using UnityEngine;

public class PetHeadDragDrop : DragDropBehavior
{
	[HideInInspector]
	public int m_SlotIndex;

	[HideInInspector]
	public int m_PetId;

	private Action<PetHeadDragDrop> m_ClickCallback;

	private Action<PetHeadDragDrop, Vector2> m_DropCallback;

	public PetHeadUI m_PetHeadUI;

	public override void Start()
	{
	}

	public void SetPetId(int petId, int petTier = 1)
	{
	}

	public void SetClickCallback(Action<PetHeadDragDrop> callback)
	{
	}

	public void SetDropCallback(Action<PetHeadDragDrop, Vector2> callback)
	{
	}

	public override void OnItemClick()
	{
	}

	public override void OnItemDrop(Vector2 dropPosition)
	{
	}

	private void Update()
	{
	}
}
