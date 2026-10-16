using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupPlayerInfo3 : BasePopup
{
	public PlayerAvatar m_PlayerAvatar;

	public GameObject m_GoTextGuildCap;

	public Text m_TextPlayername;

	public Text m_TextGuildname;

	public Text m_TextId;

	public Text m_TextPower;

	public GameObject m_GoHeadPet1;

	public GameObject m_GoHeadPet2;

	public GameObject m_GoHeadPet3;

	public HeroesLineup m_HeroesLineup1;

	public HeroesLineup m_HeroesLineup2;

	public HeroesLineup m_HeroesLineup3;

	public GameObject m_GoButton1;

	public Text m_TextAction1;

	private Action m_Cb1;

	protected override void Start()
	{
	}

	public static PopupPlayerInfo3 ShowUp(PlayerInfoData playerInfo)
	{
		return null;
	}

	public static PopupPlayerInfo3 ShowUp(int userId, LineupType lineupType)
	{
		return null;
	}

	private void RequestPlayerInfo(int userId, LineupType lineupType)
	{
	}

	public void SetPlayerInfo(PlayerInfoData info)
	{
	}

	public void SetAction(string strAction, Action callback)
	{
	}

	public void ButtonAction1Click()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ButtonCloseClick()
	{
	}

	private void Update()
	{
	}
}
