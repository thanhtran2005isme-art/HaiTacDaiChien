using UnityEngine;
using UnityEngine.UI;

public class GuildBossUI : MonoBehaviour
{
	public CanvasHero m_CanvasHero;

	public Image m_ImageFrame;

	public GameObject m_GroupNormalBoss;

	public GameObject m_GroupPrayBoss;

	public Text m_TextNormalBossId;

	public Text m_TextNormalHpPercent;

	public Slider m_SliderNormalHp;

	public TextCountdown m_TextNormalCountdown;

	public Image m_ImageNormalBgr;

	public GameObject m_GoNormalUnknown;

	public GameObject m_GoNormalVeilDeath;

	public Text m_TextPrayBoss;

	private GuildBoss m_GuildBoss;

	private void Start()
	{
	}

	public void SetNormalBossBackground(int index)
	{
	}

	public void SetGuildBoss(GuildBoss boss)
	{
	}

	private void LoadPrayBoss(GuildBoss boss)
	{
	}

	private void LoadNormalBoss(GuildBoss boss)
	{
	}

	public void OnBossClick()
	{
	}

	private void Update()
	{
	}
}
