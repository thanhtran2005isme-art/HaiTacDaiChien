using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupAtkBossScout : MonoBehaviour
{
	public Text txtStamina;

	public Text textPercentHp;

	public Text m_TextTitleReward;

	public Text m_TextDesc;

	public Slider imgFill;

	public HorizontalPoolGroup panelEnemy;

	public GameObject m_GroupReward;

	public Transform m_PanelReward;

	public GameObject m_SmashButton;

	public GameObject m_AttackButton;

	private OnPublicDataCallback smashCallback;

	private OnPublicDataCallback battleCallback;

	private void FillData(List<MonsterModel> listEnemy, int currStamina, int limitLevelAtkBoss)
	{
	}

	public void SetAtkCallback(OnPublicDataCallback callback)
	{
	}

	public void SetSmashCallback(OnPublicDataCallback callback)
	{
	}

	public void SmashCick()
	{
	}

	public void BattleClick()
	{
	}

	public static PopupAtkBossScout ShowUp(List<MonsterModel> listEnemy, int currStamina, int limitLevelAtkBoss)
	{
		return null;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void UpdateStamina()
	{
	}
}
