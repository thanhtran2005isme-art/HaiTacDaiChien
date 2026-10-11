using UnityEngine;

public class PopupAllStarsReward : BasePopup, ITabListener
{
	public TabManager m_TabRewards;

	private const int TAB_BUTTON_POINT_PER_HOUR = 1;

	private const int TAB_BUTTON_KNOCK_OUT_REWARDS = 2;

	private const int TAB_BUTTON_SEASON_REWARDS = 3;

	[SerializeField]
	private GameObject m_PrefabCellPointPerHour;

	[SerializeField]
	private GameObject m_PrefabCellSeasonReward;

	[SerializeField]
	private GameObject m_PrefabCellKnockoutReward;

	[SerializeField]
	private Transform m_TransformScrollReward;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void OnTabActive(int tabId, int buttonTabId)
	{
	}

	private void LoadPointEarnPerHour()
	{
	}

	private void LoadKnockOutRewards()
	{
	}

	private void LoadSeasonRewards()
	{
	}

	private void Update()
	{
	}

	public static void ShowUp()
	{
	}
}
