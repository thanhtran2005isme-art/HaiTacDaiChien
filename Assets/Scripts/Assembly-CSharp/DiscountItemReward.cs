using System.Collections.Generic;

public class DiscountItemReward
{
	public string m_requireLvl;

	public string m_requireVIP;

	public List<long> m_RewardData;

	public string m_TextAction;

	public string m_UrlAction;

	public string m_discountPercent;

	public string m_fullPriceDesc;

	public string m_priceDesc;

	public string m_imgPrice;

	public int m_IndexReward;

	public DiscountItemReward(int indexReward, string requireLvl, string requireVIP, List<long> rewardData, string textAction, string urlAction, string discountPercent, string fullPrice, string price, string imgPrice)
	{
	}
}
