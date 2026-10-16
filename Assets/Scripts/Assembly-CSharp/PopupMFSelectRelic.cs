using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupMFSelectRelic : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayShowCard_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public GameObject goCard;

		public Vector2 pointShow;

		public int relicId;

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
		public _003CDelayShowCard_003Ed__14(int _003C_003E1__state)
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

	[SerializeField]
	private List<MFCardRelic> m_ListCardRelic;

	public GameObject m_GoPointHideCard;

	public GameObject m_GoButtonConfirm;

	public GameObject m_GoBoxDesc;

	public Text m_TextDesc;

	public Action m_CloseCb;

	private List<int> _lstRelicIdToSelect;

	private List<MFHeroInfo> _lstPlayerMfHero;

	private MFMapData _mapDataCurrent;

	private bool _onBlockClick;

	private int _cardIdSelected;

	protected override void Start()
	{
	}

	public void SetExtraData(MFMapData mapData, List<MFHeroInfo> lstPlayerMfHero)
	{
	}

	public void SetListRelic(List<int> listRelicIdToSelect)
	{
	}

	private void BlockClickEvent(float duration)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowCard_003Ed__14))]
	private IEnumerator DelayShowCard(GameObject goCard, Vector2 pointShow, float delay, int relicId)
	{
		return null;
	}

	private void OnCardClick(int cardId)
	{
	}

	public void ButtonConfirmClick()
	{
	}

	public override void ClosePopup()
	{
	}

	public static PopupMFSelectRelic ShowUp(List<int> listRelicIdToSelect, MFMapData mapData, List<MFHeroInfo> lstPlayerMfHero)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ButtonHeroesClick()
	{
	}

	public void ButtonRelicBagClick()
	{
	}

	private void Update()
	{
	}
}
