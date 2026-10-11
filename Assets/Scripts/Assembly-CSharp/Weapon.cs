public class Weapon
{
	public static int SHURIKEN_1;

	public static int SHURIKEN_LINE;

	public static int SHURIKEN_CIRCLE;

	public static int SHURIKEN_FAN;

	public static int THROW_BOMB;

	public static int ATTACK_MELEE;

	public static int ATTACK_RANGE;

	public static int ATTACK_MAGIC;

	public static int ATTACK_BURN;

	public int Id;

	public int Type;

	public int Damage;

	public string Name;

	public string Desc;

	public string Prefabs;

	public float[] Data;

	public float TimeReload;

	public float SpeedFly;

	public float Range;

	public Weapon Clone()
	{
		return null;
	}
}
