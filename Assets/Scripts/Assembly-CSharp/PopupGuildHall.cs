using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using pbdson;

public class PopupGuildHall : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayPlaySoundWindowSlide_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

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
		public _003CDelayPlaySoundWindowSlide_003Ed__28(int _003C_003E1__state)
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

	public VerticalPoolGroup m_PoolMembers;

	public GameObject m_GoBoxGuildInfo;

	public GameObject m_GoBoxMembers;

	public GameObject m_GoPointHideGuildInfo;

	public GameObject m_GoPointHideMember;

	public GameObject m_ScrollMember;

	public Text m_TextGuildName;

	public Text m_TextGuildId;

	public Text m_TextNumMember;

	public Text m_TextMotto;

	public Text m_TextGuildLevel;

	public Slider m_SliderLevelProgress;

	public Text m_TextProgess;

	public ButtonGuildAvatar m_ButtonAvatar;

	public GameObject m_PrefabCellMember;

	private ClanInfor m_ClanInfo;

	public GameObject m_GoNotifyApplication;

	public GameObject m_GoNotifyRollCall;

	public GameObject m_GoButtonRollCall;

	public GameObject m_GoButtonRollCallIa;

	public GameObject m_GoBtnProcessApplication;

	public GameObject m_GoBtnEditInfo;

	public GameObject m_GoBoxEditMember;

	public PanelGuildManagerMenu m_ManagerMenu;

	private ActionRepeatTimer m_TimerNotification;

	[HideInInspector]
	public List<GuildMemberInfo> m_LstMemberInfo;

	public static PopupGuildHall Instance;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlaySoundWindowSlide_003Ed__28))]
	public IEnumerator DelayPlaySoundWindowSlide(float delay)
	{
		return null;
	}

	private void MakeSlideAnimation()
	{
	}

	public void ButtonQuitGuildClick()
	{
	}

	public void ButtonSettingClick()
	{
	}

	public void ButtonManageMemberClick()
	{
	}

	public void ButtonShowActionMenuClick()
	{
	}

	public void ButtonRollCallClick()
	{
	}

	public void ButtonHelpClick()
	{
	}

	public void ButtonLogActivityClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupGuildHall ShowUp()
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
