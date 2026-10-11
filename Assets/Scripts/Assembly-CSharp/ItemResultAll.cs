using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemResultAll : MonoBehaviour
{
	[SerializeField]
	private Image m_HeroHead;

	[SerializeField]
	private Slider m_Slider;

	[SerializeField]
	private Text m_TextValue;

	[SerializeField]
	private Image m_ImageFill;

	public Action<int, int> heroHeadCliclCB;

	private int HeroId;

	private int HeroKey;

	private string m_InfoTurlHero;

	public void SetData(DataItemResultAll data)
	{
	}

	public void ButtonHeroHeadClick()
	{
	}

	public void ButtonInfoClick()
	{
	}
}
