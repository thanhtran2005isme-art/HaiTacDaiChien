using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PopupSelectEquipment : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadData_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupSelectEquipment _003C_003E4__this;

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
		public _003CDelayLoadData_003Ed__8(int _003C_003E1__state)
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

	public GridPoolGroup m_PoolEquipment;

	public GameObject m_GoScrollView;

	private Action<ItemEquipment> m_ItemSelectedCb;

	public GameObject m_GroupItems;

	public GameObject m_GroupNoItems;

	private static GameObject m_GoPrefab;

	[HideInInspector]
	public List<ItemEquipment> m_LstItemEquipment;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadData_003Ed__8))]
	private IEnumerator DelayLoadData()
	{
		return null;
	}

	public void SetItemDidSelectCallback(Action<ItemEquipment> cb)
	{
	}

	public void ButtonCloseClick()
	{
	}

	private void OnItemEquipmentClick(ItemEquipment ie)
	{
	}

	public static PopupSelectEquipment ShowUp(List<ItemEquipment> lstEquipment)
	{
		return null;
	}
}
