using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHeroNew : MonoBehaviour
{
	public CanvasHero m_CanvasHero;

	public Text m_TextLevel;

	public GameObject m_GroupReward;

	public ItemConsumableUI m_ItemGoldUI;

	public ItemConsumableUI m_ItemPromoStoneUI;

	private List<GameObject> lstRewardCreate;

	private TowerLevelInfo _levelInfo;

	private void Start()
	{
	}

	public void SetHeroInfo(int heroKeyTower, int towerLevelId)
	{
	}

	public void LoadRewardLevel(TowerLevelInfo levelInfo)
	{
	}

	private void Update()
	{
	}
}
