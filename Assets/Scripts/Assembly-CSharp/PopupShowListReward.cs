using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShowListReward : BasePopup
{
	[SerializeField]
	private Transform m_TfGroupRewards;

	[SerializeField]
	private Text m_Tiltle;

	[HideInInspector]
	public List<GameItem> m_ListGoGameItem;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupShowListReward ShowUp(string tiltle, List<BonusModel> lstBonus, bool chekReceived = false)
	{
		return null;
	}

	private void ShowBonus(string tiltle, List<BonusModel> lstItemBonus, bool chekReceived)
	{
	}
}
