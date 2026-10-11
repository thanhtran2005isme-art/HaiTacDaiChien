using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroHome2 : MonoEventHandler
{
	public GameObject m_GroupHero;

	public CanvasHero m_CanvasHero;

	public HeroStars m_HeroStar;

	public Image m_ImageFaction;

	public Text m_TextLevel;

	public Text m_TextHeroName;

	public HeroModel m_HeroModel;

	public Action<HeroModel> m_HeroHomeClickCb;

	public void LoadHero(HeroModel heroModel)
	{
	}

	public void SetHeroScaleHome(float scale)
	{
	}

	public void HideHero()
	{
	}
}
