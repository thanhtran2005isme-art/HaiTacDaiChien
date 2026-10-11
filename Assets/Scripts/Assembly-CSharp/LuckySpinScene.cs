using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LuckySpinScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSpineEffect_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LuckySpinScene _003C_003E4__this;

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
		public _003CDelayLoadSpineEffect_003Ed__23(int _003C_003E1__state)
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
	private sealed class _003CDelayShowBoxSpin_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public LuckySpinScene _003C_003E4__this;

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
		public _003CDelayShowBoxSpin_003Ed__22(int _003C_003E1__state)
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

	public SpinDisk m_SpinDisk;

	[SerializeField]
	public Text m_TextChip1;

	[SerializeField]
	public Text m_TextChip10;

	[SerializeField]
	public Text m_TextGemRefresh;

	[SerializeField]
	public Text m_TextSpin1;

	[SerializeField]
	public Text m_TextSpin10;

	[SerializeField]
	private GameObject m_GoIconGemRefresh;

	[SerializeField]
	private GameObject m_GoButtonRefresh;

	[SerializeField]
	private TextCountdown m_TextCountdown;

	public SpineObject m_SpineEffectRefresh;

	public GameObject m_GoBoxSpin;

	public GameObject m_GoPointHideBoxSpin;

	private bool m_SpinDiskActive;

	private string MSG_WAIT;

	private int NUM_CHIP_TO_SPIN_10;

	public const int SPIN_TYPE_10 = 10;

	public const int SPIN_TYPE_1 = 1;

	private int m_LastSpinType;

	private int m_PriceChip;

	public Transform m_PointBtnBuy;

	private GameObject m_GoHandSelect;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayShowBoxSpin_003Ed__22))]
	private IEnumerator DelayShowBoxSpin(float delay)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSpineEffect_003Ed__23))]
	private IEnumerator DelayLoadSpineEffect()
	{
		return null;
	}

	public void SetTestRewardItem()
	{
	}

	public void ButtonShopClick()
	{
	}

	public void ButtonBuyChipClick()
	{
	}

	public void ButtonSpin1Click()
	{
	}

	public void ButtonSpin10Click()
	{
	}

	public void ButtonRefreshClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void NextSpin1Call()
	{
	}

	private void NextSpin10Call()
	{
	}

	private void Update()
	{
	}

	public void HelpClick()
	{
	}
}
