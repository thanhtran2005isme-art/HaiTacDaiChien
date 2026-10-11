using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap2
{
	public class TapPopupFeedback : MonoBehaviour
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

		public static TapPopupFeedback ShowUp()
		{
			return null;
		}

		private void Update()
		{
		}
	}
}
