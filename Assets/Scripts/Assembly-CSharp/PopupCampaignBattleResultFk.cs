using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupCampaignBattleResultFk : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayShowStarWin_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int starWin;

		public PopupCampaignBattleResultFk _003C_003E4__this;

		private int _003Ci_003E5__2;

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
		public _003CDelayShowStarWin_003Ed__7(int _003C_003E1__state)
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

	public Image m_ImgWinLose;

	public GameObject m_GoGroupStars;

	public GameObject m_GoContent;

	public List<GameObject> m_ListGoStarOn;

	public GameObject m_GoButtonClose;

	private void Start()
	{
	}

	public void SetStarWin(int starWin)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowStarWin_003Ed__7))]
	private IEnumerator DelayShowStarWin(int starWin)
	{
		return null;
	}

	public static PopupCampaignBattleResultFk ShowUp(int starWin)
	{
		return null;
	}

	public void ButtonCloseClick()
	{
	}

	private void Update()
	{
	}
}
