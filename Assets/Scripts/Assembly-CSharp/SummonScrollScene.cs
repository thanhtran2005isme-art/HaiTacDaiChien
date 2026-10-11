using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SummonScrollScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CDelayPlayAniEngergyAdd_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SummonScrollScene _003C_003E4__this;

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
		public _003CDelayPlayAniEngergyAdd_003Ed__22(int _003C_003E1__state)
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

	public Slider m_SliderEnergy;

	public GameObject m_GoSpineEnergyFull;

	public Text m_TextEngergy;

	public SpineObject m_SpineEnergyGlow;

	public SpineObject m_SpineEnegyFly;

	private int m_CurrentEnergy;

	private int m_MaxEnergy;

	public PanelBasicSummon m_BasicSummon;

	public PanelHeroicSummon m_HeroicSummon;

	public SpineObject m_SpineEffectBeer;

	public Button m_goHeroicSummon;

	public Button m_goHeroicSummonBasic;

	public Button m_btnBack;

	public Button m_goHeroicSummonGem;

	public AudioSource m_audioShowHero;

	public PopupSummonHeroes popSummon;

	protected override void Start()
	{
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	public void HelpClick()
	{
	}

	public void ButtonInfoClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void ButtonSummonEnergyClick()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAniEngergyAdd_003Ed__22))]
	public IEnumerator DelayPlayAniEngergyAdd()
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void TutorialTextCompleted11(int tutId)
	{
	}
}
