using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGCellPackValue : MonoBehaviour
	{
		public Text m_TextVipExp;

		public Text m_TextNumbBuy;

		public Text m_TextAction;

		public Action<GGPackItemInfo> m_OnButtonActionClickCb;

		public Transform m_TfRewards;

		public ScrollRect m_ScrollRect;

		private GGPackItemInfo _packInfo;

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void ReloadData()
		{
		}

		public void LoadCellData(GGPackItemInfo packInfo)
		{
		}

		public void ButtonActionClick()
		{
		}
	}
}
