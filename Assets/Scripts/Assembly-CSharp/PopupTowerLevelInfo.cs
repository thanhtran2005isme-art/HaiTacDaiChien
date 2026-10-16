using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupTowerLevelInfo : BasePopup
{
	public Text m_TextTitle;

	public Text m_TextPower;

	public List<GameObject> m_LstGoHeroHead;

	public List<GameObject> m_LstGoHeroHead2;

	public GameObject m_BoxHeroHead2;

	public GameObject m_GroupReward;

	public ItemConsumableUI m_ItemGoldUI;

	public ItemConsumableUI m_ItemPromoStoneUI;

	public GameObject m_GoButtonAttack;

	protected override void Start()
	{
	}

	public void LoadTowerLevel(TowerLevelInfo levelInfo)
	{
	}

	private void FillMonsterIdToListHeroHead(List<int> lstMonsterId, List<GameObject> lstGoHeroHead, int heroLevel)
	{
	}

	public void ShowButtonAttack(bool showAttack)
	{
	}

	public void ButtonBackClick()
	{
	}

	public static PopupTowerLevelInfo ShowUp(TowerLevelInfo lvInfo)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
