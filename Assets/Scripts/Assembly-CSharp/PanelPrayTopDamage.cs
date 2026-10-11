using UnityEngine.UI;

public class PanelPrayTopDamage : MonoEventHandler
{
	public VerticalPoolGroup m_PoolGroup;

	public Text numberMultiplier;

	public Text textProgress;

	public Slider sliderValue;

	public ButtonGuildAvatar m_GuildAvatar;

	public static int GuildId;

	private int guildAvatarId;

	protected override void Start()
	{
	}

	public void SetTopDamageStatus(long damage, long totalDmg, long multiplier)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
