using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
	public GameObject enemyTile;

	private List<EnemyTile> listEnemy;

	private List<MonoPoolItem> listGold;

	public CompositeCollider2D[] lstComposite;

	private int totalExp;

	private void Start()
	{
	}

	public void AutoCollect()
	{
	}

	public void addGold(Vector3 pos, int num)
	{
	}

	public void AddExpHero(int num)
	{
	}

	public List<EnemyTile> getListEnemy(int zone, int wave)
	{
		return null;
	}
}
