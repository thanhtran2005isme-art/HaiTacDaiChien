using System;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
	[SerializeField]
	private List<TabButton> m_LstButtonTab;

	[HideInInspector]
	public ITabListener m_Listener;

	public Action<int> m_TabClickCb;

	private int m_CurrentTabIdActive;

	[HideInInspector]
	public int m_TabId;

	[HideInInspector]
	public bool m_AllowAllTabOff;

	private void Start()
	{
	}

	public void SetListButtonTab<T>(List<T> lstTabButton) where T : TabButton
	{
	}

	public void DisableAllTabButton()
	{
	}

	public void HideTabButton(List<int> lstHideButtonIndex)
	{
	}

	public void ResetTab()
	{
	}

	public void SetTabActive(int buttonTabId)
	{
	}

	public void OnTabClick(int buttonTabId)
	{
	}

	private void Update()
	{
	}
}
