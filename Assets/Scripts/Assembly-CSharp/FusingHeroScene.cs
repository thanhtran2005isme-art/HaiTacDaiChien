using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class FusingHeroScene : BaseScene, ITabListener
{
	[CompilerGenerated]
	private sealed class _003CDelayInitListHero_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FusingHeroScene _003C_003E4__this;

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
		public _003CDelayInitListHero_003Ed__19(int _003C_003E1__state)
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
	private sealed class _003CDelayMoveBoxHeroesToScreen_003Ed__20 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public FusingHeroScene _003C_003E4__this;

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
		public _003CDelayMoveBoxHeroesToScreen_003Ed__20(int _003C_003E1__state)
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
	private sealed class _003CDelayPlaySoundWindowSlide_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
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
		public _003CDelayPlaySoundWindowSlide_003Ed__17(int _003C_003E1__state)
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
	private sealed class _003CDelayShowBonus_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BonusModel bm;

		public FusingHeroScene _003C_003E4__this;

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
		public _003CDelayShowBonus_003Ed__26(int _003C_003E1__state)
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

	private const int TAB_FORTRESS = 1;

	private const int TAB_ABYSS = 2;

	private const int TAB_FOREST = 3;

	private const int TAB_SHADOW = 4;

	private const int TAB_DARK = 5;

	private const int TAB_LIGHT = 6;

	public TabManager m_TabMngFaction;

	public GameObject m_GoScrollView;

	private List<HeroModel> m_LstHeroModelCanFuse;

	public GridPoolGroup m_PoolGrid;

	public FusingStage m_FusingStage;

	public GameObject m_GoPointHideBoxHero;

	public GameObject m_GoBoxHeroes;

	public Text m_TextTitle;

	private HeroModel currentHeroClick;

	private List<HeroModel> m_LstHeroModelFilter;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlaySoundWindowSlide_003Ed__17))]
	public IEnumerator DelayPlaySoundWindowSlide(float delay)
	{
		return null;
	}

	public static bool CheckIfEnoughMatterialHeroToFused(int heroKeyToFuse)
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CDelayInitListHero_003Ed__19))]
	private IEnumerator DelayInitListHero()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayMoveBoxHeroesToScreen_003Ed__20))]
	private IEnumerator DelayMoveBoxHeroesToScreen(float delay)
	{
		return null;
	}

	public void OnHeroHeadClick(HeroHeadClickable goHead)
	{
	}

	private void LoadHeroModel(HeroModel hm)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void ButtonBackClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowBonus_003Ed__26))]
	private IEnumerator DelayShowBonus(BonusModel bm, float delay)
	{
		return null;
	}

	public void HelpClick()
	{
	}
}
