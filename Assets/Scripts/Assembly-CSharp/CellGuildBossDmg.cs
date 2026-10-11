using UnityEngine;
using UnityEngine.UI;

public class CellGuildBossDmg : MonoBehaviour
{
	public PlayerAvatar m_PlayerAvatar;

	public Text m_TextRank;

	public Text m_TextPlayername;

	public Text m_TextDamage;

	public Image m_ImageTagRank;

	public Slider m_SliderDamage;

	public GameItem m_GameItemCoin;

	public GameItem m_GameItemGold;

	private void Start()
	{
	}

	public void SetData(GuildBossTopDamage bossDmg, GuildBossStage bossStage = null)
	{
	}

	private void Update()
	{
	}
}
