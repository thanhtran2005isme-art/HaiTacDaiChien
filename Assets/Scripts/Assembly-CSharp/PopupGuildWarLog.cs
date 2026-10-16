using UnityEngine;

public class PopupGuildWarLog : BasePopup
{
	public GameObject m_GoScrollView;

	public GameObject m_CellPrefab;

	public VerticalPoolGroup m_PoolGroup;

	private int m_FlagClan1;

	private int m_FlagClan2;

	private int m_ClanId1;

	private int m_ClanId2;

	private string m_ClanName1;

	private string m_ClanName2;

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

	public void LoadWarLog(int clanId1, int flagClan1, string clanName1, int clanId2, int flagClan2, string clanName2)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupGuildWarLog ShowUp(int clanId1, int flagClan1, string clanName1, int clanId2, int flagClan2, string clanName2)
	{
		return null;
	}

	private void Update()
	{
	}
}
