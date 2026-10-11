using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupUpgradeBuilding : BasePopup
{
	public Text m_Title;

	public Text m_TexrCurrLvl;

	public Text m_TextNextLvl;

	public Text m_TextLock;

	public Text m_ReqGold;

	public Text m_ReqStone;

	public GameObject m_Lock;

	public GameObject m_ButtonGray;

	public GameObject m_BtnInfoCurrLvl;

	public GameObject m_BtnInfoNextLvl;

	public Image m_IconBuildCurr;

	public Image m_IconBuildNext;

	public Action<BuildingModel> callbackUpgradeSuccess;

	public Text m_TextInfo1;

	public Text m_TextInfo2;

	public SpineObject m_SpineBuildUp1;

	public SpineObject m_SpineBuildUp2;

	private int typeMode;

	private bool isCheckUpgrade;

	private BuildingModel buildModel;

	private TechModel techModel;

	private string TEXT_MAX_LEVEL;

	public void LoadInfoTower()
	{
	}

	public void LoadInfoBuilding(BuildingModel buildModel)
	{
	}

	public void LoadInfoBuilding2(BuildingModel buildModel, TechModel tech)
	{
	}

	public void UpgradeClick()
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

	public void InfoLvlCurrClick()
	{
	}

	public void InfoLvlNextClick()
	{
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

	public static PopupUpgradeBuilding ShowUp()
	{
		return null;
	}

	protected override void Start()
	{
	}

	private void Update()
	{
	}

	private string FormInfoTower(int Lvl)
	{
		return null;
	}
}
