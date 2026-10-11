using pbdson;

public class SealedLandRewardMng
{
	public SealedLandMode normal;

	public SealedLandMode elite;

	private static SealedLandRewardMng m_Instance;

	public static SealedLandRewardMng Instance => null;

	private SealedLandRewardMng()
	{
	}

	public void ReadRewardFromServer(ListCommonVector lstCv)
	{
	}

	public SealedLandFactionReward GetRewardForFaction(HeroFaction faction)
	{
		return null;
	}

	public bool HasLoadedData()
	{
		return false;
	}
}
