using System.Collections.Generic;

public class ExchangeGemItem
{
	public int m_ItemId;

	public List<long> m_ListItemFrom;

	public List<long> m_ListToItem;

	public int m_quantity;

	public string m_PathIconFeat;

	public string m_UrlAction;

	public int m_Remain;

	public string m_SumDesc;

	public ExchangeGemItem(int itemId, int quantity, int remain, List<long> listItemFrom, List<long> listItemTo, string sumDesc, string pathIconFeat, string url)
	{
	}
}
