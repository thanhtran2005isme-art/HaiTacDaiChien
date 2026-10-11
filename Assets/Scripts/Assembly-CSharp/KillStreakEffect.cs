using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class KillStreakEffect : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CPlayHeroHeadEffect_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public KillStreakEffect _003C_003E4__this;

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
		public _003CPlayHeroHeadEffect_003Ed__7(int _003C_003E1__state)
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

	public const float EFFECT_DURATION = 1.5f;

	[SerializeField]
	private SpineObject m_SpineStreak;

	[SerializeField]
	private TranslateController m_TranslateHead;

	[SerializeField]
	private AlphaController m_AlphaHead;

	[SerializeField]
	private GameObject m_GoHead;

	[SerializeField]
	private Image m_ImageHeroHead;

	private void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CPlayHeroHeadEffect_003Ed__7))]
	public IEnumerator PlayHeroHeadEffect()
	{
		return null;
	}

	public void PlayStreak(int teamId, int heroKey, int streak)
	{
	}

	public static KillStreakEffect ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
