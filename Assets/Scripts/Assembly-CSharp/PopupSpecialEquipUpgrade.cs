using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupSpecialEquipUpgrade : BasePopup, ITabListener
{
	public TabManager tabInfo;

	public TabManager tabRankRole;

	private HeroModel model;

	public GameObject notiTabStrengthen;

	public GameObject notiTabCoordination;

	public GameObject panelStrengthen;

	public GameObject panelCoordination;

	public PanelEpicEquipmentStrengthen panelStrengthenScript;

	public PanelEpicEquipmentCoordination panelCoordinationScript;

	[Header("Attack")]
	public Text attackLevel;

	public Text attackContent;

	public Image attackIcon;

	public Text role;

	public GameObject goNotifyAtk;

	public CurvedItem attackGroupStar;

	[Header("Hp")]
	public Text hpLevel;

	public Text hpContent;

	public Image hpIcon;

	public GameObject goNotifyHp;

	public Text rank;

	public CurvedItem hpGroupStar;

	private const int TAB_RANK_ROLE = 1;

	private const int TAB_INFO = 2;

	private const int TAB_COODIRNATION = 2;

	private const int TAB_STRENGTHEN = 1;

	private const int TAB_ATTACK = 0;

	private const int TAB_HP = 1;

	private int currentTypeTab;

	private int currentBtnTabId;

	private int currentTabRankRole;

	private int currentTabInfo;

	private Action cbClose;

	protected override void Start()
	{
	}

	private void ReloadData()
	{
	}

	private void SetData(HeroModel heroModel, int typeClick, Action cbClose)
	{
	}

	public override void ClosePopup()
	{
	}

	private Sprite LoadImgClass(HeroClass m_class)
	{
		return null;
	}

	private Sprite LoadImgQuality(int quality)
	{
		return null;
	}

	private string GetRankHero(int quality)
	{
		return null;
	}

	public static void ShowUp(HeroModel heroModel, int typeClick, Action cbClose)
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}
}
