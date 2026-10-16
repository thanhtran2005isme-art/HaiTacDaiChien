using System.Collections.Generic;

public class MFDataSlotItemForDelayProcess
{
	public const int DATA_HERO_SUPPORT = 1;

	public const int DATA_REVINE_RANDOM = 2;

	public const int DATA_HEALING = 3;

	public const int DATA_CHOOSING_RELIC = 4;

	public int m_SLotDataType;

	public List<long> m_ListLongData;

	public MFHeroInfo m_MfHeroSupport;

	public MFMapData m_MapDataOld;
}
