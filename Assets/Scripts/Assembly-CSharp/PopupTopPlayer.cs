using UnityEngine;
using UnityEngine.UI;

public class PopupTopPlayer : BasePopup
{
	public Text m_TextTitle;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_PrefabCellTop;

	public const int TOP_CAMPAINGN = 0;

	public const int TOP_DUNGEON = 2;

	public const int TOP_TOWER = 3;

	public const int TOP_GUILD_MILL = 4;

	public const int TOP_CRYSTAL_CROWN_LAST_SEASON = 5;

	public const int TOP_TRIAL_CHAMPION_LAST_SEASON = 6;

	public const int TOP_TOWER2 = 7;

	private int m_TopType;

	protected override void Start()
	{
	}

	public void ButtonCloseClicked()
	{
	}

	public void LoadData(int topType)
	{
	}

	public static void ShowUp(int topType)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
