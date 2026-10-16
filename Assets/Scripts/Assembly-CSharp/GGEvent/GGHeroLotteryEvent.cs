using UnityEngine;

namespace GGEvent
{
	public class GGHeroLotteryEvent : GGGameEventBehavior
	{
		private static readonly int SV_LOAD_EVENT_DETAIL;

		public RectTransform m_TfScrollContent;

		public GameObject m_PrefabCellLottery;

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

		private void ParseEventDetail(string strJson)
		{
		}
	}
}
