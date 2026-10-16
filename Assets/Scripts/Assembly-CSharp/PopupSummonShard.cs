using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupSummonShard : BasePopup
{
	public GameObject m_GoButtonClose;

	public GameObject m_GoButtonMinus;

	public GameObject m_GoButtonPlus;

	public ItemShardUI m_ItemShardUI;

	public Text m_TextTitle;

	public InputField m_InputNumberItem;

	public Action m_CloseClickCb;

	private int m_Limit;

	private ItemShard m_ItemShard;

	public GameObject m_GoPointHandSummon;

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

	public void SetData(ItemShard itemShard, bool forTutorial)
	{
	}

	private void SetLimitSpeicalItem(ItemShard _itemShard, int _limit)
	{
	}

	public void SetLimitMax(int maxLimit)
	{
	}

	public static PopupSummonShard ShowUp(ItemShard itemShard, bool forTutorial = false)
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
