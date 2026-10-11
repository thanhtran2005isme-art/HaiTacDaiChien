using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelPetBuff : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSpineEffect_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelPetBuff _003C_003E4__this;

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
		public _003CDelayLoadSpineEffect_003Ed__23(int _003C_003E1__state)
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

	public Text m_TextBuffSum;

	public Text m_TextGoldUpgradeCost;

	public Text m_TextChaosUpgradeCost;

	public GameObject m_GoBoxMaxLevel;

	public GameObject m_GoBoxUpgradeCost;

	public GameObject m_GoBoxRequireTier;

	public Text m_TextRequireTier;

	public GameObject m_GoBoxSkillDesc;

	public ButtonPetSkillInfo m_ButtonSkill1;

	public ButtonPetSkillInfo m_ButtonSkill2;

	public ButtonPetSkillInfo m_ButtonSkill3;

	public ButtonPetSkillInfo m_ButtonSkill4;

	public Transform m_TransformGridAura;

	public GameObject m_PrefabCellAuraPoint;

	private List<ButtonPetSkillInfo> m_LstButtonSkill;

	public PetModel m_PetModel;

	private int m_SkillIdSelected;

	public SpineObject m_SpineEffectUp;

	public PanelPetRuin m_PanelRuin;

	public AutoWarningResource m_WarningGoldSkill;

	public AutoWarningResource m_WarningChaosStone;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSpineEffect_003Ed__23))]
	private IEnumerator DelayLoadSpineEffect()
	{
		return null;
	}

	public void SetPetModel(PetModel model)
	{
	}

	private void ButtonSkillSelected(int skillId)
	{
	}

	private void ShowUpgradeRequirement(ButtonPetSkillInfo buttonSkill)
	{
	}

	private void UpdateSkillStatus(PetModel model)
	{
	}

	public void ButtonUpSkillClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
