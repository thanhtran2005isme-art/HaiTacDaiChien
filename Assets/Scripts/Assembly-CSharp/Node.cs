using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public string name;

	public int id;

	public int numberPlayer;

	public int typeBuild;

	public int[] listIDNode;

	public int owner;

	public long timecoundownAtk;

	public long timecountdownDef;

	public Vector3 mPos;

	public Node NearestToStart;

	public int StraightLineDistanceToEnd;

	public List<Edge> Connections;

	public int? MinCostToStart;

	public bool Visited;

	public int indexAttack;

	public Node(int id, string name, int type)
	{
	}
}
