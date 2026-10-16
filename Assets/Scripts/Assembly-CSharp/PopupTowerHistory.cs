using System.Collections.Generic;
using UnityEngine;

public class PopupTowerHistory : BasePopup
{
	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_GoPrefabCell;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void LoadData(List<TowerHistoryLog> lstLog)
	{
	}

	public static PopupTowerHistory ShowUp(List<TowerHistoryLog> lstLog)
	{
		return null;
	}

	private void Update()
	{
	}
}
