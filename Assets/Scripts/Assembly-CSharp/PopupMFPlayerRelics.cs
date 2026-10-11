using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupMFPlayerRelics : BasePopup
{
	public GridPoolGroup m_PoolCard;

	public GameObject m_GoPrefabCardRelic;

	public GameObject m_GoBoxRelics;

	public GameObject m_GoBoxNoRelics;

	public Action m_CloseCb;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void LoadListRelicInfo(List<MFRelicInfo> lstRelicInfo)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupMFPlayerRelics ShowUp(List<MFRelicInfo> lstRelicInfo)
	{
		return null;
	}

	private void Update()
	{
	}
}
