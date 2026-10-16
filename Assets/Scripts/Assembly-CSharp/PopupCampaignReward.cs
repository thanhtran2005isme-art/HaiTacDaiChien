using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupCampaignReward : BasePopup
{
	public Transform m_TransformLoot;

	public GameObject m_GoScrollView;

	public Action m_CloseClickCb;

	public Text m_TextTimeAfk;

	private int m_CountTimeAfk;

	public Text m_TextGoldPerMin;

	public Text m_TextSpiritPerMin;

	public Text m_TextExpPerMin;

	public Text m_TextContent;

	private ActionRepeatTimer m_TimerCounter;

	private int m_MaxSecAfk;

	protected override void Start()
	{
	}

	public void LoadData(BonusModel bm, int secAfk, int maxSecAfk, int levelCampaignId)
	{
	}

	private int GetRewardForVip(int vip)
	{
		return 0;
	}

	public void ButtonCloseClick()
	{
	}

	private void Update()
	{
	}

	public static PopupCampaignReward ShowUp(BonusModel bonus, int secAfk, int maxSecAfk, int levelCampaignId)
	{
		return null;
	}
}
