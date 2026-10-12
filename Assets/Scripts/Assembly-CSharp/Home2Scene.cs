using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class Home2Scene : BaseScene, IG2UIEventHandler
{
	private List<HeroModel> m_ListHeroModelHome;

	public List<HeroHome2> m_ListHeroHome;

	private GameObject mHandQuest;

	public HomeQuest currentQuest;

	public Button m_BtnCamp;

	public Button m_BtnQuest;

	public Button m_BtnHeroes;

	public Button m_BtnWorld;

	public Button m_BtnInventory;

	public Button m_BtnFusion;

	public Button m_BtnAltar;

	public Button m_BtnShop;

	public Button m_BtnSummon;

	public Button m_BtnForge;

	public Button m_BtnLuckySpin;

	public Button m_BtnCheckIn;

	public Button m_BtnEvent;

	public Button m_BtnMission;

	public Button m_BtnArchievement;

	public Button m_BtnPet;

	public Button m_Menu;

	public Image m_IconBlockWorld;

	public Image m_IconBlockFusi;

	public Image m_IconBlockAltar;

	public Image m_IconBlockPet;

	public const int LEVEL_QUEST_TUTORIAL = 4;

	public const int LEVEL_QUEST_SUGGEST = 15;

	public PanelHome2Quest panelHomeQuest;

	private PopupShop popShop;

	public GameObject m_GoShipPlayer;

	public SkeletonGraphic m_SkeletonShipPlayer;

	[SerializeField]
	private AudioSource m_AudioWave;

	protected override void Start()
	{
		base.Start();
		// Apply the three user-requested Home2 HUD changes only in Play Mode.
		OfflineHome2HudCorrection.Apply(this);
		// AssetRipper restored an empty Start. Attach the new offline UI
		// presenter here as a deterministic fallback when entering Play Mode
		// directly in Home2Scene (Editor domain reload / sceneLoaded differences).
		if (GetComponent<OfflineHeroHomePresenter>() == null)
			gameObject.AddComponent<OfflineHeroHomePresenter>();
	}

	public override void OnDestroy()
	{
	}

	private void Update()
	{
	}

	public void LoadCurrentQuest(HomeQuest quest)
	{
	}

	protected override void ReciveTutorial(TutorialAction tutAct)
	{
	}

	public void ButtonShopClick()
	{
	}

	private void TutorialTextCompleted11(int tutId)
	{
	}

	private void SetUpUIForTutorial(int tutId)
	{
	}

	private void LoadHeroHome()
	{
	}

	public void HideShowFeature(HomeQuest currentQuest)
	{
	}

	public void ButtonAddGemClick()
	{
	}

	public void OnButtonCrossPuzzesClick()
	{
	}

	public void OnButtonFirstPurchaseClick()
	{
	}

	public void ButtonAddGoldClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public void ProcessG2Event(G2EventData eventData)
	{
	}
}
