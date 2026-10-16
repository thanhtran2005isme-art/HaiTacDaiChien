using UnityEngine;

namespace GGEvent
{
	public class GGAccumulatedEvent : GGGameEventBehavior
	{
		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_CALL_CELL_ACTION;

		public RectTransform m_TfScrollContent;

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

		public void PurchasePoupOpenSuccess()
		{
		}

		public void ItemActionClick(GGAccumulatedReward accReward)
		{
		}
	}
}
