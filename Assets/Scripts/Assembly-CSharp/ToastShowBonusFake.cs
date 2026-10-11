using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ToastShowBonusFake : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelaySlideUp_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ToastShowBonusFake _003C_003E4__this;

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
		public _003CDelaySlideUp_003Ed__4(int _003C_003E1__state)
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

	public Text m_Text;

	public Image m_ImageBgr;

	private ActionOnceTimer m_ActionTimer;

	private static GameObject m_PrefabToast;

	private void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelaySlideUp_003Ed__4))]
	private IEnumerator DelaySlideUp()
	{
		return null;
	}

	private void FadeOut()
	{
	}

	private void AlphaUpdate(float alpha)
	{
	}

	private void Update()
	{
	}

	public void SetText(string text)
	{
	}

	public static GameObject ShowToast(string text)
	{
		return null;
	}
}
