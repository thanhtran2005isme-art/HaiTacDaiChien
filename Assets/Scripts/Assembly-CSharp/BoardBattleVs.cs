using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BoardBattleVs : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayPlaySwordClang_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

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
		public _003CDelayPlaySwordClang_003Ed__5(int _003C_003E1__state)
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

	public Text m_TextPlayerName1;

	public Text m_TextPlayerName2;

	public SpineObject m_SpineVs;

	private void Start()
	{
	}

	public void SetData(string playerName1, string playerName2)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlaySwordClang_003Ed__5))]
	private IEnumerator DelayPlaySwordClang(float delay)
	{
		return null;
	}

	public static void ShowUp(string playerName1, string playerName2)
	{
	}

	private void Update()
	{
	}
}
