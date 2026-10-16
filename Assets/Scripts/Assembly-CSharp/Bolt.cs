using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Bolt : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CHideLightBeam_003Ed__3 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Bolt _003C_003E4__this;

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
		public _003CHideLightBeam_003Ed__3(int _003C_003E1__state)
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

	public ParticleSystem particle;

	private List<GameObject> lstLighting;

	public void SetData(PlayerController mPlayer, int damage)
	{
	}

	[IteratorStateMachine(typeof(_003CHideLightBeam_003Ed__3))]
	public IEnumerator HideLightBeam()
	{
		return null;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
