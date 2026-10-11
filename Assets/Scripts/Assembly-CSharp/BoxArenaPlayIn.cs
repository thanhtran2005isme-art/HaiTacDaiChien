using UnityEngine;
using UnityEngine.UI;

public class BoxArenaPlayIn : MonoBehaviour, IG2UIEventHandler
{
	public BoxAllStarsPlayer m_BoxPlayer1;

	public BoxAllStarsPlayer m_BoxPlayer2;

	public BoxAllStarsPlayer m_BoxPlayerWin;

	public GameObject m_LineWin1;

	public GameObject m_LineWin2;

	public PlayerAvatar m_AvatarWin;

	public Text m_NamePlayerWin;

	public Text m_GuildPlayerWin;

	public Text m_GuildCapPlayerWin;

	public Image m_ImageQuestion;

	public Button m_ButtonPlay;

	public Button m_ButtonBet;

	public Sprite m_SpriteBet;

	private AllStarBattleInfo m_BattleInfo;

	private void Start()
	{
	}

	public void OnDestroy()
	{
	}

	public void SetData(AllStarBattleInfo bi)
	{
	}

	public void ButtonBetClick()
	{
	}

	public void ButtonReplayClick()
	{
	}

	private void Update()
	{
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
