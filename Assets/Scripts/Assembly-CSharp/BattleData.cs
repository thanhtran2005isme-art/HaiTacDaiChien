using System.Collections.Generic;
using pbdson;

public class BattleData
{
	public int m_TeamIdWin;

	public int m_AuraTeam1;

	public int m_AuraTeam2;

	public PetModel m_PetModel_T1;

	public PetModel m_PetModel_T2;

	public List<HeroBattleInfo> m_LstHeroInfo_T1;

	public List<HeroBattleInfo> m_LstHeroInfo_T2;

	public List<BattleEvent> m_LstBattleEvent;

	public BattlePointDetail m_PointDetail;

	public void SetShowTotalDamage()
	{
	}

	public bool IsTeam1Win()
	{
		return false;
	}

	public static BattleData GenTestBattleData()
	{
		return null;
	}

	public static BattleData ParsePbBattleInfo(PbBattleResult pbResult)
	{
		return null;
	}

	private static void PrintPbBattle(PbBattleResult pbResult)
	{
	}

	private static void PrintBattleHeroPoint(PbBattleHero pbHero)
	{
	}
}
