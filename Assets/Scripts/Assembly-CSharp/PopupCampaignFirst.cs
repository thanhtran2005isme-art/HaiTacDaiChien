using UnityEngine;
using UnityEngine.UI;

public class PopupCampaignFirst : BasePopup
{
	[SerializeField]
	private Image m_BG;

	[SerializeField]
	private Text m_TextContent;

	protected override void Start()
	{
	}

	public void LoadEventData(int level)
	{
	}

	public static void ShowUp(int CurrentLevel)
	{
	}
}
