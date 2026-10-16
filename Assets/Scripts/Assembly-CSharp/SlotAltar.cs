using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SlotAltar : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSpineBurn_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SlotAltar _003C_003E4__this;

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
		public _003CDelayLoadSpineBurn_003Ed__5(int _003C_003E1__state)
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

	public GameObject m_GoHeroHead;

	public HeroModel m_HeroModel;

	public SpineObject m_SpineBurn;

	public Action<int> m_RemoveHeroFromAltarCb;

	private void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSpineBurn_003Ed__5))]
	private IEnumerator DelayLoadSpineBurn()
	{
		return null;
	}

	private void OnHeadClick(HeroHeadClickable headClickable)
	{
	}

	public void DisableClick()
	{
	}

	public void SetHeroModel(HeroModel hm)
	{
	}

	public void PlayBurnEffect()
	{
	}

	private void Update()
	{
	}
}
