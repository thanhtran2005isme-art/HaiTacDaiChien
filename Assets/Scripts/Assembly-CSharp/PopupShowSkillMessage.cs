using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupShowSkillMessage : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CMessage_003Ed__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupShowSkillMessage _003C_003E4__this;

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
		public _003CMessage_003Ed__20(int _003C_003E1__state)
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

	public Image leftHero;

	public Image rightHero;

	public Image imgTitleLeft;

	public Image imgTitleRight;

	public Image imgMessageLeft;

	public Image imgMessageRight;

	public Text titleLeft;

	public Text titleRight;

	public Text messageLeft;

	public Text messageRight;

	private bool goToNextMessage;

	private int index;

	public Action cbCloseMessage;

	private List<Message> lstMessage;

	private float timePause;

	private Coroutine delayCoroutine;

	private const int LEFT = 0;

	protected override void Awake()
	{
	}

	public static PopupShowSkillMessage ShowUp(List<Message> lstMessage)
	{
		return null;
	}

	private void ShowMessage(List<Message> lstMessage)
	{
	}

	[IteratorStateMachine(typeof(_003CMessage_003Ed__20))]
	private IEnumerator Message()
	{
		return null;
	}

	public override void ClosePopup()
	{
	}

	public void NextMessage()
	{
	}
}
