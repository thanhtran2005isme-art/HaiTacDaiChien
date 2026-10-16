using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class Tab3LunarEvent : LunarEventDefineTab
{
	[SerializeField]
	private GameObject m_GoContent;

	[SerializeField]
	private Text m_TextTiltle;

	[SerializeField]
	private Image m_ImgCoin;

	[SerializeField]
	private Text m_IndexCoin;

	[SerializeField]
	private GameObject m_GoPanelShowList;

	[SerializeField]
	private Text m_TimeRemain;

	private static DataTabLunarEvent m_dataTab;

	private JSONArray cost;

	private string m_ImageItemType;

	private string m_UrlBuyItem;

	private int ITEM_KEY;

	protected override void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public override void GetDataTab(DataTabLunarEvent data)
	{
	}

	public void RequestData()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void ParseDataBuyItem(string json)
	{
	}

	protected override void ParseData(string json)
	{
	}

	private List<DefineDataType2> ParseListItem(string json)
	{
		return null;
	}

	private void Update()
	{
	}

	public void ButtonBuyItemClick()
	{
	}

	public void ButtonEventClick()
	{
	}

	private void RequestBuyItem(int num)
	{
	}
}
