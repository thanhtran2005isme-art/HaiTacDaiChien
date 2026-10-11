using System.Collections.Generic;
using UnityEngine;

public class PopupGuildRaid : BasePopup
{
	private const int BOSS_PER_PAGE = 3;

	public DotIndicator m_DotIndicator;

	public RectTransform m_TransformBoss;

	public GameObject m_GoBossPrefab;

	private List<GameObject> m_LstGoGuildBoss;

	private List<GuildBoss> m_LstGuildBoss;

	private int m_TotalPage;

	public static bool ShowPopupBossAttack;

	private int m_CurrentPageIdx;

	protected override void Start()
	{
	}

	private void SetRaidInfo(int heroKey, int currentLevel, int power, float percentHp, int countDown, int gemCostAttack)
	{
	}

	private void LoadPage(int pageIdx)
	{
	}

	public override void ClosePopup()
	{
	}

	public void ButtonNextClick()
	{
	}

	public void ButtonPrevClick()
	{
	}

	public void ButtonHelpClick()
	{
	}

	public static void ShowUp()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
