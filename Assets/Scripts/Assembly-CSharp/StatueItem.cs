using System.Collections.Generic;

public class StatueItem
{
	public int m_ItemId;

	public List<long> m_ListItemRandom;

	public List<long> m_CurrReward;

	public List<long> m_ExchangeCost;

	public string m_Name;

	public string m_PathStatue;

	public string m_LimitDesc;

	public string m_UrlActive;

	public string m_UrlRefresh;

	public string m_UrlExchange;

	public StatueItem(int itemId, string name, string pathStatue, List<long> lstItemRand, List<long> currItem, string urlActive, string urlRefresh, string urlExchange, List<long> exchangeCost, string limitDesc)
	{
	}
}
