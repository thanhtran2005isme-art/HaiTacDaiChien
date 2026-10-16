using System;

[Serializable]
public class ItemIAPModel
{
	public const int ITEM_NORMAL = 0;

	public const int ITEM_MONTH_SMALL = 1;

	public const int ITEM_MONTH_BIG = 2;

	public float priceIAP;

	public int idItem;

	public int valueIAP;

	public int typeItem;

	public int totalBonus;

	public string cost;

	public string productID;

	public string desc;

	public string typePrice;

	public string name;

	public string pathIcon;

	public bool isPurchased;

	public string m_StrLocalizedPrice;
}
