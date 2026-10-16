using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CampaignMapGate : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayPlayAnimationSwordClash_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public CampaignMapGate _003C_003E4__this;

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
		public _003CDelayPlayAnimationSwordClash_003Ed__11(int _003C_003E1__state)
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

	public Text m_TextName;

	public CampaignGateInfo m_MapData;

	public GameObject m_GoFocus;

	public GameObject m_GoPointHand;

	public Image m_ImageGate;

	[HideInInspector]
	public GameObject m_GoSwordClang;

	[HideInInspector]
	public int m_GateIndex;

	private void Start()
	{
	}

	public void Hide()
	{
	}

	public void SetMap(CampaignGateInfo map)
	{
	}

	public void PlayerSowrdClashAfterDelay(float delay)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlayAnimationSwordClash_003Ed__11))]
	private IEnumerator DelayPlayAnimationSwordClash(float delay)
	{
		return null;
	}

	public void OnGateClick()
	{
	}
}
