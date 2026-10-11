using UnityEngine;
using UnityEngine.UI;

public class PanelDailyReward : MonoBehaviour
{
	public Text m_Rank;

	public Text m_TimeCountdown;

	public Text m_TitleReward;

	public GameObject m_PreItemReward;

	public Transform m_PanelRewardCurr;

	public Transform m_PanelReward;

	private TimeStruct timeCountdown;

	public void FillData(int currRank)
	{
	}

	private long GetTimeCountdownUTC()
	{
		return 0L;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
