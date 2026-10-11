using System.Collections.Generic;
using UnityEngine.Events;

public class PanelCenterTab3LunarEvent : MonoEventHandler
{
	public VerticalPoolGroup m_PoolGroup;

	private List<DefineDataType2> listItem;

	public UnityEvent EventRequestData;

	private bool CheckReLoad;

	public void ShowList(List<DefineDataType2> listItem)
	{
	}

	private void OnEnable()
	{
	}

	public void OnTabItemClick(int id)
	{
	}

	private void SendReQuest(string urlNext, int eventID)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void ParseDataExchange(string json)
	{
	}
}
