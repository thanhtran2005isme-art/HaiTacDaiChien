public class DummyShotSkillInfo : DummySkillInfo
{
	public float m_DelayFire;

	public EffectInfo m_EffectCastInfo;

	public EffectInfo m_EffectHitInfo;

	public SoundEffectInfo m_SoundEffCastInfo;

	public SkillObjectInfo m_ButtletObjectInfo;

	public DummyShotSkillInfo(string aniAttack)
		: base(null)
	{
	}

	public override void PerformSkill(DummyBattleArena arena, DummyHeroBattle heroCast, float attackDmg)
	{
	}
}
