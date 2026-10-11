using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchAndReleaseHandler : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public Action m_OnTouchCb;

	public Action m_OnReleaseCb;

	private void Start()
	{
	}

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
	}

	private void Update()
	{
	}
}
