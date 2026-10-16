using System.Collections.Generic;

public class NewFeatureManager
{
	public const int FEATURE_ARENA_CRYSTAL_CROWN = 1;

	public const int FEATURE_GUILD = 2;

	public const int FEATURE_TOWER = 3;

	public const int FEATURE_TARVEN = 4;

	public const int FEATURE_ISLAND = 5;

	public const int FEATURE_BRAVE_TRIAL = 6;

	public const int FEATURE_PET = 7;

	public const int FEATURE_ASPEND_DUNGEON = 8;

	public const int FEATURE_SEALED_LAND = 9;

	public const int FEATURE_EVENT_RAID = 10;

	private List<NewFeatureData> m_LstFeatureData;

	public static NewFeatureManager m_Instance;

	public static NewFeatureManager GetInstance()
	{
		return null;
	}

	private NewFeatureManager()
	{
	}

	public static int CheckIfNewFeatureOpen(int playerNewLevel)
	{
		return 0;
	}

	public NewFeatureData GetFeature(int featureId)
	{
		return null;
	}
}
