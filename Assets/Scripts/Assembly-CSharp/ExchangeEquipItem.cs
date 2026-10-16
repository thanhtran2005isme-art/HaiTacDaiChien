using System.Collections.Generic;

public class ExchangeEquipItem
{
	public int m_ItemId;

	public List<long> m_ListItemFrom;

	public List<long> m_ListToItem;

	public int m_Quantity;

	public string m_SumDesc;

	public string m_LabelAction;

	public string m_UrlAction;

	public ExchangeEquipItem(int itemId, int quantity, string sumDesc, string labelAction, string urlAction, List<long> lstItemFrom, List<long> lstToItem)
	{
	}
}
