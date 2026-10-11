using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using pbdson;

public class MarinefordScene : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayAdvanceNextFloor_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MarinefordScene _003C_003E4__this;

		public PbLabyrinth pbMfNew;

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
		public _003CDelayAdvanceNextFloor_003Ed__27(int _003C_003E1__state)
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
	private sealed class _003CDelayFlyHeroHeadSupport_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MFHeroHead mfHead;

		public Vector2 posTo;

		public MarinefordScene _003C_003E4__this;

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
		public _003CDelayFlyHeroHeadSupport_003Ed__24(int _003C_003E1__state)
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
	private sealed class _003CDelayFyCardRelic_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Vector2 posFrom;

		public Vector2 posTo;

		public MarinefordScene _003C_003E4__this;

		private GameObject _003CgoCard_003E5__2;

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
		public _003CDelayFyCardRelic_003Ed__25(int _003C_003E1__state)
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

	public Text m_TextFloor;

	public Image m_ImageTitle;

	public Text m_TextTimeCap;

	public TextCountdown m_TextCountdown;

	public static string DESC_ITEM_HEALING;

	public static string DESC_ITEM_REVINE;

	public static int TimeCountDownClose;

	public const int HERO_KEY_LUFFY = 190;

	public GameObject m_GoButtonHeroes;

	public GameObject m_GoButtonRelics;

	public MFMap m_MfMap;

	[HideInInspector]
	public List<MFHeroInfo> m_LstPlayerMfHero;

	public GameObject m_GoPanelLoading;

	public static MarinefordScene Instance;

	private MFMapData _currentMapData;

	protected override void Start()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void ButtonGoCenterClick()
	{
	}

	public void ButtonGoClick()
	{
	}

	public void ButtonHelpClick()
	{
	}

	public void ButtonHeroesClick()
	{
	}

	public void ButtonRelicBagClick()
	{
	}

	private void ParseStatusMap(PbLabyrinth pbMf)
	{
	}

	private void ProcessDelayCollectSlotItem(List<MFHeroInfo> lstMFHeroInfoOld)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayFlyHeroHeadSupport_003Ed__24))]
	private IEnumerator DelayFlyHeroHeadSupport(MFHeroHead mfHead, Vector2 posFrom, Vector2 posTo)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayFyCardRelic_003Ed__25))]
	private IEnumerator DelayFyCardRelic(Vector2 posFrom, Vector2 posTo)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayAdvanceNextFloor_003Ed__27))]
	private IEnumerator DelayAdvanceNextFloor(PbLabyrinth pbMfNew)
	{
		return null;
	}

	private void Update()
	{
	}
}
