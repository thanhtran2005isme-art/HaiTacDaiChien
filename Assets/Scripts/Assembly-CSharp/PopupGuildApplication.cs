using System;
using UnityEngine;

public class PopupGuildApplication : BasePopup
{
	public VerticalPoolGroup m_PoolApplication;

	public GameObject m_GoScrollView;

	public GameObject m_PrefabCellApplication;

	public Action m_GuildApplicationProcessedCb;

	private bool m_HasProcessApplication;

	protected override void Start()
	{
	}

	private void ReloadApplication()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupGuildApplication ShowUp()
	{
		return null;
	}

	private void Update()
	{
	}
}
