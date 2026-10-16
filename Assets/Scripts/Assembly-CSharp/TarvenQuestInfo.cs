using System.Collections.Generic;

public class TarvenQuestInfo
{
	public const int STATE_WAITING = 0;

	public const int STATE_ON_PROCESS = 1;

	public const int STATE_COMPLETED = 2;

	public int m_QuestId;

	public int m_QuestStar;

	public ItemModel m_RewardItem;

	public List<QuestRequirement> m_LstRequirement;

	public bool m_IsLocked;

	public int m_QuestState;

	public int m_TimeTotal;

	public int m_TimeElapsed;

	public int m_GemSpeedUp;

	public TarvenQuestInfo(int questId, int questStar, int questState, List<QuestRequirement> lstRequirement, int timeTotal, int timeElapsed = 0)
	{
	}

	public void StartQuest()
	{
	}

	public static TarvenQuestInfo GenRandomQuest()
	{
		return null;
	}
}
