using System.Collections.Generic;

public class BattleEvent
{
	public int m_BattleRound;

	public HeroFieldInfo m_HeroFieldInfoAction;

	public int m_RageHeroAction;

	public int m_SkillIndex;

	public List<HeroFieldInfo> m_LstHeroFieldInfoTarget;

	public BattleChainListEffect m_ChainListEffect;

	public List<KillStreakData> m_LstKillStreakData;
}
