using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GuildWarScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CLoadTestWar_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GuildWarScene _003C_003E4__this;

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
		public _003CLoadTestWar_003Ed__17(int _003C_003E1__state)
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

	private const int NUM_ATTACK_PER_MEMBER = 1;

	public const int WAR_ON_PREPARE = 1;

	public const int WAR_ON_BATTLE = 2;

	public GameObject m_GroupAction;

	public GameObject m_GoButtonToAlly;

	public GameObject m_GoButtonToEnemy;

	public GameObject m_GoPointHideBoxStatus;

	public ScrollWarMap m_ScrollWarMap;

	public BoxWarStatus m_BoxStatus;

	private GuildWarData m_WarData;

	private Vector2 m_PointShowBoxStatus;

	[HideInInspector]
	public bool m_ShowWarLogAfterLoadData;

	private bool m_HasLoadWarMap;

	public GameObject m_GoButtonWarLog;

	[HideInInspector]
	public int m_WarStatus;

	public static GuildWarScene Instance;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CLoadTestWar_003Ed__17))]
	private IEnumerator LoadTestWar()
	{
		return null;
	}

	public bool IsWarOnBattle()
	{
		return false;
	}

	public void ButtonHelpClick()
	{
	}

	private void LoadWarMap(GuildWarData warData)
	{
	}

	private void ShowBoxStatus(GuildWarData warData)
	{
	}

	private void RefreshWarData(GuildWarData warData)
	{
	}

	public void ShowNavigateToAlly()
	{
	}

	public void ShowNavigateToEnemy()
	{
	}

	public void ButtonToAllyClick()
	{
	}

	public void ButtonToEnemyClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void ButtonWarLogClick()
	{
	}

	public void ButtonWarHistoryClick()
	{
	}

	public void ButtonWarRewardClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
