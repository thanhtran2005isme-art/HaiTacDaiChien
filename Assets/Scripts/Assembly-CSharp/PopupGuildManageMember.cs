using System.Collections.Generic;
using UnityEngine;

public class PopupGuildManageMember : BasePopup
{
	public GameObject m_GoCellPrefab;

	public GameObject m_GoScrollView;

	public VerticalPoolGroup m_PoolGroup;

	private List<GuildMemberInfo> m_LstMemberInfo;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void SetData(List<GuildMemberInfo> lstMemberInfo)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupGuildManageMember ShowUp(List<GuildMemberInfo> lstMemberInfo)
	{
		return null;
	}

	private void Update()
	{
	}
}
