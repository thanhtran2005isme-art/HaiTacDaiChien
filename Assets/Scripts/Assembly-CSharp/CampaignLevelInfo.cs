using System.Collections.Generic;

public class CampaignLevelInfo
{
	public int m_LevelId;

	public int m_RequireLevel;

	public List<int> m_LstMonsterId;

	public List<int> m_LstItemEquipmentKey;

	public List<int> m_LstItemConsumableKey;

	public List<int> m_LstItemShardHeroKey;

	public List<int> m_LstItemShardArtifactKey;

	public List<int> m_LstArtifactKey;

	public int m_GoldPer5s;

	public int m_ExpPer5s;

	public int m_SpiritPer5s;

	public CampaignBeatingLevelReward m_RewardBeatingLevel;
}
