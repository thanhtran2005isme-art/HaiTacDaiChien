using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGBannerGrowthFund : MonoBehaviour
	{
		public GameObject m_GroupTab;

		public GameObject m_GoButtonAction;

		public Image m_ImageBanner;

		public Text m_TextAction;

		public TabManager m_TabMng;

		public Action m_ActionClickCb;

		public Action<int> m_TabSelectCb;

		public const int TAB_1 = 0;

		public const int TAB_2 = 1;

		private void Start()
		{
		}

		public void HideAction()
		{
		}

		public void SetImage(string pathBanner)
		{
		}

		public void SetAction(string titleAction, Action actionClickCb)
		{
		}

		public void ShowTab(bool show)
		{
		}

		public void SetTabActive(int tabIdx)
		{
		}

		public void ButtonActionClick()
		{
		}

		private void TabSelect(int tabId)
		{
		}

		private void Update()
		{
		}
	}
}
