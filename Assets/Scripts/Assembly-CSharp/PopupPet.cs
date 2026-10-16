using System;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class PopupPet : BasePopup, ITabListener
{
	public PanelPetLevel m_PanelLevel;

	public PanelPetBuff m_PanelBuff;

	public PanelPetRuin m_PanelRuin;

	public GameObject m_GoBoxSpine;

	public GameObject m_GoBoxInfo;

	public SkeletonGraphic m_SkeletonGraphic;

	public PetModel m_PetModel;

	public GameObject m_GoPointHideBoxSpine;

	public GameObject m_GoPointHideBoxInfo;

	public Image m_ImageBgrPet;

	public TabManager m_TabManager;

	public Text m_TextPetName;

	public SpineObject m_SpinePetUpTier;

	private const int TAB_BUTTON_LEVEL = 1;

	private const int TAB_BUTTON_BUFF = 2;

	private const int TAB_BUTTON_RUIN = 3;

	public Action<int> m_PopupCloseCb;

	protected override void Start()
	{
	}

	public void SetPetModel(PetModel petModel)
	{
	}

	private void LoadPetSpine(int petId, int petTier)
	{
	}

	public void ButtonRebirthClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public static PopupPet ShowUp(PetModel petModel)
	{
		return null;
	}

	private void Update()
	{
	}
}
