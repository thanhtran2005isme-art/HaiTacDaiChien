using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessageFK : MonoBehaviour
{
	public Text m_TextTitle;

	public Text m_TextMessage;

	public Text m_TextAction1;

	public Text m_TextAction2;

	public GameObject m_ButtonAction1;

	public GameObject m_ButtonAction2;

	private Action m_Action1Cb;

	private Action m_Action2Cb;

	private void SetData(string title, string message, string action1 = "", Action action1Cb = null, string action2 = "", Action action2Cb = null)
	{
	}

	private void SetData(string message, string action1 = "", Action action1Cb = null, string action2 = "", Action action2Cb = null)
	{
	}

	public void ButtonAction1Click()
	{
	}

	public void ButtonAction2Click()
	{
	}

	public static void ShowUp(string message, string action1 = "", Action action1Cb = null, string action2 = "", Action action2Cb = null)
	{
	}
}
