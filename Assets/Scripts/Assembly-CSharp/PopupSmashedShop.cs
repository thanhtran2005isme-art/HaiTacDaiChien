using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSmashedShop : BasePopup
{
	public Text m_TextTitle;

	public VerticalPoolGroup m_PoolGroup;

	public GameObject m_PrefabCell;

	private int m_MerchantId;

	private List<object> m_LstShopItem;

	private int m_PurchasedItemId;

	protected override void Awake()
	{
	}

	public void SetData(int merchantId)
	{
	}

	public static PopupSmashedShop ShowUp(int merchantId)
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
