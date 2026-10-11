using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CampaignScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CDelayShowLevelUp_003Ed__63 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CampaignScene _003C_003E4__this;

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
		public _003CDelayShowLevelUp_003Ed__63(int _003C_003E1__state)
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
	private sealed class _003CDelayShowLootCollected_003Ed__64 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CampaignScene _003C_003E4__this;

		public BonusModel bm;

		public int secAfk;

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
		public _003CDelayShowLootCollected_003Ed__64(int _003C_003E1__state)
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

	public ScrollIsland m_ScrollIsland;

	public Text m_TextAwardGold;

	public Text m_TextAwardSpirit;

	public Text m_TextAwardExp;

	public Text m_TextMapName;

	public ButtonCampaignMap m_ButtonCampaingMap;

	public AutoCampaign m_AutoCampaign;

	public GameObject m_GoButtonBattle;

	private ActionRepeatTimer m_ActionUpdateStatus;

	private bool m_IsFirstLoadScene;

	public GameObject m_GoButtonBack;

	public GameObject m_GoButtonHelp;

	public GameObject m_GoButtonTop;

	public GameObject m_GoButtonMap;

	public GameObject m_GoButtonCollect;

	public Button m_GoPointHandBattle;

	public GameObject m_GoPointHandReward;

	public GameObject m_GoPointHandBack;

	public SpineObject m_SpineLootChest;

	private GameObject m_GoHandReward;

	private GameObject m_GoHandBattle;

	public GameObject m_GoRewardDetail;

	public GameObject m_GoMapName;

	public static int currentLevel;

	private bool m_OnTutorial;

	private ActionRepeatTimer m_ActionCamp2State;

	private ActionRepeatTimer m_ActionBlinkButtonBattle;

	private int m_CollectState;

	private const int TUT_COLLECT_WAIT = 11;

	private const int TUT_COLLECT_MAKE_COLLECTION = 12;

	private const int TUT_COLLECT_WAIT_REWARD = 13;

	private const int TUT_COLLECT_RECEIVED = 14;

	private int m_SecAfk;

	private int m_LevelIdAuto;

	private int m_LevelIdLast;

	private int m_RequirePlayerLevel;

	private int m_MaxSecAfkAllow;

	public static CampaignScene Instance;

	private bool m_HasSetAutoFight;

	public SpineObject m_SpineLootGold;

	public SpineObject m_SpineLootSpirit;

	private PopupFormation popupFormation;

	private HomeQuest quest;

	public GameObject handSuggest;

	private bool needHelp;

	private bool canCollectAuto;

	protected override void Start()
	{
	}

	public void PlayLootAnimation()
	{
	}

	public void SuggestPickHero()
	{
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	public void PickHeroes()
	{
	}

	private void LoadCurrentTutorial11()
	{
	}

	private void CheckTutorialCollectState()
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

	private void Update()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void ButtonMapClick()
	{
	}

	public void ButtonTopClick()
	{
	}

	public void ButtonAwardClick()
	{
	}

	public void ButtonBattleClick()
	{
	}

	public void HelpClick()
	{
	}

	public void RequestQuestStatus()
	{
	}

	protected override void CheckToShowLevelUp()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowLevelUp_003Ed__63))]
	private IEnumerator DelayShowLevelUp()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayShowLootCollected_003Ed__64))]
	private IEnumerator DelayShowLootCollected(BonusModel bm, int secAfk)
	{
		return null;
	}

	public void SuggestQuest()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
