using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HomeScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadDecorSpine_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HomeScene _003C_003E4__this;

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
		public _003CDelayLoadDecorSpine_003Ed__28(int _003C_003E1__state)
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

	public ParallaxLayerMng m_ParalaxLayerMng;

	public static Vector2 LastPositionMainLayer;

	public ActionRepeatTimer m_ActionUpdateLayerMainPosition;

	public ActionRepeatTimer m_ActionRequestNotification;

	public PlayerAvatar m_PlayerAvatar;

	public BonusOnlineController bonusOnline;

	public GameObject m_GoButtonCheckIn;

	public GameObject m_GoButtonVip;

	public GameObject m_VIP;

	public PanelHomeMenu m_PanelHomeMenu;

	public PanelHomeBottom m_PanelHomeBottom;

	public PanelHomeLeft m_PanelHomeLeft;

	public PanelHomeTopRight m_PanelHomeTopRight;

	public List<GameObject> m_LstHomeBuldingButton;

	public List<GameObject> m_LstGoSpineDecor;

	private bool m_OnTutorial;

	public GameObject m_GoButtonPurchaseGem;

	public GameObject m_GoButtonBuyGold;

	public GameObject m_GoNotifyFusing;

	public Text m_TextVIP;

	public static bool m_FirstSetTestTutorial;

	public static HomeScene Instance;

	protected override void Start()
	{
	}

	public new void LoadCurrentTutorial()
	{
	}

	private void TurnOffAllUI()
	{
	}

	private void SetUpUIForTutorial(int tutId)
	{
	}

	private void TutorialTextCompleted(int tutId)
	{
	}

	public void CheckToScrollToNewFeature()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadDecorSpine_003Ed__28))]
	private IEnumerator DelayLoadDecorSpine()
	{
		return null;
	}

	private void OnApplicationPause(bool paused)
	{
	}

	public void ButtonAddGemClick()
	{
	}

	public void ButtonAddGoldClick()
	{
	}

	public void VipClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
