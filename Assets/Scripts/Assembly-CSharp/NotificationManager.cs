using System.Collections.Generic;

public class NotificationManager
{
	private static NotificationManager m_Instance;

	public const int ID_NOTIFY_TAVERN = 1;

	public const int ID_NOTIFY_DUNGEON = 2;

	public const int ID_NOTIFY_TRAINNING = 3;

	public const int ID_NOTIFY_SUMMON = 4;

	public const int ID_NOTIFY_MAIL = 5;

	public const int ID_NOTIFY_QUEST = 6;

	public const int ID_NOTIFY_CHALLENGES = 7;

	public const int ID_NOTIFY_HAND_OF_MIDAS = 8;

	public const int ID_NOTIFY_FRIEND = 14;

	public const int ID_NOTIFY_CHAT = 15;

	public const int ID_NOTIFY_BOSS_GUILD_FREE = 16;

	public const int ID_NOTIFY_CLAN_CHAT = 17;

	public const int ID_NOTIFY_RECRUIT_CHAT = 18;

	public const int ID_NOTIFY_DON_DAU = 19;

	public const int ID_NOTIFY_LIEN_DAU = 20;

	public const int ID_NOTIFY_CHAT_PRIVATE_SERVER = 22;

	public const int ID_NOTIFY_CHAT_ADMIN = 21;

	public const int ID_NOTIFY_MASTERING_HEROES = 23;

	public const int ID_NOTIFY_GUILD_ORDER_COMPLETED = 9;

	public const int ID_NOTIFY_GUILD_ROLL_CALL = 10;

	public const int ID_NOTIFY_GUILD_WAR = 11;

	public const int ID_NOTIFY_GUILD_APPLICATION_WAIT_APPROVAL = 12;

	public const int ID_NOTIFY_CHECK_IN = 13;

	public const int ID_NOTIFY_CROSS_WORD = 24;

	public const int ID_NOTIFY_MARINEFORD = 26;

	public bool m_CanFuseAny5StarHero;

	private List<int> m_ListNotifyId;

	public static NotificationManager Instance => null;

	public void SetListNotifyId(List<int> lstId)
	{
	}

	public bool ContainsNotifyId(int notifyId)
	{
		return false;
	}

	public void EnableNotifyId(int notifyId)
	{
	}

	public void RemoveNotifyId(int notifyId)
	{
	}

	public void PrintListNotification()
	{
	}

	public void Reset()
	{
	}
}
