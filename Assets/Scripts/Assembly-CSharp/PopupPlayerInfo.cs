using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupPlayerInfo : BasePopup
{
	public PlayerInfoData m_PlayerInfoData;

	public PlayerAvatar m_PlayerAvatar;

	public GameObject m_GoTextGuildCap;

	public Text m_TextPlayername;

	public Text m_TextGuildname;

	public Text m_TextId;

	public Text m_TextPower;

	public GameObject m_GoHeadPet;

	public HeroesLineup m_HeroesLineup;

	public GameObject m_GoButton1;

	public GameObject m_GoButton2;

	public GameObject m_GoButton3;

	public Text m_TextAction1;

	public Text m_TextAction2;

	public Text m_TextAction3;

	private Action m_Cb1;

	private Action m_Cb2;

	private Action m_Cb3;

	public GameObject m_GoGroupAction;

	public RectTransform m_RectTfBody;

	protected override void Start()
	{
	}

	public static PopupPlayerInfo ShowUp(PlayerInfoData playerInfo)
	{
		return null;
	}

	public static PopupPlayerInfo ShowUp(int userId, LineupType lineupType)
	{
		return null;
	}

	private void RequestPlayerInfo(int userId, LineupType lineupType)
	{
	}

	public void SetPlayerInfo(PlayerInfoData info)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void SetAction(string strAction, Action callback)
	{
	}

	public void SetAction(string strAction1, Action callback1, string strAction2, Action callback2)
	{
	}

	public void SetAction(string strAction1, Action callback1, string strAction2, Action callback2, string strAction3, Action callback3)
	{
	}

	public void ButtonAction1Click()
	{
	}

	public void ButtonAction2Click()
	{
	}

	public void ButtonAction3Click()
	{
	}

	private void Update()
	{
	}
}
