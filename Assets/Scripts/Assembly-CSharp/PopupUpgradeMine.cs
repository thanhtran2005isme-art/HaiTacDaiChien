using UnityEngine;
using UnityEngine.UI;

public class PopupUpgradeMine : BasePopup
{
	public Slider m_SliderOutput;

	public Text m_Title;

	public Text m_TextOutput;

	public Text m_ReqGold;

	public Text m_ReqStone;

	public Text m_CurrLvl;

	public Text m_NextLvl;

	public Text m_TextLock;

	public Text m_LabelAction_1;

	public Text m_LabelAction_2;

	public GameObject m_Lock;

	public Image m_IconBuildCurr;

	public Image m_IconBuildNext;

	public Image m_IconResouce;

	public Text m_TextMine1;

	public Text m_TextMine2;

	public SpineObject m_SpineBuildUp1;

	public SpineObject m_SpineBuildUp2;

	private MineModel m_Data;

	private bool isCheckUpgrade;

	private bool notEnougnGold;

	private bool notEnougnStone;

	public void InitPopup(MineModel model)
	{
	}

	public void InfoLvlNextClick()
	{
	}

	public void InfoLvlCurrClick()
	{
	}

	public void GetMine()
	{
	}

	public void UpgradeMine()
	{
	}

	public void DestroyMine()
	{
	}

	public int GetMineId()
	{
		return 0;
	}

	private void EffectUpgrade()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private string FormInfoMine(int level)
	{
		return null;
	}

	public void CloseClick()
	{
	}

	public static void ShowUp(MineModel model)
	{
	}

	protected override void Start()
	{
	}

	private void Update()
	{
	}
}
