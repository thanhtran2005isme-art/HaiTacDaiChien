using UnityEngine;
using UnityEngine.UI;

public class PanelHome2Quest : MonoEventHandler
{
	public const int SV_QUEST_STATUS = 261;

	private const int SV_QUEST_GET_REWARD = 262;

	public GameObject m_GoContent;

	public GameItem m_GameItemReward;

	public Text m_TextTitle;

	public Text m_TextDesc;

	public Text m_TextProgress;

	public Home2Scene homeScene;

	public SpineObject m_EffectCompleted;

	private HomeQuest m_CurrentQuest;

	private long m_Status;

	private const int levelQuestTutorial = 10;

	protected override void Start()
	{
	}

	public void RequestQuestStatus()
	{
	}

	public void ButtonCollectClick()
	{
	}

	public void ButtonGoToClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
