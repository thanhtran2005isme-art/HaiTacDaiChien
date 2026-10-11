using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupSummonHeroes : MonoBehaviour
{
	public Text m_TextHeroName;

	public HeroStars m_HeroStars;

	public SpineObject m_SpineRank;

	public SpineObject m_SpineFaction;

	public Image m_ImageHero;

	public Image m_ImageHero1;

	public Action m_CloseClickCb;

	private const int POINT_SLIDE_SPACING = 25;

	private string m_CurrentPathSpine;

	private HeroModel hm;

	private float tmpTime;

	private void Start()
	{
	}

	public void OnButtonClose()
	{
	}

	public void InitUI()
	{
	}

	public void LoadSpineHero(HeroModel hm)
	{
	}

	private void Update()
	{
	}

	public static PopupSummonHeroes ShowUp(HeroModel hm)
	{
		return null;
	}
}
