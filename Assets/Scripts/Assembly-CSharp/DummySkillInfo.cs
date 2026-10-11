public abstract class DummySkillInfo
{
	public string m_AnimationAttack;

	public DummySkillInfo(string animationAttack)
	{
	}

	public abstract void PerformSkill(DummyBattleArena arena, DummyHeroBattle heroCast, float attackDmg);
}
