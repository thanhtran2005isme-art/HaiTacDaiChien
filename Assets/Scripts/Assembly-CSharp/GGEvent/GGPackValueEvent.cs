using System.Collections.Generic;
using UnityEngine;

namespace GGEvent
{
	public class GGPackValueEvent : GGGameEventBehavior
	{
		private static readonly int SV_LOAD_EVENT_DETAIL;

		public RectTransform m_TfScrollContent;

		public GameObject m_PrefabCellPackValue;

		private List<GGPackItemInfo> _listPackInfo;

		private List<GameObject> _listGoCellPack;

		private bool _firstLoad;

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

		public override void ProcessKEvent(int serviceId, object data)
		{
		}

		private void ParseEventDetail(string strJson, bool refreshChange = false)
		{
		}

		public void ItemBuyClick(GGPackItemInfo packInfo)
		{
		}
	}
}
