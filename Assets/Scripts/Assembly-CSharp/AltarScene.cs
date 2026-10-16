using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class AltarScene : BaseScene, ITabListener
{
	[CompilerGenerated]
	private sealed class _003CDelayInitListHero_003Ed__76 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public AltarScene _003C_003E4__this;

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
		public _003CDelayInitListHero_003Ed__76(int _003C_003E1__state)
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
	private sealed class _003CDelayMoveBoxHeroesToScreen_003Ed__75 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public AltarScene _003C_003E4__this;

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
		public _003CDelayMoveBoxHeroesToScreen_003Ed__75(int _003C_003E1__state)
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
	private sealed class _003CDelayPlaySoundWindowSlide_003Ed__74 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

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
		public _003CDelayPlaySoundWindowSlide_003Ed__74(int _003C_003E1__state)
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
	private sealed class _003CDelayShowBonus_003Ed__100 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BonusModel bonus;

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
		public _003CDelayShowBonus_003Ed__100(int _003C_003E1__state)
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

	private const int TAB_FACTION = 1;

	private const int TAB_STAR = 2;

	private const int TAB_ATAR_OR_RETURN = 3;

	private const int TAB_BUTTON_ABYSS = 1;

	private const int TAB_BUTTON_FOREST = 2;

	private const int TAB_BUTTON_FORTRESS = 3;

	private const int TAB_BUTTON_SHADOW = 4;

	private const int TAB_BUTTON_DARK = 5;

	private const int TAB_BUTTON_LIGHT = 6;

	private const int TAB_BUTTON_STAR_1 = 1;

	private const int TAB_BUTTON_STAR_2 = 2;

	private const int TAB_BUTTON_STAR_3 = 3;

	private const int TAB_BUTTON_STAR_4 = 4;

	private const int TAB_BUTTON_STAR_5 = 5;

	private const int TAB_BUTTON_ALTAR = 1;

	private const int TAB_BUTTON_RETURN = 2;

	public List<Text> listNumberTabStar;

	public TabManager m_TabFaction;

	public TabManager m_TabStar;

	public TabManager m_TabAltarOrReturn;

	public GameObject m_GoDaDauXanh;

	public Text m_NumberDaDauXanh;

	public Image m_ImageDaDauXanh;

	public Text m_TextNumber;

	public Text m_TextFeeReturn;

	public Image m_IconGem;

	public Text m_TextFeeReturn2;

	public Image m_IconGem2;

	public GameObject m_GroupFee2;

	public GameObject m_GroupFee;

	public GameObject m_Desc;

	public GameObject m_GoScrollView;

	public GridPoolGroup m_PoolGrid;

	public Transform m_ParentPreviewBonusReturn;

	public GameObject m_PointHideBoxHeroes;

	public GameObject m_GoBoxHeroes;

	public RectTransform m_TransformContentHero;

	public GameObject m_PanelAltar;

	public GameObject m_PanelReturn;

	public GameObject m_GoGem;

	public GameObject m_GoPromotion;

	public GameObject m_GoSpirit;

	public GameObject m_GoButtonBack;

	public GameObject m_GoButtonShopAltar;

	public GameObject m_GoButtonHelp;

	public GameObject m_GoButtonPreview;

	public GameObject m_GoButtonQuickSelect;

	public GameObject m_GoButtonAltar;

	public GameObject m_GoPointHandSelect;

	public GameObject m_GoPointHandAltar;

	public GameObject m_GoPointHandBack;

	private GameObject m_GoHandSelect;

	private GameObject m_GoHandAltar;

	private bool m_OnTutorial;

	private bool m_SelectedHeroAltar;

	private bool m_AltarCompleted;

	private ActionRepeatTimer m_ActionCheckStateTut;

	private int m_TutState;

	private const int TUT_STATE_SELECT_HERO = 1;

	private const int TUT_STATE_ALTAR_HERO = 2;

	private const int TUT_STATE_BACK = 3;

	private List<HeroModel> m_LstPlayerHeroes;

	private List<HeroModel> m_LstPlayerHeroesFilter;

	private int m_FilterByFaction;

	private int m_FilterByStar;

	private int m_minLevel;

	public List<SlotAltar> m_LstSlotHeroAltar;

	public List<SlotAltar> m_LstSlotHeroReturn;

	private List<SlotAltar> m_LstSlotAltar;

	private int m_TabCurrentPanel;

	private List<GameItem> lstGamItemBonusReturn;

	public Transform m_PointBtnBuy;

	private List<HeroModel> m_lstHeroInAltar;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlaySoundWindowSlide_003Ed__74))]
	public IEnumerator DelayPlaySoundWindowSlide(float delay)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayMoveBoxHeroesToScreen_003Ed__75))]
	private IEnumerator DelayMoveBoxHeroesToScreen(float delay)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayInitListHero_003Ed__76))]
	private IEnumerator DelayInitListHero(float delay)
	{
		return null;
	}

	private new void LoadCurrentTutorial()
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

	private void DisableSelectHero()
	{
	}

	private void DisableSelectHeroExceptFirstHero()
	{
	}

	private void CheckTutorialAltarHero()
	{
	}

	public void OnButtonShopClick()
	{
	}

	public void HelpClick()
	{
	}

	private void OnHeroRemoveFromAltar(int heroId)
	{
	}

	private void RemoveHeroFromAltar(int heroId)
	{
	}

	private void RemoveHeroFromAltarOrReturn()
	{
	}

	private List<object> RemoveCurrentSelectedHero()
	{
		return null;
	}

	private bool AddHeroToAltar(HeroModel hm)
	{
		return false;
	}

	public void OnPlayerHeroHeadClick(HeroHeadClickable goHead)
	{
	}

	public void ButtonPreviewReturnClick()
	{
	}

	public void ButtonPreviewClick()
	{
	}

	public void ButtonSmartClick()
	{
	}

	public void ButtonDisassembleReturnClick()
	{
	}

	public void ButtonDisassembleClick()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void ButtonBackClick()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowBonus_003Ed__100))]
	private IEnumerator DelayShowBonus(float delay, BonusModel bonus)
	{
		return null;
	}

	private void Update()
	{
	}

	private void UpdateDaDauXanh()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
