using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	public int teamID;

	private float tmpTimeEffect;

	private List<PlayerController> lstCollision;

	private void OnTriggerExit2D(Collider2D collision)
	{
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
	}

	public void process()
	{
	}

	private void Update()
	{
	}
}
