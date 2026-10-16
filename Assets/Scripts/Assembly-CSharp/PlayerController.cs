using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CthrowShuriken_003Ed__49 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public PlayerController _003C_003E4__this;

		public Vector3 fromPosition;

		public Vector3 toPosition;

		public int damage;

		public PlayerData mData;

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
		public _003CthrowShuriken_003Ed__49(int _003C_003E1__state)
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
	private sealed class _003CthrowStone_003Ed__51 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public PlayerController _003C_003E4__this;

		public Vector3 fromPosition;

		public Vector3 toPosition;

		public int damage;

		public PlayerData mData;

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
		public _003CthrowStone_003Ed__51(int _003C_003E1__state)
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

	protected string m_StrAniRun;

	protected string m_StrAniHurt;

	protected string m_StrAniHurt2;

	protected string m_StrAniIdle;

	protected string m_StrAniDie;

	protected string m_StrAniAttack;

	protected string m_StrAniFlame;

	public const int STATE_IDLE = 0;

	public const int STATE_RUN_TO_POS = 1;

	public const int STATE_DAMAGE = 2;

	public const int STATE_DEATH = 3;

	public const int STATE_ATTACK = 4;

	public const int STATE_FLAME = 5;

	private const int AI_ATTACK = 1;

	private const int AI_MOVE_NEAR_ENEMY = 2;

	public int State;

	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	public MonoPoolItem poolItem;

	private PredictionBoucingWall m_Prediction;

	public RangeView rangeView;

	public PlayerData mData;

	public GameObject arrow;

	public GameObject avatar;

	public float height_bar;

	private float tmpTimeAttack;

	private PlayerController mEnemy;

	private MeshRenderer m_Meshrender;

	private Rigidbody2D rigid;

	public NavMeshAgent agent;

	private HealthBarFK m_HealthBar;

	public List<GameObject> lstEffect;

	private CircleAbilityManager circleAbilitys;

	private void Start()
	{
	}

	public void SetInvisible(bool invisible)
	{
	}

	public void UpdateCircleAbilitys()
	{
	}

	public void ShowRangeView(float range)
	{
	}

	public Vector3 getPosition()
	{
		return default;
	}

	public void InitAvatar(int avatar)
	{
	}

	public void initData(PlayerData mData, Vector3 pos)
	{
	}

	public void setPosition(Vector3 position)
	{
	}

	public void pushPlayer(Vector3 force)
	{
	}

	public void Idle()
	{
	}

	public void PlayAnimFlame()
	{
	}

	private void ProcessAnimationFinished(string animationName)
	{
	}

	public void setTarget(PlayerController mEnemy)
	{
	}

	public PlayerController getEnemy()
	{
		return null;
	}

	public bool hasEnemy()
	{
		return false;
	}

	public void throwShuriken(Vector3 fromPosition, Vector3 toPosition, int damage, PlayerData mData)
	{
	}

	[IteratorStateMachine(typeof(_003CthrowShuriken_003Ed__49))]
	public IEnumerator throwShuriken(Vector3 fromPosition, Vector3 toPosition, int damage, PlayerData mData, float delay)
	{
		return null;
	}

	public void actionAttack(Vector3 posAttack)
	{
	}

	[IteratorStateMachine(typeof(_003CthrowStone_003Ed__51))]
	public IEnumerator throwStone(Vector3 fromPosition, Vector3 toPosition, int damage, PlayerData mData, float delay)
	{
		return null;
	}

	public void throwStone(Vector3 fromPosition, Vector3 toPosition, int damage, PlayerData mData)
	{
	}

	public Vector2 GetPointHeathBar()
	{
		return default;
	}

	public void die()
	{
	}

	public void UpdateHpBar()
	{
	}

	public void hit(int damage, int typeDamage = 0)
	{
	}

	public float getDisEnemy()
	{
		return 0f;
	}

	private void updateSortLayer()
	{
	}

	public void processAction()
	{
	}

	private void Update()
	{
	}

	public void autoAttack()
	{
	}

	public void stopAgentFindPath()
	{
	}

	public void PlayAnimation(string aniName, bool loop = true)
	{
	}

	public void viewTarget(Vector3 targetPos)
	{
	}

	public void teleportToPosition(Vector3 targetPos)
	{
	}

	public void moveToDirection(Vector3 targetPos, int state)
	{
	}

	public void moveToPosition(Vector3 targetPos, int state)
	{
	}
}
