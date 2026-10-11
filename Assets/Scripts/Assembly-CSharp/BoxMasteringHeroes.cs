using System;
using System.Collections.Generic;
using UnityEngine;

public class BoxMasteringHeroes : MonoEventHandler
{
	public GridPoolGroup m_PoolGroupHeroes;

	public GameObject m_GoPrefabCellHero;

	private int _currentHeroKey;

	public Action<int> m_HeroSelectedCb;

	private List<HeroMasteringData> _lstHeroMasteringData;

	protected override void Start()
	{
	}

	public void Reload()
	{
	}

	public void LoadListHeroes()
	{
	}

	public void OnMasteringHeroClick(int heroKey)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
