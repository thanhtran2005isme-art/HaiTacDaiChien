using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellDailyQuest : MonoBehaviour
{
	public Text m_TextDesc;

	public Text m_TextProgress;

	public GameObject m_GoCheckCompleted;

	public GameObject m_GoButtonClaim;

	public GameObject m_GoButtonGoTo;

	public Slider m_SliderProgress;

	public List<GameObject> m_LstGameItemRewardContainer;

	private DailyQuestInfo m_QuestInfo;

	private const int DAILY_QUEST_COMPLETED_ALL = 0;

	private const int DAILY_QUEST_SMITH = 7;

	private const int DAILY_QUEST_LUCKY_SPIN = 6;

	private const int DAILY_QUEST_ARENA = 8;

	private const int DAILY_QUEST_EVENT_RAID = 10;

	private const int DAILY_QUEST_CAMPAIGN = 9;

	private const int DAILY_QUEST_SUMMON_BASIC = 1;

	private const int DAILY_QUEST_SUMMON_HEROIC = 2;

	private const int DAILY_QUEST_HAND_MIDAS = 3;

	private const int DAILY_QUEST_FRIEND_HEART = 5;

	private const int DAILY_QUEST_TARVEN = 4;

	private void Start()
	{
	}

	public void SetData(DailyQuestInfo questInfo)
	{
	}

	public void ButtonGoToClick()
	{
	}

	public void ButtonClaimClick()
	{
	}

	private void Update()
	{
	}
}
