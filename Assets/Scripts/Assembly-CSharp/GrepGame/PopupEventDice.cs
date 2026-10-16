using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace GrepGame
{
	public class PopupEventDice : MonoEventHandler, IG2UIEventHandler
	{
		[CompilerGenerated]
		private sealed class _003CDelayBeforerePlay_003Ed__42 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public PopupEventDice _003C_003E4__this;

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
			public _003CDelayBeforerePlay_003Ed__42(int _003C_003E1__state)
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
		private sealed class _003CDelayPlayStepAnimation_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public PopupEventDice _003C_003E4__this;

			public int numberStep;

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
			public _003CDelayPlayStepAnimation_003Ed__31(int _003C_003E1__state)
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

		private bool showPopupRandomBonus;

		[SerializeField]
		private CanvasHero m_CanvasHero;

		[SerializeField]
		private CanvasHero m_CanvasBossHero;

		[SerializeField]
		private List<DiceTile> m_ListAllDice;

		[SerializeField]
		private SpineObject m_SpineDiceRolling;

		[SerializeField]
		private Image m_ImgDiceNumber;

		[SerializeField]
		private GameObject m_GoDiceNumberGreen;

		[SerializeField]
		private GameObject m_GoGroupAction;

		[SerializeField]
		private Text m_NamePlayerPlaying;

		public Text m_MyHp;

		public Text m_MyAttack;

		public Text m_EnemyHp;

		public Text m_EnemyAttack;

		private long enemyHp;

		private long enemyAtk;

		private long heroHp;

		private long heroyAtk;

		private bool m_AutoPlay;

		public Image m_ImgAutoPlayOn;

		public Image m_ImgAutoPlayOff;

		public bool isBossPlaying;

		private int m_CurrentIndexPostion;

		private int m_CurrentIndexBossPosition;

		private int m_CurrentIndexMyHeroPosition;

		private float m_Duration;

		private bool isAnimationPlaying;

		private int numberStep;

		private bool checkOutGame;

		protected override void Start()
		{
		}

		public override void OnDestroy()
		{
		}

		public void OnButtonPlayClick()
		{
		}

		private void PlayDice()
		{
		}

		[IteratorStateMachine(typeof(_003CDelayPlayStepAnimation_003Ed__31))]
		private IEnumerator DelayPlayStepAnimation(int numberStep)
		{
			return null;
		}

		private void ShowBonus(DiceTile diceTitle)
		{
		}

		public void OnButtonPauseClick()
		{
		}

		private void OnBossPlay()
		{
		}

		private void PlayAnimationMoveTo(Vector2 positionStart, Vector2 positionEnd)
		{
		}

		public void OnButtonAutoClick()
		{
		}

		private void MoveToIndex(int index)
		{
		}

		public static PopupEventDice ShowUp()
		{
			return null;
		}

		private void Update()
		{
		}

		public void ProcessG2Event(G2EventData eventData)
		{
		}

		[IteratorStateMachine(typeof(_003CDelayBeforerePlay_003Ed__42))]
		private IEnumerator DelayBeforerePlay()
		{
			return null;
		}
	}
}
