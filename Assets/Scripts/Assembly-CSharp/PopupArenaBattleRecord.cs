using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupArenaBattleRecord : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadData_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupArenaBattleRecord _003C_003E4__this;

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
		public _003CDelayLoadData_003Ed__6(int _003C_003E1__state)
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

	private ArenaType m_ArenaType;

	public ScrollRect m_ScrollRect;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_CellPrefab;

	protected override void Start()
	{
	}

	public void LoadRecord(ArenaType arenaType)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadData_003Ed__6))]
	public IEnumerator DelayLoadData()
	{
		return null;
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupArenaBattleRecord ShowUp(ArenaType arenaType)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
