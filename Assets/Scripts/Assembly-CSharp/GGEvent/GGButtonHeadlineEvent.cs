using System;
using UnityEngine;
using UnityEngine.UI;

namespace GGEvent
{
	public class GGButtonHeadlineEvent : MonoBehaviour
	{
		public GameObject m_GoTabOn;

		public GameObject m_GoTabOff;

		public GameObject m_GoNotify;

		public Image m_ImageIcon;

		public Text m_TextName;

		public Text m_TextTime;

		public TextCountdown m_CountdownText;

		[HideInInspector]
		public int m_EventId;

		public GGEventHeadlineInfo m_HeadlineInfo;

		public Action<int> m_ButtonClickCallback;

		private void Start()
		{
		}

		public void SetOn(bool isOn)
		{
		}

		public void OnClick()
		{
		}

		public void SetEventHeadlineInfo(GGEventHeadlineInfo headlineInfo)
		{
		}
	}
}
