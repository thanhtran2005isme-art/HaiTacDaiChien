using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMFStage : BasePopup
{
	public List<MFHeroHead> m_ListHeroHead;

	public PetHeadUI m_PetHeadUI;

	public Transform m_TransformRewards;

	public Text m_TextTitle;

	public Text m_TextPower;

	public GameObject m_GroupAction;

	public Action m_CloseCb;

	private MFSlotData _mfSlotData;

	protected override void Start()
	{
	}

	public static PopupMFStage ShowUp(MFSlotData mfSlotData, bool showAction)
	{
		return null;
	}

	public void SetStageInfo(MFSlotData mfSlotData, bool showAction)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonBattleClick()
	{
	}

	private void Update()
	{
	}
}
