using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TarvenScene : BaseScene
{
	public static TarvenScene Instance;

	public VerticalPoolGroup m_PoolGroup;

	public Text m_TextTimeCountDown;

	public Text m_TextGemRefresh;

	public Text m_TextCostQuestBasic;

	public Text m_TextCostQuestSenior;

	public GameObject m_GoButtonRefresh;

	public GameObject m_GoScrollView;

	public GameObject m_GoCellPrefab;

	public List<TarvenQuestInfo> m_LstQuestInfo;

	private ActionRepeatTimer m_ActionUpdateRefresh;

	private int m_TimeNextRefesh;

	private int m_GemRefreshPerQuest;

	private int numberQuestComplete;

	private bool m_ReloadHeroDoingQuest;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void InitTest()
	{
	}

	public List<int> GetListHeroIdDoingQuest()
	{
		return null;
	}

	private void UpdateGemNeedToRefresh()
	{
	}

	private void ReloadHeroDoingQuest()
	{
	}

	private void ShowTarvenQuest(List<TarvenQuestInfo> lstQuestInfo)
	{
	}

	private void ReloadCells()
	{
	}

	public void ButtonBackClick()
	{
	}

	public void ButtonGenQuestBasicClick()
	{
	}

	public void ButtonGenQuestSeniorClick()
	{
	}

	public void ButtonRefreshClick()
	{
	}

	public void ButtonInfoClick()
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
