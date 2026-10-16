using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEchangeHero : MonoBehaviour
{
	public Text m_Number;

	public Image m_AvatarHero;

	public Image m_ImgFaction;

	public Image m_ImgClass;

	public GameObject m_GroupStar1;

	public GameObject m_GroupStar6;

	public GameObject m_GroupStar10;

	public List<GameObject> m_LstStar1;

	public List<GameObject> m_LstStar6;

	public Action<List<HeroModel>, List<int>, int> itemClickCB;

	private int m_NumberReq;

	private int m_StarReq;

	private List<HeroModel> listHeroFillter;

	private List<HeroModel> listHeroSelected;

	public void FillDataReq(List<long> info)
	{
	}

	public void SetHero(List<HeroModel> lstHero)
	{
	}

	public void SetStar(int heroStar)
	{
	}

	public List<int> GetListIdHeroSelect()
	{
		return null;
	}

	public void ItemClick()
	{
	}
}
