using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellRewardAccumulatedEvent : MonoBehaviour
{
	public Text m_TextTitle;

	public Text m_TextPercent;

	public Text m_TextTitleLimit;

	public Transform m_TfRewards;

	public ScrollRect m_ScrollRect;

	public Slider m_Slider;

	public GameObject m_ButtonReceive;

	public GameObject m_TextReceived;

	private AccumulatedReward m_Reward;

	private OnPublicDataCallback cellCB;

	private int m_EventId;

	private bool isComplete;

	private BonusModel bonus;

	private List<object> lstObjModel;

	private GameObject prefabGameItem;

	private void Awake()
	{
	}

	public void ShowData(int eventId, AccumulatedReward eventReward)
	{
	}

	public void SetActionCallback(OnPublicDataCallback cb)
	{
	}

	public void ButtonActionClick()
	{
	}

	private void Update()
	{
	}
}
