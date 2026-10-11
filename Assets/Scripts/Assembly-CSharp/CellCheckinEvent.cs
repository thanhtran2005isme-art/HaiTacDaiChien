using UnityEngine;
using UnityEngine.UI;

public class CellCheckinEvent : MonoBehaviour
{
	public Text m_TextTimeCountdown;

	public GameItem m_GameItem;

	public GameObject m_Tick;

	public GameObject m_ImgCover;

	public GameObject m_Flowing;

	private CheckinReward checkinData;

	private int m_EventId;

	private TimeStruct m_TimeCountdown;

	private ScrollRect m_Scrollview;

	private float heightViewport;

	private float heightElement;

	public void ShowData(int eventId, CheckinReward eventReward, ScrollRect scrollview)
	{
	}

	private void SetTimeCountdown(TimeStruct timeCountdown)
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
