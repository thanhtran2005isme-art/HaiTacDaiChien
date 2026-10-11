using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine.Unity;
using UnityEngine;

public class CanvasHeroFk : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayAniAttack_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public CanvasHeroFk _003C_003E4__this;

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
		public _003CDelayAniAttack_003Ed__24(int _003C_003E1__state)
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

	[SerializeField]
	private GameObject m_GoShadow;

	public GameObject m_GoClickArea;

	[SerializeField]
	private SkeletonGraphic m_SkeletonGraphic;

	[HideInInspector]
	private Transform m_Transform;

	private Action m_ClickCallback;

	protected string m_StrAniRun;

	protected string m_StrAniIdle;

	protected string m_StrAniAttack;

	private float m_HeroScale;

	[HideInInspector]
	public int m_CurrentHeroSpineId;

	public virtual void Awake()
	{
	}

	public virtual void Start()
	{
	}

	private void Update()
	{
	}

	public void SetAlpha(float alpha)
	{
	}

	private void SetHeroScale(float scale)
	{
	}

	public void LoadHeroModel(int spineId)
	{
	}

	public void Reset()
	{
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

	public void PlayAnimation(string aniName, bool loop = true)
	{
	}

	public void PlayAnimationOnce(string aniName)
	{
	}

	public void DelayPlayAniAttack(float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayAniAttack_003Ed__24))]
	private IEnumerator DelayAniAttack(float delay)
	{
		return null;
	}

	public void PlayAniAttack()
	{
	}

	public void PlayAniIdle()
	{
	}

	public void PlayAniRun()
	{
	}

	public void HideHero()
	{
	}

	public void ShowHero()
	{
	}

	public void SetPosition(Vector2 position)
	{
	}

	public void OnClick()
	{
	}

	public void SetClickCallback(Action cb)
	{
	}
}
