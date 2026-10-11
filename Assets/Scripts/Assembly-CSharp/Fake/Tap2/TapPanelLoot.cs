using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Fake.Tap2
{
	public class TapPanelLoot : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CDelayAddItemDrop_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public Vector2 pointDrop;

			public TapPanelLoot _003C_003E4__this;

			public TapResType resType;

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
			public _003CDelayAddItemDrop_003Ed__11(int _003C_003E1__state)
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

		public GameObject m_GoPointDrop;

		private Transform m_Transform;

		[HideInInspector]
		public GameObject m_GoPrefabItemLoot;

		private List<GameObject> m_ListPoolItem;

		private int m_minDrop;

		private int m_maxDrop;

		private List<GameObject> m_ListDropItem;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void BlashOutCoin(int totalGold, int totalChip)
		{
		}

		public GameObject GetGoLoot()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CDelayAddItemDrop_003Ed__11))]
		private IEnumerator DelayAddItemDrop(Vector2 pointDrop, TapResType resType, float delay)
		{
			return null;
		}

		private void Update()
		{
		}
	}
}
