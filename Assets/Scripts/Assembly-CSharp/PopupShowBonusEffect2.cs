using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupShowBonusEffect2 : MonoBehaviour
{
	public delegate bool DelegateCheckToPauseRevealItem(object objItemModel);

	[CompilerGenerated]
	private sealed class _003CRevealRemainItemOneByOne_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupShowBonusEffect2 _003C_003E4__this;

		private float _003Cinterval_003E5__2;

		private int _003Ci_003E5__3;

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
		public _003CRevealRemainItemOneByOne_003Ed__12(int _003C_003E1__state)
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

	public RectTransform m_TfContainer;

	public GameObject m_CloseButton;

	public Action m_CloseClickCb;

	public GridLayoutGroup m_GridLayout;

	private List<GameObject> m_LstGoGameItem;

	private int m_CountFlipItem;

	private List<object> m_LstObjItemModel;

	public DelegateCheckToPauseRevealItem m_CheckPauseReveal;

	public void SetBonusModel(BonusModel bonusModel)
	{
	}

	public void ResumeRevealBonus()
	{
	}

	private bool CheckShouldHighlightItem(object objModel)
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CRevealRemainItemOneByOne_003Ed__12))]
	private IEnumerator RevealRemainItemOneByOne()
	{
		return null;
	}

	public void ButtonCloseClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static PopupShowBonusEffect2 ShowUp(BonusModel bonusModel)
	{
		return null;
	}
}
