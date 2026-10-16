using System;
using UnityEngine;
using UnityEngine.UI;

public class CellHeroThumb : MonoBehaviour
{
	private HeroModel m_HeroModel;

	public Text m_TextLevel;

	public Text m_TextName;

	public Image m_ImageFaction;

	public Image m_ImageQuality;

	public Image m_ImageClass;

	public HeroStars m_HeroStar;

	public Image m_ImageThumb;

	public Image m_ImageTitle;

	public Image m_ImgCheck;

	public Image m_ImageFameLeft;

	public Image m_ImageFameRight;

	private const int MODE_HERO_DICTIONARY = 2;

	private int m_ModeList;

	public Action<HeroModel> m_ClickCb;

	public void SetHeroModel(HeroModel hm, int mode)
	{
	}

	public void SetClickCallback(Action<HeroModel> cb)
	{
	}

	public void OnClick()
	{
	}
}
