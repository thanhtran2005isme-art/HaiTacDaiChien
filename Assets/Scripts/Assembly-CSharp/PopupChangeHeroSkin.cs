using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class PopupChangeHeroSkin : BasePopup
{
	public DotIndicator m_DotInicator;

	public Text m_TextSkinName;

	public Text m_TextPoints;

	public Image m_ImageHead;

	public GameObject m_GoButtonNext;

	public GameObject m_GoButtonPrev;

	public GameObject m_GoButtonUse;

	public GameObject m_GoButtonUseDisable;

	public SkeletonGraphic m_SkeletonGraphic;

	public Action m_CloseCb;

	private HeroModel _heroModel;

	private int _currentDotIdx;

	private int _totalDot;

	protected override void Start()
	{
	}

	private void SetHero(HeroModel heroModel)
	{
	}

	private void CheckButtonArrow()
	{
	}

	public void ButtonNextClick()
	{
	}

	public void ButtonPrevClick()
	{
	}

	private void LoadCurrentSkin()
	{
	}

	private void LoadDefaultHero()
	{
	}

	private void LoadHeroSkin(HeroSkin heroSkin)
	{
	}

	public void ButtonUseClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupChangeHeroSkin ShowUp(HeroModel heroModel)
	{
		return null;
	}

	private void Update()
	{
	}
}
