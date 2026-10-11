using System.Collections.Generic;

public class GameFirstPurchaseEvent : GameEventData
{
	public string m_title;

	public string m_imgType;

	public string m_sumDesc;

	public string m_UrlNExt;

	public int sum;

	public List<accumulatedRewardList> lstAcc;
}
