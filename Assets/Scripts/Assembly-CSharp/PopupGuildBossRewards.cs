using UnityEngine;

public class PopupGuildBossRewards : BasePopup
{
	public VerticalPoolGroup m_PoolRewards;

	public GameObject m_GoScrollView;

	public GameObject m_GoCellRewards;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void SetGuildBoss(int bossId)
	{
	}

	public static PopupGuildBossRewards ShowUp(int bossId)
	{
		return null;
	}

	private void Update()
	{
	}
}
