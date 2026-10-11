using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupSmashBoss : BasePopup
{
	public Image m_ImageItem;

	public Text m_TextNumberTurn;

	public Text m_TextTotalTurn;

	private Action<int> m_SmashClickCb;

	private int m_TotalTurn;

	protected override void Start()
	{
	}

	public void ButtonCloseClick()
	{
	}

	public void ButtonPlusClick()
	{
	}

	public void ButtonMinusClick()
	{
	}

	public void ButtonSmashClick()
	{
	}

	public void SetData(Sprite spriteItem, int totalTurn)
	{
	}

	public void SetSmashClickCallback(Action<int> smashClickCb)
	{
	}

	public static PopupSmashBoss ShowUp(Sprite spriteItem, int totalTurn)
	{
		return null;
	}

	public static PopupSmashBoss ShowUp(string pathItem, int totalTurn)
	{
		return null;
	}

	private void Update()
	{
	}
}
