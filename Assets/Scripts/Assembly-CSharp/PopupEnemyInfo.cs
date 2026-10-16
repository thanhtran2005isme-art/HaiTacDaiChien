using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupEnemyInfo : MonoBehaviour
{
	public Text txtTitle;

	public Text txtStamina;

	public Text textPercentHp;

	public Text m_TextTitleReward;

	public Slider imgFill;

	public HorizontalPoolGroup panelEnemy;

	public Action smashCallback;

	public Action battleCallback;

	public GameObject m_GroupReward;

	public GameObject m_SmashGray;

	public GameObject m_BattleGray;

	public Transform m_PanelReward;

	public Image m_IconPrice;

	public Button btnSmash;

	public Button btnBattle;

	public Text txtBtnSmash;

	public Text txtBtnBattle;

	private void FillData(List<MonsterModel> listEnemy, int currStamina, BonusModel bonus, Sprite imgTypePrice, string title, float percentHp)
	{
	}

	public void SmashCick()
	{
	}

	public void BattleClick()
	{
	}

	public void EnableAttack(bool checkBattle)
	{
	}

	public static PopupEnemyInfo ShowUp(List<MonsterModel> listEnemy, int currStamina, BonusModel bonus, Sprite imgTypePrice, string title = "", float percentHp = -1f)
	{
		return null;
	}

	public static PopupEnemyInfo ShowUp(List<MonsterModel> listEnemy, int currStamina, BonusModel bonus, Sprite imgTypePrice, bool allowAttack, string title = "", float percentHp = -1f)
	{
		return null;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
