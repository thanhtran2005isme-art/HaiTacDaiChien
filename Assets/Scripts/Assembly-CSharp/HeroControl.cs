using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class HeroControl : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSkeletonBack_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Vector2 posSkeleton;

		public HeroControl _003C_003E4__this;

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
		public _003CDelayLoadSkeletonBack_003Ed__10(int _003C_003E1__state)
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
	private sealed class _003CPlayAniDie_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeroControl _003C_003E4__this;

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
		public _003CPlayAniDie_003Ed__13(int _003C_003E1__state)
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

	public SkeletonGraphic m_HeroSkele;

	public Image m_BarHp;

	public Action m_HeroClickCB;

	public EffectHitBoss m_EffectHitBoss;

	public HeroFkModel heroModel;

	private const string ANI_IDLE = "idling";

	private const string ANI_HURT = "damage";

	private const string ANI_ATTACK = "beat";

	private const string ANI_DIE = "dead";

	public void InitDataHero(HeroFkModel hero, bool isRefresh = false)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSkeletonBack_003Ed__10))]
	private IEnumerator DelayLoadSkeletonBack(Vector2 posSkeleton)
	{
		return null;
	}

	public void PlayAniIdle()
	{
	}

	public float PlayAniAttack()
	{
		return 0f;
	}

	[IteratorStateMachine(typeof(_003CPlayAniDie_003Ed__13))]
	public IEnumerator PlayAniDie()
	{
		return null;
	}

	public float PlayAniHeroBeHit(bool isFightBack = false)
	{
		return 0f;
	}

	public long GetAtk()
	{
		return 0L;
	}

	public void SetHpLost(int hpLost)
	{
	}

	public void SetHpBuff(int hpBuff)
	{
	}

	public void HeroClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
