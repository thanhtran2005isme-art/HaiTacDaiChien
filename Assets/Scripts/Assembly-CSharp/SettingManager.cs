public class SettingManager
{
	private const string KEY_LAST_LOGIN_TYPE = "key_login_type";

	private const string KEY_LAST_LOGIN_SERVER = "key_last_login";

	private const string KEY_LAST_TIME_SCALE = "key_time_scale";

	private const string KEY_LAST_TOWER_LEVEL = "key_tower_level";

	private const string KEY_FORMATION_CAMPAIGN = "key_formation_campaign";

	private const string KEY_FORMATION_HOME = "key_formation_home";

	private const string KEY_FORMATION_RAID = "key_formation_raid";

	private const string KEY_FORMATION_ARENA_1 = "key_formation_arena1";

	private const string KEY_FORMATION_ARENA_3 = "key_formation_arena3";

	private const string KEY_FORMATION_ALLSTARS_3 = "key_formation_allstars3";

	private const string KEY_FORMATION_PRAY = "key_formation_pray";

	private const string KEY_FORMATION_FRIEND = "key_formation_friend";

	private const string KEY_FORMATION_BRAVE = "key_formation_brave";

	private const string KEY_FORMATION_GUILD_WAR = "key_formation_guild_war";

	private const string KEY_FORMATION_GUILD_BOSS = "key_formation_guild_boss";

	private const string KEY_FORMATION_CELESTIAL = "key_formation_celestial";

	private const string KEY_FORMATION_BROKEN_SPACE = "key_formation_broken_space";

	private const string KEY_FORMATION_BROKEN_SPACE_NEW = "key_formation_broken_space_new";

	private const string KEY_DUNGEON_SKIP_BATTLE = "key_dungeon_skip_battle";

	private const string KEY_TUTORIAL_PLAYED = "key_tut_played";

	private const string KEY_GOSU_ID = "key_gosuid";

	private const string KEY_USERNAME = "key_username";

	private const string KEY_USERNAMELINK = "key_usernamelink";

	private const string KEY_PASSWORD = "key_password";

	private const string KEY_SOUND = "key_sound";

	private const string KEY_MUSIC = "key_music";

	private const string KEY_FPS_SETTING = "key_fps_setting";

	private const string KEY_LAST_LEVEL_CHECK_UNLOCK_FEATURE = "key_lv_check_ft";

	public const string KEY_URL_ASSET_BUNDLE_LOGIN_BGR = "key_url_ass_login_bgr";

	private const string KEY_CP = "key_cp";

	private const string KEY_CLIENT_LANGUAGE_CODE = "key_cli_lang_code";

	public static void SaveStringValue(string key, string value)
	{
	}

	public static string LoadStringValue(string key, string defautValue)
	{
		return null;
	}

	public static void SaveInValue(string key, int value)
	{
	}

	public static int LoadIntValue(string key, int defautValue)
	{
		return 0;
	}

	public static void SaveClientLanguage(int languageCode)
	{
	}

	public static int GetClientLanguage()
	{
		return 0;
	}

	public static bool HasUserChooseLanguage()
	{
		return false;
	}

	public static void SaveCp(string cp)
	{
	}

	public static string GetCp()
	{
		return null;
	}

	public static void SaveLastLoginType(int loginType)
	{
	}

	public static int GetLastLoginType()
	{
		return 0;
	}

	public static void SaveFpsSettingValue(int fpsValue)
	{
	}

	public static int GetFpsSettingValue()
	{
		return 0;
	}

	public static void SaveSoundSetting(bool soundOn)
	{
	}

	public static bool GetSoundSetting()
	{
		return false;
	}

	public static void SaveMusicSetting(bool musicOn)
	{
	}

	public static bool GetMusicSetting()
	{
		return false;
	}

	public static void SaveDungeonSkipBattle(bool isSkip)
	{
	}

	public static bool GetDungeonSkipBattle()
	{
		return false;
	}

	public static void SaveGosuId(string gosuId)
	{
	}

	public static string GetGosuId()
	{
		return null;
	}

	public static void SaveUsername(string username)
	{
	}

	public static string GetUsername()
	{
		return null;
	}

	public static void SaveUsernameLink(string usernamelink)
	{
	}

	public static string GetUsernameLink()
	{
		return null;
	}

	public static void SavePassword(string pwd)
	{
	}

	public static string GetPassword()
	{
		return null;
	}

	public static void SaveLastLoginServerName(string svName)
	{
	}

	public static string GetLastLoginServerName()
	{
		return null;
	}

	public static void SaveLastTimeScale(int timeScaleLevel)
	{
	}

	public static int GetLastTimeScale()
	{
		return 0;
	}

	public static void SaveLastTowerLevel(int towerLevel)
	{
	}

	public static int GetLastTowerLevel()
	{
		return 0;
	}

	public static void SetTutorialPlayed(bool played)
	{
	}

	public static bool HasTutorialPlayed()
	{
		return false;
	}

	public static void SetLastLevelCheckUnlockFeature(int playerLevel)
	{
	}

	public static int GetLastLevelCheckUnlockFeature()
	{
		return 0;
	}

	public static void SaveFormationHome(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationHome()
	{
		return null;
	}

	public static void SaveFormationBrave(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationBrave()
	{
		return null;
	}

	public static void SaveFormationGuildWar(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationGuildWar()
	{
		return null;
	}

	public static void SaveFormationGuildBoss(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationGuildBoss()
	{
		return null;
	}

	public static void SaveFormationCampaign(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationCampaign()
	{
		return null;
	}

	public static void SaveFormationRaid(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationRaid()
	{
		return null;
	}

	public static void SaveFormationPrayForFire(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationPrayForFire()
	{
		return null;
	}

	public static void SaveFormationArena1(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationArena1()
	{
		return null;
	}

	public static void SaveFormationArena3(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationArena3()
	{
		return null;
	}

	public static void SaveFormationAllStars(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationAllStars()
	{
		return null;
	}

	public static void SaveFormationFriend(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationFriend()
	{
		return null;
	}

	public static void SaveFormationCelestial(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationCelestial()
	{
		return null;
	}

	public static void SaveFormationBrokenSpace(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationBrokenSpace()
	{
		return null;
	}

	public static void SaveFormationBrokenSpaceNew(HeroesFormation heroFormation)
	{
	}

	public static HeroesFormation GetFormationBrokenSpaceNew()
	{
		return null;
	}

	private static string ConvertHeroesFormationToString(HeroesFormation heroFormation)
	{
		return null;
	}

	private static HeroesFormation ConvertStringToHeroesFormation(string strFormation)
	{
		return null;
	}

	private static string ConvertHeroesFormationToString3(HeroesFormation heroFormation)
	{
		return null;
	}

	private static HeroesFormation ConvertStringToHeroesFormation3(string strFormation)
	{
		return null;
	}
}
