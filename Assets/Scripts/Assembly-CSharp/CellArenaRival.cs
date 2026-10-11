using UnityEngine;
using UnityEngine.UI;

public class CellArenaRival : MonoBehaviour
{
	public PlayerAvatar m_PlayerAvatar;

	public Text m_TextPlayername;

	public Text m_TextPower;

	public Text m_TextPoint;

	public Text m_TextTicket;

	public Text m_TextPointCaption;

	public GameObject m_GoTicket;

	private ArenaType m_ArenaType;

	private ArenaPlayer m_Player;

	private void Start()
	{
	}

	public void SetData(ArenaType arenaType, int ticketCost, ArenaPlayer player)
	{
	}

	public void ButtonBattleClick()
	{
	}

	private void Update()
	{
	}
}
