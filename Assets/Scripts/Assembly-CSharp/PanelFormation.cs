using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelFormation : MonoBehaviour
{
	public List<GameObject> m_LstSlot;

	private List<GameObject> m_LstHeroHead;

	[HideInInspector]
	public bool m_AllowRemoveHero;

	public Transform m_TransformContainer;

	private Action<int> m_HeroRemoveCallback;

	[HideInInspector]
	public bool m_ShowHeroHp;

	[HideInInspector]
	public bool m_HasSwapHeroPosition;

	[HideInInspector]
	public bool m_OnTutorial;

	private void Start()
	{
	}

	public int CountHeroInFormation()
	{
		return 0;
	}

	public void SetHero(HeroModel heroModel, int slotIndex)
	{
	}

	public void DisableHeroDragDrop()
	{
	}

	public int GetNumberHero()
	{
		return 0;
	}

	public bool HasHeroOnAllLineUp()
	{
		return false;
	}

	public HeroesFormation GetHeroesFormation()
	{
		return null;
	}

	public long GetFormationPower()
	{
		return 0L;
	}

	public List<HeroFaction> GetListHeroFactionFull()
	{
		return null;
	}

	public List<HeroFaction> GetListHeroFaction()
	{
		return null;
	}

	public void MoveFormation1Down()
	{
	}

	public void MoveFormation2Down()
	{
	}

	public void MoveFormation2Up()
	{
	}

	public void MoveFormation3Up()
	{
	}

	private void SwitchHeroHeadSlot(int slotIndex1, int slotIndex2)
	{
	}

	public void OnHeroDrop(HeroHeadDragDrop headBeDragScript, Vector2 dropPosition)
	{
	}

	private GameObject GetHeroHeadAtSlot(int slotIdx)
	{
		return null;
	}

	public void OnHeroHeadClick(HeroHeadDragDrop headScript)
	{
	}

	public bool RemoveHeroFromFormation(int heroId)
	{
		return false;
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
