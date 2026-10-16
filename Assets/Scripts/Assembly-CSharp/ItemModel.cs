using System.Collections.Generic;

public class ItemModel
{
	public int m_ItemKey;

	public int m_SellPrice;

	public string m_ItemName;

	public string m_ItemDesc;

	public string m_Note;

	public string m_TagInfo;

	public int m_ExtraTag;

	public List<int> m_DropPosition;

	public virtual ItemModel Clone()
	{
		return null;
	}

	public override string ToString()
	{
		return null;
	}

	public virtual long GetNumber()
	{
		return 0L;
	}
}
