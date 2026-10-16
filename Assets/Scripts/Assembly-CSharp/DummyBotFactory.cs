using System.Collections.Generic;
using UnityEngine;

public class DummyBotFactory : ActionTimerManager
{
	private float m_WaitFirstSpawnBot;

	private float m_IntervalSpawnBot;

	private int m_NumBotSpawn;

	private int m_WaveNumber;

	public List<DummyBotHeroAI> m_ListBotAI;

	private Transform m_TfBotContainer;

	private StageCampaignBotInfo m_StageBotInfo;

	private bool m_HasSpawnBot;

	private GameObject m_PrefabHero;

	private int _countWave;

	public DummyBotFactory(Transform container, StageCampaignBotInfo stageBotInfo)
	{
	}

	private void SpawnBot()
	{
	}

	private BotCampaignInfo GetRandomMinionInfo()
	{
		return null;
	}

	private BotCampaignInfo GetBossInfo()
	{
		return null;
	}

	public bool HasBotDieAll()
	{
		return false;
	}

	public void Update(float deltaTime)
	{
	}
}
