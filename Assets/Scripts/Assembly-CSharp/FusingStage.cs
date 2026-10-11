using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FusingStage : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayMakeFusingAnimation_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public FusingStage _003C_003E4__this;

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
		public _003CDelayMakeFusingAnimation_003Ed__17(int _003C_003E1__state)
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

	public List<HeroMaterialSlot> m_LstHeroMaterialSlot;

	public CanvasHero m_CanvasHero;

	public GameObject m_GoPointCenter;

	public GameObject m_GoEffectContainer;

	private FusingHeroSet m_FusingSet;

	private HeroModel m_HeroFusing;

	public SpineObject m_SpineEmerge;

	public GameObject m_GroupButtonDetail;

	private List<int> m_LstLastMaterialHeroId;

	public PanelLegendFusing m_PanelLegend;

	private void Start()
	{
	}

	private List<int> GetListMaterialHeroId()
	{
		return null;
	}

	private List<int> GetListHeroIdFillInSlots(int slotIndexNotCountIn)
	{
		return null;
	}

	public void ButtonDetailClick()
	{
	}

	public void ButtonLegendClick()
	{
	}

	public void ButtonFuseClick()
	{
	}

	public void MakeFusingAnimation()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayMakeFusingAnimation_003Ed__17))]
	private IEnumerator DelayMakeFusingAnimation()
	{
		return null;
	}

	public List<object> RemoveMaterialHeroFromInventory()
	{
		return null;
	}

	public void ClearFusingStage()
	{
	}

	public void SetFusingSet(HeroModel heroFusing, FusingHeroSet fusingSet)
	{
	}

	public void AutoFillHeroToAllSlot()
	{
	}
}
