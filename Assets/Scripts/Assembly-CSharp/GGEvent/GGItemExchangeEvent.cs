using System.Collections.Generic;
using UnityEngine;

namespace GGEvent
{
	public class GGItemExchangeEvent : GGGameEventBehavior
	{
		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_EXCHANGE_ITEM;

		public RectTransform m_TfScrollContent;

		public GameObject m_PrefabCellItem;

		public GameObject m_PrefabGridItem;

		private bool _firstLoad;

		private List<GameObject> _listGoCellItem;

		private GGBannerEvent _bannerEvent;

		protected override void Start()
		{
		}

		public override void SetEventInfo(GGEventHeadlineInfo eventHeadlineInfo)
		{
		}

		private void InitContent()
		{
		}

		private void ReloadEvent()
		{
		}

		private void SetDataEvent(GGItemExchangeEventData eventData)
		{
		}

		private void UpdateDataEvent(GGItemExchangeEventData eventData)
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}

		private GGItemExchangeEventData ParseEventDetail(string strJson)
		{
			return null;
		}

		private void ItemExchangeClick(GGItemExchangeElement itmExchange)
		{
		}

		private void RequestExchangeItem(GGItemExchangeElement itemExchange, int number)
		{
		}
	}
}
