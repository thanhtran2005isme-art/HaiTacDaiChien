using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PanelLootDrop : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayAddLoot_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelLootDrop _003C_003E4__this;

		public Vector2 pointDrop;

		private int _003CnumItem_003E5__2;

		private Vector2 _003CpointCollect_003E5__3;

		private int _003Ci_003E5__4;

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
		public _003CDelayAddLoot_003Ed__18(int _003C_003E1__state)
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

	public GameObject m_GoPointCollect;

	private Transform m_Transform;

	public static PanelLootDrop Instance;

	private bool m_CollectCoinAfterDrop;

	private ItemLoot m_ItemsDrop;

	[HideInInspector]
	public GameObject m_GoPrefabGoLoot;

	private List<GameObject> m_LstPoolItem;

	private int m_minDrop;

	private int m_maxDrop;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void EnableCollectCoinAfterDrop(bool collectAfterDrop)
	{
	}

	public void SetDropItems(ItemLoot itemDrop)
	{
	}

	public void AddLootDrop(Vector2 pointDrop)
	{
	}

	public void setMinMaxDropItem(int min, int max)
	{
	}

	public void PoolGoLoot(GameObject goLoot)
	{
	}

	public GameObject GetGoLoot()
	{
		return null;
	}

	private Vector2 GetPointCollect()
	{
		return default;
	}

	[IteratorStateMachine(typeof(_003CDelayAddLoot_003Ed__18))]
	private IEnumerator DelayAddLoot(Vector2 pointDrop)
	{
		return null;
	}

	private void Update()
	{
	}
}
