using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonLineupAura : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayMakeClick_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public ButtonLineupAura _003C_003E4__this;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[DebuggerHidden]
		public _003CDelayMakeClick_003Ed__21(int _003C_003E1__state)
		{
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	public GameObject m_GoAuraActive;

	public Image m_ImageAuraInactive;

	public SpineObject m_SpineAura;

	public Action<LineupAuraInfo> m_AuraClickCb;

	public Action m_AuraReleaseCb;

	private LineupAuraInfo m_AuraInfo;

	private Vector2 mSizeCanvas;

	[HideInInspector]
	public ScrollRect m_ScrollContainer;

	private bool m_HasLoadSpineAura;

	public bool m_AllowShowSpine;

	private Vector2 m_PointDown;

	private bool m_CancelClick;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void SetAuraNone()
	{
	}

	public void SetAura(LineupAuraInfo auraInfo)
	{
	}

	public bool CheckForActive(int numFortress, int numAbyss, int numForest, int numShadow, int numDark, int numLight)
	{
		return false;
	}

	public void SetInActive()
	{
	}

	public void SetActive()
	{
	}

	public bool IsActive()
	{
		return false;
	}

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayMakeClick_003Ed__21))]
	private IEnumerator DelayMakeClick(float delay)
	{
		return null;
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{
	}

	private Vector2 ConvertMousePointerToCanvasPosition(Vector2 mousePointer)
	{
		return default;
	}
}
