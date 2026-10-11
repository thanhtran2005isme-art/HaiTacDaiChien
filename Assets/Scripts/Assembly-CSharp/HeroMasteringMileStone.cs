public class HeroMasteringMileStone
{
	public const int STT_ON_PROGRESS = 0;

	public const int STT_REWARD_AVAIL = 1;

	public const int STT_COLLECTED = 2;

	public const int STT_NOT_REACHED = 3;

	public int m_Status;

	public int m_LastMileStonePoint;

	public int m_MileStonePoint;

	public BonusModel m_BonusReward;

	public int m_MileStoneIndex;

	public HeroMasteringMileStone()
	{
	}

	public HeroMasteringMileStone(int mileStoneIdx, int lastMileStone, int mileStone, BonusModel bonusReward)
	{
	}
}
