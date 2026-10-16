using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelPetRuin : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSpineEffect_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelPetRuin _003C_003E4__this;

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
		public _003CDelayLoadSpineEffect_003Ed__17(int _003C_003E1__state)
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

	public Text m_TextGoldUpgradeCost;

	public Text m_TextChaosUpgradeCost;

	public GameObject m_GoBoxMaxLevel;

	public GameObject m_GoBoxUpgradeCost;

	public GameObject m_GoBoxRuinLock;

	public GameObject m_GoBtnUpgrade;

	public Text m_TextBuffAttack;

	public Text m_TextBuffHp;

	public Text m_TextBuffSpeed;

	public List<ButtonPetRuin> m_LstButtonRuin;

	public PetModel m_PetModel;

	private int m_RuinIdSelected;

	public SpineObject m_SpineEffectUp;

	public AutoWarningResource m_WarningGoldSkill;

	public AutoWarningResource m_WarningChaosStone;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSpineEffect_003Ed__17))]
	private IEnumerator DelayLoadSpineEffect()
	{
		return null;
	}

	public void SetPetModel(PetModel model)
	{
	}

	private void ButtonRuinSelected(int ruinId)
	{
	}

	private void ShowUpgradeRequirement(int ruinId)
	{
	}

	public void ButtonUpgradeClick()
	{
	}

	private void UpdateBuffStatus(PetModel petModel)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
