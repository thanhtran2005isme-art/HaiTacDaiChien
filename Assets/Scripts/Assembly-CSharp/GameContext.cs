using System.Collections.Generic;

public class GameContext
{
	private static GameContext m_Instance;

	public string m_GosuUsername;

	public string m_IdServer;

	public string m_Username;

	public string m_ScreenName;

	public string m_Session;

	public string m_UsernameToLink;

	public string m_Notice;

	public int m_UserId;

	public int m_PlayerLevel;

	public int m_PlayerAvatarId;

	public int m_TypeAvatar;

	public int m_GuildId;

	public string m_GuildName;

	public GuildRole m_GuildRole;

	public int m_GuildFlagId;

	public int m_LevelVIP;

	public string m_Udid;

	public float m_ExpVIP;

	public bool m_FirstTimes;

	public bool m_SoundOn;

	public bool m_MusicOn;

	public string m_UrlBase;

	public long m_Gold;

	public long m_Gem;

	public long m_Spirit;

	public long m_PromotionStone;

	public long m_MagicDust;

	public long m_ArenaTicket;

	public long m_StoneOfVoid;

	public long m_GuildCoin;

	public long m_SoulStoneShard;

	public long m_DragonScale;

	public long m_ProphetBlessing;

	public long m_Chip;

	public long m_MonsterSoul;

	public long m_ChaosStone;

	public long m_SuperChip;

	public long m_BasicSummonScroll;

	public long m_HeroicSummonScroll;

	public long m_ProphetOrb;

	public long m_Heart;

	public long m_BasicQuestScroll;

	public long m_SeniorQuestScroll;

	public long m_ChallengeBadge;

	public long m_LuckCoin;

	public long m_ArenaCoin;

	public long m_BraveCoin;

	public long m_BirdFeather;

	public long m_AllStarsCoin;

	public long m_SealBookNormal;

	public long m_SealBookDL;

	public long m_DaNangDo;

	public long m_MarinefordCoin;

	public long m_Exp;

	public int feeBrokenSpace;

	public bool isGetAllFriend;

	public bool isGetAllHero;

	public bool isShowNotice;

	public bool isGetAllAvatar;

	public int m_LoginType;

	public DataSender dataSender;

	public BaseScene m_CurrentScene;

	public ServerInfo m_ServerGame;

	public UserFriendModel m_UserFriend;

	public CelestialModel m_CelestialIsland;

	private Dictionary<int, DataCache> cacheData;

	public Dictionary<string, string> listShowBuyIAP;

	public BonusOnlineObject bonusOnline;

	public List<long> listNotify;

	public bool m_NeedUpdateNotify;

	public Dictionary<int, List<int>> m_LstAvatar;

	public int m_HideVIP;

	public static GameContext GetIntance()
	{
		return null;
	}

	private GameContext()
	{
	}

	public bool HasLogin()
	{
		return false;
	}

	public void ClearData()
	{
	}

	public void SetNumberItemConsumable(List<ItemConsumable> lstItemConsume)
	{
	}

	public string GetUserIdentity()
	{
		return null;
	}

	public void setDataCache(int key, int time, byte[] data = null)
	{
	}

	public DataCache getDataCache(int key)
	{
		return null;
	}

	public void clearDataCache(int key)
	{
	}

	public bool ExpiredTimeCache(int key)
	{
		return false;
	}
}
