using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public class CrystalCrownScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CDelayTestData_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CrystalCrownScene _003C_003E4__this;

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
		public _003CDelayTestData_003Ed__8(int _003C_003E1__state)
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

	public PanelPlayerArenaInfo m_PanelInfo;

	public PanelTopArenaPlayer m_PanelTop;

	private int m_PlayerPower;

	private int m_TicketPerBattle;

	private int m_CurrRank;

	public HeroesFormation m_DefensiveFormation;

	public static CrystalCrownScene Instance;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayTestData_003Ed__8))]
	public IEnumerator DelayTestData()
	{
		return null;
	}

	public void ButtonBackClick()
	{
	}

	public void HelpClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
