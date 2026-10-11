using UnityEngine;
using UnityEngine.UI;

public class PopupBossIsland : MonoEventHandler
{
	public Text txtTitle;

	public Text txtStamina;

	public Text textPercentHp;

	public Text m_LabelAssist;

	public Text m_LabelAssistGray;

	public Text m_TimeCountdown;

	public Text m_LabelAttack;

	public Text m_LabelAttackGray;

	public Text m_LabelSmash;

	public Text m_LabelSmashGray;

	public Slider imgFill;

	public HorizontalPoolGroup panelEnemy;

	public GameObject m_SmashGray;

	public GameObject m_AssistGray;

	public GameObject m_AtkGray;

	public Image m_IconPrice;

	private IslandModel bossData;

	public void FillData(IslandModel bossInfo, string title = "")
	{
	}

	public void SmashCick()
	{
	}

	public void BattleClick()
	{
	}

	public void AssistClick()
	{
	}

	public static PopupBossIsland ShowUp(IslandModel bossInfo, string title = "")
	{
		return null;
	}

	private void Update()
	{
	}
}
