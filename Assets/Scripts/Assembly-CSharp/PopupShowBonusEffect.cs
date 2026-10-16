using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PopupShowBonusEffect : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CShowItem10_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupShowBonusEffect _003C_003E4__this;

		private int _003Ci_003E5__2;

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
		public _003CShowItem10_003Ed__17(int _003C_003E1__state)
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
	private sealed class _003CShowItemGreaterThan10_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PopupShowBonusEffect _003C_003E4__this;

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
		public _003CShowItemGreaterThan10_003Ed__18(int _003C_003E1__state)
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

	public Transform m_TransformContent;

	public ScrollRect m_ScrollRect;

	public GameObject m_CloseButton;

	public Action m_CloseClickCb;

	public Action m_CloseMiniGameCb;

	public GridLayoutGroup m_GridLayout;

	private bool showed;

	[SerializeField]
	private RectTransform m_RectGroupAction;

	[SerializeField]
	private GameObject m_GoButtonAction2;

	[SerializeField]
	private Text m_TextCostAction2;

	[SerializeField]
	private Text m_TextAction2;

	[SerializeField]
	private Image m_ImageItemCostAction2;

	private Action m_Action2Cb;

	public void SetBonusModel(BonusModel bonusModel)
	{
	}

	public void ShowActionButton2(int itemConsumeKey, int numberCost, string strAction, Action action2Cb)
	{
	}

	public void ButtonAction2Click()
	{
	}

	private bool CheckShouldHighlightItem(object objModel)
	{
		return false;
	}

	[IteratorStateMachine(typeof(_003CShowItem10_003Ed__17))]
	private IEnumerator ShowItem10()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CShowItemGreaterThan10_003Ed__18))]
	private IEnumerator ShowItemGreaterThan10()
	{
		return null;
	}

	public void ButtonCloseClick()
	{
	}

	public void SkipAnim()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public static PopupShowBonusEffect ShowUp(BonusModel bonusModel)
	{
		return null;
	}

	public static PopupShowBonusEffect ShowUp(BonusModel bonusModel, Action<GameObject> cbTweenShow = null)
	{
		return null;
	}
}
