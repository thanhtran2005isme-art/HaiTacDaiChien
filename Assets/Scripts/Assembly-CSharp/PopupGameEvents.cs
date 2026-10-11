using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class PopupGameEvents : BaseScene
{
	public const int TYPE_LIMIT_EVENT = 1;

	public const int TYPE_COOL_EVENT = 2;

	public const int TYPE_MONTHLY_EVENT = 3;

	public const int TYPE_SPECIAL_EVENT = 4;

	public const int TYPE_7_DAYS_EVENT = 7;

	public const int TYPE_LUNA_MOON_EVENT = 9;

	public const int TYPE_SPECIAL_LOGIN_EVENT = 10;

	public const int TYPE_SPECIAL_30_4_EVENT = 11;

	public const int TYPE_SPECIAL_TAM_BAO = 13;

	public const int TYPE_EVENT_XMAX = 12;

	public static bool TEST_EVENT_ICON_IMAGE;

	public static int m_IdTab;

	public static int m_IdTabEvent;

	public VerticalPoolGroup m_GoScrollTabContent;

	private int m_CurrentEventId;

	public GameObject m_GoContent;

	public Transform m_TabGroup;

	public GameObject m_PrefabPanelAccumulated;

	public GameObject m_PrefabPanelTopRacing;

	public GameObject m_PrefabPanelGiftCode;

	public GameObject m_PrefabPanelExchangeItem;

	public GameObject m_PrefabPanelExchangeItem2;

	public GameObject m_PrefabPanelFightBoss;

	public GameObject m_PrefabPanel7DaysLogin;

	public GameObject m_PrefabDiscount;

	public GameObject m_PrefabPackValue;

	public GameObject m_PrefabOnePack;

	public GameObject m_PrefabMonth;

	public GameObject m_PrefabFistPurchase;

	public GameObject m_PrefabExchangeEquip;

	public GameObject m_PrefabExchangeHero;

	public GameObject m_PrefabBrokenSpace;

	public GameObject m_PrefabBrokenSpace2;

	public GameObject m_PrefabPanelFund;

	public GameObject m_PrefabPanelFund2;

	public GameObject m_PrefabPanelHeroLottery;

	public GameObject m_PrefabPanelEasterPray;

	public GameObject m_PrefabPanelGroup;

	public GameObject m_PrefabPanelDauDanhTrang;

	private List<GameEventInfo> m_LstEventInfo;

	private ActionRepeatTimer m_TimerCheckTimeEvent;

	private int templateId;

	private int currTypeEvent;

	private string urlEventDetail;

	private GameEventInfo eventInfo;

	private GameObject goPanelEvent;

	private GameObject goPanelLoading;

	private int currTab;

	[HideInInspector]
	public bool isRefresh;

	protected override void Start()
	{
	}

	private void InitTab()
	{
	}

	private void TabClick(int tabId)
	{
	}

	public void GetListEvent(int typeEvent)
	{
	}

	private void InitTabEvent(List<GameEventInfo> lstEventInfo)
	{
	}

	public void RefreshEvent()
	{
	}

	private void TabEventClick(int eventId)
	{
	}

	public static bool IsEventTemplateValid(int templateId)
	{
		return false;
	}

	public void ButtonCloseClick()
	{
	}

	public void BackButton()
	{
	}

	public void ButtonShopFeatherClick()
	{
	}

	private void Update()
	{
	}

	private void FillInfoEvent(GameEventData baseData)
	{
	}

	public static AudioClip LoadEventAudioClip(string pathAudio)
	{
		return null;
	}

	public static void LoadEventSkeleton(SkeletonGraphic ani, string pathSpine)
	{
	}

	public static Sprite LoadEventSprite(string pathImage)
	{
		return null;
	}

	public List<GameEventInfo> groupEvents()
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static string GetPathIconMaterial(ItemModel item)
	{
		return null;
	}

	public static void ShowYourself()
	{
	}
}
