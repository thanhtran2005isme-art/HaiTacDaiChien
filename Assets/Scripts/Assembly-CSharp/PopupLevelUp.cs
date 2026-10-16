using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupLevelUp : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayShowEffect_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupLevelUp _003C_003E4__this;

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
		public _003CDelayShowEffect_003Ed__8(int _003C_003E1__state)
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

	public Text m_TextLevel;

	public Transform m_TransformBonus;

	public GameObject m_ObjectLevelUp;

	public SpineObject m_SpineEffect;

	public Action<int> m_ChangeAvatarSuccessCb;

	public Action m_CloseClickCb;

	private static int m_TestNewFeatureOpen;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowEffect_003Ed__8))]
	private IEnumerator DelayShowEffect()
	{
		return null;
	}

	public void ShowBonusLevelUp(BonusModel bm)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static void ShowUp(BonusModel bm, Action m_CloseClickCb = null)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
