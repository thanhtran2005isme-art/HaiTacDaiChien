using System;
using UnityEngine;

public class TabButton : MonoBehaviour
{
	public int m_ButtonTabId;

	public GameObject m_GoOn;

	public GameObject m_GoOff;

	[HideInInspector]
	public bool m_IsOn;

	public Action<int> m_TabClickCallback;

	private void Start()
	{
	}

	public void SetOn(bool on)
	{
	}

	public void ButtonTabClick()
	{
	}

	private void Update()
	{
	}
}
