using UnityEngine;

public class PanelPrayStart : MonoEventHandler
{
	public const int GUILD_PRAY_SIGN_UP = 1;

	public const int GUILD_PRAY_BOSS = 2;

	public const int GUILD_PRAY_REGISTERED = 1;

	public const int GUILD_PRAY_NOT_REGISTERED = 0;

	public GameObject m_panelSignUp;

	public GameObject m_panelBattle;

	public GameObject m_panelPrayTopDamage;

	public static int bossId;

	private new void Start()
	{
	}

	private void Update()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
