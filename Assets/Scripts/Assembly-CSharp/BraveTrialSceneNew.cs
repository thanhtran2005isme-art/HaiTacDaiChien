using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraveTrialSceneNew : BaseScene, IG2UIEventHandler
{
	public Image bgrBraveTrial;

	public GameObject landSpaceDayMode;

	public GameObject bgrTrial;

	public GameObject spineTrial;

	public GameObject spineLoadNightMode;

	public List<GameObject> m_LstExtraReward;

	public List<GameObject> m_LstGoStage;

	public List<GameObject> m_LstGroupPath;

	public Animator m_AnimatorChest;

	[HideInInspector]
	public List<HeroModel> m_LstBraveHeroModel;

	public List<HeroModel> m_LstBraveHeroModelFullHp;

	public List<HeroModel> m_LstBraveHeroModelHealing;

	public List<HeroModel> m_LstBraveHeroModelRiveval;

	public GameObject m_GoButtonChest;

	public SpineObject m_SpineArrowFocus;

	public BoxHideAtFirst m_BoxHideCountDown;

	public Text m_TextCountDown;

	public Text m_TextStatus;

	private int m_TimeCountDown;

	private ActionRepeatTimer m_ActionCountDown;

	private bool m_HasReward;

	private int m_CurrentStageIdx;

	private int stageActive;

	private bool checkStageNightMod;

	private static bool isNightMode;

	private int statge;

	private bool endBraveTrialNightMode;

	private static BraveTrialSceneNew instance;

	private List<DataChest> lstDataChest;

	private BadgeInfo badgeInfo;

	public static BraveTrialSceneNew Instance
	{
		get
		{
			return null;
		}
		private set
		{
		}
	}

	private BraveTrialSceneNew()
	{
	}

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public override void OnDestroy()
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

	public void ButtonAlstarOfTrialClick()
	{
	}

	public void HelpClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	private void LoadStages(List<BraveTrialStageInfoNew> lstStageInfo)
	{
	}

	public void LoadScape(bool checkNightMode)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public List<HeroModel> SortListHeroByHP(List<HeroModel> m_LstHeroModelFullHp, List<HeroModel> m_LstHeroModelHealing, List<HeroModel> m_LstHeroModelRiveval)
	{
		return null;
	}

	private void Update()
	{
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
