using System.Collections.Generic;
using UnityEngine;

public class LunarNewYearPopup : BasePopup
{
	[SerializeField]
	private List<GameObject> m_GoListTab;

	[SerializeField]
	private List<GameObject> m_GoListContent;

	[SerializeField]
	private List<SpineObject> m_ListSpine;

	private int m_IndexTab;

	private const string URL_LUNAR_EVENT = "/events/type/9/serverId/{serverId}/userId/{userId}";

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void RequestData()
	{
	}

	private void DisableTab()
	{
	}

	public void BtnBackHome()
	{
	}

	public void ShowTab(int indexTab)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private List<DataTabLunarEvent> ParseListRankType(string json)
	{
		return null;
	}

	public static void ShowUp()
	{
	}
}
