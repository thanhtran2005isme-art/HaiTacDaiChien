using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine.Unity;
using UnityEngine;

public class HeroCampaignControl : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CPlayAniDie_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeroCampaignControl _003C_003E4__this;

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
		public _003CPlayAniDie_003Ed__7(int _003C_003E1__state)
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

	public SkeletonAnimation m_HeroSkele;

	public SkeletonAnimation m_EffectHitBoss;

	public void InitDataHero(int keyHero, Vector2 pos, float timeScale, bool isFaceRight = true)
	{
	}

	public void SetLayerOrder(int orderInLayer)
	{
	}

	public void PlayAniRun(Action completeCB = null)
	{
	}

	public void PlayAniIdle(Action completeCB = null)
	{
	}

	public float PlayAniAttack(Action completeCB = null)
	{
		return 0f;
	}

	[IteratorStateMachine(typeof(_003CPlayAniDie_003Ed__7))]
	public IEnumerator PlayAniDie()
	{
		return null;
	}

	public float PlayAniHeroBeHit(bool isFightBack = false, Action completeCB = null)
	{
		return 0f;
	}
}
