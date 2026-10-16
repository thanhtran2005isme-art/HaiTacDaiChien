using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMaterialInput : MonoBehaviour
{
	public delegate List<int> DelegateGetListHeroId();

	[SerializeField]
	private Image m_ImageHeroHead;

	[SerializeField]
	private Image m_ImageHeroHeadBw;

	[SerializeField]
	private Text m_TextNumber;

	[SerializeField]
	private Image m_ImageFaction;

	[SerializeField]
	private Image m_ImageClazz;

	[SerializeField]
	private HeroStars m_HeroStars;

	public GameObject m_GoIconPlus;

	[HideInInspector]
	public bool m_AllowEdit;

	[HideInInspector]
	public List<int> m_LstInsertedMaterialHeroId;

	public DelegateGetListHeroId m_FunctionGetListHeroIdNeedFilter;

	public Action m_HeroMaterialPickDoneCb;

	private ActionRepeatTimer m_ActionCheckFillIn;

	private HeroMaterialRequirement _matRequirement;

	private void Start()
	{
	}

	private void CheckToShowIconPlus()
	{
	}

	private void Update()
	{
	}

	public void ClearFillData()
	{
	}

	public void SetHeroRequirement(HeroMaterialRequirement req)
	{
	}

	public void OnClick()
	{
	}

	public bool IsHeroFillInMetTheRequirement()
	{
		return false;
	}

	public void OnListHeroesSelected(List<HeroModel> lstSelectedHeroModel)
	{
	}

	public void SetListHeroIdMaterial(List<int> lstHeroId)
	{
	}

	public void SetHeroToActiveHeadImage(int heroKey)
	{
	}

	private void SetNumberFillIn(int numFillIn, int numRequire)
	{
	}
}
