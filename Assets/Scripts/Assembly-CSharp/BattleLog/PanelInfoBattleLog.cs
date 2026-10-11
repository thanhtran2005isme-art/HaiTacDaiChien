using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleLog
{
	public class PanelInfoBattleLog : MonoEventHandler, ITabListener
	{
		[SerializeField]
		private GameObject m_GroupAtk;

		[SerializeField]
		private GameObject m_GroupReview;

		[SerializeField]
		private GameObject m_GroupAttackTower;

		[SerializeField]
		private InputField m_InUserId;

		[SerializeField]
		private InputField m_InNumberAtk;

		[SerializeField]
		private InputField m_InGroupBattle;

		[SerializeField]
		private Dropdown m_DropDown;

		[SerializeField]
		private GameObject m_GoPanelHistory;

		[SerializeField]
		private GameObject m_GoPanelHistoryDetail;

		[SerializeField]
		private GameObject m_GoPanelResult;

		[SerializeField]
		private GameObject m_GoPanelAddGroup;

		[SerializeField]
		private TabManager m_TabTop;

		[SerializeField]
		private InputField m_TextBattleIdReview;

		private const int TAB_ATTACK = 0;

		private const int TAB_REVIEW = 1;

		private const int TAB_ADD_GROUP = 2;

		private const int TAB_TOWER = 3;

		private List<string> lstInfoGr;

		private List<long> groupId;

		private int groupIdSelected;

		private long battleLogId;

		protected override void Start()
		{
		}

		public void OnTabActive(int tabId, int buttonTabId)
		{
		}

		private void DisableGroup()
		{
		}

		private void DisableAllPanel()
		{
		}

		public void DropdownClick()
		{
		}

		public void ButtonActiveAtkClick()
		{
		}

		public void ButtonActiveReviewClick()
		{
		}

		public void ButtonAttackBattleClick()
		{
		}

		public void ButtonReviewBattleClick()
		{
		}

		public void ButtonInfoClick()
		{
		}

		public override void ProcessKEvent(int eventId, object data)
		{
		}
	}
}
