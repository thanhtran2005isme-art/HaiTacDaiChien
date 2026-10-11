using UnityEngine;
using UnityEngine.UI;

public class SealedLandScene : BaseScene
{
	public static SealedLandScene Instance;

	[SerializeField]
	private Text m_textNumberSmash;

	[SerializeField]
	private Text m_textNumberAttack;

	[SerializeField]
	private Text m_textPower;

	public GameObject m_GoBoxSelectFaction;

	public GameObject m_GoBoxFactionLevel;

	public SealedLandSelectBattle m_PanelSelectFaction;

	private int m_feeBuySmash;

	private int m_feeBuyAtk;

	private int m_numberBuySmashLeft;

	private int m_NumberSmash;

	private int m_ShowListLevel;

	public const int PANEL_LIST_LEVEL_ACTIVE = 1;

	public const int PANEL_LIST_LEVEL_DISABLE = 0;

	public const string LIST_LEVEL_ACTIVE_KEY = "LIST_LEVEL_ACTIVE_KEY";

	public int SetShowPanelListLevel
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	private void OnDisable()
	{
	}

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	private void SetActivePanelListLevel()
	{
	}

	public void BtnBackHome()
	{
	}

	public int GetNumberSmash()
	{
		return 0;
	}

	public void ButtonShopClick()
	{
	}

	public void HelpClick()
	{
	}

	public void ButtonBuySmash()
	{
	}

	public void ButtonBuyAttack()
	{
	}

	public void UpdateUI(string numSmash, string numAtk)
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
