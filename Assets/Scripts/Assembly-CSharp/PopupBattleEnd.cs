using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupBattleEnd : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayShowGoResult_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public PopupBattleEnd _003C_003E4__this;

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
		public _003CDelayShowGoResult_003Ed__18(int _003C_003E1__state)
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
	private sealed class _003CDelayShowRewardSelection_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupBattleEnd _003C_003E4__this;

		public List<object> lstObjModel;

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
		public _003CDelayShowRewardSelection_003Ed__22(int _003C_003E1__state)
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

	public GameObject m_GoFrameWin;

	public GameObject m_GoFrameLose;

	public RectTransform m_TransformBonus;

	public GameObject m_GroupArenaPoint;

	public GameObject m_GroupRecommend;

	public GameObject m_GoPointBadge;

	public GameObject m_GoButtonBattleDetail;

	public BoxArenaBattlePoint m_BoxArenaPoint;

	public GameObject m_GoBattleResult;

	public Text m_TextBatleId;

	public Text m_TextTotalDamage;

	private bool m_EnableClose;

	public Action m_PopupClosedCallback;

	private BattlePointDetail m_BattlePointDetail;

	private long battleId;

	protected void Start()
	{
	}

	public void ShowTest()
	{
	}

	public void ShowResult(BattleResultInfo resultInfo, BattlePointDetail battlePoint)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowGoResult_003Ed__18))]
	private IEnumerator DelayShowGoResult(float delay)
	{
		return null;
	}

	public void ButtonRecommendSmithClick()
	{
	}

	public void ButtonRecommendUpgradeClick()
	{
	}

	public void ButtonRecommendSummonClick()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowRewardSelection_003Ed__22))]
	private IEnumerator DelayShowRewardSelection(List<object> lstObjModel)
	{
		return null;
	}

	public void ButtonBattleDetailClick()
	{
	}

	public void ButtonShareBattleClick()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupBattleEnd ShowUpTest()
	{
		return null;
	}

	public static PopupBattleEnd ShowUp(BattleResultInfo resultInfo, BattlePointDetail battlePoint)
	{
		return null;
	}

	public static PopupBattleEnd ShowUp(ListBattleData lstBattleData, BattlePointDetail battlePoint)
	{
		return null;
	}

	private void Update()
	{
	}
}
