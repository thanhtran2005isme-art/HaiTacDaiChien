using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGOnePackEvent : GGGameEventBehavior
	{
		public GameObject m_GoContent;

		public RectTransform m_TfRewards;

		public Text m_TextLimit;

		public Text m_TextAction;

		public Image m_ImageBgr;

		public ScrollRect m_ScrollRect;

		private GGPackItemInfo _packInfo;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		public override void SetEventInfo(GGEventHeadlineInfo eventHeadlineInfo)
		{
		}

		private void ClearContent()
		{
		}

		private void ReloadEvent()
		{
		}

		private void ParseEventDetail(string strJson)
		{
		}

		public void ButtonActionClick()
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}
	}
}
