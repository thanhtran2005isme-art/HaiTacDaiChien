using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelFormationTest : MonoBehaviour
{
	public List<GameObject> m_LstSlot;

	private List<GameObject> m_LstHeroHead;

	private int m_MaxHero;

	[HideInInspector]
	public bool m_AllowRemoveHero;

	public Transform m_TransformContainer;

	private Action<int> m_HeroRemoveCallback;

	private void Start()
	{
	}

	public void SetMaxHero(int max)
	{
	}

	public int CountHeroInFormation()
	{
		return 0;
	}

	public void SetHero(HeroModel heroModel, int slotIndex)
	{
	}

	public HeroesFormation GetHeroesFormation()
	{
		return null;
	}

	public void OnHeroDropClick(HeroHeadDragDrop headBeDragScript, Vector2 dropPosition)
	{
	}

	private GameObject GetHeroHeadAtSlot(int slotIdx)
	{
		return null;
	}

	public void OnHeroHeadClick(HeroHeadDragDrop headScript)
	{
	}

	public void removeHero(int heroIdClick)
	{
	}

	public void SetHeroRemoveCallback(Action<int> callback)
	{
	}

	public Vector2 GetSlotPosition(int slotIndex)
	{
		return default;
	}

	public int GetFreeSlot()
	{
		return 0;
	}

	private void Update()
	{
	}
}
