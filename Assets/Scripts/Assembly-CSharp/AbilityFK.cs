using System.Collections.Generic;

public class AbilityFK
{
	public static int TYPE_SUM;

	public static int TYPE_UNIQUE;

	public int id;

	public int price;

	public int type;

	public string name;

	public string desc;

	public List<EffectFK> listEffect;

	public AbilityFK Clone()
	{
		return null;
	}
}
