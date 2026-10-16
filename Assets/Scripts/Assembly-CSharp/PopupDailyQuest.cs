using UnityEngine;
using UnityEngine.UI;

public class PopupDailyQuest : BasePopup
{
	public Text m_TextTitle;

	public Text m_TextCountDown;

	public VerticalPoolGroup m_PoolGroup;

	public CellDailyQuest m_QuestSumUp;

	private ActionRepeatTimer m_ActionCountDown;

	private int m_TimeRefresh;

	private int numberQuestComplete;

	public GameObject m_GoButtonClose;

	public ScrollRect m_ScrollRect;

	private bool m_OnTutorial;

	private int m_CurrentTutorialId;

	public GameObject m_GoPointHandQuestCampaign;

	public static PopupDailyQuest Instance;

	private const int QUEST_ID_COLLECT_CAMPAIGN = 9;

	private const int QUEST_ID_GET_FREE_GOLD = 3;

	protected override void Start()
	{
	}

	private void LoadCurrentTutorial()
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

	private void MakeThisTutorialCompleted()
	{
	}

	public void ButtonCloseClicked()
	{
	}

	public void LoadData()
	{
	}

	public static void ShowUp()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
