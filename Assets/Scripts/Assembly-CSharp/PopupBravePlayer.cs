using UnityEngine;
using UnityEngine.UI;

public class PopupBravePlayer : BasePopup
{
	public PlayerAvatar m_PlayerAvatar;

	public Text m_TextPlayername;

	public Text m_TextGuildname;

	public Text m_TextPower;

	public GameObject m_GoGuild;

	public HeroesLineup m_HeroesLineup;

	public Transform m_TransformRewards;

	public RectTransform m_TransformImageBgr;

	public GameObject m_GoButtonBattle;

	public GameObject m_GoPetHead;

	public Text m_TextTitle;

	protected override void Start()
	{
	}

	public static PopupBravePlayer ShowUp(BraveTrialStageInfo stage, bool showButtonBattle)
	{
		return null;
	}

	public void SetStageInfo(BraveTrialStageInfo stage, bool showButtonBattle)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonBattleClick()
	{
	}

	private void Update()
	{
	}
}
