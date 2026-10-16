using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BaseScene : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayShowWarning_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BaseScene _003C_003E4__this;

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
		public _003CDelayShowWarning_003Ed__13(int _003C_003E1__state)
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

	[HideInInspector]
	public PanelLoading m_PanelLoading;

	public static GameObject goLoadLanguage;

	private const float INTERVAL_WARNING_IN_SECOND = 30f;

	protected override void Awake()
	{
	}

	protected virtual void CheckToShowLevelUp()
	{
	}

	public void Add18PlusIcon()
	{
	}

	public void LoadCurrentTutorial()
	{
	}

	public void DoneTutorialAction()
	{
	}

	protected virtual void ReciveTutorial(TutorialAction tutAct)
	{
	}

	private void TutorialTextCompleted(int actId)
	{
	}

	protected override void Start()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	protected void CheckToShowQuestCompletedMessage()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowWarning_003Ed__13))]
	private IEnumerator DelayShowWarning()
	{
		return null;
	}

	public void ShowLoading()
	{
	}

	public void HideLoading()
	{
	}
}
