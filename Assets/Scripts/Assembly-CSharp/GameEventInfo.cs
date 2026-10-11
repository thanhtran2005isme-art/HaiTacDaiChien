using System.Collections.Generic;

public class GameEventInfo
{
	public static int[] LIST_EVENT_BREAK;

	public const int EVENT_TOP_RACING_ITEM = 1;

	public const int EVENT_ACCUMULATED_ITEM = 2;

	public const int EVENT_FIGHT_BOSS = 3;

	public const int EVENT_EXCHANGE_ITEM = 4;

	public const int EVENT_GROWTH_FUND = 5;

	public const int EVENT_LOGIN_7_DAYS = 6;

	public const int EVENT_DISCOUNT_ITEM = 7;

	public const int EVENT_DEPLOANKHANVANG = 8;

	public const int EVENT_SHOP_PACKAGE = 9;

	public const int EVENT_MONTH_CARD = 10;

	public const int EVENT_EXCHANGE_EQUIP = 11;

	public const int EVENT_EXCHANGE_HERO = 12;

	public const int EVENT_BROKEN_SPACE = 13;

	public const int EVENT_CHECK_IN = 106;

	public const int EVENT_ACCUMULATED_ITEM_2 = 102;

	public const int EVENT_EXCHANGE_ITEM_2 = 104;

	public const int EVENT_FIRST_PURCHASE = 602;

	public const int EVENT_ACCUMULATED_PURCHASE = 202;

	public const int EVENT_ONE_PACK = 109;

	public const int EVENT_SPECIAL_CHECKIN = 206;

	public const int EVENT_TRAVEL = 14;

	public const int EVENT_MISSION = 15;

	public const int EVENT_EASTER_PRAY = 16;

	public const int EVENT_DAU_DANH_TRANG = 17;

	public const int EVENT_QUY_TRUONG_THANH_V2 = 114;

	public const int EVENT_GIFT_CODE = 24;

	public const int EVENT_BROKEN_SPACE_NEW = 113;

	public const int EVENT_HERO_LOTTERY = 302;

	public int m_EventId;

	public string m_PathImageIcon;

	public string m_EventName;

	public int m_TemplateId;

	public TimeStruct m_TimeLeftInSec;

	public string m_UrlNext;

	public string m_Group;

	public bool isGrouped;

	public bool enableNotify;

	public List<GameEventInfo> groupEvent;

	public GameEventInfo(int eventId, int templateId, string eventName, long timeLeft, string pathIcon = "", string urlNext = "", string group = "", bool notify = false)
	{
	}
}
