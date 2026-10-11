using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupDungeonSmashItem : BasePopup
{
	public Transform m_TransformContent;

	public Action m_CloseClickCb;

	public GameObject m_GoScrolView;

	public Text m_TextTitle;

	protected override void Start()
	{
	}

	public void SetBonusModel(string title, BonusModel bonusModel, DungeonLotionState potionState)
	{
	}

	public void ButtonCloseClick()
	{
	}

	public static PopupDungeonSmashItem ShowUp(string title, BonusModel bonusModel, DungeonLotionState potionState)
	{
		return null;
	}
}
