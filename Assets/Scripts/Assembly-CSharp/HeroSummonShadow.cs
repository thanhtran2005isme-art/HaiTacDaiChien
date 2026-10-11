using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine.Unity;
using UnityEngine;

public class HeroSummonShadow : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayHideSkeleton_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeroSummonShadow _003C_003E4__this;

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
		public _003CDelayHideSkeleton_003Ed__10(int _003C_003E1__state)
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

	public SkeletonGraphic m_Skeleton;

	private const int STATE_READY = 1;

	private const int STATE_GONE = 2;

	[HideInInspector]
	public int m_ShadowIndex;

	public Action<int> m_ShadowClickCb;

	private int m_ModelState;

	private void Start()
	{
	}

	public void PlayAniIdle()
	{
	}

	public void ReadyForSummon()
	{
	}

	public void PlayAniJumpOut()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayHideSkeleton_003Ed__10))]
	private IEnumerator DelayHideSkeleton()
	{
		return null;
	}

	private void PlayAniJumpIn()
	{
	}

	public void OnClick()
	{
	}
}
