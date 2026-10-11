using UnityEngine;
using UnityEngine.UI;

public class PopupChatNew : MonoEventHandler
{
	public TranslateController m_AniShowPopup;

	public TranslateController m_AniHidePopup;

	private const float TIME_INTERVAL_LOAD_MORE_CHAT = 20f;

	private const int TAB_WORLD = 1;

	private const int TAB_SERVER = 2;

	private const int TAB_GUILD = 3;

	private const int TAB_RECRUIT = 4;

	private const int TAB_ADMIN = 5;

	public BoxChatSetting m_BoxChatSetting;

	public TabManager m_TabManager;

	public ChatItem m_PreItemChat;

	public InputField m_InputChat;

	public GameObject m_GroupInput;

	public GameObject m_GroupInputBlock;

	public VerticalPoolGroup scrollView;

	public VerticalPoolGroup scrollViewRecruit;

	public GameObject m_GoNotifyWorld;

	public GameObject m_GoNotifyClan;

	public GameObject m_GoNotifyRecruit;

	public GameObject m_GoNotifyServer;

	public GameObject m_GoNotifyAdmin;

	private ActionRepeatTimer m_ActionLoadMoreChat;

	private float _countDownToLoadMoreChat;

	private int m_CurrentTabId;

	private bool _forceScrollToLastWhenDataArrived;

	private bool _onCloseing;

	protected override void Start()
	{
	}

	private void Init()
	{
	}

	private void InitPool()
	{
	}

	public void AddFriendClick(object data)
	{
	}

	public void ActionTextClick(object data)
	{
	}

	private void OnTabClick(int tabBtnId)
	{
	}

	public void ButtonSettingClick()
	{
	}

	public void ButtonSendChatClick()
	{
	}

	private ChatCategoryData GetCurrentChatData()
	{
		return null;
	}

	private void LoadMoreChat(ChatCategoryData chatCatData, bool forceScrollToLastWhenDataArrived)
	{
	}

	private void ShowChat(ChatCategoryData chatData, bool scrollToLast)
	{
	}

	private bool CheckIfShowingLastChatElement()
	{
		return false;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupChatNew ShowUp()
	{
		return null;
	}

	public void ButtonCloseClick()
	{
	}

	private void Update()
	{
	}
}
