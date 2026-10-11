using System.Collections.Generic;

internal class MapTempateEventWithPrefab
{
	private static MapTempateEventWithPrefab m_Instance;

	private Dictionary<int, string> m_DictEventTemplatePrefab;

	public static MapTempateEventWithPrefab Instance => null;

	private MapTempateEventWithPrefab()
	{
	}

	public string GetPathPanelEventPrefab(int eventTemplateId)
	{
		return null;
	}
}
