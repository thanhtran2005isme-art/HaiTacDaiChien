using System;
using UnityEngine;

public class HeroHeadDragDrop : DragDropBehavior
{
	[HideInInspector]
	public int m_SlotIndex;

	[HideInInspector]
	public HeroModel m_HeroModel;

	private Action<HeroHeadDragDrop> m_ClickCallback;

	private Action<HeroHeadDragDrop, Vector2> m_DropCallback;

	public HeroHeadUI m_HeroHeadUI;

	public override void Start()
	{
	}

	public void SetHeroModel(HeroModel model, bool showHpBar = false)
	{
	}

	public void SetClickCallback(Action<HeroHeadDragDrop> callback)
	{
	}

	public void SetDropCallback(Action<HeroHeadDragDrop, Vector2> callback)
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
