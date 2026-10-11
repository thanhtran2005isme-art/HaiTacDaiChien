using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BattleTest : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CDelayStart_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CDelayStart_003Ed__12(int _003C_003E1__state)
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
	private sealed class _003CsendTest_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BattleTest _003C_003E4__this;

		private int[] _003Cids_003E5__2;

		private int _003Ci_003E5__3;

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
		public _003CsendTest_003Ed__19(int _003C_003E1__state)
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

	public PopupHeroStat popupStat;

	public PopupFormationTest mFormation1;

	public PopupFormationTest mFormation2;

	public PopupFormationTest currentFormation;

	public GameObject m_GoSkillDesc;

	public static BattleTest Instance;

	public GameObject poupReplay;

	public Toggle mOldInput;

	public InputField inputBattleId;

	public Toggle mDebug;

	public Toggle mIsBookPoint;

	protected override void Awake()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayStart_003Ed__12))]
	private IEnumerator DelayStart()
	{
		return null;
	}

	public void loadLogin()
	{
	}

	public void loadLiveStream()
	{
	}

	public void SettingLoadHeroPointMax()
	{
	}

	protected override void Start()
	{
	}

	public void showReplay(bool isShow)
	{
	}

	public void viewReplay()
	{
	}

	[IteratorStateMachine(typeof(_003CsendTest_003Ed__19))]
	private IEnumerator sendTest()
	{
		return null;
	}

	public void HidePanelSkillDesc()
	{
	}

	public void ShowPanelSkillDesc(SkillInfo skillInfo)
	{
	}

	public void changeTeam1()
	{
	}

	public void changeTeam2()
	{
	}

	public void battle()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
