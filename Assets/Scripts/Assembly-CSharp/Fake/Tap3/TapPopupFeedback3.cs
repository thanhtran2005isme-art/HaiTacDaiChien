using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap3
{
	public class TapPopupFeedback3 : MonoBehaviour
	{
		public InputField m_InputText;

		[HideInInspector]
		public int m_MinLength;

		private Action<string> m_ActionCb;

		public void ButtonCloseClick()
		{
		}

		public void SetActionCallback(Action<string> cb)
		{
		}

		public void ButtonActionClick()
		{
		}

		public static TapPopupFeedback3 ShowUp()
		{
			return null;
		}

		private void Update()
		{
		}
	}
}
