using UnityEngine;

public class ActionMove : AAction
{
	public new static int TYPE_MOVE;

	public static int TYPE_TELEPORT;

	public static int TYPE_MOVE_NEAR_ENEMY;

	public int typeMove;

	public Vector3 pos;

	public float radius;

	public override void parseData(int type, float[] data)
	{
	}

	public void genNewPos(Vector3 currentPos)
	{
	}
}
