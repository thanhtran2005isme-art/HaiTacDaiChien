using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGGiftCodeEvent : GGGameEventBehavior
	{
		public GameObject m_GroupGiftCode;

		public InputField m_InputGiftCode;

		public Image m_ImageBackground;

		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_EXCHANGE_GIFT_CODE;

		private string _urlAction;

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

		public void OnButtonActionClick()
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}
	}
}
