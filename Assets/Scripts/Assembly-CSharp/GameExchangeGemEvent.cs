using System.Collections.Generic;

public class GameExchangeGemEvent : GameEventData
{
	public int maxItem;

	public string exchangeUrl;

	public string desc2;

	public int exchangedItems;

	public string strCost;

	public List<ExchangeGemItem> m_ListReward;

	public BonusModel m_TypeItemExchange;

	public static GameExchangeGemEvent GenTestData(int eventId)
	{
		return null;
	}
}
