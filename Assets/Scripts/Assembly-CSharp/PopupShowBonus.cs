using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupShowBonus : BasePopup
{
	public Transform m_TransformContent;

	public Action m_CloseClickCb;

	public GameObject m_GoScrolView;

	public Text m_TextTitle;

	protected override void Start()
	{
	}

	public void SetBonusModel(string title, BonusModel bonusModel)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupShowBonus ShowUp(string title, BonusModel bonusModel)
	{
		return null;
	}
}
