using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraveTrialScene : BaseScene
{
	public List<GameObject> m_LstGoStage;

	public List<GameObject> m_LstGroupPath;

	public Animator m_AnimatorChest;

	[HideInInspector]
	public List<HeroModel> m_LstBraveHeroModel;

	public GameObject m_GoButtonChest;

	public SpineObject m_SpineArrowFocus;

	public BoxHideAtFirst m_BoxHideCountDown;

	public Text m_TextCountDown;

	private int m_TimeCountDown;

	private ActionRepeatTimer m_ActionCountDown;

	private bool m_HasReward;

	private int m_CurrentStageIdx;

	public static BraveTrialScene Instance;

	protected override void Start()
	{
	}

	public void ButtonShopClick()
	{
	}

	private void ShowBoxCountDown()
	{
	}

	public void ButtonChestClick()
	{
	}

	public void HelpClick()
	{
	}

	private void LoadStages(List<BraveTrialStageInfo> lstStageInfo)
	{
	}

	public void ButtonBackClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
