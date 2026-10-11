using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHero : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayAniAttack_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public CanvasHero _003C_003E4__this;

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
		public _003CDelayAniAttack_003Ed__41(int _003C_003E1__state)
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
	private sealed class _003CDelayPlayAnimation_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public CanvasHero _003C_003E4__this;

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
		public _003CDelayPlayAnimation_003Ed__32(int _003C_003E1__state)
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
	private Slider m_SliderHp;

	[SerializeField]
	private SkeletonGraphic m_SkeletonGraphic;

	[HideInInspector]
	private Transform m_Transform;

	private Action m_ClickCallback;

	private SliderRunner m_SliderRunnerHp;

	protected string m_StrAniRun;

	protected string m_StrAniIdle;

	protected string m_StrAniAttack;

	private const float HEIGHT_BODY_STANDARD = 1.5f;

	private const float HERO_SCALE_STANDARD = 0.6f;

	private float m_HeroScale;

	[HideInInspector]
	public int m_CurrentHeroKey;

	private string _keyForCurrentModel;

	private string _animationFinishThenHide;

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

	public void SetScaleTweak(float scaleTweak)
	{
	}

	public void SetHeroInfo(HeroBattleInfo hi)
	{
	}

	private void ProcessAnimationFinished(string animationName)
	{
	}

	public void SetHpBarOnHead(Vector2 offset)
	{
	}

	public void SetHpBarAtFoot(Vector2 offset)
	{
	}

	public void LoadHeroModel(string pathSkeleton)
	{
	}

	public void LoadHeroModelFk(int heroKey)
	{
	}

	public void SetHpPercent(float percent)
	{
	}

	public void Reset()
	{
	}

	public Vector2 GetPosition()
	{
		return default;
	}

	public void MoveTo(Vector2 posDestination, float duration)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAnimation_003Ed__32))]
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

	public void PlayAnimation(string aniName, bool loop = true)
	{
	}

	public void PlayAnimationOnce(string aniName)
	{
	}

	public void PlayAnimationOnceThenHide(string aniName)
	{
	}

	public void DelayPlayAniAttack(float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayAniAttack_003Ed__41))]
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

	public void PauseAnimation()
	{
	}

	public void ResumeAnimation()
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
