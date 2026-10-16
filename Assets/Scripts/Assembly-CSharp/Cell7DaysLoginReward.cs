using UnityEngine;
using UnityEngine.UI;

public class Cell7DaysLoginReward : MonoBehaviour
{
	public Text m_TextTitle;

	public Text m_TextStatus;

	public Text m_TextAction;

	public GameObject m_GoButtonAction;

	public Transform m_TfRewards;

	public ScrollRect m_ScrollRect;

	private LoginReward m_Reward;

	private int m_EventId;

	private OnPublicDataCallback cellCB;

	public void ShowData(int eventId, LoginReward eventReward)
	{
	}

	public void SetActionCallback(OnPublicDataCallback cb)
	{
	}

	public void ButtonActionClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
