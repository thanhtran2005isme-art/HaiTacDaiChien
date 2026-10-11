using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShowListRewardRandom : BasePopup
{
	[SerializeField]
	private Transform m_TfGroupRewards;

	[SerializeField]
	private Transform m_TfGroupRewardsRandom;

	[SerializeField]
	private Text m_Tiltle;

	[HideInInspector]
	public List<GameItem> m_ListGoGameItem;

	[HideInInspector]
	public List<GameItem> m_ListGoGameItemRandom;

	protected override void Start()
	{
	}

	public static PopupShowListRewardRandom ShowUp(string tiltle, List<BonusModel> lstBonus, List<BonusModel> lstBonusRandom, List<string> lstTextItem)
	{
		return null;
	}

	private void ShowBonus(string tiltle, List<BonusModel> lstItemBonus, List<BonusModel> lstItemBonusRandom, List<string> lstTextItem)
	{
	}
}
