using System.Collections.Generic;
using GrepGame;

public class PopupSelectBattleCaseFake : GrepGame.BasePopup
{
	private BossFk m_BossFk;

	public void OnButtonAttackBossClick()
	{
	}

	public void OnButtonPlayMonopolyClick()
	{
	}

	public void OnButtonLockClick()
	{
	}

	public static PopupSelectBattleCaseFake ShowUp()
	{
		return null;
	}

	private void GenFkBattle(List<int> lstHeroKey, BossFk bossFk)
	{
	}

	private List<BattleEvent> GenListBattleEvent(List<HeroBattleInfo> lstHbiAttack, List<HeroBattleInfo> lstHbiGotHit, int atkTeamId, int gotHitTeamId, int round)
	{
		return null;
	}
}
