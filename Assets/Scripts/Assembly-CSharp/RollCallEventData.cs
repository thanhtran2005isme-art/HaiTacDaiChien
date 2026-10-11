using System.Collections.Generic;

public class RollCallEventData : SpEventDataBase
{
	public List<RollCallItem> m_ListRollCallItem;

	public string m_ActionPurchaseTitle;

	public bool m_IapEnable;

	public int m_VipExpGet;

	public string m_IapProductId;

	public string m_IapData;

	public RollCallEventData(string eventName, string eventDesc, string pathBanner)
		: base(null, null, null)
	{
	}
}
