using UnityEngine;

namespace GGEvent
{
	public abstract class GGGameEventBehavior : MonoEventHandler
	{
		private ActionRepeatTimer m_ActionCheckBonus;

		private bool _needCheckForBonus;

		protected GGEventHeadlineInfo _eventHeadlineInfo;

		protected override void Start()
		{
		}

		public virtual void SetEventInfo(GGEventHeadlineInfo eventHeadlineInfo)
		{
		}

		protected void StartCheckForBonusPeriodically()
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}

		protected GameObject GenBanner(RectTransform tfParent)
		{
			return null;
		}

		protected void UpdateAndShowEventReward(string strJson)
		{
		}

		private void Update()
		{
		}
	}
}
