using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelTravelEvent : MonoEventHandler
{
	[CompilerGenerated]
	private sealed class _003CShowBonus_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public List<object> lstObjModel;

		public PanelTravelEvent _003C_003E4__this;

		private List<object>.Enumerator _003C_003E7__wrap1;

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
		public _003CShowBonus_003Ed__23(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
		}
	}

	private const string ANIM_IDLE = "AnimCharacterIdle";

	private const string ANIM_MOVE = "AnimCharacterRun";

	public Image m_BgrEvent;

	public Image m_BgrTrip;

	public Image m_Avatar;

	public Image m_ImgItem;

	public Text m_NumberMaterial;

	public List<GameObject> m_ListChest;

	public PathEditor m_Path;

	public GameObject m_PanelChooseWay;

	public GameObject m_Charac;

	public List<GameObject> m_ListButtonChooses;

	public GameObject m_ItemBonusShow;

	public Transform m_TrfBonus;

	private TravelEvent m_EventData;

	private int m_IdTrip;

	private bool m_IsMove;

	private bool m_EndTrip;

	private string urlEventDetail;

	private Vector3 posTarget;

	protected override void Start()
	{
	}

	public void LoadEventData(TravelEvent gameEvent)
	{
	}

	private void FillDataTrip()
	{
	}

	[IteratorStateMachine(typeof(_003CShowBonus_003Ed__23))]
	public IEnumerator ShowBonus(List<object> lstObjModel)
	{
		return null;
	}

	public void BackClick()
	{
	}

	public void HelpClick()
	{
	}

	private void ChooseClick(TripInfo tripInfo)
	{
	}

	public void MoveClick()
	{
	}

	public void ChestClick(int id)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
