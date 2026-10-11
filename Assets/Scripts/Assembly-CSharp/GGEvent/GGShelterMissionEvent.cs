using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGShelterMissionEvent : GGGameEventBehavior
	{
		public GameObject m_GoContent;

		public Image m_ImageBgr;

		public Text m_TextTimeCaption;

		public TextCountdown m_CountdownText;

		public DotIndicator m_DotIndicator;

		public GameObject m_GoButtonNext;

		public GameObject m_GoButtonPrev;

		public List<GameObject> m_ListGoShelterHero;

		private static readonly int SV_STARTER;

		private static readonly int SV_LOAD_EVENT_DETAIL;

		private static readonly int SV_EXCHANGE_HERO;

		private List<GGHeroExchangeElement> _listExchangeHeroElement;

		private int _totalPage;

		private int _currentPageIdx;

		private List<int> _listSendHeroId;

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

		private void LoadPage(int pageIdx)
		{
		}

		public void ExchangeHeroClick(GGHeroExchangeElement exchangeData)
		{
		}

		public void ButtonNextClick()
		{
		}

		public void ButtonPrevClick()
		{
		}

		public override void ProcessKEvent(int serviceId, object data)
		{
		}
	}
}
