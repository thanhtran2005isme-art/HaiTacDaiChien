using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelScout : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CGetStatusScout_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelScout _003C_003E4__this;

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
		public _003CGetStatusScout_003Ed__19(int _003C_003E1__state)
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

	public Text timeCountdown;

	public Text textButton;

	public Text textPercentHp;

	public Transform panelReward;

	public Transform panelBoss;

	public GameObject panelRewardBoss;

	public GameObject m_ButtonAct;

	public GameObject m_GreyButtonAct;

	public Animator iconSearch;

	public Slider imgFill;

	private TimeStruct timeRefresh;

	private List<MonsterModel> listBoss;

	private BonusModel bonusKillBoss;

	private int limitLevelAtkBoss;

	public void FillData()
	{
	}

	private void FillRewardKillBoss()
	{
	}

	private void FillInfoBoss()
	{
	}

	private void DisableGUI()
	{
	}

	public void ActionClick()
	{
	}

	[IteratorStateMachine(typeof(_003CGetStatusScout_003Ed__19))]
	private IEnumerator GetStatusScout()
	{
		return null;
	}

	public void AttackBoss(object data)
	{
	}

	public void SmashAttackBoss(object data)
	{
	}

	public void HelpClick()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
