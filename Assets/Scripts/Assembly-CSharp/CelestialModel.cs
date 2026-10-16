using System.Collections.Generic;

public class CelestialModel
{
	public const int MODE_TOWER = 0;

	public const int MODE_TECH_BUILD = 1;

	public const int MODE_TECH = 2;

	public const int MODE_MINE = 3;

	public const int TYPE_BUILDING_AGI = 0;

	public const int TYPE_BUILDING_MILI = 1;

	public const int TYPE_BUILDING_DAFFODIL = 5;

	public const int TYPE_MINE_GOLD = 2;

	public const int TYPE_MINE_GEM = 3;

	public const int TYPE_MINE_MAGIC_DUST = 4;

	public const int TYPE_TECH_HARVEST = 0;

	public const int TYPE_TECH_DAFFODIL = 1;

	public const int TYPE_TECH_ATK = 2;

	public const int TYPE_TECH_HP = 3;

	public int m_TowerLevel;

	public int m_NumberDaffodil;

	public int m_CapacityDaffodil;

	public int m_LimitPit;

	public int m_LimitBuyDaffodil;

	public int m_MaxBuyDaffodil;

	public int m_FeeBuyDaffodil;

	public TimeStruct m_TimeCountdown;

	public long m_TimeSpawnDaffodil;

	public List<MineModel> m_ListMineModel;

	public List<IslandModel> m_ListIslandModel;

	public List<BuildingModel> m_ListBuilding;

	public BuildingModel GetBuildingByType(int typeBuilding)
	{
		return null;
	}

	public MineModel GetMineById(int idMine)
	{
		return null;
	}

	public int CountMine(int typeMine)
	{
		return 0;
	}

	public static string GetMineName(int typeMine)
	{
		return null;
	}
}
