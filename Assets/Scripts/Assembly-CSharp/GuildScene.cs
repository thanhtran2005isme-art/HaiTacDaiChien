using UnityEngine;

public class GuildScene : BaseScene
{
	private ActionRepeatTimer m_TimerNotification;

	public GameObject m_GoNotifyMill;

	public GameObject m_GoNotifyWar;

	public GameObject m_GoNotifyHall;

	public GameObject m_GoNotifyBoss;

	public Transform m_PointBtnShop;

	public Transform m_PointBtnBoss;

	public Transform m_PointBtnSkill;

	private int m_GuildLevel;

	public GameObject m_GoBoardNameMill;

	public GameObject m_GoBoardNameWar;

	private GameObject m_GoHandSelect;

	public GameObject m_GoGroupResources;

	protected override void Start()
	{
	}

	public void ButtonShopClick()
	{
	}

	public void GuildHallClick()
	{
	}

	public void GuildPrayForFireClick()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void GuildMillClick()
	{
	}

	public void GuildRaidClick()
	{
	}

	public void GuildWarClick()
	{
	}

	public void GuildSkillClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
