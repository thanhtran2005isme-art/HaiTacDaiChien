using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupFeedbackFk : MonoBehaviour
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

	public static PopupFeedbackFk ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
