using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using pbdson;

public class PopupPrepareGuildWar : BasePopup
{
	public ScrollRect m_ScrollRect;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_CellPrefab;

	public Text m_TextNumber;

	public TextCountdown m_TextCountdown;

	public GameObject m_GoButtonApply;

	public GameObject m_GoButtonRegister;

	private List<PlayerGuildWarRegister> m_LstPlayer;

	private int m_NumberMemberRequire;

	public static PopupPrepareGuildWar Instance;

	protected override void Start()
	{
	}

	public void ButtonHelpClick()
	{
	}

	public void ButtonWarHistoryClick()
	{
	}

	public void ButtonWarRewardClick()
	{
	}

	private void ToggleSelectChanged(CellWarRegister cellWarRegister, bool check)
	{
	}

	public bool HasSelectedEnoughMember()
	{
		return false;
	}

	private void CountNumberSelected()
	{
	}

	public void ButtonRegisterClick()
	{
	}

	public void ButtonApllyClick()
	{
	}

	public void ButtonScoutClick()
	{
	}

	public void SetDataStatus(ListCommonVector lstCv)
	{
	}

	private void TestData()
	{
	}

	public static PopupPrepareGuildWar ShowUp(ListCommonVector lstCv)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
