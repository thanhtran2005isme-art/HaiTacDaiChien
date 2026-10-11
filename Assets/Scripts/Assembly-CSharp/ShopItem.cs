using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSpineEffect_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ShopItem _003C_003E4__this;

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
		public _003CDelayLoadSpineEffect_003Ed__13(int _003C_003E1__state)
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

	public GameItem m_GameItem;

	public Text m_TextPrice;

	public Text m_TextPriceInactive;

	public Image m_IconPrice;

	public Image m_IconPriceInactive;

	public SpineObject m_SpineEffect;

	public GameObject m_GoPrice;

	public GameObject m_GoPriceInactive;

	private ShopItemInfo m_ShopItemInfo;

	public bool needConfirm;

	private void Start()
	{
	}

	public void SetData(ShopItemInfo itemInfo)
	{
	}

	public void PlayRefreshAnimation()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSpineEffect_003Ed__13))]
	private IEnumerator DelayLoadSpineEffect()
	{
		return null;
	}

	public void PurchaseItem()
	{
	}

	private void Update()
	{
	}
}
