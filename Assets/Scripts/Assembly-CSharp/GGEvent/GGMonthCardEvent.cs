using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGMonthCardEvent : GGGameEventBehavior
	{
		public Text m_TextAction;

		public Text m_TextPrice;

		public Text m_TextDesc;

		public Image m_ImageBgr;

		public Image m_ImageButton;

		public GameObject m_GoButtonAction;

		public GameObject m_GoBoxPrice;

		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

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

		private void ParseEventDetail(string strJson)
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}

		public void ActionClick()
		{
		}
	}
}
