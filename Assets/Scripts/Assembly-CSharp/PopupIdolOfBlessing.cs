using UnityEngine;
using UnityEngine.UI;

public class PopupIdolOfBlessing : BasePopup
{
	public Image m_ImgCoin;

	public Image m_ImgCoinButton;

	public Image imageBadge;

	public Text numberBadge;

	public Image imageCoin;

	public Text textNumberCoin;

	public Text numberRage;

	public Text numberRock;

	public Text numberStorm;

	public GameObject bgrRage;

	public GameObject bgrRock;

	public GameObject bgrStorm;

	public GameObject spineRage;

	public GameObject spineRock;

	public GameObject spineStorm;

	private int KEY_COIN;

	private BadgeInfo badgeInfo;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public static void ShowPopup(BadgeInfo badgeInfo)
	{
	}

	public void LoadData(BadgeInfo badgeInfo)
	{
	}

	public void OnClickBless()
	{
	}

	public void ClickButtonAddBadge()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
