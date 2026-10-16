using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupMFShowRelic : BasePopup
{
	[SerializeField]
	private MFCardRelic m_CardRelic;

	[SerializeField]
	private Text m_TextDesc;

	public GameObject m_GoBoxDesc;

	public Action m_CloseCb;

	protected override void Start()
	{
	}

	public void SetRelic(MFRelicInfo relicInfo)
	{
	}

	public override void ClosePopup()
	{
	}

	public static PopupMFShowRelic ShowUp(MFRelicInfo relicInfo)
	{
		return null;
	}

	private void Update()
	{
	}
}
