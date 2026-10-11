using System.Collections.Generic;

public class StaticData
{
	public static string URL_GAME_PREFIX;

	public static string API_PORT;

	public static int m_PrizeArenaTicket;

	public static int m_SelectedCampaignMapIndex;

	public static int m_LastCampaignLevelId;

	public static int m_AutoCampaignLevelId;

	public static bool m_HasJustPassCampaignGate;

	public static int m_LastTypeTowerOpen;

	public static int m_HeroIdToReplace;

	public static int m_NewFeatureOpen;

	public static bool m_SkipBattleRaid;

	public static bool m_SkipBattleTower;

	public static BonusModel m_BonusLevelUp;

	public static string m_MessageHomeQuestCompleted;

	public static List<ShopNotification> m_ShopNotification;

	public static DungeonLotionState m_DungeonLotionState;

	public static ListBattleData m_ListBattleData;

	public static string m_SceneBackAfterBattle;

	public static PopupId m_PopupIdOpenWhenLoadScene;

	public static bool m_HasOpenEventPopup;

	public static bool m_HasSentGuildRecruit;

	public static ArenaRivalManager m_ArenaRivalCrystal;

	public static ArenaRivalManager m_ArenaRivalTrial;

	public static ArenaRivalManager m_ArenaRivalAllStars;

	public static LuaPlaceholderInfo m_LuaPlaceHolderInfo;

	public static MFStateLast m_MfStateLast;

	public static List<int> m_ListEventNeedNotice;

	public static bool m_GetSpecialEventFirstTime;

	public static bool m_HasLoadCheckInStatus;

	public static bool m_IsFkGame;

	public static MFDataSlotItemForDelayProcess DataMFSlotItemDelayProcess;

	public static void SetSceneBackAfterBattle(string sceneName)
	{
	}

	public static void ResetSceneBack()
	{
	}

	public static void Reset()
	{
	}
}
