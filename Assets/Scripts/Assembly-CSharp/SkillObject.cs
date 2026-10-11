using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine;
using Spine.Unity;
using UnityEngine;

public class SkillObject : BattleObject
{
	[CompilerGenerated]
	private sealed class _003CDelayMoveTo_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SkillObject _003C_003E4__this;

		public float delay;

		public bool needRotate;

		public Vector2 to;

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
		public _003CDelayMoveTo_003Ed__18(int _003C_003E1__state)
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
	private sealed class _003CDelayPool_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public SkillObject _003C_003E4__this;

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
		public _003CDelayPool_003Ed__21(int _003C_003E1__state)
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

	public const float VELOCITY_OBJECT_CAST = 12.1f;

	private List<BattleHero> m_ListHeroCheckOrder;

	private ActionRepeatTimer m_ActionCheckLayerOrder;

	public SpriteRenderer m_SpriteRender;

	[SerializeField]
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	private MeshRenderer m_Meshrender;

	private SkillObjectInfo m_SkillObjInfo;

	public override void Start()
	{
	}

	public void LoadSkillData(SkillObjectInfo skillObjInfo)
	{
	}

	public void SetLayerOrder(int order)
	{
	}

	public int GetLayerOrder()
	{
		return 0;
	}

	public void PlayAnimationLoop(string aniName)
	{
	}

	public void MoveTo2(Vector2 to, bool needRotate, float duration)
	{
	}

	public void MoveToAfterDelay2(float delay, Vector2 to, bool needRotate, float duration)
	{
	}

	public float MoveTo(Vector2 to, bool needRotate)
	{
		return 0f;
	}

	public float MoveToAfterDelay(float delay, Vector2 to, bool needRotate)
	{
		return 0f;
	}

	public void Delay(float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayMoveTo_003Ed__18))]
	public IEnumerator DelayMoveTo(Vector2 to, bool needRotate, float duration, float delay)
	{
		return null;
	}

	public void SetListHeroToCheckLayerOrder(List<BattleHero> lstHeroScript, float durationCheck)
	{
	}

	public void PoolAfter(float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPool_003Ed__21))]
	private IEnumerator DelayPool(float delay)
	{
		return null;
	}

	public override void OnGoIntoPool()
	{
	}

	public void Update()
	{
	}
}
