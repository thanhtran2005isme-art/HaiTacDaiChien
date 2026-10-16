public class ItemEquipment : ItemModel
{
	public long m_Number;

	public int m_Star;

	public int m_CostForge;

	public int m_MaterialItemKey;

	public int m_MaterialNumber;

	public int m_NumberUsed;

	public PointModel[] lstPoint;

	public EquipmentType m_Type;

	public EquipmentQuality m_Quality;

	public override string ToString()
	{
		return null;
	}

	public override long GetNumber()
	{
		return 0L;
	}

	public long GetNumberUnused()
	{
		return 0L;
	}

	public string GetItemTypeInString()
	{
		return null;
	}

	public HeroClass GetDedicatedHeroClass()
	{
		return (HeroClass)0;
	}
}
