using System;
using UnityEngine;

public class MonoClickable : MonoBehaviour
{
	public GameObject m_GoClickBox;

	public Action m_ClickedCb;

	private bool _clickable;

	private bool _onMouseDown;

	private float _timeMouseDown;

	private const float TIME_COUNT_A_CLICK = 0.16f;

	private void SetClickable(bool clickable)
	{
	}

	protected virtual void Update()
	{
	}

	private GameObject GetClickedObject()
	{
		return null;
	}
}
