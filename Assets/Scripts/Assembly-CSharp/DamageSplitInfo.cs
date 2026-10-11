using System;
using SimpleJSON;

[Serializable]
public class DamageSplitInfo
{
	public float m_TimeDelay;

	public DamageSplitInfo(float delay)
	{
	}

	public DamageSplitInfo(JSONClass jsonObj)
	{
	}

	public JSONClass ToJSONObject()
	{
		return null;
	}

	public void ImportFromJSONObject(JSONClass jsonObj)
	{
	}
}
