public class DummyPrefs
{
	private const string KEY_GOLD = "dummy_gold";

	private const string KEY_GEM = "dummy_gem";

	private const string KEY_CHIP = "dummy_chip";

	private const string KEY_FINISH_TUT = "dummy_first_help";

	public const string STAR_STAGE_PREFIX = "star_stage_";

	public const string HERO_LEVEL_PREFIX = "hero_level_";

	public const string HERO_UNLOCK_PREFIX = "hero_unlock_";

	public static void SaveInt(string key, int value)
	{
	}

	public static int GetInt(string key, int defaultValue)
	{
		return 0;
	}

	public static bool HasFinishTutorial()
	{
		return false;
	}

	public static void SetFinishTutorial(bool finished)
	{
	}

	public static void GemChanged(int changedValue)
	{
	}

	public static void GoldChanged(int changedValue)
	{
	}

	public static void ChipChanged(int changedValue)
	{
	}

	public static void SetGem(int gem)
	{
	}

	public static int GetGem()
	{
		return 0;
	}

	public static void SetGold(int gold)
	{
	}

	public static int GetGold()
	{
		return 0;
	}

	public static void SetChip(int chip)
	{
	}

	public static int GetChip()
	{
		return 0;
	}
}
