using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGBannerEvent : MonoBehaviour
	{
		public Image m_ImageBanner;

		public Image m_ImageIconFeat;

		public Text m_TextTime;

		public Text m_TextTimeDesc;

		public Text m_TextNumberFeat;

		public Text m_TextAction;

		public Text m_TextInfo;

		public GameObject m_GoGroupInfo;

		public GameObject m_GoGroupFeat;

		public GameObject m_GoButtonHelp;

		public GameObject m_GoButtonReward;

		public GameObject m_GoButtonAction;

		private Action m_RewardClickCallback;

		private Action m_ActionClickCallback;

		private string _strHelp;

		private void Start()
		{
		}

		public void InitBanner()
		{
		}

		public void SetInfo(string strInfo)
		{
		}

		public void SetFeat(long numberFeat, string pathIconFeat)
		{
		}

		public void SetFeat(long numberFeat, Sprite spriteFeat)
		{
		}

		public void SetFeat(long numberFeat)
		{
		}

		public void SetTimeCountDown(string timeCaption, int countTime)
		{
		}

		public void SetBannerImage(string pathBanner)
		{
		}

		public void SetHelpText(string strHelp)
		{
		}

		public void SetRewardClickCb(Action rewardClickCb)
		{
		}

		public void SetActionCb(string actionTitle, Action actionClickCb)
		{
		}

		public void HideButtonAction()
		{
		}

		public void ButtonHelpClick()
		{
		}

		public void ButtonRewardClick()
		{
		}

		public void ButtonActionClick()
		{
		}

		private void Update()
		{
		}
	}
}
