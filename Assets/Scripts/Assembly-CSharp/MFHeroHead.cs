using System;
using UnityEngine;
using UnityEngine.UI;

public class MFHeroHead : MonoBehaviour
{
	public HeroHeadUI m_HeroHeadUI;

	public Slider m_SliderHp;

	public SpineObject m_SpineEff;

	private ValueUpdater _valueUpdateHp;

	public Action<int> m_ClickCb;

	public MFHeroInfo m_MfHeroInfo;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SetHero(MFHeroInfo mfHeroInfo)
	{
	}

	public bool IsContainHeroId(int heroId)
	{
		return false;
	}

	public void PlayEffectHealing(float newHpPercent)
	{
	}

	public void PlayEffectAddSupport()
	{
	}

	public void PlayEffectRevine(float newHpPercent)
	{
	}

	public void SetNullHero()
	{
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
