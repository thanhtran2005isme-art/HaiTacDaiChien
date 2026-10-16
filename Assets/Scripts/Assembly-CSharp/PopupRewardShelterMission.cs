using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupRewardShelterMission : MonoBehaviour
{
	public Text m_TitleReward;

	public Text m_TitleReq;

	public Text m_ActionLabel;

	public HorizontalPoolGroup m_PoolGroupReward;

	public Transform m_PanelReq;

	public GameObject m_PreSlotHero;

	public Action<string, string, List<int>> m_ActionCb;

	private ExchangeHeroItem m_ExchangeItem;

	private string listItemSelect;

	private List<int> listDataHeroBonus;

	private int numberReq;

	private bool checkEnoughReqItem;

	public void ShowData(ExchangeHeroItem model)
	{
	}

	public void ActionClick()
	{
	}

	public static PopupRewardShelterMission ShowUp()
	{
		return null;
	}
}
