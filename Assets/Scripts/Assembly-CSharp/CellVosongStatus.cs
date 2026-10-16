using UnityEngine;
using UnityEngine.UI;

public class CellVosongStatus : MonoBehaviour
{
	public TextCountdown m_TextCountDown;

	public Text m_TextTitle;

	public Image m_ChildImage;

	public Sprite m_Round16;

	public Sprite m_ChildRound16;

	public Sprite m_Round8;

	public Sprite m_ChildRound8;

	public Sprite m_Round4;

	public Sprite m_ChildRound4;

	public Sprite m_Final;

	public Sprite m_ChildFinal;

	private int indexRound;

	private const int STAR_LOAD_TO_ROUND_16 = 15;

	private const int STAR_LOAD_TO_QUATER_FINAL = 7;

	private const int STAR_LOAD_TO_SEMI_FINAL = 3;

	private const int STAR_LOAD_TO_FINAL = 1;

	public void LoadCellData(AllStarBattleInfo data)
	{
	}

	public void OnCellClick()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
