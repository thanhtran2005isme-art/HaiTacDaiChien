using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SealedLandSelectBattle : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CCheckRotateText_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public SealedLandSelectBattle _003C_003E4__this;

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
		public _003CCheckRotateText_003Ed__35(int _003C_003E1__state)
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
	private sealed class _003CDelayDoubleClickNext_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SealedLandSelectBattle _003C_003E4__this;

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
		public _003CDelayDoubleClickNext_003Ed__32(int _003C_003E1__state)
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
	private sealed class _003CDelayDoubleClickPre_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SealedLandSelectBattle _003C_003E4__this;

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
		public _003CDelayDoubleClickPre_003Ed__31(int _003C_003E1__state)
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
	private sealed class _003CDelayRotate_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int dirrection;

		public SealedLandSelectBattle _003C_003E4__this;

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
		public _003CDelayRotate_003Ed__34(int _003C_003E1__state)
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

	private int m_levelAbyss;

	private int m_levelShadow;

	private int m_levelFortress;

	private int m_levelForest;

	private int m_levelDark;

	private int m_levelLight;

	[HideInInspector]
	private static int m_CurrentFactionIndex;

	[SerializeField]
	private List<Text> m_lstTextLevel;

	[SerializeField]
	private List<GameObject> P_text;

	[SerializeField]
	private Image m_ImageCenter;

	private float m_PreTime;

	private float m_PosTime;

	public GameObject m_GoBoxFactionLevel;

	private const int DIRECTION_NEXT = 1;

	private const int DIRECTION_PRE = -1;

	private const float TIME_ROTATE = 0.5f;

	private const int DIRECTION_NULL = 0;

	public GameObject m_GoRotate;

	private int[] m_ArrFactionLevel;

	private List<HeroFaction> m_ListSealedFaction;

	public SpineObject m_EffectCenter;

	public SpineObject m_EffectGlow;

	private static int factionLevel;

	protected override void Start()
	{
	}

	public HeroFaction GetFactionSelected()
	{
		return (HeroFaction)0;
	}

	private void PlayCenterAnimationForFaction(HeroFaction faction)
	{
	}

	public void SetPanelData(int lvAbyss, int lvShadow, int lvFortress, int lvForest, int lvDark, int lvLight)
	{
	}

	public void ButtonPrevFactionClick()
	{
	}

	public void ButtonNextFactionClick()
	{
	}

	public void ButtonFactionClick(int faction)
	{
	}

	private void SetDirectionFactionButtonClick(int value)
	{
	}

	private void SetDirectionFactionButtonDoubleClick(int value)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayDoubleClickPre_003Ed__31))]
	private IEnumerator DelayDoubleClickPre()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CDelayDoubleClickNext_003Ed__32))]
	private IEnumerator DelayDoubleClickNext()
	{
		return null;
	}

	private void ButtonSelectClick(int oldFactionIndex, int currentFactionIndex, int dirrection)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayRotate_003Ed__34))]
	private IEnumerator DelayRotate(int dirrection)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CCheckRotateText_003Ed__35))]
	private IEnumerator CheckRotateText(float delay)
	{
		return null;
	}

	public void AlphaUpdateImageCenter(float alphaValue)
	{
	}

	private void PlayCenterEffect(int indexStage)
	{
	}

	private void PlayGlowEffect()
	{
	}

	private void CheckStage()
	{
	}

	private void ShowPanelListLevel()
	{
	}

	public void ButtonShowLevelFactionClick()
	{
	}

	private void SetPosSelect(GameObject obj)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
