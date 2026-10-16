using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupMFHeroHeadEffect : BasePopup
{
	public RectTransform m_TfHeadContainer;

	public Action m_CloseCb;

	public const int EFFECT_MF_REVINNE = 1;

	protected override void Awake()
	{
	}

	public void ShowEffectData(List<MFHeroInfo> lstMfHero, List<long> lstHeroData, int effectType)
	{
	}

	private void DestroyPopup()
	{
	}

	public void ButtonRevineClick()
	{
	}

	public static PopupMFHeroHeadEffect ShowUp(List<MFHeroInfo> lstAllMfHero, List<long> lstHeroData, int effectType = 0)
	{
		return null;
	}

	private void Update()
	{
	}
}
