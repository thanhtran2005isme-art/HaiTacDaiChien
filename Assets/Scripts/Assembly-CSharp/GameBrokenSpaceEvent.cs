using System.Collections.Generic;

public class GameBrokenSpaceEvent : GameEventData
{
	public int maxItem;

	public string exchangeUrl;

	public string desc2;

	public int exchangedItems;

	public List<ItemBrokenSpace> listMission;

	public BonusModel m_TypeItemExchange;

	public int m_RequireLevel;
}
