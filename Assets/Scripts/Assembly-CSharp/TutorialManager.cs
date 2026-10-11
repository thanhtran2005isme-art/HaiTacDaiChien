using System.Collections.Generic;

public class TutorialManager
{
	public const int TUT_STORY = 1;

	public const int TUT_SET_NAME = 2;

	public const int TUT_CAMPAIGN_1 = 3;

	public const int TUT_HERO_EQUIP_AND_UPGRADE = 4;

	public const int TUT_CAMPAIGN_2 = 5;

	public const int TUT_COMPLETE_QUEST = 6;

	public const int TUT_SUGGEST_CAMP = 7;

	public const int TUT_SUGGEST_CAMP_GO_HOME = 8;

	public const int TUT_COMBINE_HERO_SHARD_ZORO = 9;

	public const int TUT_CHECKIN = 10;

	public const int TUT_SHOP = 11;

	public const int TUT_MISSION = 12;

	public const int TUT_FORGE = 13;

	public const int TUT_ARCHIEVEMENT = 14;

	public const int TUT_LUCK_SPIN = 15;

	public const int TUT_EVENET = 16;

	public const int TUT_COMBINE_HERO_SHARD_NAMI = 17;

	public const int TUT_COMBINE_HERO_SHARD_USOP = 18;

	public const int TUT_COMBINE_HERO_SHARD_SANJI = 19;

	public const int TUT_FEATURE_ARENA_CRYSTAL_CROWN = 20;

	public const int TUT_FEATURE_GUILD = 21;

	public const int TUT_FEATURE_TOWER = 22;

	public const int TUT_FEATURE_TARVEN = 23;

	public const int TUT_FEATURE_ISLAND = 24;

	public const int TUT_FEATURE_BRAVE_TRIAL = 25;

	public const int TUT_FEATURE_PET = 26;

	public const int TUT_FEATURE_DUNGEON = 27;

	public const int TUT_FEATURE_SEALED_LAND = 28;

	public const int TUT_FEATURE_EVENT_RAID = 29;

	public const int TUT_HOME_WELCOME_AND_NAME = 1;

	public const int TUT_HOME_BEFORE_SUMMON_HEROES = 2;

	public const int TUT_SUMMON_HEROES = 300;

	public const int TUT_SUMMON_HEROES_DONE = 400;

	public const int TUT_HOME_BEFORE_CAMPAIGN_1 = 5555;

	public const int TUT_HOME_BEFORE_UPGRADE_HERO = 5;

	public const int TUT_LINEUP_1 = 4;

	public const int TUT_BATTLE_X2 = 5;

	public const int TUT_FINISH_CAMPAIGN_1 = 6;

	public const int TUT_SELECT_HERO_UPGRADE_1 = 11;

	public const int TUT_HOME_BEFORE_CAMPAIGN_2 = 13;

	public const int TUT_CAMPAIGN_COLLECT_2 = 14;

	public const int TUT_LINEUP_2 = 16;

	public const int TUT_FINISH_CAMPAIGN_2 = 17;

	public const int TUT_HOME_DAI_KIEU = 18;

	public const int TUT_COLLECT_DAI_KIEU = 19;

	public const int TUT_HOME_BEFORE_UPGRADE_DAI_KIEU = 20;

	public const int TUT_SELECT_UPGRADE_DAIKIEU = 21;

	public const int TUT_UPGRADE_DAIKIEU = 22;

	public const int TUT_HOME_BEFORE_CAMPAIGN_3 = 23;

	public const int TUT_CAMPAIGN_COLLECT_3 = 24;

	public const int TUT_CAMPAIGN_3 = 25;

	public const int TUT_LINEUP_3 = 26;

	public const int TUT_FINISH_CAMPAIGN_3 = 27;

	public const int TUT_HOME_DAILY_QUEST = 28;

	public const int TUT_MAKE_QUEST_COLLECT_CAMPAIGN = 29;

	public const int TUT_CAMPAIGN_COLLECT_4 = 30;

	public const int TUT_HOME_DAILY_QUEST_2 = 31;

	public const int TUT_SELECT_QUEST_FREE_GOLD = 32;

	public const int TUT_MAKE_QUEST_FREE_GOLD = 33;

	public const int TUT_HOME_BEFORE_COMBINE_HERO_SHARD = 34;

	public const int TUT_TUTORIAL_DONE = 36;

	public const int TUT_BATTLE_OPEN_RESTRAIN = 9999;

	public const int TUT_HOME_BEFORE_ALTAR_HERO = 18000;

	public const int TUT_ALTAR_HERO = 19000;

	public const int TUT_SALE_OFF = 20000;

	private static TutorialManager m_Instance;

	private List<TutorialData> m_LstTutData;

	private Dictionary<int, List<TutorialAction>> m_DicTutData;

	public bool m_OnTutorial;

	private List<TutorialAction> m_CurrentTutorial;

	public bool IsTutorialCompleted()
	{
		return false;
	}

	public static TutorialManager GetInstance()
	{
		return null;
	}

	public void Reset()
	{
	}

	public TutorialAction GetCurrentTutorialAction()
	{
		return null;
	}

	private TutorialManager()
	{
	}

	public void LoadTutorialData()
	{
	}

	public List<TutorialAction> getCurrentTutorial()
	{
		return null;
	}

	public int GetCurrentTutorialId()
	{
		return 0;
	}

	public TutorialData GetCurrentTutorialData()
	{
		return null;
	}

	public void SetCurrentTutorialCompleted()
	{
	}

	public void SetCurrentTutorial(int tutorialId)
	{
	}
}
