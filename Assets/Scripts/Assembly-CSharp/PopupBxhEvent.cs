using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PopupBxhEvent : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CdelayFillData_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public List<EventRankEntity> lstRankEntity;

		public PopupBxhEvent _003C_003E4__this;

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
		public _003CdelayFillData_003Ed__5(int _003C_003E1__state)
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

	public VerticalPoolGroup m_PoolGroup;

	private bool m_InitPool;

	private void Start()
	{
	}

	public void buttonCloseClick()
	{
	}

	private void fillData(List<EventRankEntity> lstRankEntity)
	{
	}

	[IteratorStateMachine(typeof(_003CdelayFillData_003Ed__5))]
	private IEnumerator delayFillData(List<EventRankEntity> lstRankEntity)
	{
		return null;
	}

	public static void ShowYourSelf(List<EventRankEntity> lstRankEntity)
	{
	}
}
