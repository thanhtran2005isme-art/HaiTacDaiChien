using UnityEngine;
using UnityEngine.UI;

public class PopupHandOfMidas : MonoEventHandler
{
	private const int GROUP_FREE = 0;

	private const int GROUP_GEM_0 = 1;

	private const int GROUP_GEM_1 = 2;

	public GroupHandOfMidas[] m_GroupHandOfMidas;

	public Text m_TextTime;

	public GameObject m_TimeCountdown;

	private TimeStruct timeCountdown;

	public GameObject m_GoButtonClose;

	public GameObject m_GoPointHandClose;

	public GameObject m_GoPointHandCollect;

	private bool m_OnTutorial;

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

	public void ButtonCloseClick()
	{
	}

	public void ReceiveClick(int Index)
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static void ShowUp()
	{
	}
}
