using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AutoCampaign : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayHideDialogChat_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public AutoCampaign _003C_003E4__this;

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
		public _003CDelayHideDialogChat_003Ed__26(int _003C_003E1__state)
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
	private sealed class _003CDelayInitPlayerHeroes_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoCampaign _003C_003E4__this;

		public List<HeroModel> lstAutoHeroModel;

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
		public _003CDelayInitPlayerHeroes_003Ed__32(int _003C_003E1__state)
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
	private sealed class _003CDelayStartFightingBoss_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoCampaign _003C_003E4__this;

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
		public _003CDelayStartFightingBoss_003Ed__33(int _003C_003E1__state)
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
	private sealed class _003CDoChainBatleAction_003Ed__39 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public List<BattleAction> lstBa;

		public AutoCampaign _003C_003E4__this;

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
		public _003CDoChainBatleAction_003Ed__39(int _003C_003E1__state)
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
	private sealed class _003CLoopToBattleAct_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AutoCampaign _003C_003E4__this;

		private bool _003CbattleEnd_003E5__2;

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
		public _003CLoopToBattleAct_003Ed__41(int _003C_003E1__state)
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

	private int m_AutoState;

	private List<GameObject> m_LstGoAutoHero;

	private List<GameObject> m_LstGoAutoBoss;

	public Transform m_TransformGroupHero;

	public GameObject m_GoPointHeroStart;

	public GameObject m_GoPointHeroBoss;

	private const float DISTANCE_HERO = 1.3f;

	public static AutoCampaign m_Instance;

	private List<BattleAutoEvent> m_LstBattleEvent;

	[SerializeField]
	private GGPool m_PoolGameObject;

	[SerializeField]
	private GoFolower m_CameraFollower;

	private float m_DistanceHeroAndBoss;

	private float m_WidthViewPortInWorld;

	private Vector2 m_PointHeroAutoNow;

	private bool m_BattleFinished;

	private float m_CountTimeRest;

	private List<HeroModel> m_LstBossModel;

	private ActionRepeatTimer m_TimerTest;

	public GameObject m_GoDialogChat;

	private int m_BeIndex;

	public const float TIME_SCALE_FACTOR = 1.5f;

	private const float HERO_SPEED = 1.6666666f;

	private const float TIME_REST_AFTER_BATTLE = 3f;

	private const float TIME_REST_BEFORE_BATTLE = 3f;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void BossDropLoot()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayHideDialogChat_003Ed__26))]
	private IEnumerator DelayHideDialogChat(float delay)
	{
		return null;
	}

	private void HideDialogChat()
	{
	}

	private void ShowDialogChat()
	{
	}

	private void UpdateChatDialogPosition()
	{
	}

	public void SetListAutoHero(List<HeroModel> lstAutoHeroModel)
	{
	}

	public void StartFightingBoss(CampaignLevelInfo levelInfo)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayInitPlayerHeroes_003Ed__32))]
	private IEnumerator DelayInitPlayerHeroes(List<HeroModel> lstAutoHeroModel)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayStartFightingBoss_003Ed__33))]
	private IEnumerator DelayStartFightingBoss(CampaignLevelInfo levelInfo)
	{
		return null;
	}

	private HeroModel GetRandomCampaignBoss()
	{
		return null;
	}

	public void SortHeroBeforeAnBattleAction(int teamIdAction, int heroAttackIndexAction)
	{
	}

	public BattleHero GetHeroScript(int teamId, int heroIndex)
	{
		return null;
	}

	public void Test()
	{
	}

	[IteratorStateMachine(typeof(_003CDoChainBatleAction_003Ed__39))]
	private IEnumerator DoChainBatleAction(List<BattleAction> lstBa)
	{
		return null;
	}

	private void GenAutoBattleEvent()
	{
	}

	[IteratorStateMachine(typeof(_003CLoopToBattleAct_003Ed__41))]
	private IEnumerator LoopToBattleAct()
	{
		return null;
	}

	private void GenBossForNextFight()
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

	private void Update()
	{
	}
}
