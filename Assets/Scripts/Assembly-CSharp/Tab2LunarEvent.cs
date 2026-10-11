using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab2LunarEvent : LunarEventDefineTab
{
	[Serializable]
	public class DefineCoin
	{
		public Image m_ImgBG;

		public Text m_IndexValue;
	}

	[SerializeField]
	private GameObject m_GoContent;

	[SerializeField]
	private Text m_TextTiltle;

	[SerializeField]
	private List<DefineCoin> m_Coin;

	[SerializeField]
	private GameObject m_GoPanelShowList;

	[SerializeField]
	private Text m_TimeRemain;

	private static DataTabLunarEvent m_dataTab;

	private string m_DescHelp;

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

	public void HelpClick()
	{
	}
}
