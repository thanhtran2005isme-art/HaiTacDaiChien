using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using pbdson;

public class PopupShop : BasePopup, ITabListener
{
	[CompilerGenerated]
	private sealed class _003CDelayRequestShopData_003Ed__60 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public PopupShop _003C_003E4__this;

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
		public _003CDelayRequestShopData_003Ed__60(int _003C_003E1__state)
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

	public const int SHOP_MARKET = 1;

	public const int SHOP_CASINO = 2;

	public const int SHOP_ALTAR = 3;

	public const int SHOP_GUILD = 4;

	public const int SHOP_ARENA = 5;

	public const int SHOP_BRAVE_TRIAL = 6;

	public const int SHOP_ALL_STARS = 7;

	public const int SHOP_BIRD_FEATHER_HERO = 8;

	public const int SHOP_BIRD_FEATHER_ARTIFACT = 9;

	public const int SHOP_SEALEDLAND = 10;

	public const int SHOP_MARINEFORD = 13;

	private int m_ShopId;

	public RectTransform m_TransformContainer;

	public GameObject m_PrefabShopItem;

	public Text m_TextCostRefresh;

	public Image m_ImageIconCostRefresh;

	public GameObject m_ButtonRefresh;

	public TextCountdown m_TextCountdown;

	public GameObject m_GoTextRefresh;

	public RectTransform m_TfGroupAsset;

	public GameObject m_FieldGem;

	public GameObject m_FieldGold;

	public GameObject m_FieldSoulCoin;

	public GameObject m_FieldBraveCoin;

	public GameObject m_FieldLuckCoin;

	public GameObject m_FieldArenaCoin;

	public GameObject m_FieldGuildCoin;

	public GameObject m_FieldBirdFeather;

	public GameObject m_FieldVoSongCoin;

	public GameObject m_FieldBookNormal;

	public GameObject m_FieldBookDL;

	public GameObject m_FieldMarinefordCoin;

	private const int ITEM_PER_PAGE = 12;

	private int m_NumPage;

	private int m_CurPageIdx;

	private bool m_IsFreeToRefresh;

	private bool m_IsShopJustRefresh;

	private List<ShopItemInfo> m_LstShopItemData;

	private List<GameObject> m_LstGoShopItem;

	private List<TabShopInfo> m_LstTabShopInfo;

	public TabManager m_TabManager;

	public GameObject m_GoPrefabTabButton;

	public Transform m_TransformTabContainer;

	public Button m_BtnBack;

	public Action m_LoadCompleteCb;

	public Action m_BuyCompleteCb;

	private Dictionary<int, ListCommonVector> _cacheShopData;

	private ActionRepeatTimer _timerCountDownCacheShop;

	[SerializeField]
	private VerticalPoolGroup _poolGroup;

	protected override void Awake()
	{
	}

	private void HideAllFieldMaterial()
	{
	}

	protected override void Start()
	{
	}

	private void Update()
	{
	}

	public void LoadShopData(int shopId)
	{
	}

	public void HideTab()
	{
	}

	public void GenAllShopTab(int shopIdActive)
	{
	}

	public void GenListShopTab(List<int> lstShopId)
	{
	}

	public string GetShopName(int shopId)
	{
		return null;
	}

	private void GenShopTab(List<TabShopInfo> lstTabInfo, int shopIdActive)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayRequestShopData_003Ed__60))]
	private IEnumerator DelayRequestShopData(float delay)
	{
		return null;
	}

	public ShopItem GetFirstShopItem()
	{
		return null;
	}

	private void ShowPlayerAsset(int shopId)
	{
	}

	public void ButtonRefreshClick()
	{
	}

	private void FirstLoadShop(int currentPageIdx = 0)
	{
	}

	private void LoadPageNew()
	{
	}

	private void LoadPage(int pageIdx, bool playRefreshOnItem)
	{
	}

	public static PopupShop ShowUp()
	{
		return null;
	}

	public static PopupShop ShowUp(int shopId)
	{
		return null;
	}

	public static PopupShop ShowUp(List<int> lstShopId)
	{
		return null;
	}

	private void ShowShopItems(ListCommonVector lstCv)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ButtonCloseClick()
	{
	}
}
