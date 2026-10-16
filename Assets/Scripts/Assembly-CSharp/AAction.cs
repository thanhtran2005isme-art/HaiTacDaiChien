public class AAction
{
	public const int TYPE_MOVE = 0;

	public const int TYPE_ATTACK = 1;

	public const int TYPE_SUMMON = 7;

	public int type;

	public float[] data;

	public bool isDone;

	public bool isProcess;

	public virtual void reset()
	{
	}

	public virtual void parseData(int type, float[] data)
	{
	}

	public AAction Clone()
	{
		return null;
	}
}
