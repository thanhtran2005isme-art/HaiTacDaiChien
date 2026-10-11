using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BattleScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadBuffAndDebuff_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CDelayLoadBuffAndDebuff_003Ed__36(int _003C_003E1__state)
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

	public bool m_IsPlayStartBattleScene;

	public Text m_TextFps;

	public BattleArena m_BattleArena;

	private const int LEVEL_ALLOW_SKIP = 80;

	private const int LEVEL_ALLOW_X3 = 50;

	private int m_CountFrame;

	private float m_CountTime;

	public GameObject m_GoGroupButtonTestSkill;

	public GameObject m_GoButtonNextTurn;

	public ButtonBattleSpeed m_ButtonBattleSpeed;

	private int m_TimeScaleLevel;

	public PanelDebugPoint m_PanelDebugPoint;

	public GameObject m_GroupTopRight;

	public GameObject m_GroupTopLeft;

	public GameObject m_GoAura1;

	public GameObject m_GoAura2;

	public GameObject m_GoPointHandRestrain;

	public GameObject m_GoPointHandX2;

	private GameObject m_GoHandRestrain;

	private GameObject m_GoHandX2;

	public GameObject m_GoButtonSkip;

	public GameObject m_GoButtonX2;

	public GameObject m_GoButtonRestrain;

	public static bool checkBattleCampaign;

	private bool m_PauseBattle;

	public const int TIME_SCALE_LEVEL_FK = 1;

	protected override void Start()
	{
	}

	private void TurnOffAllUI()
	{
	}

	private void SetUpUIForTutorial(int tutId)
	{
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	public void ResumeBattle()
	{
	}

	private void LoadSKillMessage(int level)
	{
	}

	private void TutorialTextCompleted11(int tutId)
	{
	}

	public void ButtonTimeScaleClick()
	{
	}

	public static float MapTimeScaleFromTimeScaleLevel(int level)
	{
		return 0f;
	}

	public void ButtonCounterClick()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadBuffAndDebuff_003Ed__36))]
	private IEnumerator DelayLoadBuffAndDebuff()
	{
		return null;
	}

	public void ButtonSkipClick()
	{
	}

	public void ButtonTest1Click()
	{
	}

	public void ButtonTest2Click()
	{
	}

	public void ButtonTest3Click()
	{
	}

	public void ButtonTest4Click()
	{
	}

	public void ButtonPet1Click()
	{
	}

	public void ButtonPet2Click()
	{
	}

	public override void OnDestroy()
	{
	}

	private void Update()
	{
	}
}
