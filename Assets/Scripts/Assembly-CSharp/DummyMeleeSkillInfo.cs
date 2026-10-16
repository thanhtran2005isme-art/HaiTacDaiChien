public class DummyMeleeSkillInfo : DummySkillInfo
{
	public float m_DelayHit;

	public EffectInfo m_EffectInfo;

	public SoundEffectInfo m_SoundEffInfo;

	public DummyMeleeSkillInfo(string aniAttack)
		: base(null)
	{
	}

	public override void PerformSkill(DummyBattleArena arena, DummyHeroBattle heroCast, float attackDmg)
	{
	}
}
