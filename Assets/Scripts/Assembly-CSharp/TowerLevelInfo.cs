using System.Collections.Generic;

public class TowerLevelInfo
{
	public int m_TypeTower;

	public int m_LevelId;

	public int m_HeroLevel;

	public int m_PowerLevel;

	public List<int> m_LstMonsterId;

	public List<int> m_LstMonsterId2;

	public List<long> m_LstDataBonusDaily;

	public List<long> m_LstDataBonusWin;

	public int m_Gold;

	public int m_PromotionStone;

	public int m_HeroShardKey;

	public int m_HeroShardNumber;

	public int m_EquipmentKey;

	public int m_ItemConsumableKey;

	public int m_ItemConsumableNumber;

	public int m_ItemShardKey;

	public int m_ItemShardNumber;

	public BonusModel GetRewardWinning()
	{
		return null;
	}
}
