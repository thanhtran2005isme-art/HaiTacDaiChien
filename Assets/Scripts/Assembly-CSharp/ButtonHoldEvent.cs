using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoldEvent : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	private bool mTouchDown;

	private float mCountTime;

	public float m_IntervalFireEvent;

	private bool m_FireEventAtFirstTouch;

	public Action m_ButtonReleasedCb;

	private void Start()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}

	public void MakeEventFiredAtFirstTouch()
	{
	}

	public void Reset()
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}
}
