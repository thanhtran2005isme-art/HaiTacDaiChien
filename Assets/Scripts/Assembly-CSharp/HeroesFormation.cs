using System.Collections.Generic;

public class HeroesFormation
{
	public List<HeroSlotInfo> m_LstSlotInfo;

	public int m_PetId_1;

	public int m_PetId_2;

	public int m_PetId_3;

	public int m_PetTier_1;

	public int m_PetTier_2;

	public int m_PetTier_3;

	public bool m_SkipBattle;

	public void PrintFormation()
	{
	}

	public bool IsFormationEmpty()
	{
		return false;
	}

	public void LookupForPetTier()
	{
	}

	public List<int> ToListServerHeroId1LineUp()
	{
		return null;
	}

	public List<int> ToListServerHeroId2LineUp()
	{
		return null;
	}

	public List<int> ToListServerHeroId3LineUp()
	{
		return null;
	}

	public int CountNumberHero()
	{
		return 0;
	}

	public void ImportFromListServerHeroId(List<int> lstServerHeroId)
	{
	}

	public List<int> GetListHeroId()
	{
		return null;
	}
}
