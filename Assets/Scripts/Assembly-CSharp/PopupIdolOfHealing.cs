using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupIdolOfHealing : BasePopup
{
	public Image m_ImgCoin;

	public Image m_ImgCoinButton;

	public GameObject buttonHealing;

	public GameObject notifyNoHero;

	public GameObject scrollView;

	private List<HeroModel> lstHealing;

	private BadgeInfo badgeInfo;

	public GridPoolGroup m_PoolGrid;

	private long HERO_ID;

	public Text badgeNumber;

	public Text feeHeal;

	private int KEY_COIN;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public static void ShowPopup(BadgeInfo badgeInfo, List<HeroModel> lstHealing)
	{
	}

	public void LoadData(BadgeInfo badgeInfo, List<HeroModel> lstHealing)
	{
	}

	public void ClickButtonAddBadge()
	{
	}

	public void OnPlayerHeroHeadClick(HeroHeadClickable goHead)
	{
	}

	public void OnClickHead()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
