public class CampaignGateInfo
{
	public string m_MapName;

	public int m_MapImageId;

	public int m_MapIndex;

	public bool m_IsLocked;

	public bool m_OnBattle;

	public bool m_IsLastestGate;

	public bool m_IsOnLeftOfLastestGate;

	public CampaignGateInfo(int mapIndex, string mapName, int mapImageId, bool locked = false)
	{
	}
}
