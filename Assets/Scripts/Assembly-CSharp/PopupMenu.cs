using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenu : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDown_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupMenu _003C_003E4__this;

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
		public _003CDown_003Ed__8(int _003C_003E1__state)
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

	private const string ANIMATION_UP = "Up";

	private const string ANIMATION_DOWN = "Down";

	public Text m_Title;

	public Animator ani;

	public GameObject m_Panel;

	private BuildingModel buildModel;

	public void InitData(BuildingModel model)
	{
	}

	private void Up()
	{
	}

	[IteratorStateMachine(typeof(_003CDown_003Ed__8))]
	private IEnumerator Down()
	{
		return null;
	}

	public void UpgradeClick()
	{
	}

	public void ResearchClick()
	{
	}

	public static void ShowUp(BuildingModel model)
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
