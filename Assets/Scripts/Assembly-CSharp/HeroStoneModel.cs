using System.Collections.Generic;

public class HeroStoneModel
{
	public int m_StoneId;

	public string m_StoneName;

	public int m_StoneType;

	public int m_Star;

	public int m_UpgradeGoldCost;

	public int m_UpgradeDustCost;

	public int m_ReplaceGoldCost;

	public int m_ReplaceDustCost;

	public int m_RetainAttributeGemCost;

	public string m_DescEffect;

	public List<HeroUpgradeStoneFactionData> m_LstDataEpic;

	public List<long> GetListLongUpgradeEpic(HeroFaction heroFaction)
	{
		return null;
	}

	public List<long> GetListLongConvertEpic(HeroFaction heroFaction)
	{
		return null;
	}

	public HeroStoneModel()
	{
	}

	public static string parseDesc(string desc)
	{
		return null;
	}

	public HeroStoneModel(int stoneId, string stoneName, int stoneType, int star)
	{
	}

	public virtual HeroStoneModel Clone()
	{
		return null;
	}
}
