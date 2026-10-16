using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine;
using Spine.Unity;
using UnityEngine;

public class BattleHero : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayMoveHero_003Ed__79 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattleHero _003C_003E4__this;

		public Vector2 toPostion;

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
		public _003CDelayMoveHero_003Ed__79(int _003C_003E1__state)
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
	private sealed class _003CDelayPlayAniHurt2_003Ed__117 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattleHero _003C_003E4__this;

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
		public _003CDelayPlayAniHurt2_003Ed__117(int _003C_003E1__state)
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
	private sealed class _003CDelayPlayAniHurt_003Ed__116 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattleHero _003C_003E4__this;

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
		public _003CDelayPlayAniHurt_003Ed__116(int _003C_003E1__state)
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
	private sealed class _003CDelayPlayAnimation_003Ed__91 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattleHero _003C_003E4__this;

		public string aniName;

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
		public _003CDelayPlayAnimation_003Ed__91(int _003C_003E1__state)
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
	private sealed class _003CDelayRevine_003Ed__64 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattleHero _003C_003E4__this;

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
		public _003CDelayRevine_003Ed__64(int _003C_003E1__state)
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
	private sealed class _003CDelaySetBattleStandingStatus_003Ed__54 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattleHero _003C_003E4__this;

		public int status;

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
		public _003CDelaySetBattleStandingStatus_003Ed__54(int _003C_003E1__state)
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
	private sealed class _003CDelaySetHeathValue_003Ed__103 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattleHero _003C_003E4__this;

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
		public _003CDelaySetHeathValue_003Ed__103(int _003C_003E1__state)
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

	public float m_AnimationTimeScale;

	private const float VELOCITY_HERO = 3f;

	private const float SCALE_EXTRA_BOSS = 1.35f;

	[HideInInspector]
	public HealthBar m_HealthBar;

	[SerializeField]
	private GameObject m_GoMesh;

	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	private MeshRenderer m_Meshrender;

	private MaterialPropertyBlock m_MaterialPropBlock;

	private Vector2 m_InitPosition;

	[SerializeField]
	private GameObject m_GoPointGotHit;

	[SerializeField]
	private GameObject m_GoPointFire;

	[SerializeField]
	private BoxCollider2D m_BoxBody;

	[SerializeField]
	private BoxCollider2D m_BoxAttackRange;

	[SerializeField]
	private BoxCollider2D m_BoxAttackRangeSkill;

	[HideInInspector]
	public GameObject m_GoHero;

	private Transform m_Transform;

	private Vector2 m_PointHealthBar;

	private bool m_OnMouseDown;

	private Vector2 m_OffsetDown;

	[HideInInspector]
	public IHeroDragDropListener m_HeroDragDropListener;

	private const float TIME_SCALE_ANIMATION_DEFAULT = 1.1f;

	private const float TIME_SCALE_ANIMATION_IDLE = 1f;

	protected string m_StrAniRun;

	protected string m_StrAniHurt;

	protected string m_StrAniHurt2;

	protected string m_StrAniIdle;

	protected string m_StrAniDie;

	[HideInInspector]
	public bool m_NeedUpdateBuffStatus;

	public HeroBattleInfo m_HeroBattleInfo;

	[HideInInspector]
	public bool m_IsSelected;

	[SerializeField]
	private GameObject m_GoSelection;

	private Hashtable m_HtbBodyEffect;

	private Hashtable m_HtbBuffEffect;

	private ActionRepeatTimer m_TimerCheckHpAndRage;

	[HideInInspector]
	public GGPool m_PoolGameObject;

	[HideInInspector]
	public float m_HeroScale;

	[HideInInspector]
	public bool m_AllowDragDrop;

	private bool m_Death;

	private const float HEIGHT_BODY_STANDARD = 1.5f;

	private const float HERO_SCALE_STANDARD = 1f;

	[HideInInspector]
	public int m_BattleStandingStatus;

	public const int BATTLE_ON_BACKDOWN = 1;

	public const int BATTLE_IN_FRONT_LINE = 2;

	private List<BattleEffectData> m_QueueInstaceBattleEffect;

	public virtual void Awake()
	{
	}

	public void SetPoolObject(GGPool ggPool)
	{
	}

	public void SetSkillDebugInfo(string strDebugSkill)
	{
	}

	public string GetStrDebugInfo()
	{
		return null;
	}

	public BaseSkill GetHeroSkill(int skillIndex)
	{
		return null;
	}

	public BaseSkill GetFirstSkill()
	{
		return null;
	}

	public virtual void Start()
	{
	}

	public void SetBattleStandingStatus(int status, float delay = 0f)
	{
	}

	public void QueueIntanceEffect(BattleEffectData effectData)
	{
	}

	[IteratorStateMachine(typeof(_003CDelaySetBattleStandingStatus_003Ed__54))]
	private IEnumerator DelaySetBattleStandingStatus(int status, float delay)
	{
		return null;
	}

	public void DestroyHero()
	{
	}

	public void PrintHeroInfo()
	{
	}

	public float GetHeroScaleCompareToStandard()
	{
		return 0f;
	}

	public void SetHeroScale(float scale)
	{
	}

	public void CheckAnimationTimeScale()
	{
	}

	private void SetAnimationTimeScale(float aniTimeScale)
	{
	}

	private void UpdateHpAndRageToBar()
	{
	}

	public void SetHeroInfo(HeroBattleInfo hi)
	{
	}

	public void Revine()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayRevine_003Ed__64))]
	private IEnumerator DelayRevine(float delay)
	{
		return null;
	}

	private void CalculatePointHealthBar()
	{
	}

	public void Reset()
	{
	}

	public bool IsBodyFreezeByEffect()
	{
		return false;
	}

	public bool IsDeath()
	{
		return false;
	}

	public void SetHp(float hpNew)
	{
	}

	public void SetRage(float rageNew)
	{
	}

	public void CheckBodyEffectToSetHeroColor()
	{
	}

	private Color GetEffectBodyColor(int effectId)
	{
		return default;
	}

	private void SetHeroColor(Color color, int type)
	{
	}

	public void AddBodyEffect(int bodyEffectId, BattleEffectData effData)
	{
	}

	public void RemoveBodyEffect(int bodyEffectId)
	{
	}

	public void ClearBodyEffect()
	{
	}

	public List<ClientBuffEffectData> GetListBuffEffectIdForHealthBar()
	{
		return null;
	}

	public void KnockBack()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayMoveHero_003Ed__79))]
	private IEnumerator DelayMoveHero(float delay, Vector2 toPostion, float duration)
	{
		return null;
	}

	public bool CheckIsOnSlotBattleField(HeroFieldInfo hfi)
	{
		return false;
	}

	public void SetSelected(bool selected)
	{
	}

	public bool CheckIsOnSlotBattleField(int teamId, int slotIdx)
	{
		return false;
	}

	public GameDirection GetBattleSide()
	{
		return (GameDirection)0;
	}

	public Vector2 GetPosition()
	{
		return default;
	}

	public Vector2 GetInitPosition()
	{
		return default;
	}

	public Vector2 GetPointHeathBar()
	{
		return default;
	}

	public void SetInitPosition(Vector2 pos, bool setHeroToPosition = true)
	{
	}

	public void MoveTo(Vector2 posDestination, float duration = -1f)
	{
	}

	public void BackDown(float durationMoving)
	{
	}

	public void MoveBack(float durationMoving)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAnimation_003Ed__91))]
	public IEnumerator DelayPlayAnimation(string aniName, float delay)
	{
		return null;
	}

	public void SetFacingDirection(GameDirection direction)
	{
	}

	public GameDirection GetFacingDirection()
	{
		return (GameDirection)0;
	}

	public bool IsFacingRight()
	{
		return false;
	}

	public Vector2 GetPointGotHit(EffectImpactPosition impactPos = EffectImpactPosition.BODY)
	{
		return default;
	}

	public Vector2 GetPointFire()
	{
		return default;
	}

	public Vector2 GetHeroSize()
	{
		return default;
	}

	public float GetHeroAttackRange(SkillNumber skillNumber)
	{
		return 0f;
	}

	public float GetHeroAttackRange(int skillIndex)
	{
		return 0f;
	}

	public void SetLayerOrder(int orderInLayer)
	{
	}

	public int GetLayerOrder()
	{
		return 0;
	}

	public void SetHealthValue(float healthValue, float delay = 0f)
	{
	}

	[IteratorStateMachine(typeof(_003CDelaySetHeathValue_003Ed__103))]
	private IEnumerator DelaySetHeathValue(float delay, float healthValue)
	{
		return null;
	}

	public void PlayAnimationForce(string aniName, bool loop = true)
	{
	}

	public void PlayAnimation(string aniName, bool loop = true)
	{
	}

	public void PlayAnimationOnce(string aniName)
	{
	}

	public void PlayAniIdle()
	{
	}

	public void PlayAniRun()
	{
	}

	public void PlayAniPause()
	{
	}

	public void PlayAniDie()
	{
	}

	private void ProcessAnimationFinished(string animationName)
	{
	}

	public void HideHero()
	{
	}

	public void ShowHero()
	{
	}

	public void PlayAniHurt()
	{
	}

	public void PlayAniHurt2()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAniHurt_003Ed__116))]
	public IEnumerator DelayPlayAniHurt(float delay)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAniHurt2_003Ed__117))]
	public IEnumerator DelayPlayAniHurt2(float delay)
	{
		return null;
	}

	public virtual void Update()
	{
	}

	private GameObject GetClickedObject()
	{
		return null;
	}
}
