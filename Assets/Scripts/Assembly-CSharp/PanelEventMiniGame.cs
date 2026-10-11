using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelEventMiniGame : SpEventPanelBase
{
	[CompilerGenerated]
	private sealed class _003CStartRoll_003Ed__55 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public PanelEventMiniGame _003C_003E4__this;

		private int _003Cstep_003E5__2;

		private float _003CtimePass_003E5__3;

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
		public _003CStartRoll_003Ed__55(int _003C_003E1__state)
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

	private string m_UrlEventDetail;

	public VerticalPoolGroup poolGroup;

	public GameObject goBuyBadge;

	public GameObject goAction;

	public Image bgrLeft;

	public Image bgrRight;

	public List<Transform> lstTransformSlot;

	public RectTransform hightLight;

	public Image m_ImgMaterial;

	public Text txtNumberCoin;

	public Text txtTotalTurn;

	public Image iconBtnOneTurn;

	public Image iconBtnTenTurn;

	private BonusModel bonusModel;

	private ItemConsumable itemConsumableCost;

	[Header("Hole")]
	public GameItem dailyReward;

	public GameObject objDailyReward;

	public Text dailyDesc;

	[Header("Tween")]
	[SerializeField]
	private iTween.EaseType m_EaseTypeShow;

	public Vector3[] pos;

	public float duration;

	private float maxSpeed;

	private float minSpeed;

	private float currentSpeed;

	private float threadStep;

	private int fadeStep;

	private int finalIndex;

	private int currentIndex;

	private int inputIndex;

	private bool isDone;

	private bool isRunning;

	private bool isDecrSpeed;

	private bool isClickPlay;

	private int evenId;

	private string exchangeUrl;

	private ItemConsumable itemConsumableTotalTurn;

	private string m_textNumberHornBuy;

	private int m_MaxItem;

	private int numberCanBuy;

	private int NumberTurnRoll;

	private const int ONE_TURN_ROLL = 1;

	private string urlSpin;

	private const int TEN_TURN_ROLL = 10;

	private string help;

	private void ShowTween(GameObject obj)
	{
	}

	public override void LoadEventDetail(string url)
	{
	}

	private void ReloadEventDetail()
	{
	}

	public override void GetEventId(int eventId)
	{
	}

	protected override void Awake()
	{
	}

	public void SetData(DataMiniGameEvent data)
	{
	}

	private void SetGameItemForTreasure(List<object> lstObject)
	{
	}

	public void RequestRoll(int numberRoll)
	{
	}

	public void ResquestBuyTurn(int numberTurn)
	{
	}

	public void OnOneTurnClick()
	{
	}

	public void OnTenTurnClick()
	{
	}

	[IteratorStateMachine(typeof(_003CStartRoll_003Ed__55))]
	private IEnumerator StartRoll()
	{
		return null;
	}

	private void ShowReward(BonusModel bonusModel)
	{
	}

	private void CallStartRoll()
	{
	}

	private void ResetRoll()
	{
	}

	public void BuyClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void HelpClick()
	{
	}

	private void ReLoadBuyItem()
	{
	}

	private DataMiniGameEvent ParseData(string json)
	{
		return null;
	}
}
