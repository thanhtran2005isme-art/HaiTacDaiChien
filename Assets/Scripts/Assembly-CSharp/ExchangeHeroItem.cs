using System.Collections.Generic;

public class ExchangeHeroItem
{
	public int m_ItemId;

	public List<long> m_ListItemFrom;

	public List<long> m_ListHeroFrom;

	public List<long> m_ListToItem;

	public int m_HeroId;

	public string m_LabelAction;

	public string m_UrlAction;

	public ExchangeHeroItem(int itemId, int heroId, string labelAction, string urlAction, List<long> lstHeroFrom, List<long> lstItemFrom, List<long> lstToItem)
	{
	}
}
