using System.Collections.Generic;
using UnityEngine.Events;

public class PanelCenterTab2LunarEvent : MonoEventHandler
{
	public VerticalPoolGroup m_PoolGroup;

	public UnityEvent EventRequestData;

	private List<DefineDataType2> listItem;

	private bool CheckReLoad;

	public void ShowList(List<DefineDataType2> listItem)
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

	private void OnEnable()
	{
	}
}
