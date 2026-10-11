using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupMailBox : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayTestData_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupMailBox _003C_003E4__this;

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
		public _003CDelayTestData_003Ed__19(int _003C_003E1__state)
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

	public ScrollRect m_ScrollRect;

	public VerticalPoolGroup m_PoolMail;

	public GameObject m_GoCellMailPrefab;

	public GameObject m_BoxMail;

	public GameObject m_BoxNoMail;

	private List<MailContent> m_LstMail;

	private MailContent m_CurrentMail;

	private bool m_MailCollectedAll;

	private bool m_MailDeletedAll;

	private int numberMailNotReceive;

	public Text m_TextContent;

	public Transform m_TransformBonus;

	public GameObject m_GoTickCollected;

	public GameObject m_GoButtonClaim;

	public GameObject m_GoButtonDelete;

	public GameObject m_GoButtonReply;

	public GameObject m_GoBoxBonus;

	protected override void Start()
	{
	}

	private void LoadMailContent(MailContent mail)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayTestData_003Ed__19))]
	private IEnumerator DelayTestData()
	{
		return null;
	}

	private void LoadListMail(List<MailContent> lstMail)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonClaimClick()
	{
	}

	public void ButtonReplyClick()
	{
	}

	public void ButtonClaimAllClick()
	{
	}

	public void ButtonDeleteAllClick()
	{
	}

	public void ButtonDeleteClick()
	{
	}

	public static void ShowUp()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
