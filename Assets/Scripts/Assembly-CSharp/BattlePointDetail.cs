using System.Collections.Generic;

public class BattlePointDetail
{
	public List<HeroBattlePointDetail> m_LstBattlePointAttacker;

	public List<HeroBattlePointDetail> m_LstBattlePointDefender;

	public PetBattlePointDetail m_PetBattlePointAttacker;

	public PetBattlePointDetail m_PetBattlePointDefender;

	public bool m_ShowTotalDamage;

	public static BattlePointDetail GenTest()
	{
		return null;
	}

	public long GetTotalDamageAttacker()
	{
		return 0L;
	}
}
