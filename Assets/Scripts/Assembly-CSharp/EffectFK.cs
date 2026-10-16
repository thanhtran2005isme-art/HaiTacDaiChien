using System.Collections.Generic;
using UnityEngine;

public class EffectFK
{
	public static int EFFECT_BUFF_POINT;

	public static int EFFECT_ADD_ABILITY;

	public static int EFFECT_FREEZE;

	public static int EFFECT_POISON;

	public static int EFFECT_BLAZE;

	public int type;

	public float[] data;

	public float tmpTime;

	public float time;

	public GameObject goEffect;

	public EffectFK Clone()
	{
		return null;
	}

	public static List<EffectFK> CreateFreezeInvisible()
	{
		return null;
	}

	public static EffectFK CreateFreezeEffect()
	{
		return null;
	}

	public static EffectFK CreateBloodThirst()
	{
		return null;
	}

	public static EffectFK CreatePoisonEffect(int damage)
	{
		return null;
	}

	public static EffectFK CreateBlazeEffect(int damage)
	{
		return null;
	}
}
