using UnityEngine.UI;

public class PopupEventRaid : BasePopup
{
	public const int SUB_POPUP_RAID_GOLD = 1;

	public const int SUB_POPUP_RAID_SPIRIT = 2;

	public const int SUB_POPUP_RAID_HERO = 3;

	private static int LastSubPopup;

	public Button m_ButtonBeli;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonChallengeGoldClick()
	{
	}

	public void ButtonChallengeSpiritClick()
	{
	}

	public void ButtonChallengeHeroClick()
	{
	}

	public void HelpChallengeGoldClick()
	{
	}

	public void HelpChallengeSpiritClick()
	{
	}

	public void HelpChallengeHeroClick()
	{
	}

	public static PopupEventRaid ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
