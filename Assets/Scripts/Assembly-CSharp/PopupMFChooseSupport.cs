using System;
using System.Collections.Generic;
using UnityEngine;

public class PopupMFChooseSupport : BasePopup
{
	public GameObject m_PrefabCell;

	public RectTransform m_TfContainer;

	public GameObject m_GoButtonChooseActive;

	public GameObject m_GoButtonChooseInActive;

	public Action m_CloseCb;

	public GameObject m_GroupAction;

	private List<CellMFHeroSupport> _listCellSupport;

	private MFSlotData _mfSlotData;

	private bool _showAction;

	private int m_HeroIndexPicked;

	protected override void Start()
	{
	}

	public static PopupMFChooseSupport ShowUp(MFSlotData mfSlotData, bool showAction)
	{
		return null;
	}

	public void LoadData(MFSlotData mfSlotData, bool showAction)
	{
	}

	public void OnCellClick(int heroIdx)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonChooseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
