using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMFPlayerHeroes : BasePopup
{
	public Image m_ImagePotionRevine;

	public Text m_TextNumItemRevine;

	public RectTransform m_TfHeadContainer;

	public GameObject m_GroupRevine;

	public Action m_CloseCb;

	private const int ITEM_KEY_POTION_REVINE = 2049;

	private List<MFHeroInfo> _lstMfHeroInfo;

	private List<MFHeroHead> _listMFHeroHead;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void LoadListHero(List<MFHeroInfo> lstMfHero)
	{
	}

	private void LoadTestData()
	{
	}

	private void HeroHeadClick(int heroIdClick)
	{
	}

	public void ShowEffectRevineRandom(List<long> lstDataLong)
	{
	}

	public void ShowEffectAddSupport(MFHeroInfo mfHeroInfoSupport)
	{
	}

	public void ShowEffectHealing(List<long> lstDataLong)
	{
	}

	public void ShowGroupRevine(bool showRevine)
	{
	}

	public void ButtonRevineClick()
	{
	}

	public void OnMaterialClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupMFPlayerHeroes ShowUp(List<MFHeroInfo> lstMfHero, bool showRevineButton)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
