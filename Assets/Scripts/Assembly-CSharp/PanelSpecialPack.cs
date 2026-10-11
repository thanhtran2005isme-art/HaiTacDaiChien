using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSpecialPack : MonoEventHandler
{
	public Image m_Bgr;

	public Text m_TitleLeft;

	public Text m_TitleRight;

	public List<Text> m_TxtVIPBonus;

	public List<Text> m_TxtLimit;

	public List<Text> m_TxtCost;

	public List<Transform> m_TfRewards;

	public List<ScrollRect> m_ScrollRect;

	private bool m_InitPool;

	private GamePackValueEvent m_GameEvent;

	private float timeRefresh;

	private bool stopUpdateBonus;

	public void LoadEventData(GamePackValueEvent gameEvent, bool isRefresh = false)
	{
	}

	public void ButtonActionClick(int index)
	{
	}

	public void StartUpdateBonus()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
