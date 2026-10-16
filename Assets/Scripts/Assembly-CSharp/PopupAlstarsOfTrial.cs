using System.Collections.Generic;
using UnityEngine.UI;

public class PopupAlstarsOfTrial : BasePopup, IG2UIEventHandler
{
	public Text numberBadge;

	public Image imageBadge;

	private BadgeInfo badgeInfo;

	private List<HeroModel> lstHealing;

	private List<HeroModel> lstRiveval;

	private int KEY_COIN;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public override void OnDestroy()
	{
	}

	public static void ShowPopup(BadgeInfo badgeInfo, List<HeroModel> lstHealing, List<HeroModel> lstRiveval)
	{
	}

	public void LoadData(BadgeInfo badgeInfo, List<HeroModel> lstHealing, List<HeroModel> lstRiveval)
	{
	}

	public void HelpClick()
	{
	}

	public void IdleOfRevivalClick()
	{
	}

	public void IdleOfHealingClick()
	{
	}

	public void IdleOfBlessingClick()
	{
	}

	public void ClickButtonAddBadge()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	void IG2UIEventHandler.ProcessG2Event(G2EventData eventData)
	{
	}
}
