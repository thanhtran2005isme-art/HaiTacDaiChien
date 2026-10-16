public class HeroFkModel
{
	public int m_Id;

	public string m_HeroName;

	public int m_BaseAtk;

	public int m_Atk;

	public int m_BaseHp;

	public int m_Hp;

	public int m_HpMax;

	public int m_UnlockFee;

	public bool m_TagSelected;

	public bool IsUnlocked()
	{
		return false;
	}

	public void CalculateInfo(int levelAtk, int levelHp)
	{
	}

	public HeroModel ToHeroModel()
	{
		return null;
	}
}
