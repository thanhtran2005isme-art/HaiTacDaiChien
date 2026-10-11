using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GrepGame;

public class PopupDiceTarotCardFake : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayBossOpen_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupDiceTarotCardFake _003C_003E4__this;

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
		public _003CDelayBossOpen_003Ed__12(int _003C_003E1__state)
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
	private sealed class _003CDelayClosePopup_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupDiceTarotCardFake _003C_003E4__this;

		public int randomIndexBonus;

		private GameContextFake _003Cgc_003E5__2;

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
		public _003CDelayClosePopup_003Ed__10(int _003C_003E1__state)
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

	public List<DiceCarotItem> m_LstDiceCardItem;

	private bool m_isOpen;

	private Action<int> m_CbMoveToIndex;

	private long hp;

	private long atk;

	private bool isBossOpen;

	private bool isBossPlay;

	private int itemId;

	private string msg;

	public void OnButtonOpenChestClick(int itemId)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayClosePopup_003Ed__10))]
	private IEnumerator DelayClosePopup(int randomIndexBonus)
	{
		return null;
	}

	public void StartOpenBossBonus()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayBossOpen_003Ed__12))]
	private IEnumerator DelayBossOpen()
	{
		return null;
	}

	public static void ShowUp(Action<int> cbMoveToIndex, long hp, long atk, bool showPopupRandomBonus)
	{
	}
}
