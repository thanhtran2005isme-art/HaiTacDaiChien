using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerNewScene : BaseScene
{
	public Text m_TextTurnLeft;

	public TextCountdown m_TextCountDown;

	public Text m_TextLevel;

	public BoxTowerDailyReward m_BoxDailyReward;

	public List<TowerStage> m_ListTowerStages;

	public TowerPlayerData m_Tower1Data;

	public TowerPlayerData m_Tower2Data;

	public TowerPlayerData m_CurrentTowerData;

	public BoxHideAtFirst m_BoxTitle;

	public ScrollRect m_ScrollTower;

	public TabManager m_TabManager;

	private const int TAB_BUTTON_EASY = 1;

	private const int TAB_BUTTON_HARD = 2;

	[SerializeField]
	private GameObject m_GoCloudEasy;

	[SerializeField]
	private GameObject m_GoCloudHard;

	[SerializeField]
	private GameObject m_GoBackground2;

	public BackgroundSwitcher m_BgrSwitcher;

	public BeanTrunkMng m_TrunkMng;

	[SerializeField]
	private TranslateController m_AniShowTowerTypeButton;

	private bool _gotStatusTowerOpen;

	private int _currentTabBtnId;

	private bool _allowSwitchTab;

	protected override void Start()
	{
	}

	private void ScaleCloud2ToMatchBackgroud()
	{
	}

	private void LoadMapTowerEasy()
	{
	}

	private void LoadMapTowerHard()
	{
	}

	private void SwitchToMapTowerEasy()
	{
	}

	private void SwitchToMapTowerHard()
	{
	}

	private void OnTabButtonClicked(int tabBtnId)
	{
	}

	public void ShowLogPassedTowerStage()
	{
	}

	public void ButtonBuyTurnClick()
	{
	}

	public void ButtonTopClick()
	{
	}

	public void ButtonFightClick()
	{
	}

	private void LoadTowerHeroes(int typeTower, int currentLevelId)
	{
	}

	public void ButtonBackClick()
	{
	}

	public void CollectedDailyRewardSuccess()
	{
	}

	public void ShowPlayerDataTower(TowerPlayerData towerData)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}

	public void HelpClick()
	{
	}
}
