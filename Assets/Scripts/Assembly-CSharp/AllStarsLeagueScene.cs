using UnityEngine;

public class AllStarsLeagueScene : BaseScene
{
	public PanelPlayerAllStarsLeagueInfo m_PanelInfo;

	public PanelTopAllStarsPlayer m_PanelTop;

	private int m_PlayerPower;

	private int m_CurrRank;

	private HeroesFormation m_DefensiveFormation;

	[HideInInspector]
	public int m_CostBuyTurn;

	[HideInInspector]
	public int m_NumPurchaseLeft;

	[HideInInspector]
	public int m_StatusAllStars;

	private ActionRepeatTimer m_ActionCountdownTimer;

	private int m_CountdownPlayOff;

	private bool m_FirstLoadStatus;

	public static AllStarsLeagueScene Instance;

	protected override void Start()
	{
	}

	public bool IsOnPrepareLineUpForTop16()
	{
		return false;
	}

	public void ButtonShowBoardKnockOut()
	{
	}

	public void ButtonBackClick()
	{
	}

	private void ShowResultPrepare()
	{
	}

	public void HelpClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
