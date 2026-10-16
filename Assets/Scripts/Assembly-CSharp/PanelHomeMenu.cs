using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PanelHomeMenu : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayOpenFb_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelHomeMenu _003C_003E4__this;

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
		public _003CDelayOpenFb_003Ed__17(int _003C_003E1__state)
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

	public GameObject m_GroupMenu;

	public GameObject m_GoPointHandAchivement;

	public GameObject m_GoPointHandDailyQuest;

	public GameObject m_GoPointHandRaid;

	public GameObject m_ButtonAchievement;

	public GameObject m_ButtonQuest;

	public GameObject m_ButtonEventRaid;

	public GameObject m_ButtonGroupFb;

	public List<GameObject> m_LstGoButton;

	private bool m_LeftAppForFb;

	private void Start()
	{
	}

	public void TurnOffForTutorial()
	{
	}

	public void SetUpForTutorial(int tutorialId)
	{
	}

	public void LoadHandTutorialOnButtonAchievement()
	{
	}

	public void LoadHandTutorialOnButtonDailyQuest()
	{
	}

	public void LoadHandTutorialOnButtonEventRaid()
	{
	}

	public void ButtonFbClick()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayOpenFb_003Ed__17))]
	private IEnumerator DelayOpenFb()
	{
		return null;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
	}

	public void ButtonChallengeClick()
	{
	}

	public void ButtonQuestClick()
	{
	}

	public void ButtonEventRaidClick()
	{
	}

	public void ButtonSettingClick()
	{
	}

	private void ShowMenu()
	{
	}

	private void HideMenu()
	{
	}

	private void Update()
	{
	}
}
