using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpinDisk : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelaySwing_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public SpinDisk _003C_003E4__this;

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
		public _003CDelaySwing_003Ed__9(int _003C_003E1__state)
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

	public const int TYPE_CASINO = 0;

	public const int TYPE_SUPER_CASINO = 1;

	public Transform m_Transform;

	public List<GameItem> m_LstGameItem;

	private float m_RotateVelocity;

	private float m_RotateAcceleration;

	private BonusModel m_BonusModel;

	private int typeSpin;

	private NextSpinActionData m_NextActionData;

	private bool m_LastSwingRight;

	private void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelaySwing_003Ed__9))]
	private IEnumerator DelaySwing(float delay)
	{
		return null;
	}

	public void SetListRewardItems(List<ItemModel> lstItem, List<long> lstEnableStt, int typeSpin)
	{
	}

	public bool IsOnSpinToShowResult()
	{
		return false;
	}

	public void SpinIt(int arcResultIndex, BonusModel bonusModel)
	{
	}

	public void SetNextActionData(NextSpinActionData nextActData)
	{
	}

	private void SwingOnWait()
	{
	}

	private void Update()
	{
	}
}
