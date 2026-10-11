using System.Collections.Generic;

public class GameDiscountItemEvent : GameEventData
{
	public List<DiscountItemReward> m_LstRewardData;

	public BonusModel m_TypeItemExchange;

	public static GameDiscountItemEvent GenTestData(int eventId)
	{
		return null;
	}
}
