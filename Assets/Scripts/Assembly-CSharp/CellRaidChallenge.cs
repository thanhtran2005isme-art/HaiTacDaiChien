using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellRaidChallenge : MonoBehaviour
{
	public Text m_TextIndex;

	public Text m_TextPower;

	public GameObject m_GoButtonBattle;

	public GameObject m_GoGroupLocked;

	public GameObject m_GoGroupBattle;

	public Action<PopupFormation> ShowPopupFormation;

	public Text m_TextLocked;

	public List<GameObject> m_LstGameItemReward;

	private RaidChallengeInfo m_RaidInfo;

	public static int NumberSmashLastSelected;

	private void Start()
	{
	}

	public void SetData(RaidChallengeInfo raidInfo)
	{
	}

	public void OnSmashClick()
	{
	}

	public void OnBattleClick()
	{
	}

	private void Update()
	{
	}
}
