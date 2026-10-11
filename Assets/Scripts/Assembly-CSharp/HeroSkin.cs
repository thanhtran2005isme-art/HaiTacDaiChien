using System.Collections.Generic;

public class HeroSkin
{
	public int m_SkinKey;

	public string m_SkinName;

	public PointModel m_Points;

	public List<int> m_ListHeroKey;

	public bool m_Released;

	public int m_Number;

	public int m_NumberEquip;

	public bool m_IsDictionarySkin;

	public int GetNumberAvaiable()
	{
		return 0;
	}

	public void CloneData()
	{
	}

	public HeroSkin Clone()
	{
		return null;
	}
}
