using System;
using SimpleJSON;
using UnityEngine;

[Serializable]
public class SkillObjectInfo
{
	public string m_PathSpineSkeleton;

	public string m_AnimationName;

	public string m_PathSprite;

	public float m_Scale;

	public float m_RotationInitDegree;

	public float m_SpeedFactor;

	public Vector2 m_OffsetPosition;

	public SkillObjectInfo()
	{
	}

	public SkillObjectInfo(JSONClass jsonObj)
	{
	}

	public SkillObjectInfo(string pathSkeleton, string animationName, float scale = 0.6f, float rotationInitDegree = 0f)
	{
	}

	public SkillObjectInfo(string pathSprite, float scale = 0.6f, float rotationInitDegree = 0f)
	{
	}

	public JSONClass ToJSONObject()
	{
		return null;
	}

	public void ImportFromJSONObject(JSONClass jsonObj)
	{
	}

	public string GetKeyToStoreInPool()
	{
		return null;
	}

	public bool IsEmptySpine()
	{
		return false;
	}
}
