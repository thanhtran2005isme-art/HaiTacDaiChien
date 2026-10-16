using System.Collections.Generic;
using UnityEngine;

public class PopupArenaReward : BasePopup, ITabListener
{
	public TabManager m_TabCrystalCrown;

	private const int TAB_BUTTON_DAILY_CRYSTAL = 1;

	private const int TAB_BUTTON_SEASON_CRYSTAL = 2;

	public GameObject m_GoTabCrystal;

	public GameObject m_GoRewardDetail;

	private ArenaType m_ArenaType;

	private int m_CurrentRank;

	private const int REWARD_DAILY_CRYSTAL_CROWN = 1;

	private const int REWARD_SEASON_CRYSTAL_CROWN = 2;

	private const int REWARD_SEASON_TRIAL_CHAMP = 3;

	[SerializeField]
	private GameObject m_PrefabCellReward;

	[SerializeField]
	private GameObject m_PrefabCellPossibleReward;

	[SerializeField]
	private Transform m_TransformScrollReward;

	private GameObject _goCellPossibleReward;

	private List<GameObject> _lstCellRankReward;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	public void SetData(ArenaType arenaType, int currentRank)
	{
	}

	private void MakeRewardDetailFillParent()
	{
	}

	private void LoadRewardList(int typeReward, int currentRank)
	{
	}

	private void Update()
	{
	}

	public static void ShowUp(ArenaType arenaType, int currentRank)
	{
	}
}
