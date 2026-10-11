using UnityEngine;
using UnityEngine.UI;

public class PanelPrayBattle : MonoEventHandler
{
	public Text feeAtk;

	public GameObject boss;

	public Text txtNumAttack;

	public TextCountdown textcountDown;

	public GameObject m_GoNumberAttack;

	public Text m_Notify;

	public GameObject m_GoFeeAtk;

	public GameObject m_buttonBattle;

	public HorizontalLayoutGroup m_LayoutButtonAttack;

	public CanvasHero m_HeroBoss;

	private int bossId;

	public const int GUILD_PRAY_BOSS = 2;

	protected override void Start()
	{
	}

	public void GiftClick()
	{
	}

	public void HelpClick()
	{
	}

	public void ButtonBattleClick()
	{
	}

	public void BattleStart(HeroesFormation heroFormation)
	{
	}

	public void SetPrayBattleData(long countTime, int bossId, bool allowToBattle, int turnLeft, int totalTurn, int priceCost)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
