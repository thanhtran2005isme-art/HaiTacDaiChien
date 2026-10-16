using System.Collections.Generic;

public class PopupAllStarsResult : BasePopup
{
	public VerticalPoolGroup m_PoolGroup;

	public const int ALL_STAR_TOTAL_MATCH = 15;

	public TextCountdown m_CountDownRound16;

	public TextCountdown m_CountDownQuaterFinal;

	public TextCountdown m_CountDownSemiFinal;

	public TextCountdown m_CountDownFinal;

	public List<AllStarBattleInfo> lstBattleInfo;

	public static PopupAllStarsResult instance;

	protected override void Awake()
	{
	}

	public void CountDown(int round, int coutdown)
	{
	}

	protected override void Start()
	{
	}

	public static PopupAllStarsResult ShowUp()
	{
		return null;
	}

	public void ButtonFinalClick()
	{
	}

	public void ButtonSemiFinalClick()
	{
	}

	public void ButtonQuaterFinalClick()
	{
	}

	public void ButtonTop8Click()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
