using System;
using SimpleJSON;
using UnityEngine;

[Serializable]
public class EffectInfo
{
	public string m_EffectName;

	public Vector2 m_OffsetPosition;

	public OriginPositionType m_OriginPosType;

	public string m_PathSpineSkeleton;

	public string m_SortingLayer;

	public float m_TimeDelay;

	public string m_AnimationName;

	public string m_AnimationNameStart;

	public int m_OffsetLayerOrder;

	public float m_Scale;

	public string m_Sound;

	public EffectImpactPosition m_ImpactPosition;

	public EffectInfo()
	{
	}

	public EffectInfo(float delay, string soundId = "")
	{
	}

	public EffectInfo(JSONClass jsonObj)
	{
	}

	public EffectInfo(string pathSkeleton, string animationName, int offsetLayerOrder = 1, EffectImpactPosition impactPostion = EffectImpactPosition.BODY)
	{
	}

	public EffectInfo(string pathSkeleton, string animationStart, string animation2, int offsetLayerOrder = 1, Vector2 offsetPosition = default(Vector2), float delay = 0f, float scale = 0.6f, string soundId = "")
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
