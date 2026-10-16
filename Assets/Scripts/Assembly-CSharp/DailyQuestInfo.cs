public class DailyQuestInfo
{
	public int m_QuestId;

	public string m_ChallengeDesc;

	public BonusModel m_BonusModel;

	public int m_ProgressValue;

	public int m_GoalValue;

	public bool m_OnTutorialCampaign;

	public MissionStatus m_Status;

	public DailyQuestInfo(int questId, string desc, int progressValue, int goalValue, BonusModel bonusModel)
	{
	}
}
