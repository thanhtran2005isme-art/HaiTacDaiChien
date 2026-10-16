using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEvent2PackVertical : SpEventPanelBase
{
	[Serializable]
	private class DefineDataPackInView
	{
		public Image m_ImageBanner;

		public Text m_TextVipExp;

		public Text m_TextButtonAction;

		public Image m_ImageButtonPurchase;

		public Text m_TextLimit;

		public RectTransform m_TfItemContainer;

		public Image m_ImgIcon;

		public Text m_TextNamePack;
	}

	[SerializeField]
	private List<DefineDataPackInView> listPack;

	private List<DefineDataType3> m_ListData;

	private List<GameItem> m_ListGoGameItemLeft;

	private List<GameItem> m_ListGoGameItemRight;

	private bool stopUpdateBonus;

	private float timeRefresh;

	private string m_UrlEventDetail;

	public override void LoadEventDetail(string url)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void ParseData(string json)
	{
	}

	private void SetDataView()
	{
	}

	private void SetImageButtonBuy()
	{
	}

	private void Update()
	{
	}

	public void BuyPack(int id)
	{
	}

	public void StartUpdateBonus()
	{
	}
}
