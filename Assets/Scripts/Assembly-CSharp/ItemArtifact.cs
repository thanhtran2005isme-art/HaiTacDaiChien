using System.Collections.Generic;

public class ItemArtifact : ItemModel
{
	public ArtifactType m_Type;

	public int m_Number;

	public int m_Star;

	public int m_NumberUsed;

	public int m_Release;

	public List<int> m_ListSellPrice;

	public Dictionary<int, List<PointModel>> lstPoint;

	public HeroFaction m_FactionActiveHero;

	public void SetHeroFactionActive()
	{
	}

	public string GetArtifactEffect(HeroFaction heroFactionEquip, HeroClass heroClassEquip)
	{
		return null;
	}

	public int GetSellPrice()
	{
		return 0;
	}

	public int GetMaxStar()
	{
		return 0;
	}

	public bool IsSameType(ItemArtifact art2)
	{
		return false;
	}

	public bool IsSameType(int artifactKey, int star)
	{
		return false;
	}

	public override long GetNumber()
	{
		return 0L;
	}

	public long GetNumberUnused()
	{
		return 0L;
	}
}
