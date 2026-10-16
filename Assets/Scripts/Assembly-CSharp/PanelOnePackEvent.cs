using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOnePackEvent : MonoEventHandler
{
	public Text m_TxtLimit;

	public Text m_TxtLabel;

	public Transform m_TfRewards;

	private PackItem m_Reward;

	public Image m_ImageBgr;

	public ScrollRect m_ScrollRect;

	private int m_EventId;

	private string productId;

	private GamePackValueEvent m_GameEvent;

	private float timeRefresh;

	private bool stopUpdateBonus;

	private List<GameObject> listGoReward;

	public void LoadEventData(GamePackValueEvent gameEvent, bool isRefresh = false)
	{
	}

	public void StartUpdateBonus()
	{
	}

	public void CloseClick()
	{
	}

	public void ItemBuy()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
