using System;
using UnityEngine;
using UnityEngine.UI;

public class TabSuggest : MonoBehaviour
{
	public Text m_Label;

	public GameObject m_GoOn;

	public GameObject m_GoOff;

	[HideInInspector]
	public bool m_IsOn;

	public Action<int> m_TabClickCallback;

	private int m_ButtonTabId;

	private void Start()
	{
	}

	public void SetData(string label, int id)
	{
	}

	public void SetOn(bool on)
	{
	}

	public int GetIdTab()
	{
		return 0;
	}

	public void ButtonTabClick()
	{
	}

	private void Update()
	{
	}
}
