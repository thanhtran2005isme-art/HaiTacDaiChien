using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : MonoBehaviour
{
	public CircleCollider2D box;

	public int teamID;

	private float tmpTimeEffect;

	private float timeEffect;

	private float timeRemain;

	private float tmpTimeRemain;

	private int damage;

	private List<PlayerController> lstCollision;

	private GameObject follower;

	private List<EffectFK> lstEffect;

	public void setData(float size, float timeRemain, float timeEffect, int damage, int teamID, GameObject follower, List<EffectFK> lstEffect)
	{
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
	}

	public void process()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
	}
}
