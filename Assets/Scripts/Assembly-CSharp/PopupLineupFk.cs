using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupLineupFk : MonoBehaviour
{
	public HorizontalPoolGroup m_PoolGrid;

	public GameObject m_GoScrollView;

	public Action<List<int>> m_LineupSelectedCb;

	public List<HeroSlotFk> m_ListSlotFk;

	private List<DummyHeroModel> m_LstHeroFkModel;

	private List<int> m_ListHeroIdPicked;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void OnHeroSelected(DummyHeroModel heroModel)
	{
	}

	private void AddHeroLineup(int heroSpineId)
	{
	}

	private void RemoveHeroLineup(int heroSpineId)
	{
	}

	public void ButtonBattleClick()
	{
	}

	private HeroSlotFk GetFreeSlot()
	{
		return null;
	}

	public void ClosePopup()
	{
	}

	public static PopupLineupFk ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
