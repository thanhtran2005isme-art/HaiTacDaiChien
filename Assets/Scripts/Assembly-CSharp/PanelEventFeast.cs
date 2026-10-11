using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEventFeast : SpEventPanelBase
{
	[Serializable]
	private class DefineCoin
	{
		public Text m_textIndexCoin;

		public Image m_imgCoin;
	}

	[SerializeField]
	private Text m_TextTiltle;

	[SerializeField]
	private List<DefineCoin> m_Coin;

	[SerializeField]
	private Image m_ImgTiltle;

	[SerializeField]
	private VerticalPoolGroup m_PoolItem;

	[SerializeField]
	private GameObject m_GoPrefabCellItem;

	private string m_DescHelp;

	private bool CheckReLoad;

	private List<DefineDataType2> listItem;

	private string m_UrlEventDetail;

	public override void LoadEventDetail(string url)
	{
	}

	private void ReloadEventDetail()
	{
	}

	protected override void Awake()
	{
	}

	public void HelpClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void ParseData(string json)
	{
	}

	private List<DefineDataType2> ParseListData(string json)
	{
		return null;
	}

	private void OnTabItemClick(int id, int limit)
	{
	}
}
