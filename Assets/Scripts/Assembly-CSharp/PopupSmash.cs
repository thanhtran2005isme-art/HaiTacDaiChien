using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupSmash : BasePopup
{
	public Image m_ImageItemCost;

	public Text m_TextCost;

	public GameObject m_GroupCost;

	public Text m_TextTurn;

	private Action<int> m_SmashClickCb;

	private int m_MaxTurn;

	private int m_Turn;

	private int m_CostPerTurn;

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

	public void SetData(Sprite spriteItemCost, int costPerTurn, int maxTurn, int defautTurn)
	{
	}

	public void SetData(int maxTurn, int defautTurn)
	{
	}

	public void SetSmashClickCallback(Action<int> smashClickCb)
	{
	}

	public static PopupSmash ShowUp(Sprite spriteItemCost, int costPerTurn, int maxTurn, int defautTurn = 0)
	{
		return null;
	}

	public static PopupSmash ShowUp(int maxTurn, int defautTurn = 0)
	{
		return null;
	}

	private void Update()
	{
	}
}
