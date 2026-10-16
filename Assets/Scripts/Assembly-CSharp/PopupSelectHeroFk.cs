using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PopupSelectHeroFk : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadData_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupSelectHeroFk _003C_003E4__this;

		private List<DummyHeroModel> _003Cm_LstAllHeroModel_003E5__2;

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
		public _003CDelayLoadData_003Ed__6(int _003C_003E1__state)
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

	public Action<int> m_HeroSelectedCb;

	public GameObject m_GoPrefabHead;

	public Transform m_TfContainer;

	private int m_HeroIdSelected;

	private List<GameObject> m_ListGoHead;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadData_003Ed__6))]
	private IEnumerator DelayLoadData()
	{
		return null;
	}

	public void ButtonCloseClick()
	{
	}

	public void OnHeadClick(DummyHeroModel model)
	{
	}

	public void ButtonOkClick()
	{
	}

	public static PopupSelectHeroFk ShowUp()
	{
		return null;
	}
}
