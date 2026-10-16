using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AutoCampaignBattle : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass34_0
	{
		public HeroCampaignControl scriptBoss;

		public bool nextTurn;

		public Action _003C_003E9__3;

		public Func<bool> _003C_003E9__1;

		internal void _003CActBattle_003Eb__3()
		{
		}

		internal bool _003CActBattle_003Eb__1()
		{
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass34_1
	{
		public int i;

		public _003C_003Ec__DisplayClass34_0 CS_0024_003C_003E8__locals1;
	}

	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass34_2
	{
		public HeroCampaignControl script;

		public _003C_003Ec__DisplayClass34_1 CS_0024_003C_003E8__locals2;

		internal void _003CActBattle_003Eb__0()
		{
		}

		internal void _003CActBattle_003Eb__2()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CActBattle_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoCampaignBattle _003C_003E4__this;

		private _003C_003Ec__DisplayClass34_1 _003C_003E8__1;

		private _003C_003Ec__DisplayClass34_2 _003C_003E8__2;

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
		public _003CActBattle_003Ed__34(int _003C_003E1__state)
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
	private sealed class _003CDelayInitPlayerHeroes_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoCampaignBattle _003C_003E4__this;

		public List<int> lstAutoHeroKey;

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
		public _003CDelayInitPlayerHeroes_003Ed__27(int _003C_003E1__state)
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
	private sealed class _003CDelayStartFightingBoss_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoCampaignBattle _003C_003E4__this;

		public CampaignLevelInfo levelInfo;

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
		public _003CDelayStartFightingBoss_003Ed__28(int _003C_003E1__state)
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
	private sealed class _003CDoChainBatleAction_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public List<BattleAction> lstBa;

		public AutoCampaignBattle _003C_003E4__this;

		private List<BattleAction>.Enumerator _003C_003E7__wrap1;

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
		public _003CDoChainBatleAction_003Ed__31(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CLoopToBattleAct_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoCampaignBattle _003C_003E4__this;

		public int numTurn;

		private bool _003CbattleEnd_003E5__2;

		private int _003CcountTurn_003E5__3;

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
		public _003CLoopToBattleAct_003Ed__33(int _003C_003E1__state)
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

	private const int STATE_MOVE_TO_NEXT_BOSS = 1;

	private const int STATE_REST_BEFORE_FIGHT = 2;

	private const int STATE_FIGHT_BOSS = 3;

	private const int STATE_REST_AFTER_FIGHT = 4;

	private const float DISTANCE_HERO = 1.5f;

	private int m_AutoState;

	private List<GameObject> m_LstGoAutoHero;

	private GameObject m_GoAutoBoss;

	public Transform m_TransformGroupHero;

	public GameObject m_GoPointHeroStart;

	public GameObject m_GoPointEnemy;

	public GameObject m_GoPointEnemySpawn;

	public AutoGround m_AutoGround;

	public float m_Speed;

	public static AutoCampaignBattle m_Instance;

	private List<BattleAutoEvent> m_LstBattleEvent;

	[SerializeField]
	private GGPool m_PoolGameObject;

	private float m_DistanceHeroAndBoss;

	private Vector2 m_PointHeroAutoNow;

	private bool m_BattleFinished;

	private float m_CountTimeRest;

	private List<HeroModel> m_LstBossModel;

	private float m_SpeedBossRun;

	private float step;

	public const float TIME_SCALE_FACTOR = 1.5f;

	private const float HERO_SPEED = 1.6666666f;

	private const float TIME_REST = 1.5f;

	private void Start()
	{
	}

	private void BossDropLoot()
	{
	}

	public void SetListAutoHero(List<int> lstAutoHeroKey)
	{
	}

	public void StartFightingBoss(CampaignLevelInfo levelInfo)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayInitPlayerHeroes_003Ed__27))]
	private IEnumerator DelayInitPlayerHeroes(List<int> lstAutoHeroKey)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayStartFightingBoss_003Ed__28))]
	private IEnumerator DelayStartFightingBoss(CampaignLevelInfo levelInfo)
	{
		return null;
	}

	private HeroModel GetRandomCampaignBoss()
	{
		return null;
	}

	public void SortHeroBeforeAnBattleAction()
	{
	}

	[IteratorStateMachine(typeof(_003CDoChainBatleAction_003Ed__31))]
	private IEnumerator DoChainBatleAction(List<BattleAction> lstBa)
	{
		return null;
	}

	private void GenAutoBattleEvent()
	{
	}

	[IteratorStateMachine(typeof(_003CLoopToBattleAct_003Ed__33))]
	private IEnumerator LoopToBattleAct(int numTurn)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CActBattle_003Ed__34))]
	private IEnumerator ActBattle()
	{
		return null;
	}

	public float GetTimeDelayAnim(float timeAnim)
	{
		return 0f;
	}

	private void GenBossForNextFight()
	{
	}

	private void MoveToNextBoss()
	{
	}

	private void MoveToRestBeforeFightState()
	{
	}

	private void MoveToRestAfterFightState()
	{
	}

	private void ProcessStateMoveToNextBoss()
	{
	}

	private void ProcessStateBattle()
	{
	}

	private void ProcessStateRestAfterFight()
	{
	}

	private void ProcessStateRestBeforeFight()
	{
	}

	private void ProcessStopSpin()
	{
	}

	private void Update()
	{
	}
}
