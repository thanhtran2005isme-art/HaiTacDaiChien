using UnityEngine;
using UnityEngine.UI;

public class BoxReplacement : MonoEventHandler
{
	public Text m_TextBlessingCost;

	public HeroReplace m_HeroReplaceInput;

	public HeroReplace m_HeroReplaceOutput;

	public PanelHeroList m_PanelHeroList;

	public SpineObject m_SpineOutput;

	public SpineObject m_SpineInputShowUp;

	public SpineObject m_SpineInputPlus;

	private HeroModel m_HeroModelInput;

	private int m_Blessing4Star;

	private int m_Blessing5Star;

	public GameObject m_GroupActionReplace;

	public GameObject m_GroupActionConfirm;

	protected override void Awake()
	{
	}

	protected override void Start()
	{
	}

	public void ResetBox()
	{
	}

	private void SetHeroModelInput(HeroModel heroModel)
	{
	}

	public void ShowActionReplace()
	{
	}

	public void ShowActionConfirm()
	{
	}

	public void ButtonReplaceHeroClick()
	{
	}

	public void HelpClick()
	{
	}

	public void ButtonOkClick()
	{
	}

	public void ButtonCancelClick()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}
}
