using UnityEngine;

public class PoolObject
{
	public int index { get; set; }

	public string prefabName { get; set; }

	public bool isAvailable { get; set; }

	public GameObject gameObj { get; set; }

	public void recycleObject()
	{
	}
}
