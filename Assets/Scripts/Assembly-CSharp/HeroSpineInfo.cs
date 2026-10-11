using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSpineInfo : MonoBehaviour
{
	public GameObject m_PrefabPointFlyUp;

	public GameObject m_PrefabPointFlyDown;

	public SpineObject m_EffectHeroUp;

	public SpineObject m_EffectHeroAwake;

	public Text m_TextHeroName;

	public HeroStars m_HeroStars;

	public SpineObject m_SpineRank;

	public SpineObject m_SpineFaction;

	public Image m_ImageHero;

	public GameObject m_GoPointSlideInfo;

	private const int POINT_SLIDE_SPACING = 25;

	private void Start()
	{
	}

	public void ShowPointSlideUp(List<string> lstStrPoint)
	{
	}

	public void ShowPointSlideDown(List<string> lstStrPoint)
	{
	}

	public void ShowEffectHeroLevelUp()
	{
	}

	public void ShowEffectHeroAwake()
	{
	}

	public void OnHeroClick()
	{
	}

	public void LoadSpineHero(HeroModel hm)
	{
	}

	private void Update()
	{
	}
}
