using System;
using UnityEngine;
using UnityEngine.UI;

public class CellGrowthFundEvent : MonoBehaviour
{
	public Text m_TextTitle;

	public Text m_TextStatus;

	public Text m_TextPercent;

	public Transform m_TfRewards;

	public ScrollRect m_ScrollRect;

	public Slider m_Slider;

	[SerializeField]
	private GameObject m_ButtonSelect;

	private GrowthFundReward m_Reward;

	private int m_EventId;

	private string iAPID;

	public Action m_CellButtonAction;

	public static int QUY_TRUONG_THANH_2_GET_REWARD;

	public void ShowData(int eventId, GrowthFundReward eventReward, string qButtonLabel = "", string IapID = "", int TabActive = 0, string qUrlNext = "")
	{
	}

	public void ButtonActionClick()
	{
	}
}
