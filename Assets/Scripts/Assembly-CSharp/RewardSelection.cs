using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RewardSelection : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayShowReward_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public RewardSelection _003C_003E4__this;

		public object itemModelObj;

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
		public _003CDelayShowReward_003Ed__6(int _003C_003E1__state)
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

	public SpineObject m_SpineReward;

	public GameItem m_GameItem;

	[HideInInspector]
	public int m_SlotIndex;

	public Action<int> m_OnClickCallback;

	private void Start()
	{
	}

	public void SetItem(object modelObj, bool makeAnimationOpen)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowReward_003Ed__6))]
	private IEnumerator DelayShowReward(float delay, object itemModelObj)
	{
		return null;
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
