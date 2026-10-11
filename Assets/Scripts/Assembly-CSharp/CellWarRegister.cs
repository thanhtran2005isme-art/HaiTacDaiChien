using System;
using UnityEngine;
using UnityEngine.UI;

public class CellWarRegister : MonoBehaviour
{
	public PlayerAvatar m_PlayerAvatar;

	public Text m_TextIndex;

	public Text m_TextPlayername;

	public Text m_TextPower;

	public HeroesLineup m_HeroesLineup;

	public PetHeadUI m_PetHeadUI;

	public GameObject m_GoGroupSort;

	public Image m_ImageCellBgr;

	public GameObject m_GoTick;

	public ButtonToggle m_ToggleTick;

	private PlayerGuildWarRegister m_Player;

	public Action<CellWarRegister, bool> m_ToggleSelectChanged;

	private void Start()
	{
	}

	public void SetPlayerRegister(PlayerGuildWarRegister player, bool allowEdit = false)
	{
	}

	private void ToggleCheckChanged(bool check)
	{
	}

	private void Update()
	{
	}
}
