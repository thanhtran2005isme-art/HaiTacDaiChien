using UnityEngine;
using UnityEngine.UI;

public class PopupFriendController : MonoEventHandler
{
	public const int TAB_FRIEND_LIST = 0;

	public const int TAB_APPLY = 1;

	public const int TAB_APPLY_LIST = 2;

	public const int TAB_SCOUT = 3;

	public Transform tabGroups;

	public GameObject[] listPanelContent;

	public Text title;

	public Text txt_NumberFriend;

	public GameObject m_NotifyReqApply;

	public GameObject m_NotifyScout;

	private int currTab;

	public void TabOnClick(int tabIndex)
	{
	}

	protected override void Start()
	{
	}

	private void Update()
	{
	}

	public void CloseOnClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static void ShowYourself(int tabFocus = 0)
	{
	}
}
