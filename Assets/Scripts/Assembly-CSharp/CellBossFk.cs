using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellBossFk : MonoBehaviour
{
	public CanvasHero m_CanvasHero;

	public Image m_ImageBgr;

	public GameObject m_GoShadeDeath;

	public GameObject m_GoShadeLocked;

	public GameObject m_GoGemReward;

	public Text m_TextGemReward;

	public GameObject m_GoButtonAttack;

	private BossFk m_BossFk;

	private void Start()
	{
	}

	public void SetBossData(BossFk bossFk)
	{
	}

	public void ButtonAttackClick()
	{
	}

	private void GenFkBattle(List<int> lstHeroKey, BossFk bossFk)
	{
	}

	private List<BattleEvent> GenListBattleEvent(List<HeroBattleInfo> lstHbiAttack, List<HeroBattleInfo> lstHbiGotHit, int atkTeamId, int gotHitTeamId, int round)
	{
		return null;
	}

	private void Update()
	{
	}
}
