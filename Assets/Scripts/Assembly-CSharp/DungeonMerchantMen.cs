using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class DungeonMerchantMen : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayPlayAnimation_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public DungeonMerchantMen _003C_003E4__this;

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
		public _003CDelayPlayAnimation_003Ed__15(int _003C_003E1__state)
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

	public GameObject m_GoClickArea;

	public Text m_TextName;

	[SerializeField]
	private SkeletonGraphic m_SkeletonGraphic;

	[HideInInspector]
	private Transform m_Transform;

	private Action m_ClickCallback;

	protected string m_StrAniIdle;

	private int m_CurrentMerchantId;

	public virtual void Awake()
	{
	}

	public virtual void Start()
	{
	}

	private void Update()
	{
	}

	public void SetHeroScale(float scale)
	{
	}

	public void SetMerchantId(int merchantId)
	{
	}

	public void Reset()
	{
	}

	public Vector2 GetPosition()
	{
		return default;
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAnimation_003Ed__15))]
	public IEnumerator DelayPlayAnimation(string aniName, float delay)
	{
		return null;
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

	public void Hide()
	{
	}

	public void Show()
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
