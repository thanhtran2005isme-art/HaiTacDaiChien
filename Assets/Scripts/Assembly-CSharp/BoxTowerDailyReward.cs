using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoxTowerDailyReward : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayRebuildUI_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BoxTowerDailyReward _003C_003E4__this;

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
		public _003CDelayRebuildUI_003Ed__9(int _003C_003E1__state)
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

	public GameObject m_GoContents;

	public RectTransform m_TfContents;

	public RectTransform m_TfRewards;

	public GameObject m_GoButtonCollect;

	public Action m_CollectedRewardSuccessCb;

	protected override void Start()
	{
	}

	public void Hide()
	{
	}

	public void Show()
	{
	}

	public void SetTower2Level(int levelId, bool collected)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayRebuildUI_003Ed__9))]
	private IEnumerator DelayRebuildUI()
	{
		return null;
	}

	public void ButtonCollectClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
