using UnityEngine;
using UnityEngine.UI;

public class CellAllStarsTop8 : MonoBehaviour, IG2UIEventHandler
{
	public PlayerAvatar m_Avatar1;

	public PlayerAvatar m_Avatar2;

	public Text m_TextPlayer1;

	public Text m_TextPlayer2;

	public Text m_Guild1;

	public Text m_Guild2;

	public GameObject m_GoImageVs;

	public Image m_ImgWinLose1;

	public Image m_ImgWinLose2;

	public Text m_TextWinLose1;

	public Text m_TextWinLose2;

	public GameObject m_GoButtonReplay;

	public GameObject m_GoButtonBet;

	public Sprite m_SpriteBet;

	public Sprite m_SpriteBgr;

	public GameObject m_GroupMid;

	public Image m_ImageBgr;

	public Image m_Sicbo;

	private AllStarBattleInfo m_BattleInfo;

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	public void SetBattleInfo(AllStarBattleInfo battleInfo)
	{
	}

	public void ButtonReplayClick()
	{
	}

	public void ButtonBetClick()
	{
	}

	private void Update()
	{
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
