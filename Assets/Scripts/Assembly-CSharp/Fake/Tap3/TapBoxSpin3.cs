using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Fake.Tap3
{
	public class TapBoxSpin3 : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CDelaySwing_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public TapBoxSpin3 _003C_003E4__this;

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
			public _003CDelaySwing_003Ed__13(int _003C_003E1__state)
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

		public Transform m_TfSpin;

		public List<Text> m_LstLabelItem;

		private int[] _arrIndexBonus;

		private float _rotateVelocity;

		private float _rotateAcceleration;

		private bool _spinning;

		private int _indexResult;

		private static List<TapSpinReward> _listReward;

		private bool m_LastSwingRight;

		static TapBoxSpin3()
		{
		}

		private void Start()
		{
		}

		public void InitSpin()
		{
		}

		public void OnLoadBoxChip()
		{
		}

		public void ButtonSpinClick()
		{
		}

		[IteratorStateMachine(typeof(_003CDelaySwing_003Ed__13))]
		private IEnumerator DelaySwing(float delay)
		{
			return null;
		}

		public bool IsSpinning()
		{
			return false;
		}

		public void SpinIt()
		{
		}

		private void SwingOnWait()
		{
		}

		private void Update()
		{
		}

		private void ShowReward(int indexReward)
		{
		}
	}
}
