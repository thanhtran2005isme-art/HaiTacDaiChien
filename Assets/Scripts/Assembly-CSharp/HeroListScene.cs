using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HeroListScene : BaseScene, ITabListener
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadData_003Ed__47 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public HeroListScene _003C_003E4__this;

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
		public _003CDelayLoadData_003Ed__47(int _003C_003E1__state)
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

	public GridPoolGroup m_PoolGrid;

	public List<HeroModel> m_LstPlayerHero;

	public GameObject m_GoScrollView;

	public GameObject m_GoMiscInfo;

	public Text m_TextHeroCapacity;

	public Text m_TextTitle;

	public PanelHeroMenuSort m_PanelMenuSort;

	public TabManager m_TabHeroes;

	public TabManager m_TabFaction;

	private const int TAB_HEROES = 1;

	private const int TAB_FACTION = 2;

	private const int TAB_BUTTON_PLAYER_HERO = 1;

	private const int TAB_BUTTON_ALL_HERO = 2;

	private int m_ButtonTabHeroesActive;

	private const int TAB_BUTTON_FORTRESS = 1;

	private const int TAB_BUTTON_ABYSS = 2;

	private const int TAB_BUTTON_FOREST = 3;

	private const int TAB_BUTTON_SHADOW = 4;

	private const int TAB_BUTTON_DARK = 5;

	private const int TAB_BUTTON_LIGHT = 6;

	private bool m_SortByLevel;

	public GameObject m_GoButtonBack;

	public GameObject m_GoButtonSort;

	public GameObject m_GoButtonAddSlot;

	public GameObject m_GoButtonBag;

	public Transform m_TransformHeroHead;

	private const int HERO_KEY_DAIKIEU = 120;

	private bool m_OnTutorial;

	public GameObject m_GoPointHandSelect;

	private GameObject m_GoHandSelect;

	private List<HeroModel> m_LstShowingHeroModel;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void LoadHeroInventory()
	{
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

	private int DisableAllHeroHeadExceptFirstDaiKieu()
	{
		return 0;
	}

	public void ButtonBuyHeroCapacityClick()
	{
	}

	public void ButtonSortClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void SortByLevel()
	{
	}

	public void SortByStar()
	{
	}

	public void ButtonBagClick()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	private void ShowListHeroes(List<HeroModel> lstHeroModel)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadData_003Ed__47))]
	private IEnumerator DelayLoadData()
	{
		return null;
	}

	public void OnHeroHeadClick(HeroHeadClickable script)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
