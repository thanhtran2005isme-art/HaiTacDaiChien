using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HeadHeroFusing : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayDestroy_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public HeadHeroFusing _003C_003E4__this;

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
		public _003CDelayDestroy_003Ed__10(int _003C_003E1__state)
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

	[CompilerGenerated]
	private sealed class _003CDelayMoveIt_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public HeadHeroFusing _003C_003E4__this;

		public Vector2 toPosition;

		public float duration;

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
		public _003CDelayMoveIt_003Ed__7(int _003C_003E1__state)
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

	public List<Image> m_LstImage;

	public Image m_ImageHeroHead;

	private void Start()
	{
	}

	public void SetHero(int heroKey)
	{
	}

	public void SetSprite(Sprite sprite)
	{
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public void MoveIt(Vector2 toPosition, float duration, float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayMoveIt_003Ed__7))]
	private IEnumerator DelayMoveIt(Vector2 toPosition, float duration, float delay)
	{
		return null;
	}

	private void FadeOut(float duration)
	{
	}

	private void UpdateAlpha(float alpha)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayDestroy_003Ed__10))]
	private IEnumerator DelayDestroy(float delay)
	{
		return null;
	}

	private void Update()
	{
	}
}
