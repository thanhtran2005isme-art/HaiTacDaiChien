using UnityEngine;
using UnityEngine.UI;

public class PopupGameChallenge : BasePopup
{
	public Text m_TextTitle;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_GoButtonClose;

	public ScrollRect m_ScrollRect;

	private const int QUEST_ID_DAI_KIEU = 23;

	public GameObject m_GoPointHandCollect;

	public GameObject m_GoPointHandClose;

	private bool m_OnTutorial;

	private bool m_ProcessStatusReturnOnce;

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

	private void Update()
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
}
