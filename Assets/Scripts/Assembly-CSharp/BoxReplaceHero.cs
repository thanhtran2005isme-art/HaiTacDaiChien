using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BoxReplaceHero : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CDelayMakeAnimationOutputAppear_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public BoxReplaceHero _003C_003E4__this;

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
		public _003CDelayMakeAnimationOutputAppear_003Ed__21(int _003C_003E1__state)
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

	public HeroMaterialSlot m_HeroMaterialInput;

	public HeroMaterialSlot m_Hero5StarInput;

	public ItemMaterial m_ItemStoneInput;

	private int m_StoneCost;

	private int m_NumHeroReplacementRequire;

	public Transform m_TfOutput;

	private HeroModel m_HmMaterialInput;

	public GameObject m_GoBoxInput;

	public GameObject m_GoBoxOutPut;

	public GameObject m_GoPointDownInput;

	public GameObject m_GoPointUpInput;

	public GameObject m_GoPointDownOutput;

	public GameObject m_GoPointUpOutput;

	public SpineObject m_EffectOutput;

	private List<int> m_LstHeroId5StarMaterial;

	protected override void Start()
	{
	}

	public void SetHeroToReplace(HeroModel hm)
	{
	}

	public void ClearOutPut()
	{
	}

	public void Hero5StarReplacementInputDone()
	{
	}

	public void MakeAnimationInputAppear()
	{
	}

	private void MakeAnimationOutputAppear()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayMakeAnimationOutputAppear_003Ed__21))]
	private IEnumerator DelayMakeAnimationOutputAppear()
	{
		return null;
	}

	public void CalculateHeroReturn(int heroKeyOutput)
	{
	}

	private int GetNumberHeroReplacementRequire(int heroStarInput)
	{
		return 0;
	}

	private int GetNumberStoneCostRequire(int heroStarInput)
	{
		return 0;
	}

	public void ButtonReplaceClick()
	{
	}

	public List<object> DeleteMaterialHero5Star()
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
