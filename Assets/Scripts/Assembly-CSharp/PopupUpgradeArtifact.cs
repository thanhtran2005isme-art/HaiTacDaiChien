using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupUpgradeArtifact : BasePopup
{
	[CompilerGenerated]
	private sealed class _003CDelayLoadSpineEffect_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupUpgradeArtifact _003C_003E4__this;

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
		public _003CDelayLoadSpineEffect_003Ed__17(int _003C_003E1__state)
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

	public GameObject m_GoScrollArtifact;

	public GridPoolGroup m_PoolArtifact;

	public List<SlotArtifactMaterial> m_LstSlotMaterial;

	private List<ItemArtifact> m_ListItemArtifact;

	public ItemArtifactUI m_ArtifactUI_1;

	public ItemArtifactUI m_ArtifactUI_2;

	public Text m_TextMaterialFill;

	public Slider m_SliderMaterialFill;

	public Action m_UpgradeSuccessCallback;

	private int m_ValueMaterialCost;

	private bool m_ArtifactUpgraded;

	private int m_HeroId;

	private int m_ArtifactKeyUpgrade;

	private int m_ArtifactStarUpgrade;

	public SpineObject m_SpineEffectUp;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayLoadSpineEffect_003Ed__17))]
	private IEnumerator DelayLoadSpineEffect()
	{
		return null;
	}

	public override void ClosePopup()
	{
	}

	public void ButtonUpgradeClick()
	{
	}

	private void MaterialReturnCallback(ItemArtifact artifactReturn)
	{
	}

	private void ShowPopupAddMaterial(ItemArtifact artifactMaterial)
	{
	}

	private bool AddMaterial(ItemArtifact addArtifact)
	{
		return false;
	}

	private int CalculateFilledMaterialValue()
	{
		return 0;
	}

	public void SetArtifactToUpgrade(int heroId, ItemArtifact itemArtifact)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupUpgradeArtifact ShowUp(int heroId, ItemArtifact itemArtifact)
	{
		return null;
	}

	private void Update()
	{
	}
}
