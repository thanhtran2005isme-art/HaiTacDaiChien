using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class WorldScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CCheckTutorial_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameObject clone;

		public WorldScene _003C_003E4__this;

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
		public _003CCheckTutorial_003Ed__19(int _003C_003E1__state)
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
	private sealed class _003CDelayLoadChildObject_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WorldScene _003C_003E4__this;

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
		public _003CDelayLoadChildObject_003Ed__17(int _003C_003E1__state)
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

	public Button m_BtnAreana;

	public Button m_BtnTower;

	public Button m_BtnIsland;

	public Button m_BtnEventRaid;

	public Button m_BtnTarven;

	public Button m_BtnGuild;

	public Button m_BtnBraveTrial;

	public Button m_BtnDungEon;

	public Button m_BtnSealLand;

	public GameObject m_ContentParent;

	public ScrollRect m_ScrollMap;

	public RectTransform m_ContentMap;

	private PopupFormation popupFormation;

	private static Vector2 NomalizedScrollValue;

	protected override void Start()
	{
	}

	private void Update()
	{
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadChildObject_003Ed__17))]
	private IEnumerator DelayLoadChildObject()
	{
		return null;
	}

	public void PickHeroes()
	{
	}

	[IteratorStateMachine(typeof(_003CCheckTutorial_003Ed__19))]
	private IEnumerator CheckTutorial(GameObject clone)
	{
		return null;
	}

	public void OnMapScroll()
	{
	}

	private void SetPopupFormation(PopupFormation popup)
	{
	}

	public void OnBackClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
