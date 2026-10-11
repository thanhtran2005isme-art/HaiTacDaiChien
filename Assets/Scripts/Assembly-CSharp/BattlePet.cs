using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine;
using Spine.Unity;
using UnityEngine;

public class BattlePet : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayPlayAnimation_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattlePet _003C_003E4__this;

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
		public _003CDelayPlayAnimation_003Ed__19(int _003C_003E1__state)
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
	private sealed class _003CDelaySetToPosition_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BattlePet _003C_003E4__this;

		public Vector2 pos;

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
		public _003CDelaySetToPosition_003Ed__31(int _003C_003E1__state)
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

	[SerializeField]
	private GameObject m_GoMesh;

	[HideInInspector]
	public PetRageBar m_PetRageBar;

	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	private MeshRenderer m_Meshrender;

	private Transform m_Transform;

	private float m_PetScale;

	protected string m_StrAniAttack;

	public BaseSkill m_Skill;

	public PetBattleInfo m_PetBattleInfo;

	[HideInInspector]
	public GGPool m_PoolGameObject;

	public virtual void Awake()
	{
	}

	public virtual void Start()
	{
	}

	public void DestroyPet()
	{
	}

	public void SetPetInfo(PetBattleInfo petInfo, int petTier = 1)
	{
	}

	public void SetPosition(Vector2 pos)
	{
	}

	public void Reset()
	{
	}

	public GameDirection GetBattleSide()
	{
		return (GameDirection)0;
	}

	public Vector2 GetPosition()
	{
		return default;
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAnimation_003Ed__19))]
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

	public void SetLayerOrder(int orderInLayer)
	{
	}

	public int GetLayerOrder()
	{
		return 0;
	}

	public void SetRage(float rageNew)
	{
	}

	public void SetRageFull()
	{
	}

	public void SetPetScale(float scale)
	{
	}

	public void PlayAnimation(string aniName, bool loop = true)
	{
	}

	public void PlayAnimationOnce(string aniName)
	{
	}

	public void PlayAniAttack()
	{
	}

	[IteratorStateMachine(typeof(_003CDelaySetToPosition_003Ed__31))]
	private IEnumerator DelaySetToPosition(Vector2 pos, float delay)
	{
		return null;
	}

	private void ProcessAnimationFinished(string animationName)
	{
	}

	public void HidePet()
	{
	}

	public void ShowPet()
	{
	}

	public virtual void Update()
	{
	}
}
