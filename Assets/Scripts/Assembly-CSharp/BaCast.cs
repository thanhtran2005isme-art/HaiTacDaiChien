using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class BaCast : BattleAction
{
	[CompilerGenerated]
	private sealed class _003CDelayPerformSkillEffect_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BaCast _003C_003E4__this;

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
		public _003CDelayPerformSkillEffect_003Ed__7(int _003C_003E1__state)
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

	private List<BattleHero> m_LstAllHero;

	private BattleChainListEffect m_ChainListEffect;

	private int m_SkillIndex;

	public BaCast(BattleHero heroAction, int skillIndex, BaseSkill skillData, List<BattleHero> lstHeroBeImpact, List<BattleHero> lstAllHeroes, BattleChainListEffect chainListBattleEff)
		: base(null, 0f)
	{
	}

	public override void PerformAction()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPerformSkillEffect_003Ed__7))]
	private IEnumerator DelayPerformSkillEffect()
	{
		return null;
	}
}
