public class GuildBoss
{
	public int m_HeroKey;

	public int m_BossId;

	public int m_Power;

	public float m_PercentHp;

	public bool m_Death;

	public bool m_OnBattle;

	public int m_CountFreeAttack;

	public int m_GemCostAttack;

	public int m_TypeBoss;

	public int m_PrayState;

	public int m_CountDownPray;

	public const int PRAY_STATE_SIGNUP = 1;

	public const int PRAY_STATE_BATTLE = 2;

	public const int BOSS_NORMAL = 1;

	public const int BOSS_PRAY = 2;

	public GuildBoss(int bossId, int heroKey, int power, float percentHp)
	{
	}

	public GuildBoss(int prayState, int countDown, int bossId)
	{
	}
}
