using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpecialEvent : MonoBehaviour
{
	public Image m_Icon;

	public Text m_NameEvent;

	public GameObject m_Notify;

	public Action<int> m_ItemClickCB;

	private ActionRepeatTimer m_TimerCheckNotify;

	private int type_Event;

	public void InitButton(int typeEvent, string pathIcon, string name)
	{
	}

	public void ButtonClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
