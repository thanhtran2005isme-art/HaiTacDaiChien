using System.Collections.Generic;

public class DataMiniGameEvent
{
	public ItemConsumable itemConsumableCost;

	public string rightImage;

	public string leftImage;

	public string help;

	public string desc2;

	public string ExchangeUrl;

	public int m_Id;

	public int m_EventID;

	public int maxItem;

	public int exchangerNumber;

	public List<object> lstObjectRoll;

	public BonusModel m_TypeItemExchange;

	public ItemConsumable iconTurn;

	public string spinUrl;

	public string totalTurn;

	public string dailyDesc;

	public List<long> lstDailyReward;

	public List<LevelRewardMiniGameData> lstLevelReward;
}
