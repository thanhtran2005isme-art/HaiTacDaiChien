using System.Collections.Generic;

public class GuildWarData
{
	public List<GuildWarPlayer> m_LstPlayerAlly;

	public List<GuildWarPlayer> m_LstPlayerEnemy;

	public string m_ClanNameAlly;

	public string m_ClanNameEnemy;

	public int m_ClanIdAlly;

	public int m_ClanIdEnemy;

	public int m_ServerIdAlly;

	public int m_ServerIdEnemy;

	public int m_NumStarAlly;

	public int m_NumStarEnemy;

	public int m_MaxStar;

	public int m_GuildFlagAlly;

	public int m_GuildFlagEnemy;

	public int m_NumAttackAlly;

	public int m_NumAttackEnemy;

	public int m_TotalAttack;

	public int m_TimeCountdown;

	public static GuildWarData GenTestData()
	{
		return null;
	}
}
