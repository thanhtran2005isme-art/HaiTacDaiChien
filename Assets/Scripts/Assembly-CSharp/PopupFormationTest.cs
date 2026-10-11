using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupFormationTest : MonoBehaviour
{
	public PanelHeroList m_PanelHeroList;

	public PanelFormationTest m_PanelFormation;

	public GameObject m_GoPanelHeroes;

	public GameObject m_GoPanelTop;

	private List<int> m_LstHeroIdChecked;

	public GameObject m_GoPointHideHeroes;

	public GameObject m_GoPointHideTop;

	public InputField mSearch;

	public Action<HeroesFormation> m_ButtonActionClickCb;

	private List<HeroModel> m_LstAllHeroModel;

	public int cloneID;

	public PopupHeroStat popupHero;

	public PopupRelics popupRelics;

	public List<BattleHeroTest> lstHeroBattle;

	public Dropdown mDropPet;

	public int petId;

	private static readonly string[] VietnameseSigns;

	private void Start()
	{
	}

	public void changePet()
	{
	}

	public void LoadData()
	{
	}

	public void HeroRemoveFromFormation(int heroId)
	{
	}

	public void RemoveHero(int heroId)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void searchHero()
	{
	}

	public void searchHero(string heroName)
	{
	}

	public static string RemoveSign4VietnameseString(string str)
	{
		return null;
	}

	public void OnHeroHeadClick(HeroModel model)
	{
	}

	public void addHeroToFormation(int freeSlotIdx, BattleHeroTest mHeroTest)
	{
	}

	public void SetActionCallback(string titleAction, Action<HeroesFormation> cb)
	{
	}

	public void ButtonActionClick()
	{
	}
}
