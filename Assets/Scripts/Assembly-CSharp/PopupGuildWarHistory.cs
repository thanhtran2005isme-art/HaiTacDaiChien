using UnityEngine;

public class PopupGuildWarHistory : BasePopup
{
	public GameObject m_GoScrollView;

	public GameObject m_CellPrefab;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_GoBoxMsgNoWar;

	public static int IndexScrollTo;

	private ActionRepeatTimer m_TimerCheckIndex;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void LoadTestData()
	{
	}

	public void LoadWarHistory(int clanId, bool showWarningNoWar)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupGuildWarHistory ShowUp(int clanId, bool showWarningNoWar = false)
	{
		return null;
	}

	private void Update()
	{
	}
}
