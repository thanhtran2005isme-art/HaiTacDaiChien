using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupUseItemConsume : BasePopup
{
	public GameObject m_GoButtonClose;

	public GameObject m_GoButtonMinus;

	public GameObject m_GoButtonPlus;

	public ItemConsumableUI m_ItemConsumeUI;

	public Text m_TextTitle;

	public InputField m_InputNumberItem;

	public Action m_CloseClickCb;

	private int m_Limit;

	private ItemConsumable m_ItemConsume;

	private Action<int> cbUse;

	protected override void Start()
	{
	}

	public void NumberItemChanged()
	{
	}

	public void ButtonPlusClick()
	{
	}

	public void ButtonMinusClick()
	{
	}

	private void SetItemNumber(int numItem)
	{
	}

	public void ButtonSummonClick()
	{
	}

	public void SetData(ItemConsumable itemConsume, Action<int> cbUse = null)
	{
	}

	public static PopupUseItemConsume ShowUp(ItemConsumable item, Action<int> cbuse = null)
	{
		return null;
	}

	public override void ClosePopup()
	{
	}

	public override void ProcessKEvent(int eventId, object data)
	{
	}

	private void Update()
	{
	}
}
