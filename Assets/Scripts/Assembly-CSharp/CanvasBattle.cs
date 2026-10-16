using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CanvasBattle : MonoBehaviour
{
	private class HpOffsetInfo
	{
		public int m_OffsetValue;

		public float m_TimeAddEffect;
	}

	[CompilerGenerated]
	private sealed class _003CDelayAddLostHpText_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public int type;

		public CanvasBattle _003C_003E4__this;

		public Vector3 position;

		public float xOffset;

		public long value;

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
		public _003CDelayAddLostHpText_003Ed__10(int _003C_003E1__state)
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
	private sealed class _003CDelayAddLostHpText_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public bool playHurt;

		public BattleHero hero;

		public HpLostType type;

		public CanvasBattle _003C_003E4__this;

		public float yOffset;

		public long value;

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
		public _003CDelayAddLostHpText_003Ed__19(int _003C_003E1__state)
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

	public static CanvasBattle m_CanvasBattle;

	public GGPool m_PoolBattleCanvas;

	private Hashtable m_HtbStatusImage;

	private Hashtable m_HtbLostHpOffsetInfo;

	private Color COLOR_HP_NORMAL;

	private Color COLOR_HP_CRIT;

	private Color COLOR_HP_RESTRAIN;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	public void AddLostHpText(int id, long value, Vector3 position, int type)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayAddLostHpText_003Ed__10))]
	private IEnumerator DelayAddLostHpText(float delay, float xOffset, Vector3 position, long value, int type)
	{
		return null;
	}

	public void AddBuffHpText(Vector3 pos, long value)
	{
	}

	public void AddBuffHpText(Vector3 pos, string value)
	{
	}

	public void AddBattleStatusText(BattleHero hero, int statusType)
	{
	}

	public void AddBlockAttackText(BattleHero hero)
	{
	}

	public void AddRevineText(BattleHero hero)
	{
	}

	public void AddBuffHpText(BattleHero hero, long value)
	{
	}

	public void AddLostHpText(BattleHero hero, long value, HpLostType type = HpLostType.NORMAL)
	{
	}

	public void AddLostHpTextAndSplit(BattleHero hero, long value, List<DamageSplitInfo> lstSplitInfo, HpLostType lostType = HpLostType.NORMAL)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayAddLostHpText_003Ed__19))]
	private IEnumerator DelayAddLostHpText(float delay, float yOffset, BattleHero hero, long value, HpLostType type = HpLostType.NORMAL, bool playHurt = false)
	{
		return null;
	}

	private Vector2 ConvertWordPointToCanvasPoint(Vector2 pointWord)
	{
		return default;
	}

	private void CheckToResetLostHpOffset()
	{
	}

	private void Update()
	{
	}
}
