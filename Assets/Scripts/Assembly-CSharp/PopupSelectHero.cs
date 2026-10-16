using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelectHero : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadData_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupSelectHero _003C_003E4__this;

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
		public _003CDelayLoadData_003Ed__15(int _003C_003E1__state)
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

	public GridPoolGroup m_PoolHeroes;

	public GameObject m_GoScrollView;

	private Action<List<HeroModel>> m_HeroesSelectedCb;

	private Action m_CloseClickCb;

	private static GameObject m_GoPrefab;

	[HideInInspector]
	public List<HeroModel> m_LstAllHeroModel;

	[HideInInspector]
	public List<int> m_LstHeroIdSelectedBefore;

	public GameObject m_GroupHeroes;

	public GameObject m_GroupNoHeroes;

	public Text m_TextTitle;

	[HideInInspector]
	public int m_MaxSelectedAllow;

	[HideInInspector]
	public bool m_DisableLockedHero;

	[HideInInspector]
	public int m_MinSelectedRequire;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadData_003Ed__15))]
	private IEnumerator DelayLoadData()
	{
		return null;
	}

	public void SetTitle(string title)
	{
	}

	public void SetHeroesDidSelectCallback(Action<List<HeroModel>> cb)
	{
	}

	public void SetCloseClickCallback(Action cb)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonRecommendSummonClick()
	{
	}

	public void ButtonRecommendShopClick()
	{
	}

	public void ButtonOkClick()
	{
	}

	private void OnHeroHeadClick(HeroHeadClickable heroHead)
	{
	}

	public static PopupSelectHero ShowUp(List<HeroModel> lstAllHeroModel, List<int> lstSelectedHeroId, int maxSelectedAllow = 9999, bool disableLockedHero = false)
	{
		return null;
	}

	public static PopupSelectHero ShowUp(List<HeroModel> lstAllHeroModel, int maxSelectedAllow = 9999)
	{
		return null;
	}
}
