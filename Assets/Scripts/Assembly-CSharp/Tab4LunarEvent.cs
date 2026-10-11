using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class Tab4LunarEvent : LunarEventDefineTab
{
	[Serializable]
	public struct DefineDataPack
	{
		public Text m_TextExp;

		public Text m_TextTiltle;

		public Image m_ImgButtonBuy;

		public Text m_LabelButton;

		public Text m_TextLimit;

		public Image m_ImgPack;

		public Transform m_TfReward;
	}

	private class LunarEventDefineDataTab4
	{
		public string m_Image;

		public string m_Tiltle;

		public string m_Desc;

		public string m_ImgType;

		public LunarEventDataPack m_Pack;

		public void SetData(string m_Image, string m_Tiltle, string m_Desc, string m_ImgType, LunarEventDataPack Pack)
		{
		}
	}

	private class LunarEventDataPack
	{
		public int m_Id;

		public JSONArray m_Item;

		public int m_Quantity;

		public int m_Price;

		public int m_VipExp;

		public string m_Data;

		public string m_IapProductId;

		public void SetData(int m_Id, JSONArray m_Item, int m_Quantity, int m_Price, int m_VipExp, string m_Data, string m_IapProductId)
		{
		}
	}

	[SerializeField]
	private GameObject m_GoContent;

	[SerializeField]
	private Text m_TextTimeCountDown;

	[SerializeField]
	private List<DefineDataPack> m_dataPack;

	[SerializeField]
	private Text m_TextDescTop;

	[SerializeField]
	private Text m_TextTiltle;

	private static DataTabLunarEvent m_dataTab;

	private List<LunarEventDefineDataTab4> listData;

	private List<GameItem> m_ListGoGameItemTop;

	private List<GameItem> m_ListGoGameItemDown;

	public OnPublicDataCallback itemBuyCB;

	private bool stopUpdateBonus;

	private float timeRefresh;

	public override void GetDataTab(DataTabLunarEvent data)
	{
	}

	public void RequestData()
	{
	}

	protected override void Awake()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	protected override void ParseData(string json)
	{
	}

	private void SetDataView()
	{
	}

	public void BuyPack(int id)
	{
	}

	public void StartUpdateBonus()
	{
	}

	private void SetImageButtonBuy()
	{
	}

	private void Update()
	{
	}
}
