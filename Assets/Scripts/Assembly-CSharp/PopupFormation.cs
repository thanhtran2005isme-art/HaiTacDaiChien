using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupFormation : MonoBehaviour, IG2UIEventHandler
{
	public AuraNew m_AuraNew;

	public PanelHeroList m_PanelHeroList;

	public PanelFormation m_PanelFormation;

	public GameObject m_GoPanelHeroes;

	public GameObject m_GoPanelTop;

	private List<int> m_LstHeroIdChecked;

	public GameObject m_GoPointHideHeroes;

	public GameObject m_GoPointHideTop;

	public Text m_TextAction;

	public Text m_TextTitle;

	public Text m_TextPower;

	public GameObject m_GoButtonClose;

	public GameObject m_GoButtonBack;

	public GameObject m_GoButtonShowFilter;

	public GameObject m_GoButtonAction;

	public RectTransform m_TransformContentAura;

	public BoxAuraDetail m_BoxAuraDetail;

	public ScrollRect m_ScrollAura;

	public PetHeadUI m_PetHeadUI;

	public Toggle m_ToggleSkip;

	public Action<HeroesFormation> m_ButtonActionClickCb;

	private Action m_ButtonBackClickCb;

	private Action m_ButtonCloseClickCb;

	public Action m_LoadDataDoneCB;

	[HideInInspector]
	public bool m_ClosePopupAfterActionClick;

	private bool m_ShowHeroHp;

	private List<HeroModel> m_LstAllHeroModel;

	private List<GameObject> m_LstGoButtonAura;

	private int m_MinHero;

	private bool m_OnTutorial;

	private ActionRepeatTimer m_ActionTutorialState;

	public GameObject m_GoPointHandPickHero;

	public GameObject m_GoPointHandSwapHero;

	public GameObject m_GoPointHandBattle;

	private GameObject m_GoHandPickHero;

	private GameObject m_GoHandSwapHero;

	private GameObject m_GoHandBattle;

	private const int TUT_STATE_WAIT = 1;

	private const int TUT_STATE_CHECK_HERO = 2;

	private const int TUT_STATE_SWAP_HERO = 3;

	private const int TUT_STATE_READY_BATTLE = 4;

	private int m_TutState;

	private const int CURRENT_AURA = 2;

	private const int AURA_1 = 1;

	private const int AURa_2 = 2;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
	}

	private void InitListAura()
	{
	}

	private void LoadCurrentTutorial()
	{
	}

	private void CheckTutorialStateLineUp1()
	{
	}

	private void CheckTutorialStateLineUp234()
	{
	}

	private void TurnOffAllUI()
	{
	}

	private void SetUpUIForTutorial(int tutId)
	{
	}

	private void TutorialTextCompleted(int tutId)
	{
	}

	private void CheckForAuraActive()
	{
	}

	public void SetPanelData(int maxHero, int minHero, bool allowClose = true)
	{
	}

	public void ButtonSlotPetClick()
	{
	}

	public void ShowBackButton(Action backClickCb)
	{
	}

	public void SetCloseClickCb(Action closeClickCb)
	{
	}

	public void SetTitle(string title)
	{
	}

	public void LoadData()
	{
	}

	public void HeroRemoveFromFormation(int heroId)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void OnHeroHeadClick(HeroModel model)
	{
	}

	public void SetActionCallback(string titleAction, Action<HeroesFormation> cb)
	{
	}

	public void ButtonActionClick()
	{
	}

	public void InitPanel()
	{
	}

	public void SetHeroFormation(HeroesFormation heroFormation)
	{
	}

	public void ShowCheckSkip(bool showIt)
	{
	}

	public void SetCheckSkip(bool checkSkip)
	{
	}

	private void CheckAuraNew()
	{
	}

	public static PopupFormation ShowUp(List<HeroModel> lstHeroModel, HeroesFormation heroesFormation = null, bool showHeroHp = false)
	{
		return null;
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
