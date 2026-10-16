using System.Collections.Generic;

public class GameFightBossEvent : GameEventData
{
	public float m_DurationEffect;

	public float m_Scale;

	public string m_UrlHitBoss;

	public string m_ActionTitle;

	public string m_UrlGetReward;

	public string m_PathBoss;

	public int m_TotalHp;

	public int m_LostHp;

	public int m_FeatCostPerHit;

	public int m_IdBoss;

	public List<ItemModel> m_LstReward;

	public List<ItemModel> m_LstSpecialReward;

	public List<int> m_LstSpecialRewardPercent;
}
