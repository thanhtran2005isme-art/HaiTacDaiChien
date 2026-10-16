public class ItemShard : ItemModel
{
	public const int KEY_HERO_HEAD_4_STAR = 20000;

	public const int KEY_HERO_HEAD_5_STAR = 30000;

	public const int TYPE_SHARD_HERO = 2;

	public const int TYPE_SHARD_ARTIFACT = 3;

	public long m_Number;

	public int m_Star;

	public int m_RequireToMerge;

	public string m_ShortDesc;

	public BonusModel m_BonusPreview;

	public int m_ShardType;

	public const int HERO_SHARD_RANDOM_ID_START = 10000;

	public int m_HeroKey;

	public void SetShardKeyAndType(int itemKey, int type)
	{
	}

	public override string ToString()
	{
		return null;
	}

	public bool IsSame(int itemKey, int shardType)
	{
		return false;
	}

	public bool IsSame(ItemShard shard2)
	{
		return false;
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
