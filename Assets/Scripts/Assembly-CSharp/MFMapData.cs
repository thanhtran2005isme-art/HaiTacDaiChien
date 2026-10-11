using System.Collections.Generic;

public class MFMapData
{
	public int m_CurrentRowIndex;

	public int m_CurrentSlotIndex;

	public List<MFRowData> m_ListRowData;

	public List<int> m_ListRelicId;

	public bool m_HasPortal;

	public bool m_AllowSelectDifficulty;

	public bool HasReachLastRow()
	{
		return false;
	}
}
