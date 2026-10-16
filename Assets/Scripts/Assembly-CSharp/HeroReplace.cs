using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroReplace : MonoBehaviour
{
	public CanvasHero m_CanvasHero;

	public Image m_ImageFaction;

	public Text m_TextHeroName;

	public HeroStars m_HeroStars;

	public GameObject m_GoButtonDetail;

	public Text m_TextLevel;

	public RectTransform m_TransformFaction;

	public Action m_ClickCallback;

	private HeroModel m_HeroModel;

	private void Start()
	{
	}

	public void SetHeroModel(HeroModel hm)
	{
	}

	public void SetReplacementInfo(HeroFaction faction, int heroStar, int level)
	{
	}

	public void SetEmpty()
	{
	}

	public void ButtonDetailClick()
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
