using System.Collections.Generic;

public class BaTeamMoveBack : BattleAction
{
	private BattlePet m_PetAction;

	private List<BattleHero> m_LstAllHero;

	public BaTeamMoveBack(BattlePet petAction, float duration, List<BattleHero> lstAllHeroes)
		: base(null, 0f)
	{
	}

	public override void PerformAction()
	{
	}
}
