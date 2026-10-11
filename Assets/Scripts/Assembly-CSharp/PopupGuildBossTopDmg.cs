using UnityEngine;

public class PopupGuildBossTopDmg : BasePopup
{
	public VerticalPoolGroup m_PoolTopDmg;

	public GameObject m_GoScrollTop;

	public GameObject m_GoCellTop;

	private int m_BossId;

	private GuildBossStage m_BossStage;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void TestTopDmg()
	{
	}

	public void SetBossLevel(int bossId)
	{
	}

	public static PopupGuildBossTopDmg ShowUp(int bossId)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
