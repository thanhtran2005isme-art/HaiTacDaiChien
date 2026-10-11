using UnityEngine;
using UnityEngine.UI;

public class PopupUpgradeDaffodil : MonoEventHandler
{
	public Slider m_SliderOutput;

	public Text m_Title;

	public Text m_TexrCurrLvl;

	public Text m_TextNextLvl;

	public Text m_TextLock;

	public Text m_TextOutput;

	public Text m_ReqGold;

	public Text m_ReqStone;

	public GameObject m_Lock;

	public GameObject m_ButtonGray;

	public GameObject m_BtnInfoCurrLvl;

	public GameObject m_BtnInfoNextLvl;

	public Image m_IconBuildCurr;

	public Image m_IconBuildNext;

	public Text m_TextInfo1;

	public Text m_TextInfo2;

	public SpineObject m_SpineBuildUp1;

	public SpineObject m_SpineBuildUp2;

	private int typeMode;

	private bool isCheckUpgrade;

	private BuildingModel buildModel;

	private const string TEXT_MAX_LEVEL = "Cấp tối đa";

	private string MSG_UPGRADE_DAFFODIL;

	public void LoadInfoBuilding(BuildingModel buildModel)
	{
	}

	public void UpgradeClick()
	{
	}

	public void GetDaffodil()
	{
	}

	private string GetTextInfo1()
	{
		return null;
	}

	private string GetTextInfo2()
	{
		return null;
	}

	private void EffectUpgrade()
	{
	}

	public void CloseClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	public static PopupUpgradeDaffodil ShowUp()
	{
		return null;
	}

	protected override void Start()
	{
	}

	private void Update()
	{
	}
}
