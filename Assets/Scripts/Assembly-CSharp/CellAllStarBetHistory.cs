using UnityEngine;
using UnityEngine.UI;

public class CellAllStarBetHistory : MonoBehaviour
{
	public PlayerAvatar m_AvatarPlayer1;

	public PlayerAvatar m_AvatarPlayer2;

	public Text m_NamePlayer1;

	public Text m_NamePlayer2;

	public Text m_NameGuildPlayer1;

	public Text m_NameGuildPlayer2;

	public Text m_TextBet;

	public Text m_TextResult;

	public Image m_BgrImage;

	public Image m_ImageBet;

	public GameObject m_BgrResult;

	private const int BET_NOT_YET = 0;

	private const int BET_WIN = 1;

	private const int BET_LOST = 2;

	private void Start()
	{
	}

	public void LoadCellData(TopCellBetHistory data)
	{
	}

	private void Update()
	{
	}
}
