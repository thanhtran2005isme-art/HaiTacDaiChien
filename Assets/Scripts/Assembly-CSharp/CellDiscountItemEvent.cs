using UnityEngine;
using UnityEngine.UI;

public class CellDiscountItemEvent : MonoBehaviour
{
	public Text m_TextRequireLvl;

	public Text m_TextRequireVIP;

	public Text m_TextDiscountPercent;

	public Text m_TextBasePrice;

	public Text m_TextPrice;

	public Text m_TextAction;

	public Image iconPrice;

	public Image iconFullPrice;

	public GameObject m_GoButtonAction;

	public Transform m_TfRewards;

	public ScrollRect m_ScrollRect;

	private DiscountItemReward m_Reward;

	private OnPublicDataCallback cellCB;

	private int m_EventId;

	public void ShowData(int eventId, DiscountItemReward eventReward)
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
