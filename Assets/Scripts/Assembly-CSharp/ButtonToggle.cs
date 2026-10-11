using System;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
	public ToggleScript m_Toggle;

	public Action<bool> m_ValueChangedCb;

	private void Start()
	{
	}

	public void OnClick()
	{
	}

	public bool IsOn()
	{
		return false;
	}

	public void SetOn()
	{
	}

	public void SetOff()
	{
	}

	private void Update()
	{
	}
}
