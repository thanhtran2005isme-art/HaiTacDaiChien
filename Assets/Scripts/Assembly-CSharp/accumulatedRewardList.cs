using SimpleJSON;

public class accumulatedRewardList
{
	public int m_Id;

	public int m_Sum;

	public string m_SumDesc;

	public int m_Threshold;

	public string m_ThresholdDesc;

	public JSONArray m_Reward;

	public string ButtonLabel;

	public string m_UrlNext;

	public accumulatedRewardList(int m_Id, int m_Sum, string m_SumDesc, int m_Threshold, string m_ThresholdDesc, JSONArray m_Reward, string ButtonLabel, string m_UrlNext)
	{
	}

	public accumulatedRewardList()
	{
	}
}
