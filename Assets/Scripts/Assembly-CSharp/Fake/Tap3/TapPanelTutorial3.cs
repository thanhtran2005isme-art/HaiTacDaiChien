using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap3
{
	public class TapPanelTutorial3 : MonoBehaviour
	{
		public Text m_TextTutorial;

		public GameObject m_GoButtonNext;

		public static TapPanelTutorial3 Instance;

		public GameObject m_GoContents;

		private int _currentIndex;

		private int _currentTutId;

		public Action<int> m_TutChangedCb;

		public Action m_TutDoneCb;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void LoadFirstTut()
		{
		}

		public int GetCurrentTutId()
		{
			return 0;
		}

		public bool MoveToNextTut()
		{
			return false;
		}

		public void ButtonNextClick()
		{
		}

		private void Update()
		{
		}
	}
}
