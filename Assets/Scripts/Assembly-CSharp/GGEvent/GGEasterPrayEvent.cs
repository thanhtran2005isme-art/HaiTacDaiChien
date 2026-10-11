using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGEasterPrayEvent : GGGameEventBehavior
	{
		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_ACTIVE_STATUE;

		private static readonly int SV_REFRESH_STATUE;

		private static readonly int SV_EXCHANGE_ITEM_OF_STATUE;

		public GameObject m_GoContent;

		public Image m_ImageBgr;

		public Text m_TextNumberItem1;

		public Text m_TextNumberItem2;

		public Image m_ImageIconItem1;

		public Image m_ImageIconItem2;

		public ItemConsumable m_ItemConsumeActiveCost;

		public ItemConsumable m_ItemConsumeRefreshCost;

		public GGBoxEasterStatues m_BoxStatues;

		public GameObject m_GoButtonRefresh;

		public GameObject m_GoButtonActive;

		public GameObject m_GoButtonExchange;

		public Image m_ImageItemRefresh;

		public Image m_ImageItemActive;

		public Image m_ImageItemExchange;

		public Text m_TextItemRefreshCost;

		public Text m_TextItemActiveCost;

		public Text m_TextItemExchangeCost;

		public Text m_TextActionRefresh;

		public Text m_TextActionActive;

		public Text m_TextActionExchange;

		public Text m_TextLimit;

		private GGEasterPrayEventData _eventData;

		private bool _firstLoad;

		private GGEasterStatueData _currentStatueData;

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

		private void DelayReloadEvent(float delay)
		{
		}

		private void LoadEventData(GGEasterPrayEventData eventData)
		{
		}

		private void UpdateEventData(GGEasterPrayEventData eventData)
		{
		}

		public void ButtonLeftClick()
		{
		}

		public void ButtonRightClick()
		{
		}

		public void ButtonHelpClick()
		{
		}

		public void OnStatueChanged(GGEasterStatue statue)
		{
		}

		public void OnStatueClicked(GGEasterStatue statue)
		{
		}

		public void ButtonActiveClick()
		{
		}

		public void ButtonRefreshClick()
		{
		}

		public void ButtonExchangeClick()
		{
		}

		private GGEasterPrayEventData ParseEventDetail(string strJson)
		{
			return null;
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}
	}
}
