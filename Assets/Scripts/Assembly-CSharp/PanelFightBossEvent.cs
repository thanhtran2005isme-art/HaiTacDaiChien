using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class PanelFightBossEvent : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayReloadEventData_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public PanelFightBossEvent _003C_003E4__this;

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
		public _003CDelayReloadEventData_003Ed__23(int _003C_003E1__state)
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

	public BoxGameEventInfo m_BoxEventInfo;

	public Image m_ImageBossBgr;

	public Image m_ImageHit;

	private GameFightBossEvent m_GameEvent;

	public Slider m_SliderHp;

	public Text m_TextItemCost;

	public Text m_TextHpPercent;

	public GameObject m_GoButtonHit;

	public GameObject m_GoButtonGetReward;

	public EffectHitBoss m_EffectHitBoss;

	public SkeletonGraphic m_SkeletonGraphic;

	private AudioClip m_AudioHit;

	private AudioClip m_AudioHurt;

	private string m_UrlEventDetail;

	private bool reload;

	private bool isDead;

	private string assetSoundHit;

	private string assetSoundRevival;

	public void LoadEventData(string urlEventDetail, GameFightBossEvent gameEvent)
	{
	}

	private void LoadEvent(GameFightBossEvent gameEvent)
	{
	}

	public void PlayAniBossIdle()
	{
	}

	public void PlayAniBossDie()
	{
	}

	public void PlayAniBossBeHit()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayReloadEventData_003Ed__23))]
	private IEnumerator DelayReloadEventData(float delay)
	{
		return null;
	}

	public void ButtonHitBossClick()
	{
	}

	public void ButtonRequestRewardClick()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
