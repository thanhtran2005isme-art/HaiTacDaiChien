using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Zone : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CFinalGame_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

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
		public _003CFinalGame_003Ed__19(int _003C_003E1__state)
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
	private sealed class _003CGenMonster_003Ed__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Zone _003C_003E4__this;

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
		public _003CGenMonster_003Ed__24(int _003C_003E1__state)
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
	private sealed class _003CGenMonsterTime_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Zone _003C_003E4__this;

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
		public _003CGenMonsterTime_003Ed__23(int _003C_003E1__state)
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
	private sealed class _003CaddMonster_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int monsterId;

		public Zone _003C_003E4__this;

		public Vector3 pos;

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
		public _003CaddMonster_003Ed__16(int _003C_003E1__state)
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

	public Door[] lstDoor;

	public List<PlayerController> listEnemy;

	public int zoneId;

	public Collider2D size;

	private bool isClear;

	private IEnumerator genMon;

	public bool isFinal;

	private int currentWave;

	public int zoneType;

	public float waveTime;

	private float endTime;

	private float currentPower;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private Vector3 RandomPointInBox(Vector3 center, Vector3 size)
	{
		return default;
	}

	public Vector3 RandomInZone()
	{
		return default;
	}

	[IteratorStateMachine(typeof(_003CaddMonster_003Ed__16))]
	public IEnumerator addMonster(int monsterId, int level, Vector3 pos)
	{
		return null;
	}

	public void OpenDoor(bool isOpen)
	{
	}

	public void EndWave()
	{
	}

	[IteratorStateMachine(typeof(_003CFinalGame_003Ed__19))]
	private IEnumerator FinalGame()
	{
		return null;
	}

	public void SummonMonsterPortal(Vector3 pos, int monsterId, int level)
	{
	}

	public void SummonMonterRandomPosition(int monsterId, int level)
	{
	}

	public void AddMonsters()
	{
	}

	[IteratorStateMachine(typeof(_003CGenMonsterTime_003Ed__23))]
	private IEnumerator GenMonsterTime()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CGenMonster_003Ed__24))]
	private IEnumerator GenMonster()
	{
		return null;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
	}

	private void Update()
	{
	}
}
