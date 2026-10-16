using UnityEngine;
using UnityEngine.UI;

public class BoxArenaBattlePoint : MonoBehaviour
{
	public PlayerAvatar m_Avatar1;

	public PlayerAvatar m_Avatar2;

	public Text m_TextScore1;

	public Text m_TextScore2;

	public Text m_TextScoreCap1;

	public Text m_TextScoreCap2;

	public Text m_TextPlayer1;

	public Text m_TextPlayer2;

	public GameObject m_GoImageVs;

	private void Start()
	{
	}

	public void SetArenaPointResult(BattlePlayerInfo attacker, BattlePlayerInfo defender, ArenaPointResult pointResult)
	{
	}

	private void Update()
	{
	}
}
