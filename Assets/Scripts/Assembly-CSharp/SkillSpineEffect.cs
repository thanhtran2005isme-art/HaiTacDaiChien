using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine;
using Spine.Unity;
using UnityEngine;

public class SkillSpineEffect : BattleObject
{
	[CompilerGenerated]
	private sealed class _003CDelayPlayAnimationLoop_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public SkillSpineEffect _003C_003E4__this;

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
		public _003CDelayPlayAnimationLoop_003Ed__4(int _003C_003E1__state)
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
	private sealed class _003CDelayPlayAnimationOne_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public SkillSpineEffect _003C_003E4__this;

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
		public _003CDelayPlayAnimationOne_003Ed__5(int _003C_003E1__state)
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
	private SkeletonAnimation m_SkeletonAnimation;

	private Spine.AnimationState m_SpineAnimationState;

	private MeshRenderer m_Meshrender;

	private string m_AniNamePlayOne;

	public override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAnimationLoop_003Ed__4))]
	public IEnumerator DelayPlayAnimationLoop(string aniName, float delay)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAnimationOne_003Ed__5))]
	public IEnumerator DelayPlayAnimationOne(string aniName, float delay)
	{
		return null;
	}

	public void SetAnimationTimeScale(float timeScale)
	{
	}

	public void PlayAnimationLoop(string aniName)
	{
	}

	public void PlayAnimationOne(string aniName)
	{
	}

	public void SetSortingLayer(string sortingLayer)
	{
	}

	public void SetLayerOrder(int orderInLayer)
	{
	}

	public int GetLayerOrder()
	{
		return 0;
	}

	public bool LoadSpine(string pathSkeleton, string keyToPool)
	{
		return false;
	}
}
