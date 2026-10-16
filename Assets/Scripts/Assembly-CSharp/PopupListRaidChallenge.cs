using UnityEngine;
using UnityEngine.UI;

public class PopupListRaidChallenge : BasePopup
{
	public Text m_TextTitle;

	public Text m_TextBattleRemain;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject Content;

	private int m_RaidType;

	[HideInInspector]
	public int m_NumAttackLeft;

	[HideInInspector]
	public int m_MaxAttack;

	public static PopupListRaidChallenge Instance;

	public int m_NumBuyLeft;

	public int m_PrizeBuyAtk;

	protected override void Start()
	{
	}

	public void ButtonCloseClicked()
	{
	}

	public void LoadData(int raidType)
	{
	}

	public void ButtonBuyTurnClick()
	{
	}

	public static void ShowUp(int raidType)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
