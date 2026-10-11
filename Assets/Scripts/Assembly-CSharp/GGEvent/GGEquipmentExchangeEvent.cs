using System.Collections.Generic;
using UnityEngine;

namespace GGEvent
{
	public class GGEquipmentExchangeEvent : GGGameEventBehavior
	{
		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_EXCHANGE_EQUIPMENT;

		public RectTransform m_TfScrollContent;

		public GameObject m_PrefabCellItem;

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

		private void SetDataEvent(GGEquipmentExchangeEventData eventData)
		{
		}

		private void UpdateDataEvent(GGEquipmentExchangeEventData eventData)
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}

		private GGEquipmentExchangeEventData ParseEventDetail(string strJson)
		{
			return null;
		}

		private void EquipmentExchangeClick(GGEquipmentExchangeElement itmExchange)
		{
		}

		private void RequestExchangeItem(GGEquipmentExchangeElement itemExchange)
		{
		}
	}
}
