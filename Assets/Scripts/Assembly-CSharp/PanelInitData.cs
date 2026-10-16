using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelInitData : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass17_0
	{
		public float timeElapsed;

		public float interval;

		public float timeWaitForData;

		public PanelInitData _003C_003E4__this;

		internal void _003CDelayLoadData_003Eb__0()
		{
		}

		internal void _003CDelayLoadData_003Eb__1()
		{
		}
	}

	[CompilerGenerated]
	private sealed class _003CCheckInitDataBeforeLoadHomeScene_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelInitData _003C_003E4__this;

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
		public _003CCheckInitDataBeforeLoadHomeScene_003Ed__14(int _003C_003E1__state)
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
	private sealed class _003CDelayLoadData_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelInitData _003C_003E4__this;

		private _003C_003Ec__DisplayClass17_0 _003C_003E8__1;

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
		public _003CDelayLoadData_003Ed__17(int _003C_003E1__state)
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

	public GameObject m_GoImageBottomBar;

	public Slider m_SliderProgress;

	public Text m_TextProgress;

	private bool m_HeroLoaded;

	private bool m_ArtifactLoaded;

	private bool m_EquipmentLoaded;

	private bool m_ItemConsumableLoaded;

	private bool m_DataConfigLoaded;

	private bool m_IsSkinLoaded;

	private bool m_PetLoaded;

	private ActionRepeatTimer m_TimerWaitDataLoaded;

	private SliderRunner m_SliderRunner;

	private float mDelayLoading;

	protected override void Start()
	{
	}

	public void StartLoadData()
	{
	}

	[IteratorStateMachine(typeof(_003CCheckInitDataBeforeLoadHomeScene_003Ed__14))]
	private IEnumerator CheckInitDataBeforeLoadHomeScene()
	{
		return null;
	}

	private void Update()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadData_003Ed__17))]
	private IEnumerator DelayLoadData()
	{
		return null;
	}

	public List<int> GetListUnloadService()
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
