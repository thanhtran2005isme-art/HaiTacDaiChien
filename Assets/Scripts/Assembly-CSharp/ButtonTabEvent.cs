using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTabEvent : MonoBehaviour
{
	public GameObject m_TabOn;

	public GameObject m_TabOff;

	public GameObject m_Notify;

	public Image m_ImageIcon;

	public Text m_TextName;

	public Text m_TextTimeCountdown;

	[HideInInspector]
	public int m_EventId;

	public GameEventInfo m_EventInfo;

	public Action<int> m_TabClickCallback;

	public void SetOn(bool isOn)
	{
	}

	public void OnClick()
	{
	}

	public void SetEventInfo(GameEventInfo eventInfo)
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
