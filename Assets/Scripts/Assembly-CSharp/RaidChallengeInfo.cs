public class RaidChallengeInfo
{
	public const int RAID_GOLD = 0;

	public const int RAID_SPIRIT = 1;

	public const int RAID_HERO = 2;

	public BonusModel m_BonusModel;

	public int m_Power;

	public int m_LevelRequire;

	public int m_RaidType;

	public int m_RaidIndex;

	public RaidChallengeInfo(int raidType, int raidIndex, int levelRequire, int power, BonusModel bonusModel)
	{
	}
}
