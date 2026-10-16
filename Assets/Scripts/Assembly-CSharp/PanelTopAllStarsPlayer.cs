using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTopAllStarsPlayer : MonoEventHandler, ITabListener
{
	private const int TAB_BUTTON_TOP_RANK = 1;

	private const int TAB_BUTTON_TOP_POINT = 2;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_GoCellPrefab;

	public GameObject m_GoScrollView;

	public TextCountdown m_TextCountdown;

	public Text m_TextTimeCaption;

	public TabManager m_TabTop;

	private string MSG_LOCK_LINEUP_STAGE_1_16;

	protected override void Start()
	{
	}

	public void FirstLoadTopRank()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void ShowTopPlayer(List<ArenaPlayer> lstPlayer)
	{
	}

	public void SetCountDown(int countdown)
	{
	}

	public void SetCountDown(string timeCaption, int countdown)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
