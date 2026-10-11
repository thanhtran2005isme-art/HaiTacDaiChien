using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupBattleNext : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayShowButtonNext_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public PopupBattleNext _003C_003E4__this;

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
		public _003CDelayShowButtonNext_003Ed__9(int _003C_003E1__state)
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

	public GameObject m_GoFrameWin;

	public GameObject m_GoFrameLose;

	public GameObject m_GoPointBadge;

	public Action m_PopupClosedCallback;

	public BattlePointDetail m_BattlePointDetail;

	public GameObject m_GoButtonNext;

	public Text m_TextButtonNext;

	protected override void Start()
	{
	}

	public void ShowResult(bool isWin, string textButtonNext, BattlePointDetail battlePoint)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowButtonNext_003Ed__9))]
	private IEnumerator DelayShowButtonNext(float delay)
	{
		return null;
	}

	public void ButtonBattleDetailClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupBattleNext ShowUp(bool isWin, string textButtonNext, BattlePointDetail battlePoint)
	{
		return null;
	}

	private void Update()
	{
	}
}
