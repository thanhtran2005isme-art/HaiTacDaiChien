using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChestBraveReward : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayPlayOpening_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public ChestBraveReward _003C_003E4__this;

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
		public _003CDelayPlayOpening_003Ed__8(int _003C_003E1__state)
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

	public SpineObject m_SpineChest;

	private const string ANI_CLOSE = "Closed";

	private const string ANI_OPENED = "Opening";

	private const string ANI_OPENING = "Animation";

	private void Start()
	{
	}

	public void SetStateOpened()
	{
	}

	public void SetStateClose()
	{
	}

	public void PlayOpening(float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlayOpening_003Ed__8))]
	private IEnumerator DelayPlayOpening(float delay)
	{
		return null;
	}

	private void Update()
	{
	}
}
