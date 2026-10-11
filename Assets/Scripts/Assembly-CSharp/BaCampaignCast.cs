using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class BaCampaignCast : BattleAction
{
	[CompilerGenerated]
	private sealed class _003CDelayPerformSkillEffect_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BaCampaignCast _003C_003E4__this;

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
		public _003CDelayPerformSkillEffect_003Ed__5(int _003C_003E1__state)
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

	private BaseSkill m_SkillData;

	private List<BattleHero> m_LstHeroBeImpact;

	private BattleChainListEffect m_ChainListEffect;

	public BaCampaignCast(BattleHero heroAction, BaseSkill skillData, List<BattleHero> lstHeroBeImpact)
		: base(null, 0f)
	{
	}

	public override void PerformAction()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPerformSkillEffect_003Ed__5))]
	private IEnumerator DelayPerformSkillEffect()
	{
		return null;
	}
}
