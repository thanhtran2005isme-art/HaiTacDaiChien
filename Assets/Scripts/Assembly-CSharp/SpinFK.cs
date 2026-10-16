using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SpinFK : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelaySwing_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public SpinFK _003C_003E4__this;

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
		public _003CDelaySwing_003Ed__8(int _003C_003E1__state)
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

	public Transform m_Transform;

	public List<Text> m_LstLabelItem;

	private int[] m_ArrIndexBonus;

	private float m_RotateVelocity;

	private float m_RotateAcceleration;

	private bool m_Spinning;

	private int m_IndexResult;

	private bool m_LastSwingRight;

	private void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelaySwing_003Ed__8))]
	private IEnumerator DelaySwing(float delay)
	{
		return null;
	}

	public bool IsSpinning()
	{
		return false;
	}

	public void SpinIt()
	{
	}

	private void SwingOnWait()
	{
	}

	private void Update()
	{
	}
}
