using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class CampaignMapScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CShowPopupFirst_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CampaignMapScene _003C_003E4__this;

		public int level;

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
		public _003CShowPopupFirst_003Ed__4(int _003C_003E1__state)
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

	private float m_timeDelayAnim;

	private int level;

	protected override void Start()
	{
	}

	public void ButtonBackClick()
	{
	}

	[IteratorStateMachine(typeof(_003CShowPopupFirst_003Ed__4))]
	private IEnumerator ShowPopupFirst(int level)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
