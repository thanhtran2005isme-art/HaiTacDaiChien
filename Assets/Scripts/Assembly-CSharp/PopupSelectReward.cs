using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PopupSelectReward : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayShow2OtherSlotAndClose_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int excludeSlotIndex;

		public PopupSelectReward _003C_003E4__this;

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
		public _003CDelayShow2OtherSlotAndClose_003Ed__6(int _003C_003E1__state)
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

	private List<object> m_ListObjectModel;

	public List<GameObject> m_ListGoRewardSelection;

	private bool m_SelectedReward;

	protected override void Start()
	{
	}

	public void OnBackgroundClick()
	{
	}

	public void OnSlotClick(int slotIdx)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShow2OtherSlotAndClose_003Ed__6))]
	private IEnumerator DelayShow2OtherSlotAndClose(int excludeSlotIndex)
	{
		return null;
	}

	public void SetData(List<object> lstModelObj)
	{
	}

	public static void ShowUp(List<object> lstModelObj)
	{
	}

	private void Update()
	{
	}
}
