using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMaterialSlot : MonoBehaviour
{
	public delegate List<int> DelegateGetListHeroId(int currentSlotIdx);

	[SerializeField]
	public Image m_ImageHeroHead;

	[SerializeField]
	public Image m_ImageHeroHeadBw;

	[SerializeField]
	public Text m_TextHeroKey;

	[SerializeField]
	public Text m_TextNumber;

	[SerializeField]
	public Image m_ImageFaction;

	[SerializeField]
	public HeroStars m_HeroStars;

	[HideInInspector]
	public int m_NumHeroRequire;

	[HideInInspector]
	public int m_HeroKeyRequire;

	[HideInInspector]
	public int m_SlotIndex;

	public GameObject m_GoNameTag;

	public Text m_TextHeroName;

	public GameObject m_GoIconPlus;

	[HideInInspector]
	public bool m_ShowNameTag;

	[HideInInspector]
	public bool m_AllowEdit;

	[HideInInspector]
	public List<int> m_LstMaterialHeroId;

	public DelegateGetListHeroId m_FunctionGetListHeroIdNeedFilter;

	public Action m_HeroMaterialPickDoneCb;

	public HeroModel m_HeroModelRequire;

	private ActionRepeatTimer m_ActionCheckFillIn;

	private void Start()
	{
	}

	private void CheckToShowIconPlus()
	{
	}

	private void Update()
	{
	}

	public void SetFusingSlotData(FusingHeroSlotData slotData)
	{
	}

	public void ClearSlot()
	{
	}

	public void ClearFillData()
	{
	}

	private void SetHeroMaterial(HeroModel hm, int numberRequire)
	{
	}

	private void SetHeroMaterial(int heroStar, HeroFaction faction, int numberRequire)
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
