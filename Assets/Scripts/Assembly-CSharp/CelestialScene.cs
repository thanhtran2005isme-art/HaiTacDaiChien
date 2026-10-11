using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CelestialScene : BaseScene
{
	[CompilerGenerated]
	private sealed class _003CShowBonus_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public List<object> lstObjModel;

		public CelestialScene _003C_003E4__this;

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

	public const int BUILD_ZONE = 0;

	public const int CAMP_ZONE = 1;

	[HideInInspector]
	public bool isTutorial;

	public ScrollRect m_Map;

	public Text m_NumberDaffodil;

	public List<MineController> m_ListMine;

	public GameObject m_AGIBuil;

	public GameObject m_MiliBuild;

	public GameObject m_DaffodilBuild;

	public CelesMainBuilding m_MainBuilding;

	public GameObject m_GoButtonSmash;

	public GameObject m_ItemBonusShow;

	public Transform m_TrfBonus;

	public Transform m_PointBtnBuy;

	private float timeUpdateInfoMine;

	private Vector2 m_PointBuildZone;

	private Vector2 m_PointCampZone;

	public RectTransform contentMap;

	[HideInInspector]
	public GameObject m_GoHandSelect;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	public void MoveBallTitle(Vector2 position)
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

	private void FillDataBuild()
	{
	}

	private void FillDataMine(bool isUpdate = false)
	{
	}

	public void RefreshBuilding()
	{
	}

	public GameObject ShowFinger(Transform parent, int zone)
	{
		return null;
	}

	public void TowerClick()
	{
	}

	public void AddDaffodil()
	{
	}

	public void SmashClick()
	{
	}

	public void HelpClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
