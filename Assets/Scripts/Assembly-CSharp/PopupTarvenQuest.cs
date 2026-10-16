using System.Collections.Generic;
using UnityEngine;

public class PopupTarvenQuest : BasePopup
{
	public RectTransform m_TransformBody;

	public Transform m_TransformGroupSlot;

	public Transform m_TransformGroupRequirement;

	public GameItem m_GameItemUI;

	public GameObject m_GoGroupAction;

	public TarvenQuestStar m_QuestStar;

	public TextCountdown m_TextCountdown;

	private TarvenQuestInfo m_QuestInfo;

	private List<GameObject> m_LstGoSlotQuest;

	private List<GameObject> m_LstGoRequirement;

	protected override void Start()
	{
	}

	public void ButtonHelpClick()
	{
	}

	public void SetQuestInfo(int timeCountDown, TarvenQuestInfo questInfo)
	{
	}

	public void ButtonCloseClick()
	{
	}

	private void OnQuestSlotClick(SlotHeroQuest slot)
	{
	}

	private void CheckHeroSatisfiyRequirement()
	{
	}

	public static PopupTarvenQuest ShowUp(int timeCountDownInSec, TarvenQuestInfo questInfo)
	{
		return null;
	}

	public void ButtonRemoveHeroClick()
	{
	}

	public void ButtonAutoFillClick()
	{
	}

	public void ButtonStartQuestClick()
	{
	}

	private void Update()
	{
	}
}
