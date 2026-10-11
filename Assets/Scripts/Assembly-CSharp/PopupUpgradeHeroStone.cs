using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupUpgradeHeroStone : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSpineEffect_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupUpgradeHeroStone _003C_003E4__this;

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
		public _003CDelayLoadSpineEffect_003Ed__30(int _003C_003E1__state)
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

	public GameObject m_GoArrowConvert;

	public BoxStoneInfo m_BoxStone1;

	public BoxStoneInfo m_BoxStone2;

	public Text m_TextGoldCost;

	public Text m_TextDustCost;

	public Text m_TextGemLock;

	public Text m_TextTitle;

	public GameObject m_GroupActionUpgrade;

	public GameObject m_GroupActionConvert;

	public GameObject m_GoButtonSave;

	public GameObject m_GoButtonSaveDisable;

	public GameObject m_GoButtonUpgrade;

	public Toggle m_ToggleLockAttribute;

	public GameObject m_GoIconGemLock;

	public GameObject m_GoToggle;

	public Text m_TextAttributeStone2;

	private const string STONE_ATTRIBUTE_UNKOWN = "????";

	public const int MODE_UPGRADE = 1;

	public const int MODE_CONVERT = 2;

	private int m_Mode;

	private int m_HeroId;

	private int m_InitStoneId;

	private string m_StoneEffectNext;

	private HeroStoneModel m_stone1;

	public AutoWarningResource m_WarningGold;

	public AutoWarningResource m_WarningDust;

	private bool m_HasChangeStone;

	public SpineObject m_SpineEffectBox2;

	public SpineObject m_SpineEffectBox1;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSpineEffect_003Ed__30))]
	private IEnumerator DelayLoadSpineEffect()
	{
		return null;
	}

	public void SetHeroStone(int heroId, int stoneId, int mode)
	{
	}

	public void SetStone1(HeroStoneModel stone1)
	{
	}

	public void SetStone2(HeroStoneModel stone)
	{
	}

	public void ButtonUpgadeClick()
	{
	}

	public void ButtonConvertClick()
	{
	}

	public void ShowInfoDropPosition()
	{
	}

	public void ButtonSaveConvertlick()
	{
	}

	public override void ClosePopup()
	{
	}

	public static PopupUpgradeHeroStone ShowUp(int heroId, int stoneId, int mode)
	{
		return null;
	}

	public void ToggleLockSwitch()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
