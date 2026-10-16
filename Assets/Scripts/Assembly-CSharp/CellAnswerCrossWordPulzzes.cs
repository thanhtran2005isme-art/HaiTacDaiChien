using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CellAnswerCrossWordPulzzes : MonoEventHandler, IG2UIEventHandler
{
	[CompilerGenerated]
	private sealed class _003CSetAnimation_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CellAnswerCrossWordPulzzes _003C_003E4__this;

		public Sprite spriteOff;

		public Sprite spriteOn;

		public float timeDelay;

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
		public _003CSetAnimation_003Ed__14(int _003C_003E1__state)
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

	public Text answer;

	public Text indexAnswer;

	public Button touchAnswer;

	public Image imgCheckTrue;

	public Image imgCheckFalse;

	public GameObject checkTrue;

	public GameObject checkFail;

	[HideInInspector]
	public int index;

	public Action<int> callBackSendAnswer;

	public Action cbPlayNextQuesOnEndAnimation;

	protected override void Awake()
	{
	}

	public override void OnDestroy()
	{
	}

	public void SetData(string answer, int indexAnswer)
	{
	}

	private void SetIndex(int index)
	{
	}

	[IteratorStateMachine(typeof(_003CSetAnimation_003Ed__14))]
	private IEnumerator SetAnimation(float timeDelay, Sprite spriteOn, Sprite spriteOff)
	{
		return null;
	}

	public void OnAnswerClick()
	{
	}

	public void ResetData()
	{
	}

	public void PlayAnimationTrue()
	{
	}

	public void PlayAnimationFail()
	{
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
