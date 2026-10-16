using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ItemLootDrop : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayMoveToCollectPoint_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ItemLootDrop _003C_003E4__this;

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
		public _003CDelayMoveToCollectPoint_003Ed__19(int _003C_003E1__state)
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

	private RectTransform m_Transform;

	private float m_GravityAcceleration;

	private float m_Vx;

	private float m_Vy;

	private float m_CurrentX;

	private float m_CurrentY;

	private float m_InitVx;

	private float m_InitVy;

	private float m_YGround;

	private Vector2 m_PointCollect;

	public Image m_ImageItem;

	private ValueUpdater m_ScaleUpdater;

	[HideInInspector]
	public bool m_EnableFlyToCollectPoint;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void ScaleChanged(float scale)
	{
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public void BlashOut(float yGround, Vector2 velocity, Vector2 pointCollect)
	{
	}

	public void MakeBounceUp(Vector2 vInit)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayMoveToCollectPoint_003Ed__19))]
	private IEnumerator DelayMoveToCollectPoint()
	{
		return null;
	}

	public void InitItem(ItemLoot mItems)
	{
	}

	private void Update()
	{
	}
}
