public class ItemConsumable : ItemModel
{
	public const int ITEM_KEY_GEM = 1000;

	public const int ITEM_KEY_GOLD = 1001;

	public const int ITEM_KEY_EXP = 1002;

	public const int ITEM_KEY_SPIRIT = 1;

	public const int ITEM_KEY_PROMOTION_STONE = 2;

	public const int ITEM_KEY_MAGIC_DUST = 3;

	public const int ITEM_KEY_ARENA_TICKET = 4;

	public const int ITEM_KEY_STONE_OF_VOID = 5;

	public const int ITEM_KEY_GUILD_COIN = 6;

	public const int ITEM_KEY_SOUL_STONE_SHARD = 7;

	public const int ITEM_KEY_DRAGON_SCALE = 8;

	public const int ITEM_KEY_PROPHET_BLESSING = 9;

	public const int ITEM_KEY_CHIP = 10;

	public const int ITEM_KEY_MONSTER_SOUL = 11;

	public const int ITEM_KEY_CHAOS_STONE = 12;

	public const int ITEM_KEY_SUPER_CHIP = 13;

	public const int ITEM_KEY_BASIC_SUMMON_SCROLL = 14;

	public const int ITEM_KEY_HEROIC_SUMMON_SCROLL = 15;

	public const int ITEM_KEY_PROPHET_ORB = 16;

	public const int ITEM_KEY_BASIC_QUEST_SCROLL = 17;

	public const int ITEM_KEY_SENIOR_QUEST_SCROLL = 18;

	public const int ITEM_KEY_GLORY_COIN = 19;

	public const int ITEM_KEY_CHALLENGE_BADGE = 20;

	public const int ITEM_KEY_HEART = 21;

	public const int ITEM_KEY_LUCK_COIN = 22;

	public const int ITEM_KEY_GUILD_EXP = 23;

	public const int ITEM_KEY_DAFFODIL = 24;

	public const int ITEM_KEY_BOSS_MIDAS = 25;

	public const int ITEM_KEY_REGRESSION_STONE = 33;

	public const int ITEM_KEY_BIRD_FEATHER = 36;

	public const int ITEM_KEY_ALL_STARS_COIN = 40;

	public const int ITEM_KEY_SEAL_BOOK_NORMAL = 49;

	public const int ITEM_KEY_SEAL_BOOK_DL = 50;

	public const int ITEM_KEY_DA_NANG_DO = 2028;

	public const int ITEM_KEY_MARINEFORD_COIN = 2058;

	public const int TYPE_ITEM_CONSUMABLE = 1;

	public const int TYPE_CONSUME_NORMAL = 1;

	public const int TYPE_CONSUME_USE_DIRECTLY = 2;

	public const int TYPE_CONSUME_CHOOSE_ONE = 3;

	public const int TYPE_CONSUME_MAKE_HERO_SKIN = 4;

	public long m_Number;

	public string m_ShortDesc;

	public int m_TypeConsume;

	public BonusModel m_BonusPreview;

	public override string ToString()
	{
		return null;
	}

	public override long GetNumber()
	{
		return 0L;
	}

	public bool HasPreview()
	{
		return false;
	}
}
