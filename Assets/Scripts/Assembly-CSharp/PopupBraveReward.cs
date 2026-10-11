using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupBraveReward : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayShowBonus_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BonusModel bm;

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
		public _003CDelayShowBonus_003Ed__15(int _003C_003E1__state)
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

	public List<GameObject> m_LstGoChest;

	public Slider m_SliderProgress;

	private const float m_Progress0 = 0f;

	private const float m_Progress3 = 0.1f;

	private const float m_Progress6 = 0.3f;

	private const float m_Progress9 = 0.5f;

	private const float m_Progress12 = 0.715f;

	private const float m_Progress15 = 0.9f;

	private float[] m_ArrProgressValue;

	protected override void Start()
	{
	}

	public static PopupBraveReward ShowUpWithReward(int chestStartOpen, int chestEndOpen, BonusModel bonus, int currentStageIndex)
	{
		return null;
	}

	public static PopupBraveReward ShowUpNoReward(int numChestAlreadyOpen, int currentStageIndex)
	{
		return null;
	}

	public void SetDataNoReward(int numChestAlreadyOpen, int currentStageIndex)
	{
	}

	public void SetDataWithReward(int chestStartIndex, int chestEndIndex, BonusModel bonus, int currentStageIndex)
	{
	}

	public void ButtonCloseClick()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowBonus_003Ed__15))]
	private IEnumerator DelayShowBonus(BonusModel bm)
	{
		return null;
	}

	private void Update()
	{
	}
}
