using UnityEngine;

public class PanelHome2Center : MonoEventHandler
{
	public GameObject m_GoNotifyEvent;

	public GameObject m_GoNotifyCheckin;

	private ActionRepeatTimer m_TimerCheckNotify;

	private const float TIME_CACHE_LIST_SPECIAL_EVENT = 1800f;

	private const float TIME_CACHE_LIST_COOL_EVENT = 1920f;

	public TranslateController m_TranslateController;

	public GameObject m_PrefabIconEvent;

	public Transform m_TfButtonContainer;

	public GameObject m_GoButtonFirstPurchase;

	public static bool m_HasAutoShowFirstPurchase;

	public static bool m_ForceReloadListSpecialEvent;

	private const int HOME2_EVENT_LOAD_LIST_POPUP_EVENT = 7000;

	private const int HOME2_EVENT_LOAD_FIRST_PURCHASE_EVENT = 7001;

	private const int GAME_EVENT_ID_FIRST_PURCHASE = 602;

	private GameFirstPurchaseEvent _eventFirstPurchase;

	private string _urlEventFirstPurchaseData;

	private const int HOME2_EVENT_LOAD_CHECK_IN_EVENT = 7003;

	protected override void Start()
	{
	}

	public void ButtonLuckySpinClick()
	{
	}

	public void ButtonRollCallClick()
	{
	}

	public void ButtonEventClick()
	{
	}

	private void EventClick(int typeEvent)
	{
	}

	private void LoadListPopupEvent()
	{
	}

	public void ButtonFirstPurchaseClick()
	{
	}

	private void ParseListCoolEventToFindFirstPurchaseEvent(string strData)
	{
	}

	private void CheckNotifiLogin()
	{
	}

	private void ParseAndShowListEventSpecial(string strData)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public bool IsSpecialEventTypeValid(int type)
	{
		return false;
	}

	private void Update()
	{
	}
}
