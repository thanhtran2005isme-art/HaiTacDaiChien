using System.Collections.Generic;

public class ItemUseFK
{
	public static int TYPE_POINT;

	public static int TYPE_ITEM;

	public int id;

	public int price;

	public int capacity;

	public int type;

	public float time_reload;

	public string name;

	public string desc;

	public List<EffectFK> listEffect;

	public int number;

	public float tmpTimeReload;

	public ItemUseFK Clone()
	{
		return null;
	}
}
