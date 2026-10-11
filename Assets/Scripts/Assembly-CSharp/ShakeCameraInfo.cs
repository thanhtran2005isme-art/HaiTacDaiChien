using System;
using SimpleJSON;

[Serializable]
public class ShakeCameraInfo
{
	public float m_TimeDelay;

	public float m_ShakeDuration;

	public ShakeCameraInfo(float delay, float duration)
	{
	}

	public ShakeCameraInfo(JSONClass jsonObj)
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
