using System.Collections.Generic;

public class MissionItem
{
	public int m_ItemId;

	public int m_EventId;

	public List<long> m_ListReward;

	public string m_Name;

	public string m_Desc;

	public string m_LabelAction;

	public string m_UrlAction;

	public MissionItem(int itemId, int eventId, string name, string desc, string labelAction, string urlAction, List<long> lstReward)
	{
	}
}
