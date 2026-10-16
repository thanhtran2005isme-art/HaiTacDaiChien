public class MonsterModel
{
	public int m_MonsterId;

	public int m_HeroLink;

	public int m_Level;

	public int m_LevelShow;

	public int m_Star;

	public long m_Hp;

	public long m_MaxHp;

	public HeroFaction m_Faction;

	public void Init()
	{
	}

	private void SetFaction()
	{
	}

	public virtual MonsterModel Clone()
	{
		return null;
	}
}
