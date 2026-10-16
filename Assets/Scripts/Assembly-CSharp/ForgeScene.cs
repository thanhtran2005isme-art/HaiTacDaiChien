using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ForgeScene : BaseScene, ITabListener
{
	[CompilerGenerated]
	private sealed class _003CDelayMakeForgeAnimation_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ForgeScene _003C_003E4__this;

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
		public _003CDelayMakeForgeAnimation_003Ed__36(int _003C_003E1__state)
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
	private sealed class _003CDelayMoveBoxItemsToScreen_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public ForgeScene _003C_003E4__this;

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
		public _003CDelayMoveBoxItemsToScreen_003Ed__29(int _003C_003E1__state)
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
	private sealed class _003CDelayPlaySoundWindowSlide_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

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
		public _003CDelayPlaySoundWindowSlide_003Ed__28(int _003C_003E1__state)
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
	private sealed class _003CDelayShowBonus_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public BonusModel bonus;

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
		public _003CDelayShowBonus_003Ed__38(int _003C_003E1__state)
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
	private sealed class _003CPlaySoundHammerHitAnvil_003Ed__37 : IEnumerator<object>, IEnumerator, IDisposable
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
		public _003CPlaySoundHammerHitAnvil_003Ed__37(int _003C_003E1__state)
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

	private const int TAB_BUTTON_WEAPON = 1;

	private const int TAB_BUTTON_ARMOR = 2;

	private const int TAB_BUTTON_SHOES = 3;

	private const int TAB_BUTTON_ACCESSORY = 4;

	public TabManager m_TabItems;

	public GridPoolGroup m_PoolEquipment;

	public BoxForge m_BoxForge;

	public GameObject m_GoCellForgePrefab;

	public GameObject m_GoSrollEquipment;

	public GameObject m_GoButtonForge;

	public GameObject m_PointHideBoxItems;

	public GameObject m_GoBoxItems;

	public Text m_TextItem;

	public GameObject m_GoNotifyWeapon;

	public GameObject m_GoNotifyArmor;

	public GameObject m_GoNotifyShoes;

	public GameObject m_GoNotifyAccessory;

	private List<ItemEquipment> m_LstWeapon;

	private List<ItemEquipment> m_LstArmor;

	private List<ItemEquipment> m_LstShoes;

	private List<ItemEquipment> m_LstAccessory;

	private ItemEquipment m_CurrentOutputItemEquipment;

	private bool m_HasWeaponCanBeForge;

	private bool m_HasArmorCanBeForge;

	private bool m_HasShoesCanBeForge;

	private bool m_HasAccessoryCanBeForge;

	private int m_CurrentButtonTab;

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayPlaySoundWindowSlide_003Ed__28))]
	public IEnumerator DelayPlaySoundWindowSlide(float delay)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayMoveBoxItemsToScreen_003Ed__29))]
	private IEnumerator DelayMoveBoxItemsToScreen(float delay)
	{
		return null;
	}

	private void InitListEquipmentData()
	{
	}

	private void RemoveItemCannotBeForged(List<ItemEquipment> lstItem)
	{
	}

	public void ButtonBackClick()
	{
	}

	private void OnItemEquipmentClick(ItemEquipment itemOutput)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void CheckTabNotification()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayMakeForgeAnimation_003Ed__36))]
	private IEnumerator DelayMakeForgeAnimation()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CPlaySoundHammerHitAnvil_003Ed__37))]
	private IEnumerator PlaySoundHammerHitAnvil()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayShowBonus_003Ed__38))]
	private IEnumerator DelayShowBonus(float delay, BonusModel bonus)
	{
		return null;
	}

	private bool CheckForEquipmentHasEnoughMatterial(List<ItemEquipment> lstEquipment)
	{
		return false;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void HelpClick()
	{
	}
}
