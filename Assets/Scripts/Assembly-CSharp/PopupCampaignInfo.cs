using UnityEngine;
using UnityEngine.UI;

public class PopupCampaignInfo : BasePopup
{
	public Text m_TextGold;

	public Text m_TextExp;

	public Text m_TextSpirit;

	public Transform m_TransformLoot;

	public RectTransform m_TransformBgr;

	public GameObject m_GoButtonGoTo;

	public GameObject m_GoScrollView;

	private int m_LevelId;

	private bool m_OnAuto;

	public GameObject m_GoButtonClose;

	public GameObject m_GoPointHandTutorial;

	protected override void Start()
	{
	}

	public void LoadLevelData(int levelId, bool onAuto)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonAutoClick()
	{
	}

	public static PopupCampaignInfo ShowUp(int levelId, bool onAuto)
	{
		return null;
	}
}
