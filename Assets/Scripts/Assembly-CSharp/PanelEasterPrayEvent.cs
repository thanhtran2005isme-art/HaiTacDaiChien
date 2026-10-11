using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelEasterPrayEvent : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CMoveStatueLeftToRight_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelEasterPrayEvent _003C_003E4__this;

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
		public _003CMoveStatueLeftToRight_003Ed__19(int _003C_003E1__state)
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
	private sealed class _003CMoveStatueRightToLeft_003Ed__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelEasterPrayEvent _003C_003E4__this;

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
		public _003CMoveStatueRightToLeft_003Ed__20(int _003C_003E1__state)
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

	public List<Transform> m_ListPoints;

	public List<ItemStatueControl> m_ListStatues;

	public List<ItemExchangeControl> m_ListItemExchange;

	public BoxGameEventInfo m_BoxGameEventInfo;

	public float m_TimeTransition;

	public GameObject m_BtnAct1;

	public GameObject m_BtnAct2;

	public Text m_FeeExchange;

	public Text m_FeeActive;

	public Text m_Limit;

	public Image m_IconItemExchange;

	public Image m_IconItemActive;

	private ItemStatueControl m_StatueCurr;

	private EasterPrayEvent m_GameEvent;

	private BonusModel activePrice;

	private BonusModel refreshPrice;

	public void LoadEvent(EasterPrayEvent gameEvent, bool reload = false)
	{
	}

	public void NextClick()
	{
	}

	public void PrevClick()
	{
	}

	[IteratorStateMachine(typeof(_003CMoveStatueLeftToRight_003Ed__19))]
	private IEnumerator MoveStatueLeftToRight()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CMoveStatueRightToLeft_003Ed__20))]
	private IEnumerator MoveStatueRightToLeft()
	{
		return null;
	}

	private bool CheckAllStatueMoveDone()
	{
		return false;
	}

	private void SetStatueCurrent()
	{
	}

	private void ResetUIStatue()
	{
	}

	private void InitStatue(bool isReload)
	{
	}

	private void StatueClick(int layer, int idStatue)
	{
	}

	private void SetLayerStatue(bool LeftToRight = true)
	{
	}

	private void ActiveClick()
	{
	}

	private void RefreshClick()
	{
	}

	private void ExchangeClick()
	{
	}

	private void FillCommonInfo()
	{
	}

	private void ShowButton()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
