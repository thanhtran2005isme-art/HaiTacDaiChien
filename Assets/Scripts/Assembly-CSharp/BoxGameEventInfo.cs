using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxGameEventInfo : MonoEventHandler
{
	public Image m_ImageFeat;

	public Text m_TextTitle;

	public Text m_TextTime;

	public Text m_TextTimeDesc;

	public Text m_TextFeat;

	public GameObject m_GroupVertical;

	public Image m_ImageBgr;

	public GameObject m_GoFeat;

	public GameObject m_BtnAddFeat;

	[HideInInspector]
	public TimeStruct m_TimeLeft;

	private string m_Desc;

	public GameObject m_GoButtonHelp;

	public GameObject m_GoButtonHelpVer;

	public GameObject m_GoButtonReward;

	public GameObject m_GoButtonRewardVer;

	public GameObject m_GoButtonAction;

	public GameObject m_GoButtonActionVer;

	private Action m_BxhClickCallback;

	private Action m_RewardClickCallback;

	private Action m_ActionClickCallback;

	private int m_CurrentEventSubId;

	private List<GameEventInfo> m_LstEventInfoSub;

	private bool showFeat;

	private bool showGroupBtnVertical;

	private string exchangeUrl;

	private int evenId;

	private string m_textNumberHornBuy;

	private int m_MaxItem;

	private string m_ArrCostBuyFeat;

	private BonusModel featDesc;

	public void SetBackground(string pathBgr)
	{
	}

	public void ButtonBuyClick()
	{
	}

	public void SetDataBuyItem(BonusModel featDesc, string exchangeUrl = "", int evenId = -1, string m_textNumberHornBuy = "", int m_MaxItem = -1, string m_ArrCostBuyFeat = "")
	{
	}

	public void SetInfo(string eventTitle, TimeStruct timeLeft, string timeDesc, string featDesc, string pathIconFeat, string strDesc = "", bool showGrBtnVertical = false)
	{
	}

	public void SetInfo(string eventTitle, TimeStruct timeLeft, string timeDesc, BonusModel featDesc, string pathIconFeat, string strDesc = "", bool showGrBtnVertical = false)
	{
	}

	public void SetInfo(string strDescLoop = "", bool showGrBtnVertical = false)
	{
	}

	public void SetShowGoFeat(bool isOn)
	{
	}

	public void SetFeatValue(int numFeat)
	{
	}

	public void ButtonHelpClick()
	{
	}

	public void SetRewardClickCallback(Action cb)
	{
	}

	public void SetActionClickCallback(Action cb, string label = "")
	{
	}

	public void ButtonRewardClick()
	{
	}

	public void ButtonActionClick()
	{
	}

	private string formatHourNumber(int value)
	{
		return null;
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
