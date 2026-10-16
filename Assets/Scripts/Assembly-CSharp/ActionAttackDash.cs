using UnityEngine;

public class ActionAttackDash : AAction
{
	public Vector3 oldPos;

	public Vector3 pos;

	public float speedDash;

	public float timeDash;

	public float tmpTime;

	public override void parseData(int type, float[] data)
	{
	}
}
