using UnityEngine;
using UnityEngine.UI;

public class PopupAllStarBet : BasePopup, ITabListener
{
	public PlayerAvatar m_playerAvatar1;

	public PlayerAvatar m_playerAvatar2;

	public Text m_namePlayer1;

	public Text m_namePlayer2;

	public Text m_NameGuildPlayer1;

	public Text m_nameGuildPlayer2;

	public GameObject m_GoWinEven;

	public GameObject m_GoWinOdd;

	public Button m_Button_AcceptBet;

	public Button m_BetOdd;

	public Button m_BetEven;

	public Text m_Text_Bet;

	public TabManager m_TabManager;

	public const int BET_NOT_YET = 0;

	public const int BET_ODD = 1;

	public const int BET_EVEN = 2;

	private int idBet;

	private int matchIndex;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void ShowBattleInfo(AllStarBattleInfo battleInfo)
	{
	}

	public static void ShowUp(AllStarBattleInfo battleInfo)
	{
	}

	public void OnClickAccept()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}
}
