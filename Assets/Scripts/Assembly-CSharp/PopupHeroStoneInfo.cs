using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupHeroStoneInfo : BasePopup
{
	public HeroStoneUI m_HeroStoneUI;

	public Text m_TextTitle;

	public Text m_TextInfo;

	public Text m_TextDesc;

	public RectTransform m_TransformImageBgr;

	public GameObject m_GoButtonAction1;

	public GameObject m_GoButtonAction2;

	public Text m_TextAction1;

	public Text m_TextAction2;

	private HeroStoneModel m_HeroStone;

	private Action m_Action1Cb;

	private Action m_Action2Cb;

	protected override void Start()
	{
	}

	public void SetStone(int heroId, HeroStoneModel heroStone)
	{
	}

	public void HideActions()
	{
	}

	public void SetAction(string title1 = null, Action action1 = null, string title2 = null, Action action2 = null)
	{
	}

	public void ButtonAction1Click()
	{
	}

	public void ButtonAction2Click()
	{
	}

	public static PopupHeroStoneInfo ShowUp(int heroId, HeroStoneModel stone)
	{
		return null;
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
