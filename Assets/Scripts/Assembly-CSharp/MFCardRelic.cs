using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MFCardRelic : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayPlayOpenCard_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MFCardRelic _003C_003E4__this;

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
		public _003CDelayPlayOpenCard_003Ed__33(int _003C_003E1__state)
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
	private sealed class _003CDelayPlaySelectCard_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MFCardRelic _003C_003E4__this;

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
		public _003CDelayPlaySelectCard_003Ed__36(int _003C_003E1__state)
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

	[HideInInspector]
	public int m_CardId;

	public Image m_ImageQuality;

	public Text m_TextName;

	public Text m_TextDesc;

	public Image m_ImageRelic;

	public GameObject m_GoCardBack;

	public GameObject m_GoCardFront;

	public GameObject m_GroupContents;

	public GameObject m_GroupAffect;

	public GameObject m_GoAffectHeroHead;

	public GameObject m_GoAffectText;

	public GameObject m_GoAffectFaction;

	public GameObject m_GoAffectClass;

	public Text m_TextAffect;

	public Image m_ImageHeroFaction;

	public Image m_ImageHeroClass;

	public Image m_ImageHeroHead;

	public SpineObject m_SpineCardOpen;

	private Color COLOR_RARE;

	private Color COLOR_ELITE;

	private Color COLOR_LEGENDARY;

	public AlphaController m_AlphaRelic;

	public TranslateController m_MoveRelic;

	public TranslateController m_MoveContent;

	public ScaleController m_ScaleQuality;

	public Action<int> m_OnCardClick;

	public MFRelicInfo m_RelicInfo;

	private void Start()
	{
	}

	public int GetRelicId()
	{
		return 0;
	}

	public void SetRelicInfo(MFRelicInfo relicInfo)
	{
	}

	public void ShowCardBack()
	{
	}

	public void ShowCardFont(MFRelicInfo relicInfo)
	{
	}

	public void PlayOpenCard(MFRelicInfo relicInfo)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlayOpenCard_003Ed__33))]
	private IEnumerator DelayPlayOpenCard()
	{
		return null;
	}

	public void ResumeOpenState()
	{
	}

	public void PlaySelectCard()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlaySelectCard_003Ed__36))]
	private IEnumerator DelayPlaySelectCard()
	{
		return null;
	}

	public void OnClick()
	{
	}

	private void Update()
	{
	}
}
