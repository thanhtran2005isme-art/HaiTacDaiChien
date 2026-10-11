using System.Collections.Generic;
using UnityEngine;

public class PopupBattleDetail : BasePopup, ITabListener
{
	public GameObject m_PrefabCellAttacker;

	public GameObject m_PrefabCellDefender;

	public GameObject m_GoGroupAttacker;

	public GameObject m_GoGroupDefender;

	public TabManager m_TabManager;

	private const int TAB_DAMAGE = 1;

	private const int TAB_HURT = 2;

	private const int TAB_HEAL = 3;

	private BattlePointDetail m_BattlePointDetail;

	private List<CellDetailBattle> m_LstCellAttacker;

	private List<CellDetailBattle> m_LstCellDefender;

	private CellDetailBattle m_CellPetAttacker;

	private CellDetailBattle m_CellPetDefender;

	protected override void Start()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void LoadData(BattlePointDetail battlePoint)
	{
	}

	public static PopupBattleDetail ShowUp(BattlePointDetail battlePointDetail)
	{
		return null;
	}

	private void Update()
	{
	}
}
