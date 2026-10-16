using System.Collections.Generic;

public class BossFk
{
	public const int BOSS_DEATH = 1;

	public const int BOSS_ON_FIGHT = 2;

	public const int BOSS_LOCKED = 3;

	public int m_Id;

	public List<int> m_LstHeroId;

	public long m_BossAtk;

	public long m_BossHp;

	public int m_GemReward;

	public int m_BossState;

	public int GetHeroIdAvatar()
	{
		return 0;
	}
}
