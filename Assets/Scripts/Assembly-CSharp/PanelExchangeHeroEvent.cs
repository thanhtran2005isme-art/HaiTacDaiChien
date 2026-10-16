using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelExchangeHeroEvent : MonoEventHandler
{
	public ScrollRect m_ScrollRect;

	public BoxGameEventInfo m_BoxEventInfo;

	public GameObject m_PreSlotHero;

	public Image m_ImgBgr;

	private GameExchangeHeroEvent m_GameEvent;

	private string m_UrlEventDetail;

	private List<int> listIdHeroSelect;

	public void LoadEventData(string urlEventDetail, GameExchangeHeroEvent gameEvent)
	{
	}

	private void LoadEvent(GameExchangeHeroEvent gameEvent)
	{
	}

	public void ReloadEvent()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
