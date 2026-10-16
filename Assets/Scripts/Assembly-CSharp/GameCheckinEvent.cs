using System.Collections.Generic;

public class GameCheckinEvent : GameEventData
{
	public string m_ReceiveButtonLabel;

	public List<CheckinReward> m_LstRewardData;

	public static GameCheckinEvent GenTestData(int eventId)
	{
		return null;
	}
}
