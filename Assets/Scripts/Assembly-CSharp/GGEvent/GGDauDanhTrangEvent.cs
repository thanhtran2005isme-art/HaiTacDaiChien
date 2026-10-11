using System.Collections.Generic;
using UnityEngine;

namespace GGEvent
{
	public class GGDauDanhTrangEvent : GGGameEventBehavior
	{
		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		public RectTransform m_TfScrollContent;

		public GameObject m_PrefabCellItem;

		private bool _firstLoad;

		private List<GameObject> _listGoCellItem;

		private GGBannerEvent _bannerEvent;

		private GGDauDanhTrangEventData _eventData;

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

		private void SetDataEvent(GGDauDanhTrangEventData eventData)
		{
		}

		private void UpdateDataEvent(GGDauDanhTrangEventData eventData)
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}

		private GGDauDanhTrangEventData ParseEventDetail(string strJson)
		{
			return null;
		}

		private void PurchaseClick()
		{
		}
	}
}
