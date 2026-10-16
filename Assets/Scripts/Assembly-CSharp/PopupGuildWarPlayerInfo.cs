using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupGuildWarPlayerInfo : BasePopup
{
	public PlayerAvatar m_PlayerAvatar;

	public Text m_TextPlayername;

	public Text m_TextId;

	public Text m_TextPower;

	public HeroesLineup m_HeroesLineup;

	public Text m_TextAttackEnemy;

	public Text m_TextDefenseFromEnemy;

	public WarStarStatus m_StarAttack;

	public WarStarStatus m_StarDefense;

	public List<GameObject> m_LstGoGameItemReward;

	public GameObject m_GoGroupAction;

	public RectTransform m_RectTfBody;

	public PetHeadUI m_PetHeadUI;

	private GuildWarPlayer m_WarPlayer;

	protected override void Start()
	{
	}

	public static PopupGuildWarPlayerInfo ShowUp(GuildWarPlayer warPlayer, bool showButtonAttack)
	{
		return null;
	}

	public void SetPlayerInfo(GuildWarPlayer warPlayer, bool showButtonAttack)
	{
	}

	public void ButtonAttackClick()
	{
	}

	private void Update()
	{
	}
}
