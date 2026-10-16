using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupFormation3 : MonoBehaviour
{
	public PanelHeroList m_PanelHeroList;

	public PanelFormation m_PanelFormation;

	public PanelFormationPet m_PanelFormationPet;

	public GameObject m_GoPanelHeroes;

	public GameObject m_GoPanelTop;

	private List<int> m_LstHeroIdChecked;

	public GameObject m_GoPointHideHeroes;

	public GameObject m_GoPointHideTop;

	public Text m_TextTitle;

	public Text m_TextAction;

	public GameObject m_GoButtonClose;

	public GameObject m_GoButtonBack;

	public Text m_TextPower;

	public Action<HeroesFormation> m_ButtonActionClickCb;

	public Action m_ButtonBackClickCb;

	[HideInInspector]
	public bool m_ClosePopupAfterActionClick;

	private List<HeroModel> m_LstAllHeroModel;

	private int m_MinHero;

	private void Start()
	{
	}

	public void CalculatePower()
	{
	}

	public void SetTitle(string title)
	{
	}

	public void SetPanelData(int maxHero, int minHero, bool allowClose = true)
	{
	}

	public void ShowBackButton(Action backClickCb)
	{
	}

	public void LoadData()
	{
	}

	public void HeroRemoveFromFormation(int heroId)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void OnHeroHeadClick(HeroModel model)
	{
	}

	public void SetActionCallback(string titleAction, Action<HeroesFormation> cb)
	{
	}

	public void ButtonActionClick()
	{
	}

	public void InitPanel()
	{
	}

	public void SetHeroFormation(HeroesFormation heroFormation)
	{
	}

	public static PopupFormation3 ShowUp(List<HeroModel> lstHeroModel, HeroesFormation heroesFormation = null)
	{
		return null;
	}
}
