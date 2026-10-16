using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public static PoolManager instanse;

	public GGPool m_PoolBattleObject;

	public GGPool m_PoolBattleCanvas;

	private EffectInfo effPortal;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	public GameObject CreatePredictionShuriken(Vector3 from, Vector3 to, bool hasBoucing)
	{
		return null;
	}

	public GameObject createBurnDamage(float size, float timeRemain, float timeEffect, int damage, int teamID, GameObject follower)
	{
		return null;
	}

	public GameObject createDeathBomb(int damage, int teamID)
	{
		return null;
	}

	public GameObject CreateChillingBlast(int teamID, List<EffectFK> effectFKs)
	{
		return null;
	}

	public GameObject CreateEffectBolt(PlayerController mPlayer, int damage)
	{
		return null;
	}

	public GameObject CreateLighting()
	{
		return null;
	}

	public void AddEffectBuffHP(Vector3 pos)
	{
	}

	public void addEffectSummon(Vector3 pos)
	{
	}

	public void AddEffectPortal(Vector3 pos)
	{
	}

	public void AddPoolEffectPortal(int number)
	{
	}

	public void AddPoolHealthBar()
	{
	}

	public void AddPoolPlayer(int number)
	{
	}

	public void AddHitPosition(Vector3 pos)
	{
	}
}
