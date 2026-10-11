using UnityEngine.UI;

public class PopupHelp : BasePopup
{
	public const string KEY_HELP_SPECIAL_EQUIP = "key_help_special_equip";

	public const string KEY_CLIENT_CONFIG = "client_config";

	public const string KEY_HELP_SUMMON_RATE = "hero_summon";

	public const string KEY_HELP_PROPHET_RATE = "prophet";

	public const string KEY_HELP_TARVEN_RATE = "tarven";

	public const string KEY_HELP_GUILD_WAR = "guild_war";

	public const string KEY_HELP_CAMPAIGN = "auto_campaign";

	public const string KEY_HELP_ISLAND = "help_island";

	public const string KEY_HELP_SUMMON_SCROLL = "help_summon_scroll";

	public const string KEY_HELP_SUMMON_ORB = "help_summon_orb";

	public const string KEY_HELP_REPLACE_HERO = "help_replace_hero";

	public const string KEY_HELP_CRYSTAL_CROWN = "help_crystal_crown";

	public const string KEY_HELP_TRIAL_CHAMPION = "help_trial_champion";

	public const string KEY_HELP_ALL_STARS_LEAGUE = "help_allstars_league";

	public const string KEY_HELP_TOWER = "help_tower";

	public const string KEY_HELP_BRAVE_TRIAL = "help_brave_trial";

	public const string KEY_HELP_BRAVE_TRIAL_NEW = "help_brave_trial_new";

	public const string KEY_HELP_ASPEN_DUNGEON = "help_dungeon";

	public const string KEY_HELP_TARVEN = "help_tarven";

	public const string KEY_HELP_ALTAR = "help_altar";

	public const string KEY_HELP_BLACKSMITH = "help_blacksmith";

	public const string KEY_HELP_LUCKY_SPIN = "help_lucky_spin";

	public const string KEY_HELP_PET = "help_pet";

	public const string KEY_HELP_FUSE_HERO = "help_fuse_hero";

	public const string KEY_HELP_GUILD_BOSS = "help_guild_boss";

	public const string KEY_HELP_REGRESS_HERO = "help_regress_hero";

	public const string KEY_HELP_REGRESS_CHANGE_HERO = "help_regress_change_hero";

	public const string KEY_HELP_SEALEDLAND = "help_sealedland";

	public const string KEY_HELP_PRAY_FOR_FIRE = "help_pray_for_fire";

	public const string KEY_HELP_BRAVE_TRIAL_NEW_ALSTARS = "key_help_brave_trial_new_stars";

	public const string KEY_HELP_GUILD_SKILL = "help_guild_skill";

	public const string KEY_HELP_GUILD_HALL = "help_guild_hall";

	public const string KEY_HELP_UPGRADE_HERO = "key_help_ugrade_hero";

	public const string KEY_HELP_MARINEFORD = "key_help_marineford";

	public Text content_lb;

	public Text m_TextTitle;

	public void ShowContentHelp(string content)
	{
	}

	public void RequestHelpContent(string keyHelp)
	{
	}

	public void SetTitle(string title)
	{
	}

	protected override void Start()
	{
	}

	private void Update()
	{
	}

	public static PopupHelp showYourself(string content)
	{
		return null;
	}

	public static PopupHelp showYourselfWithKey(string keyHelp)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
