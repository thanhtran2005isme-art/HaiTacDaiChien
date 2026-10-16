using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelEventHarvestDay : SpEventPanelBase
{
	[SerializeField]
	private Text m_TextTiltle;

	[SerializeField]
	private Image m_ImgTiltle;

	[SerializeField]
	private VerticalPoolGroup m_PoolItem;

	[SerializeField]
	private GameObject m_GoPrefabCellItem;

	private string m_UrlEventDetail;

	private List<DataEvenHarvestDay> listItem;

	public override void LoadEventDetail(string url)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void ShowData(string TextTiltle, string img)
	{
	}

	private List<DataEvenHarvestDay> ParseListItem(string json)
	{
		return null;
	}

	protected override void Awake()
	{
	}
}
