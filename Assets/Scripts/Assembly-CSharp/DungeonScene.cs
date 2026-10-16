using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DungeonScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CDelayShowSmashItem_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DungeonScene _003C_003E4__this;

		public BonusModel bmItems;

		public DungeonLotionState potionState;

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
		public _003CDelayShowSmashItem_003Ed__45(int _003C_003E1__state)
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

	public GameObject m_GoPointHideBoxCountdownOpen;

	public GameObject m_GoPointHideBoxHp;

	public GameObject m_GoPointHideBoxBuff;

	public GameObject m_GoPointHideBoxHeroes;

	public GameObject m_GoButtonShowSmashedMerchant;

	public GameObject m_GoBoxCountDownClose;

	private Vector2 m_PointBoxCountDownOpen;

	private Vector2 m_PointBoxHp;

	private Vector2 m_PointBoxBuff;

	private Vector2 m_PointBoxHeroes;

	public GameObject m_GoBoxCountdownOpen;

	public GameObject m_GoBoxHp;

	public GameObject m_GoBoxBuff;

	public GameObject m_GoBoxHeroes;

	public GameObject m_GoCheckSkipBattle;

	public DungeonLotion m_LotionReward;

	public ToggleScript m_ToggleSkipBattle;

	public List<GameObject> m_ListHeroHead;

	public CanvasHero m_HeroAlly;

	public CanvasHero m_HeroStage;

	public DungeonMerchantMen m_MerchantMen;

	public const float SCALE_FACTOR_HERO_DUNGEON = 1.35f;

	public List<GameObject> m_LstGoLotionHp;

	public List<GameObject> m_LstGoLotionBuff;

	public DungeonFlagPole m_FlagPole;

	public SpineObject m_EffectShowHeroStage;

	public SpineObject m_EffectSmash;

	public SpineObject m_EffectCoin;

	public SpineObject m_EffectPotion;

	public Text m_TextStage;

	public TextCountdown m_CountdownOpen;

	public TextCountdown m_CountdownClose;

	private int m_CurrentHeroIndex;

	private DungeonMerchant m_CurrentDungeonMerchant;

	private List<HeroModel> m_LstCurrentStageHeroModel;

	private int m_LastPotionIndexUsed;

	protected override void Start()
	{
	}

	public void ButtonLotionHpClick(int lotionId, int number, string desc)
	{
	}

	public void ButtonLotionBuffClick(string desc)
	{
	}

	public void ButtonBattleClick()
	{
	}

	public void ButtonSkipMerchantClick()
	{
	}

	public void ButtonToggleSkipBattleClick()
	{
	}

	public void HelpClick()
	{
	}

	public void ButtonTopClick()
	{
	}

	public void ButtonSmashedMerchantClick()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowSmashItem_003Ed__45))]
	private IEnumerator DelayShowSmashItem(BonusModel bmItems, DungeonLotionState potionState)
	{
		return null;
	}

	private void ShowBoxLotionAndHeroes()
	{
	}

	private void ShowBoxCountDownOpen()
	{
	}

	private void ShowBoxCountDownClose()
	{
	}

	private void TestHero()
	{
	}

	private void SummonPlayerHero(int slotIndex)
	{
	}

	private void LoadPlayerHeroes(List<HeroModel> lstHeroModel, int selectedIndex)
	{
	}

	private bool IsAllAllyHeroDeath()
	{
		return false;
	}

	private void LoadStageHeroes(List<HeroModel> lstHeroModel)
	{
	}

	private void LoadPotionReward(int potionId)
	{
	}

	private void LoadMerchantHeroes(int merchantId, BonusModel bonusItem, BonusModel bonusPrize)
	{
	}

	public void SetPotionNumber(int[] arrPotionNumber)
	{
	}

	public void AddPotion(int potionIndex, int numberAdd)
	{
	}

	public void ButtonBackClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
