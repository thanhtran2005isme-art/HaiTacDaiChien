using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PanelHeroList : MonoBehaviour, ITabListener
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadListHero_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelHeroList _003C_003E4__this;

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
		public _003CDelayLoadListHero_003Ed__31(int _003C_003E1__state)
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

	public HorizontalPoolGroup m_PoolHeroes;

	public TabManager m_TabClass;

	private List<int> m_LstHeroIdChecked;

	private Action<HeroModel> m_HeroClickCallback;

	public GameObject m_PanelFilter;

	public GameObject m_GoScrollView;

	public GameObject m_GoButtonShowFilter;

	private bool m_DisableLockedHero;

	private const int TAB_BTN_LIGHT = 1;

	private const int TAB_BTN_DARK = 2;

	private const int TAB_BTN_SHADOW = 3;

	private const int TAB_BTN_FOREST = 4;

	private const int TAB_BTN_ABYSS = 5;

	private const int TAB_BTN_FORTRESS = 6;

	private bool m_ShowHeroHp;

	private Vector2 m_PointScrollViewInit;

	private List<HeroModel> m_LstAllHeroes;

	private bool m_DisablePanel;

	public Action m_LoadDataDoneCB;

	private void Start()
	{
	}

	public int GetIndexOfFirstHeroNotSelected()
	{
		return 0;
	}

	public void ResetPanel()
	{
	}

	public void DisablePanel()
	{
	}

	public void HideTabButtonLightDark()
	{
	}

	public void ReloadHeroData()
	{
	}

	public void ButtonFilterClick()
	{
	}

	public void SetHeroClickCallback(Action<HeroModel> callback)
	{
	}

	private void OnHeroHeadClick(HeroHeadClickable script)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void SetListHero(List<HeroModel> lstHeroes, bool showHeroHp = false, bool disableLockedHero = false)
	{
	}

	public void SetListHeroIdCheck(List<int> lstHeroId)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadListHero_003Ed__31))]
	public IEnumerator DelayLoadListHero()
	{
		return null;
	}

	public void Show()
	{
	}

	public void Hide()
	{
	}

	private void Update()
	{
	}
}
