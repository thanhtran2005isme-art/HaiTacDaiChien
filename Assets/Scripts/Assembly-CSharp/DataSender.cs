using System.Collections.Generic;
using UnityEngine;

public class DataSender
{
	public const int LOGIN_ACCOUNT = 7891;

	public const int LOGIN_QUICK = 7792;

	public const int LOGIN_FB = 7793;

	public const int LINK_ACCOUNT = 7794;

	public const int LOGIN_GOSU = 7795;

	public const int LOGIN_GOSU_BY_ID = 7796;

	public const int REGISTER = 7892;

	public const int CHANGE_PASS = 7893;

	public const int REQUEST_LIST_ASSEST_BUNLDE = 7894;

	public const int SERVICE_ERRROR_NETWORK = 7895;

	public const int GET_INFO_MONTH_CARD = 7896;

	public const int REQUEST_LIST_ASSEST_BUNLDE_TO_DEBUG = 7899;

	public const int GET_LIST_EVENT = 8000;

	public const int TAB_CLICK_EVENT = 8001;

	public const int CELL_EVENT_CLICK = 8002;

	public const int BUY_GROWTH_FUND_EVENT = 8003;

	public const int RELOAD_EVENT = 8004;

	public const int HIT_BOSS = 8005;

	public const int GET_INFO_TRIP_EVENT = 8006;

	public const int MOVE = 8007;

	public const int GET_LIST_SPECIAL_EVENT = 8008;

	public const int GET_LIST_MISSION_TAB = 8009;

	public const int GET_MISSION_INFO = 8010;

	public const int ACTIVE_AND_REFRESH_STATUE = 8011;

	public const int EXCHANGE_ITEM_EASTER_PRAY = 8012;

	public const int EXCHANGE_BUY_HORN = 8013;

	public const int TOAST_MSG = 1;

	public const int POPUP_MSG = 2;

	public const int SLIDE_MSG = 3;

	public const int PLAYER_LEVEL_UP = 9;

	public const int FEED_BACK = 115;

	public const int VERIFY_FB = 125;

	public const int REQUEST_CONFIG_DATA = 113;

	public const int NOTIFY_GAME = 114;

	public const int NOTIFY_GAME_UPDATE = 126;

	public const int ACCOUNT_RENAME = 110;

	public const int ACCOUNT_GET_LIST_AVATAR = 111;

	public const int ACCOUNT_CHANGE_AVATAR = 112;

	public const int RE_LOGIN = 8;

	public const int SHOPPING_NOTIFY = 7;

	public const int LOGIN_GAME = 100;

	public const int LOGIN_GAME_FAIL = 101;

	public const int BATTLE_REPLAY = 97;

	public const int REQUEST_TOP_PLAYER = 124;

	public const int REQUEST_TOP_MY_RANK = 125;

	public const int REQUEST_HELP_TEXT = 96;

	public const int UPDATE_HERO_SLOT = 127;

	public const int SHARE_BATTLE = 109;

	public const int HERO_LIST = 200;

	public const int HERO_EQUIP_ITEM = 201;

	public const int HERO_EQUIP_ARTIFACT = 202;

	public const int HERO_UPGRADE_LEVEL_NEW = 470;

	public const int HERO_UPGRADE_TIER = 205;

	public const int HERO_AWAKEN = 206;

	public const int HERO_PREVIEW_UPGRADE_TIER = 239;

	public const int HERO_UNLOCK_STONE = 231;

	public const int HERO_UPGRADE_STONE = 232;

	public const int HERO_CONVERT_STONE = 235;

	public const int HERO_SAVE_CONVERTED_STONE = 238;

	public const int HERO_GET_STONE_INFO = 236;

	public const int HERO_UPDATE_INFO = 207;

	public const int HERO_UNEQUIP_ITEM = 208;

	public const int HERO_QUICK_EQUIP_ITEM = 203;

	public const int HERO_ALTAR_DISASSEMBLE = 209;

	public const int HERO_ALTAR_PREVIEW = 237;

	public const int LOCAL_NOTIFICATION = 130;

	public const int HERO_RETURN_PREVIEW = 246;

	public const int HERO_RETURN_DISASSEMBLE = 245;

	public const int HERO_FUSE = 230;

	public const int HERO_BUY_CAPACITY = 234;

	public const int HERO_PROPHET_SUMMON = 212;

	public const int HERO_PROPHET_REPLACE = 213;

	public const int HERO_PROPHET_STATUS = 215;

	public const int HERO_PROPHET_CANCEL = 214;

	public const int HERO_SET_LOCK = 240;

	public const int HERO_UNLOCK_ALL = 247;

	public const int HERO_AWAKEN_REPLACE = 241;

	public const int HERO_AWAKEN_REPLACE_RETURN = 244;

	public const int HERO_AWAKEN_REGRESS = 242;

	public const int HERO_PICK_ESKILL = 243;

	public const int SKIN_GET_LIST = 770;

	public const int SKIN_EQUIP = 771;

	public const int SKIN_UNEQUIP = 772;

	public const int SUMMON_STATUS = 210;

	public const int SUMMON_PERFORM = 211;

	public const int SUMMON_ENERGY = 233;

	public const int ITEM_EQUIPMENT_LIST = 250;

	public const int ITEM_CONSUMABLE_LIST = 251;

	public const int ITEM_ARTIFACT_LIST = 253;

	public const int ITEM_FORGE = 252;

	public const int ITEM_SUMMON = 254;

	public const int ITEM_ARTIFACT_UPGRADE = 255;

	public const int ITEM_SELL_EQUIPMENT = 256;

	public const int ITEM_SELL_ARTIFACT = 259;

	public const int ITEM_SELL_CONSUMABLE = 260;

	public const int CAMPAIGN_STATUS = 300;

	public const int CAMPAIGN_COLLECT_AUTO_AWARDS = 302;

	public const int CAMPAIGN_COLLECT_AUTO_LOOTS = 303;

	public const int CAMPAIGN_BATTLE = 304;

	public const int CAMPAIGN_AUTO_LEVEL = 306;

	public const int LUCKY_STATUS = 319;

	public const int LUCKY_REFRESH = 320;

	public const int LUCKY_SPIN = 321;

	public const int LUCKY_RECORD = 322;

	public const int LUCKY_BUY_CHIP = 323;

	public const int SUPER_LUCKY_STATUS = 344;

	public const int SUPER_LUCKY_REFRESH = 324;

	public const int SUPER_LUCKY_SPIN = 325;

	public const int SUPER_LUCKY_RECORD = 326;

	public const int BATTLE_RELIC_TEST = 88;

	public const int BATTLE_TEST = 94;

	public const int BATTLE_TEST_REPLAY_NEW = 93;

	public const int BATTLE_TEST_REPLAY = 97;

	public const int LIVE_STREAM_INFOR = 89;

	public const int LIVE_STREAM_BATTLE = 90;

	public const int BATTLE_REPLAY_WITH_MONGO = 91;

	public const int PET_REQUEST_LIST = 360;

	public const int PET_ACTIVATE = 361;

	public const int PET_UPGRADE_LEVEL = 362;

	public const int PET_EVOLVE = 363;

	public const int PET_UPGRADE_PASSIVE_SKILL = 364;

	public const int PET_REBIRTH = 365;

	public const int PET_UPGRADE_RUIN = 366;

	public const int SV_QUEST_STATUS = 261;

	public const int SV_QUEST_GET_REWARD = 262;

	public const int HOME_QUEST_COMPLETED = 263;

	public const int TARVEN_STATUS = 222;

	public const int TARVEN_GEN_QUEST = 223;

	public const int TARVEN_SPEED_UP_QUEST = 225;

	public const int TARVEN_SWITCH_LOCK_QUEST = 226;

	public const int TARVEN_START_QUEST = 227;

	public const int TARVEN_CANCEL_QUEST = 229;

	public const int TARVEN_COMPLETED_QUEST = 228;

	public const int TARVEN_REFRESH = 224;

	public const int TOWER_STATUS = 216;

	public const int TOWER_BATTLE = 217;

	public const int TOWER_BUY_TURN = 257;

	public const int TOWER_PLAY_HISTORY_LOG = 258;

	public const int TOWER_2_STATUS = 585;

	public const int TOWER_2_ATTACK = 581;

	public const int TOWER_2_TOP = 582;

	public const int TOWER_2_BUY_TURN = 583;

	public const int TOWER_2_PLAY_HISTORY_LOG = 584;

	public const int TOWER_2_COLLECT_DAILY_REWARDS = 586;

	public const int EVENT_RAID_STATUS = 316;

	public const int EVENT_RAID_BATTLE = 317;

	public const int EVENT_RAID_BUY_TURN = 318;

	public const int EVENT_RAID_SMASH = 349;

	public const int CHALLENGES_STATUS = 309;

	public const int CHALLENGES_COLLECT = 310;

	public const int DAILY_QUEST_STATUS = 307;

	public const int DAILY_QUEST_COLLECT = 308;

	public const int BRAVE_CHECK_OPENING = 311;

	public const int BRAVE_STATUS = 312;

	public const int BRAVE_OPEN_CHEST = 313;

	public const int BRAVE_ATTACK = 314;

	public const int BRAVE_UPDATE_STATUS = 315;

	public const int BRAVE_NEW_CHECK_OPENING = 655;

	public const int BRAVE_NEW_ATTACK = 658;

	public const int BRAVE_NEW_REVIVE_HERO = 659;

	public const int BRAVE_NEW_HEAL_HERO = 660;

	public const int BRAVE_NEW_BUFF = 661;

	public const int BRAVE_NEW_STATUS = 656;

	public const int BRAVE_NEW_OPEN_CHEST = 662;

	public const int BRAVE_NEW_BUY_BADGE = 657;

	public const int COMMON_GET_PLAYER_LINEUP = 119;

	public const int ARENA_STATUS = 327;

	public const int CRYSTAL_STATUS = 328;

	public const int CRYSTAL_BATTLE_RECORD = 329;

	public const int CRYSTAL_SET_DEFENSE = 331;

	public const int CRYSTAL_BUY_TICKET = 332;

	public const int CRYSTAL_REVENGE = 330;

	public const int CRYSTAL_SEARCH_OPPONENT = 333;

	public const int CRYSTAL_BATTLE = 334;

	public const int CRYSTAL_TOP_PLAYER = 340;

	public const int CRYSTAL_NOTIFY_LOG = 345;

	public const int CRYSTAL_TOP_PLAYER_LAST_SEASON = 347;

	public const int TRIAL_CHAMP_STATUS = 336;

	public const int TRIAL_CHAMP_BATTLE_RECORD = 337;

	public const int TRIAL_CHAMP_SET_DEFENSE = 339;

	public const int TRIAL_CHAMP_REVENGE = 338;

	public const int TRIAL_CHAMP_SEARCH_OPPONENT = 341;

	public const int TRIAL_CHAMP_BATTLE = 342;

	public const int TRIAL_CHAMP_TOP_PLAYER = 343;

	public const int TRIAL_CHAMP_NOTIFY_LOG = 346;

	public const int TRIAL_CHAMP_TOP_PLAYER_LAST_SEASON = 348;

	public const int ALL_STARS_LEAGUE_STATUS = 550;

	public const int ALL_STARS_LEAGUE_BATTLE_RECORD = 551;

	public const int ALL_STARS_LEAGUE_SET_DEFENSE = 552;

	public const int ALL_STARS_LEAGUE_SEARCH_OPPONENT = 553;

	public const int ALL_STARS_LEAGUE_BATTLE = 554;

	public const int ALL_STARS_LEAGUE_TOP_PLAYER_BY_RANK = 555;

	public const int ALL_STARS_LEAGUE_TOP_PLAYER_BY_POINT = 561;

	public const int ALL_STARS_LEAGUE_NOTIFY_LOG = 556;

	public const int ALL_STARS_LEAGUE_BET = 562;

	public const int ALL_STARS_LEAGUE_PLAYOFF_BET_HISTORY = 563;

	public const int ALL_STARS_LEAGUE_PLAYOFF_PREPARE = 558;

	public const int ALL_STARS_LEAGUE_PLAYOFF_RESULT = 559;

	public const int ALL_STARS_LEAGUE_BUY_TURN_ATTACK = 560;

	public const int ALL_STARS_LEAGUE_BATTLE_STATUS = 564;

	public const int FRIEND_STATUS = 430;

	public const int FRIEND_LIST = 448;

	public const int FRIEND_APPLY_LIST = 432;

	public const int FRIEND_RECOMMEND = 433;

	public const int FRIEND_APPLY = 434;

	public const int FRIEND_RESPONES_APPLY = 435;

	public const int FRIEND_DELETE = 436;

	public const int FRIEND_SEND_MAIL = 437;

	public const int FRIEND_FIGHT = 438;

	public const int FRIEND_SCOUT_STATUS = 439;

	public const int FRIEND_GET_SCOUT = 444;

	public const int FRIEND_SEND_FRIENDSHIP = 440;

	public const int FRIEND_RECEIVED_FRIENDSHIP = 441;

	public const int FRIEND_HELP_FIGHT = 442;

	public const int FRIEND_FIGHT_BOSS = 443;

	public const int FRIEND_SEND_AND_RECEIVE = 445;

	public const int FRIEND_SMASH_BOSS = 446;

	public const int SMASH_HELP_FIGT_BOSS = 447;

	public const int MAIL_REQUEST_LIST = 219;

	public const int MAIL_COLLECT_BONUS = 220;

	public const int MAIL_DELETE = 221;

	public const int SHOP_REQUEST_DATA = 120;

	public const int SHOP_REQUEST_DATA_NEW = 128;

	public const int SHOP_BUY = 121;

	public const int SHOP_REFRESH = 122;

	public const int GUILD_CREATE = 400;

	public const int GUILD_EDIT_INFO = 466;

	public const int GUILD_APPLY = 402;

	public const int GUILD_REQUEST_INFO = 405;

	public const int GUILD_SEARCH = 408;

	public const int GUILD_REQUEST_MEMBER = 404;

	public const int GUILD_QUIT = 407;

	public const int GUILD_WAITING_APPLICATION = 401;

	public const int GUILD_RESPONSE_APPLICATION = 403;

	public const int GUILD_KICK = 406;

	public const int GUILD_SET_ROLE = 410;

	public const int GUILD_ROLL_CALL = 463;

	public const int GUILD_STATUS = 427;

	public const int GUILD_RECRUIT = 107;

	public const int GUILD_MAIL_MEMBER = 428;

	public const int GUILD_RENAME = 429;

	public const int GUILD_TECH_STATUS = 450;

	public const int GUILD_TECH_UPGRADE = 451;

	public const int GUILD_TECH_RESET = 452;

	public const int GUILD_TECH_GET_RESET_STATUS = 467;

	public const int GUILD_TECH_NEXT_LEVEL_STATUS = 468;

	public const int GUILD_TECH_NEXT_LEVEL_UPGRADE = 469;

	public const int GUILD_MILL_ORDER_STATUS = 456;

	public const int GUILD_MILL_ORDER_GET = 459;

	public const int GUILD_MILL_UPGRADE_ORDER = 457;

	public const int GUILD_MILL_START_ORDER = 458;

	public const int GUILD_MILL_ORDER_REWARD = 464;

	public const int GUILD_MILL_STATUS = 460;

	public const int GUILD_MILL_DONATE = 461;

	public const int GUILD_MILL_TOP_DONATE = 462;

	public const int GUILD_RAID_STATUS = 453;

	public const int GUILD_RAID_TOP_DAMAGE = 454;

	public const int GUILD_RAID_BATTLE = 455;

	public const int GUILD_RAID_LOG_DAMAGE = 465;

	public const int GUILD_WAR_STATUS = 2100;

	public const int GUILD_WAR_REGISTER_LINEUP = 2101;

	public const int GUILD_WAR_GET_REGISTER_MEMBER = 2103;

	public const int GUILD_WAR_APPLY_MEMBER = 2104;

	public const int GUILD_WAR_ATTACK = 2105;

	public const int GUILD_WAR_GET_BATTLE_LOG = 2106;

	public const int GUILD_WAR_GET_HISTORY = 2107;

	public const int GUILD_WAR_PREVIEW_REWARD = 2108;

	public const int GUILD_PRAY_STATUS = 511;

	public const int GUILD_PRAY_TOP_DAMAGE = 512;

	public const int GUILD_PRAY_LIST_BONUS = 513;

	public const int GUILD_PRAY_BATTLE = 514;

	public const int GUILD_PRAY_SIGN_UP = 515;

	public const int CROSS_WORD_PULZZES_INFO = 275;

	public const int CROSS_WORD_PULZZES_SEND_ANSWER = 276;

	public const int CROSS_WORD_PULZZES_SELECT_BONUS = 277;

	public const int UPGRADE_HERO_SPECIAL_INFO = 267;

	public const int UPGRADE_HERO_SPECIAL_ENHANCE = 265;

	public const int UPGRADE_HERO_SPECIAL_REUP = 268;

	public const int UPGRADE_HERO_SPECIAL_UPGRADE = 266;

	public const int UPGRADE_HERO_SPECIAL_FIRST_UNLOCK = 264;

	public const int DUNGEON_STATUS = 500;

	public const int DUNGEON_RESQUEST_SELECT_HERO = 501;

	public const int DUNGEON_SELECT_HERO = 502;

	public const int DUNGEON_BEING_CLOSED = 509;

	public const int DUNGEON_BATTLE = 504;

	public const int DUNGEON_USE_POTION = 510;

	public const int DUNGEON_BUY_MERCHANT_IN_BATTLE = 505;

	public const int DUNGEON_SMASHED_DATA = 503;

	public const int DUNGEON_REQUEST_SMASHED_MERCHANT = 506;

	public const int DUNGEON_BUY_SMASHED_MERCHANT = 507;

	public const int GLOBAL_CHAT = 102;

	public const int GLOBAL_CHAT_LIST = 103;

	public const int CLAN_CHAT = 104;

	public const int CLAN_CHAT_LIST = 105;

	public const int CLAN_RECRUIT_LIST = 108;

	public const int CHAT_HIDE_VIP = 106;

	public const int CHAT_PRIVATE_SERVER = 280;

	public const int CHAT_PRIVATE_SERVER_LIST = 281;

	public const int CHAT_ADMIN = 701;

	public const int VERIFY_IAP = 123;

	public const int UPDATE_BONUS = 116;

	public const int BONUS_ONLINE_STATUS = 117;

	public const int BONUS_ONLINE_RECEIVE = 118;

	public const int HAND_OF_MIDAS_STATUS = 350;

	public const int HAND_OF_MIDAS_RECEIVE = 351;

	public const int CELESTIAL_STATUS = 411;

	public const int CELESTIAL_TOWER_UPGRADE = 412;

	public const int CELESTIAL_MAKE_CONSTRUCTION = 413;

	public const int CELESTIAL_DISMANTLE_MINE_FIELD = 414;

	public const int CELESTIAL_BUILDING_UPGRADE = 415;

	public const int CELESTIAL_TECH_UPGRADE = 424;

	public const int CELESTIAL_GET_MINE_FIELD = 416;

	public const int CELESTIAL_MINE_UPDATE = 425;

	public const int CELESTIAL_ISLAND_STATUS = 417;

	public const int CELESTIAL_BOSS_STATUS = 418;

	public const int CELESTIAL_ATTACK = 419;

	public const int CELESTIAL_AUTO_ATTACK = 420;

	public const int CELESTIAL_SMASH = 421;

	public const int CELESTIAL_USE_ASSIST = 423;

	public const int CELESTIAL_BUY_DAFFODIL = 422;

	public const int GET_DAFFODIL = 426;

	public const int MASTERING_HERO_STATUS = 270;

	public const int MASTERING_HERO_DETAIL = 271;

	public const int MASTERING_HERO_GRAND_STATUS = 272;

	public const int MASTERING_HERO_COLLECT_REWARD = 273;

	public const int SABAODY_GET_CONFIG = 274;

	public const int MF_GET_STATUS = 610;

	public const int MF_ATTACK = 611;

	public const int MF_CHOOSE_SUPPORT = 612;

	public const int MF_CHOOSE_RELIC = 613;

	public const int MF_HEALING = 614;

	public const int MF_CHOOSE_REVINE_RANDOM = 616;

	public const int MF_ADVANCE_NEXT_FLOOR = 615;

	public const int MF_REVINE_ALL = 617;

	public const int MF_GET_ITEM_DESC = 618;

	public const int SEALED_LAND_STATUS = 650;

	public const int SEALED_LAND_SMASH = 651;

	public const int SEALED_LAND_CHALLENGE = 652;

	public const int SEALED_LAND_BUYATTACK = 653;

	public const int SEALED_LAND_REWARD = 654;

	public const int ATK_BROKEN_SPACE = 600;

	public const int ATK_BROKEN_SPACE_NEW = 602;

	public const int NOTIFY_SHOW_CONFIRM = 92;

	public const int PEEK_OF_TIME_CHECK_PATH = 735;

	private string URL_SERVER_LOGIN;

	private string URL_REQUEST_LIST_ASSET;

	private string URL_SPECIAL_EVENT_LIST;

	private NetworkController m_NetworkController;

	public void RequestAssetBundleInfo()
	{
	}

	public void RequestAssetBundleInfoToDebug()
	{
	}

	public void RequestHelpText(string helpKey)
	{
	}

	public void LoginByAccount(string usn, string pwd)
	{
	}

	public void LoginQuick()
	{
	}

	public void Register(string username, string password, string phone)
	{
	}

	public void LoginGoSu(string accessToken)
	{
	}

	public void LoginGosuById(string gosuID)
	{
	}

	public void changePassWord(string username, string oldPass, string newPass)
	{
	}

	public void RequestListNotification()
	{
	}

	public void LoginGame(int serverId, string usn)
	{
	}

	public void RequestConfigData()
	{
	}

	public void AccountRename(string screenName)
	{
	}

	public void AccountChangeAvatar(int type, int avatarId)
	{
	}

	public void AccountGetListAvatar()
	{
	}

	public void RequestEvent(int Service, string URL_EVENT)
	{
	}

	public void RequestTopPlayer(int type)
	{
	}

	public void BattleReplay(long battleId)
	{
	}

	public void GetPlayerLineUp(int userId, LineupType type)
	{
	}

	public void SkinGetList()
	{
	}

	public void SkinEquip(int heroId, int skinKey)
	{
	}

	public void SkinUnEquip(int heroId)
	{
	}

	public void DungeonStatus()
	{
	}

	public void DungeonSelectHero(List<int> lstHeroId)
	{
	}

	public void DungeonBattle(int heroSlotIdx)
	{
	}

	public void DungeonUsePotionHp(int heroSlotIdx, int potionId)
	{
	}

	public void DungeonResponeMerchantInBattle(bool purchased)
	{
	}

	public void DungeonRequestedSmashedMerchant(int merchantId)
	{
	}

	public void DungeonBuySmashedMerchantItem(int merchantId, int itemId)
	{
	}

	public void GuildCreate(string guildName, int guildAvatarId)
	{
	}

	public void GuildTechNewLevelUpGrade(int classIndex, int techIndex)
	{
	}

	public void GuildUpdateInfo(int guildAvatarId, string motto)
	{
	}

	public void GuildResponseApplication(int userId, bool approved)
	{
	}

	public void GuildGetStatus()
	{
	}

	public void GuildRename(string newGuildName)
	{
	}

	public void GuildApply(int guildId)
	{
	}

	public void GuildRequestWaitingApplication()
	{
	}

	public void GuildRequestInfo(int guildId)
	{
	}

	public void GuildSearch(string guildName)
	{
	}

	public void GuildRequestMember(int guildId)
	{
	}

	public void GuildQuit()
	{
	}

	public void GuildRollCall()
	{
	}

	public void GuildKick(int userId)
	{
	}

	public void GuildSetRole(int userId, int newRole)
	{
	}

	public void GuildSendRecruit(long lastReq, string message)
	{
	}

	public void GuildSendMailMember(string message)
	{
	}

	public void GuildTechNewLevelStatus()
	{
	}

	public void GuildWarStatus()
	{
	}

	public void GuildWarRegister(List<int> lstServerHeroId)
	{
	}

	public void GuildWarGetRegisterMember(int guildId)
	{
	}

	public void GuildWarApplyMember(List<int> lstPlayerId)
	{
	}

	public void GuildWarAttack(int enemyUserId, List<int> lstHeroId)
	{
	}

	public void GuildWarGetBattleLog()
	{
	}

	public void GuildWarGetHistory(int guildId)
	{
	}

	public void GuildWarPreviewReward()
	{
	}

	public void GuildTechStatus()
	{
	}

	public void GuildTechUpgrade(int classIndex, int techIndex)
	{
	}

	public void GuildTechReset(int classIndex)
	{
	}

	public void GuildTechGetResetStatus()
	{
	}

	public void GuildMillOrderStatus()
	{
	}

	public void GuildMillOrderGet()
	{
	}

	public void GuildMillStartOrder(int orderId)
	{
	}

	public void GuildMillUpgradeOrder(int orderId)
	{
	}

	public void GuildMillStatus()
	{
	}

	public void GuildMillDondate(int typeDonate)
	{
	}

	public void GuildMillRequestTopDonate()
	{
	}

	public void GuildRaidStatus()
	{
	}

	public void GuildRaidTopDamage()
	{
	}

	public void GuildRaidBattle(List<int> lstServerHeroId)
	{
	}

	public void GuildRaidLogDamage(int levelId)
	{
	}

	public void GuildPrayGetStatus()
	{
	}

	public void GuildPrayTopdamage()
	{
	}

	public void GuildPrayListBonus()
	{
	}

	public void GuildPraySignUp()
	{
	}

	public void GuildPrayBattle(List<int> lstHeroId)
	{
	}

	public void ShopRequestData(int shopId)
	{
	}

	public void ShopBuy(int shopId, int itemId, int number = 1)
	{
	}

	public void ShopRefresh(int shopId, bool isFree)
	{
	}

	public void MailRequestList()
	{
	}

	public void MailCollectBonus(int mailId)
	{
	}

	public void MailDelete(int mailId)
	{
	}

	public void ArenaStatus()
	{
	}

	public void CrystalStatus()
	{
	}

	public void CrystalRevenge(int userId, List<int> lstHeroId)
	{
	}

	public void CrystalTopPlayer()
	{
	}

	public void CrystalTopPlayerLastSeason()
	{
	}

	public void CrystalGetBattleRecord()
	{
	}

	public void CrystalBuyTicket(int numTicket)
	{
	}

	public void CrystalSaveDefensiveLineup(List<int> lstServerHeroId)
	{
	}

	public void CrystalSearchRival()
	{
	}

	public void CrystalBattle(int userId, List<int> lstServerHeroId)
	{
	}

	public void TrialChampStatus()
	{
	}

	public void TrialChampGetBattleRecord()
	{
	}

	public void TrialChampSaveDefensiveLineup(List<int> lstServerHeroId)
	{
	}

	public void TrialChampSearchRival()
	{
	}

	public void TrialChampRevenge(int userId, List<int> lstHeroId)
	{
	}

	public void TrialChampBattle(int userId, List<int> lstServerHeroId3Team)
	{
	}

	public void TrialChampTopPlayer()
	{
	}

	public void TrialChampTopPlayerLastSeason()
	{
	}

	public void AllStarsLeagueStatus()
	{
	}

	public void AllStarsLeagueGetBattleRecord()
	{
	}

	public void AllStarsLeagueSaveDefensiveLineup(List<int> lstServerHeroId)
	{
	}

	public void AllStarsLeagueSearchRival()
	{
	}

	public void AllStarsLeaguePlayOffBetHistory()
	{
	}

	public void AllStarsLeaguePlayOffPrepare()
	{
	}

	public void AllStarsLeaguePlayOffResult()
	{
	}

	public void AllStarsLeagueBuyTurnAttack(int numTurn)
	{
	}

	public void AllStarsLeagueGetVoSongStatus()
	{
	}

	public void AllStarsLeagueBet(int matchIndex, int bet)
	{
	}

	public void AllStarsLeagueBattle(int rivalUserId, int rivalRankPoint, List<int> lstServerHeroId3Team)
	{
	}

	public void AllStarsLeagueTopPlayerByRank()
	{
	}

	public void AllStarsLeagueTopPlayerByPoint()
	{
	}

	public void SummonRequestStatus()
	{
	}

	public void SummonHero(int summonType)
	{
	}

	public void SummonHeroByEnergy()
	{
	}

	public void ItemEquipmentRequestList()
	{
	}

	public void ItemConsumableRequestList()
	{
	}

	public void ItemArtifactRequestList()
	{
	}

	public void ItemForge(int itemKey, int number)
	{
	}

	public void ItemShardSummon(int typeShard, int itemKey, int numberSummon)
	{
	}

	public void ItemConsumableUsed(int itemKey, int numberSummon)
	{
	}

	public void ItemConsumableUsed2(int itemKey, int numberSummon, int subItemIndex)
	{
	}

	public void ItemArtifactUpgrade(int heroId, int artifactKey, int artifactStar, List<ArtifactMaterial> lstArtMat)
	{
	}

	public void ItemSellEquipment(int equipmentKey, int number)
	{
	}

	public void ItemSellArtifact(int artKey, int star, int number)
	{
	}

	public void HeroEquipItem(int heroId, int equipmentKey)
	{
	}

	public void HeroUnequipItem(int heroId, int slotId)
	{
	}

	public void HeroQuickEquipItem(int heroId)
	{
	}

	public void HeroEquipArtifact(int heroId, int artifactKey, int star)
	{
	}

	public void HeroRequestInfo(int heroId)
	{
	}

	public void HeroRequestInfo(List<int> lstHeroId)
	{
	}

	public void HeroSetLock(int heroId, bool locked)
	{
	}

	public void HeroUnlockAll(int heroId)
	{
	}

	public void HeroRequestList()
	{
	}

	public void HeroUpgradeLevel(int heroId, int numLevelUp = 1)
	{
	}

	public void HeroUpgradeTier(int heroId)
	{
	}

	public void HeroPreviewUpgradeTier(int heroId)
	{
	}

	public void HeroAwaken(int heroId, List<int> lstMaterialHeroId)
	{
	}

	public void HeroUnlockStone(int heroId)
	{
	}

	public void HeroUpgradeStone(int heroId, bool lockAttribute)
	{
	}

	public void HeroConvertStone(int heroId)
	{
	}

	public void HeroSaveConvertedStone()
	{
	}

	public void HeroGetStoneInfo(int heroId)
	{
	}

	public void CampaignStatus()
	{
	}

	public void CampaignCollectAutoAwards()
	{
	}

	public void CampaignCollectAutoLoots()
	{
	}

	public void CampaignAutoLevel(int levelId)
	{
	}

	public void CampaginBattle(HeroesFormation formation)
	{
	}

	public void LuckyRequestStatus()
	{
	}

	public void LuckyBuyChip(int numberChip)
	{
	}

	public void LuckyRefresh()
	{
	}

	public void LuckyRotate(int spinType)
	{
	}

	public void SuperLuckySpinStatus()
	{
	}

	public void SuperLuckySpinRefresh()
	{
	}

	public void SuperLuckySpinRotate(int spinType)
	{
	}

	public void replayBattle(int type, long battleId)
	{
	}

	public void replayBattle(int type, string battleId)
	{
	}

	public void LiveStreamBattle(int type, int user1, int user2)
	{
	}

	public void LiveStreamInfor(int type, int user1, int user2)
	{
	}

	public void TestBattle(List<BattleHeroTest> lstTeam1, List<BattleHeroTest> lstTeam2, int petId1, int petId2, RelicTest relic1, RelicTest relic2)
	{
	}

	public void GetCrossWordInfo()
	{
	}

	public void SendCrossWordSendAnswer(int quesId, int answerId)
	{
	}

	public void SendCrossWordSelectBonus(int bonusIndex)
	{
	}

	public void TarvenRequestStatus()
	{
	}

	public void TarvenSpeedUpQuest(int questId)
	{
	}

	public void TarvenCompleteQuest(int questId)
	{
	}

	public void TarvenSwitchLockQuest(int questId)
	{
	}

	public void TarvenCancelQuest(int questId)
	{
	}

	public void TarvenStartQuest(int questId, List<int> lstHeroId)
	{
	}

	public void TarvenRefresh()
	{
	}

	public void TarvenGenNewQuest(int typeScroll)
	{
	}

	public void SealedLandGetStatus()
	{
	}

	public void SealedLandSmash(int type, int level, int number)
	{
	}

	public void SealedLandChallenge(int type, List<int> lstPlayerHeroId)
	{
	}

	public void SealedLandBuyAttack(int type, int number)
	{
	}

	public void SealedLandGetReward()
	{
	}

	public void LocalNotificationGetStatus()
	{
	}

	public void TowerStatus()
	{
	}

	public void Tower2Status()
	{
	}

	public void TowerBattle(List<int> lstPlayerHeroId)
	{
	}

	public void Tower2Battle(List<int> lstPlayerHeroId)
	{
	}

	public void TowerBuyTurn(int numTurn)
	{
	}

	public void Tower2BuyTurn(int numTurn)
	{
	}

	public void Tower2CollectDailyBonus()
	{
	}

	public void TowerPlayHistoryLog(long battleId)
	{
	}

	public void Tower2PlayHistoryLog(long battleId)
	{
	}

	public void BraveCheckOpening()
	{
	}

	public void BraveStatus()
	{
	}

	public void BraveOpenChest()
	{
	}

	public void BraveAttack(List<int> lstPlayerHeroId)
	{
	}

	public void BraveNewAttack(List<int> lstPlayerHeroId)
	{
	}

	public void BraveNewCheckOpening()
	{
	}

	public void BraveNewBuff()
	{
	}

	public void BraveNewStatus()
	{
	}

	public void BraveNewReviveHero(long heroKey)
	{
	}

	public void BraveNewHealHero(long heroKey)
	{
	}

	public void BraveNewOpenChest(long level, long index)
	{
	}

	public void BraveNewBuyBadge(long number)
	{
	}

	public void PetRequestList()
	{
	}

	public void PetActivate(int petId)
	{
	}

	public void PetUpgradeLevel(int petId)
	{
	}

	public void PetEvolve(int petId)
	{
	}

	public void PetRebirth(int petId)
	{
	}

	public void PetUpgradeSkill(int petId, int skillIndex)
	{
	}

	public void PetUpgradeRuin(int petId, int ruinIndex)
	{
	}

	public void ChallengeStatus()
	{
	}

	public void ChallengeCollectReward(int challengeId)
	{
	}

	public void DailyQuestStatus()
	{
	}

	public void DailyQuestCollectReward(int questId)
	{
	}

	public void HeroReturnPreview(List<int> lstPlayerHeroId)
	{
	}

	public void HeroReturnDisassemnle(List<int> lstPlayerHeroId)
	{
	}

	public void HeroAltarDisassemble(List<int> lstPlayerHeroId)
	{
	}

	public void HeroAltarPreview(List<int> lstPlayerHeroId)
	{
	}

	public void HeroBuyCapacity()
	{
	}

	public void HeroFuse(List<int> lstMaterialHeroId)
	{
	}

	public void HeroProphetSummon(HeroFaction faction, int number)
	{
	}

	public void HeroProphetCancel()
	{
	}

	public void HeroProphetReplace(int heroId)
	{
	}

	public void HeroProphetStatus()
	{
	}

	public void HeroAwakenReplace(int replaceHeroId, List<int> lstHeroIdMaterial)
	{
	}

	public void HeroAwakenRegress(int regressHeroId)
	{
	}

	public void HeroPickESkill(int heroId, int groupSkillIndex, int skillIndex)
	{
	}

	public void EventRaidStatus(int raidType)
	{
	}

	public void EventRaidBattle(int raidType, int levelIndex, List<int> lstHeroIdServer)
	{
	}

	public void EventRaidSmash(int raidType, int levelIndex, int numberSmash, List<int> lstHeroIdServer)
	{
	}

	public void EventRaidBuyTurn(int raidType, int numBuy)
	{
	}

	public void SabaodyGetConfig()
	{
	}

	public void MFGetStatus()
	{
	}

	public void MFGetItemDescription()
	{
	}

	public void MFAttack(int rowIdx, int slotIdx, List<int> lstHeroId)
	{
	}

	public void MFChooseRelic(int relicIndex)
	{
	}

	public void MFChooseSupport(int rowIdx, int slotIdx, int heroIdx)
	{
	}

	public void MFHealing(int rowIdx, int slotIdx)
	{
	}

	public void MFRevineRandom(int rowIdx, int slotIdx)
	{
	}

	public void MFRevineAll()
	{
	}

	public void MFAdvanceFloor(int type)
	{
	}

	public void FriendStatus()
	{
	}

	public void GetListFriend()
	{
	}

	public void GetListApply()
	{
	}

	public void GetRecommendFriend()
	{
	}

	public void ApplyFriend(int userId)
	{
	}

	public void ResponesApply(int userId, int status)
	{
	}

	public void DeleteAllApply(List<int> lstUserId)
	{
	}

	public void DeleteFriend(int userId)
	{
	}

	public void SendMailFriend(int userId, string content)
	{
	}

	public void FightFriend(int userId, List<int> idHero)
	{
	}

	public void GetScoutStatus()
	{
	}

	public void GetScout()
	{
	}

	public void SendFriendship(int userId)
	{
	}

	public void ReceiveFriendship(int userId)
	{
	}

	public void HelpFightBossFriend(int userId, List<int> idHero)
	{
	}

	public void FightBossScout(List<int> idHero)
	{
	}

	public void SendAndReceiveHeart()
	{
	}

	public void SmashBossScout(List<int> info)
	{
	}

	public void SmashHelpFightBoss(int friendId, int numbAtk, List<int> heros)
	{
	}

	public void SendChatPrivateServer(long lastRequestTime, int typeMsg, int idHero, string msg = "")
	{
	}

	public void getHistoryChatPrivateServer(long lastRequestTime)
	{
	}

	public void getHistoryChatAdmin(long lastRequestTime)
	{
	}

	public void sendChatWorld(long lastRequestTime, int typeMsg, int idHero = 0, string msg = "")
	{
	}

	public void getHistoryChatWorld(long lastRequestTime)
	{
	}

	public void sendChatClan(long lastRequestTime, int typeMsg, int idHero = 0, string msg = "")
	{
	}

	public void getHistoryChatClan(long lastRequestTime)
	{
	}

	public void SetChatHidenVIP()
	{
	}

	public void getHistoryClanRecruit(long lastRequestTime)
	{
	}

	public void VerifyIAP(string receipt, string itemId)
	{
	}

	public void UpdateBonus()
	{
	}

	public void ReceiveBonusOnline()
	{
	}

	public void GetMidasStatus()
	{
	}

	public void ReceiveMidas(int index)
	{
	}

	public void SendFeedback(string feedback)
	{
	}

	public void GetCelestialStatus()
	{
	}

	public void TowerUpgrade()
	{
	}

	public void MakeMineField(int idPit, int typeMine)
	{
	}

	public void DismantleMine(int idPit)
	{
	}

	public void BuildingUpgade(int id, int typeBuilding)
	{
	}

	public void TechUpgade(int typeBuilding, int typeTech)
	{
	}

	public void GetMineField(int id, int typeMine)
	{
	}

	public void UpdateMine()
	{
	}

	public void GetIslandStatus()
	{
	}

	public void GetBossVoidStatus()
	{
	}

	public void AttackIsland(int id, List<int> teamUp)
	{
	}

	public void AutoAttackIsland(int id, int numberAtk, List<int> teamUp)
	{
	}

	public void SmashIsland(List<int> teamUp)
	{
	}

	public void UseAssistAtkBoss(int idUserFriend)
	{
	}

	public void BuyDaffofil()
	{
	}

	public void GetDaffodil()
	{
	}

	public void MasteringHeroStatus()
	{
	}

	public void MasteringHeroDetail(int heroKey)
	{
	}

	public void MasteringHeroGrandStatus()
	{
	}

	public void MasteringHeroCollectReward(int heroKey, int questId)
	{
	}

	public void RequestUpgradeHeroSpecialInfo(int idHero)
	{
	}

	public void RequestUpgradeHeroSpecialUpgrade(int idHero, int type)
	{
	}

	public void RequestUpgradeHeroSpecialReup(int idHero, int type, int isPreview)
	{
	}

	public void RequestUpgradeHeroSpecialEnhance(int idHero, int type)
	{
	}

	public void RequestUpgradeHeroSpecialFistUnlock(int idHero)
	{
	}

	public void AtkBrokenSpace(int eventId, int idMission, int numbAtk, List<int> heroIds)
	{
	}

	public void AtkBrokenSpaceNew(int eventId, int idMission, int numbAtk, List<int> heroIds)
	{
	}

	public void SendConfirmShowPopupNotifyNewEvent()
	{
	}

	public void RequestBuyHorn(int number, string urlBuyHorn, int eventId)
	{
	}

	public void GetInfoMonthCard()
	{
	}

	public void ShareBattle(long battleId, string msg, int positionShare = 0)
	{
	}

	public void GetListSpecialEvent(bool forceReload = false)
	{
	}

	public void RequestCheckPathPeekOfTime(List<Vector2> lstPath)
	{
	}
}
