using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelPetLevel : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSpineEffect_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelPetLevel _003C_003E4__this;

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
		public _003CDelayLoadSpineEffect_003Ed__15(int _003C_003E1__state)
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

	public Text m_TextLevel;

	public Text m_TextTierCaption;

	public Text m_TextGoldUpgradeCost;

	public Text m_TextSoulUpgradeCost;

	public HorizontalSlotPoint m_SlotTier;

	public GameObject m_GoBoxMaxLevel;

	public GameObject m_GoBoxUpgradeCost;

	public GameObject m_GoBtnUpTier;

	public GameObject m_GoButtonUpLevel;

	public PetModel m_PetModel;

	public Text m_TextActiveSkillDesc;

	public SpineObject m_SpineEffectUp;

	public AutoWarningResource m_WarningGoldLevel;

	public AutoWarningResource m_WarningMonsterSoul;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSpineEffect_003Ed__15))]
	private IEnumerator DelayLoadSpineEffect()
	{
		return null;
	}

	public void SetPetModel(PetModel model)
	{
	}

	private void SetTierAndLevel(int tier, int level)
	{
	}

	public void ButtonUpLevelClick()
	{
	}

	public void ButtonUpTierClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
