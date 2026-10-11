using UnityEngine;
using UnityEngine.UI;

public class PopupGuildBossAttack : BasePopup
{
	public VerticalPoolGroup m_PoolTopDmg;

	public GameObject m_GoScrollView;

	public CanvasHero m_CanvasHero;

	public Text m_TextPower;

	public Text m_TextHp;

	public Text m_TextCountDown;

	public Text m_TextGemCost;

	public Text m_TextTitle;

	public GameObject m_GoButtonAttack;

	public GameObject m_GoIconGemCost;

	public Slider m_SliderHp;

	public GameObject m_GoScrollTop;

	public GameObject m_GoCellTop;

	public TextCountdown m_TextCountdown;

	private GuildBoss m_GuildBoss;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void TestTopDmg()
	{
	}

	public void SetGuildBoss(GuildBoss boss)
	{
	}

	public void ButtonInfoClick()
	{
	}

	public void ButtonBattleClick()
	{
	}

	public static PopupGuildBossAttack ShowUp(GuildBoss guildBoss)
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
