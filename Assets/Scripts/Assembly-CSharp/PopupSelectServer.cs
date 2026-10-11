using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupSelectServer : BasePopup
{
	private const int TAB_BTN_RECOMMEND = 2;

	private const int TAB_BTN_HAS_CHARACTER = 1;

	private const int TAB_BTN_ALL = 3;

	public GridPoolGroup m_PoolGroup;

	public Action<ServerInfo> m_ServerSelectedCb;

	public GameObject m_GoPrefabCellServer;

	public Action cbClickChangeServer;

	public TabManager m_TabMng;

	private List<ServerInfo> _lstInputServer;

	private List<ServerInfo> _lstAllServer;

	private List<ServerInfo> _lstRecommendServer;

	private List<ServerInfo> _lstHasCharacterServer;

	protected override void Awake()
	{
	}

	public void OnTabClick(int btnTabId)
	{
	}

	protected override void Start()
	{
	}

	public void ShowListServer(List<ServerInfo> lstServer)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupSelectServer ShowUp(List<ServerInfo> lstServer)
	{
		return null;
	}
}
