using System.Collections.Generic;

public class CheckinReward
{
	public List<int> m_RewardData;

	public string m_UrlAction;

	public string m_ActionText;

	public int m_IndexReward;

	public TimeStruct m_Countdown;

	public CheckinReward(int indexReward, List<int> rewardData, string actionText, string urlAction, long countdown)
	{
	}

	public bool BeAvailCheckInInFuture()
	{
		return false;
	}

	public bool AvaiableToCheckIn()
	{
		return false;
	}

	public bool CheckInWaiting()
	{
		return false;
	}
}
